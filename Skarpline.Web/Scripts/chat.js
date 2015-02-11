$(function () {
    // Declare a proxy to reference the hub.
    var setUser = false, customerid, customername,typeoflogin;
    var chat = $.connection.chatHub;
    var typingTimer;
    var doneTypingInterval = 10;
    var finaldoneTypingInterval = 500;
    loadSession();
    function loadSession() {
        if (typeof (Storage) != "undefined") {
            if(localStorage.getItem("myusername")!=null &&localStorage.getItem("myusername")!="")
            {
                $.connection.hub.start().done(function () {
                    typeoflogin = "reconnect";
                    setUser = false;
                    $('#loading').show();
                    $('#displayname').text(localStorage.getItem("myusername"));
                    chat.server.loadCurrentStatus($('#displayname').text());
                });
            }
        }
    }
    $('#userlist').hide();
    $('#intializechat').show();
    $('#chathistory').hide();
    $('#username').focus();

    $('#userloggin').click(function () {
        TryToLogin();
    });

    $('#sendmessage').click(function () {
        sendmessage();
    });

    $('#logout').click(function () {
        $('#loading').show();
        Logout();
    });

    $("#username").keypress(function (e) {
        if (e.which == 13) {
            TryToLogin();
            return false;
        }
    });

    $("#message").keypress(function (e) {
        if (e.which == 13) {
            sendmessage();
            return false;
        }
    });

    function TryToLogin() {
        $('#username').val().length > 1 ? StartChat($('#username').val()) : $('#loginerrormessage').text("Please enter user name");
    }

    function StartChat(user) { 
        $.connection.hub.start().done(function () {
            setUser = false;
            user = GetPascal(user);
            typeoflogin = "newLogin";
            $('#loading').show();
            $('#displayname').text(user);
            chat.server.login(user);
        });
    }

    chat.client.InitializeChat = function (users, currentuser) {
        UserListWithoutLogin(users, currentuser);
    };

    function UserListWithoutLogin(users, currentuser) {
        $('#tbluserlist').empty();
        $.each(users, function (index, value) {
            $('#tbluserlist').append('<tr><td><label>' + value.Username
            + '</label></td></tr>');
        });
    }

    function UserListAfterLogin(users, currentuser, lastmessage) {
        $('#tbluserlist').empty();

        $.each(users, function (index, value) {
            if (!setUser && value.Username == $('#displayname').text()) {
                customerid = value.Id;
                customername = $('#displayname').text();
                $('#tbluserlist').append('<tr class="active"><td><label>' + value.Username
                + '</label></td></tr>');

                $('#userlist').show();
                $('#loading').hide();
                $('#intializechat').hide();
                $('#chathistory').show();
                $('#message').focus();

                setUser = true;
            }
            else {
                if (value.Username == customername) {
                    $('#tbluserlist').append('<tr class="active"><td><label>' + value.Username
                    + '</label></td></tr>');
                }
                else {
                    $('#tbluserlist').append('<tr><td><label>' + value.Username
                    + '</label></td></tr>');
                }
            }
        });
        if (typeoflogin!="reconnect" && currentuser != $('#displayname').text()) {
            var encodedName = $('<div />').text(currentuser).html();
            $('#discussion').append('<li><i>' + encodedName
                + ':&nbsp;&nbsp; is logged in</i></li>');
        }
        else {
            $('#discussion').empty();
            $.each(lastmessage, function (index, value) {
                var encodedName = $('<div />').text(value.UserName).html();
                var encodedMsg = $('<div />').text(value.Message).html();
                $('#discussion').append('<li><strong>' + encodedName
                    + '</strong>:&nbsp;&nbsp; ' + encodedMsg + '</li>');
            });
            if (typeof (Storage) != "undefined") {
                localStorage.setItem("myusername", currentuser);
            }
        }
    }

    function Logout() {
        chat.server.logout(customerid, customername);
        $('#loading').show();
        if (typeof (Storage) != "undefined") {
            localStorage.setItem("myusername", "");
        }
    }

    chat.client.newuserloggedinsuccess = function (users, current_user, lastmessage) {
        UserListAfterLogin(users, current_user, lastmessage);
        $('#loginerrormessage').text("");
        $('#loading').hide();
    };

    chat.client.newuserloggedinfailed = function (username, errorcode) {
        if (username == $('#displayname').text()) {
            var errorMessage;
            errorcode == -1 ? errorMessage = "Only 20 user allow at a time. Please try after some time." : errorMessage = "This username is already in use";
            $('#loginerrormessage').text(errorMessage);
            $('#loading').hide();
        }
    };

    chat.client.successfullLogout = function (users, logoutuser) {
        $('#tbluserlist').empty();
        if (logoutuser == $('#displayname').text()) {
            $('#chathistory').hide();
            $('#userlist').hide();
            $('#loading').hide();
            $('#intializechat').show();
            $('#username').val("");
            $('#username').focus();
            $('#displayname').text("");
        }
        else {
            var encodedName = $('<div />').text(logoutuser).html();
            var encodedMsg = $('<div />').text('').html();
            $('#discussion').append('<li><i>' + encodedName
                + ':&nbsp;&nbsp; is logged out</i></li>');
        }

        $.each(users, function (index, value) {
            if (value.Username == $('#displayname').text()) {
                $('#tbluserlist').append('<tr class="active"><td><label>' + value.Username
                + '</label></td></tr>');
            }
            else {
                $('#tbluserlist').append('<tr><td><label>' + value.Username
                + '</label></td></tr>');
            }
        });
    };

    chat.client.broadcastMessage = function (name, message) {
        var encodedName = $('<div />').text(name).html();
        var encodedMsg = $('<div />').text(message).html();
        $('#discussion').append('<li><strong>' + encodedName
            + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
    };

    function sendmessage() {
        chat.server.send(customerid, $('#displayname').text(), $('#message').val());
        // Clear text box and reset focus for next comment.
        $('#message').val('').focus();
    };

    $('#message').keydown(function () {
        clearTimeout(typingTimer);
        if ($('#message').val) {
            typingTimer = setTimeout(function () {
                chat.server.typingstart($('#displayname').text(), $('#message').val());
            }, doneTypingInterval);
        }
    });

    $('#message').keyup(function () {
        clearTimeout(typingTimer);
        typingTimer = setTimeout(function () {
            chat.server.typingstop($('#displayname').text());
        }, finaldoneTypingInterval);
    });

    chat.client.starttyping = function (name, message) {
        if ($('#displayname').text() != name && typeof ($('#discussion li#' + name)[0]) == "undefined") {
            $('#discussion').append('<li id=' + name + '><strong>' + name
                + '</strong>:<i>&nbsp;&nbsp; Typing...</i></li>')
        }
    }

    chat.client.stoptyping = function (name) {
        if ($('#displayname').text() != name) {
            $('#discussion li#' + name).remove();
        }
    }

    function GetPascal(keyword) {
        if (keyword.length <= 0) {
            return "";
        }
        else {
            return keyword.substring(0, 1).toUpperCase() + keyword.substring(1).toLowerCase();
        }
    }
});