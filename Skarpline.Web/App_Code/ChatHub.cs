#region using

using Microsoft.AspNet.SignalR;
using RestSharp;
using RestSharp.Deserializers;
using Skarpline.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

#endregion

public class ChatHub : Hub
{
    string endpoint = System.Configuration.ConfigurationManager.AppSettings["api"];
    RestSharp.Deserializers.JsonDeserializer deserial; 
    public ChatHub()
    {
        deserial = new JsonDeserializer();
    }

    public async Task<bool> Send(int uerId, string name, string message)
    {
        var _stDeveloperApi = new RestClient(endpoint);
        var url = string.Format("save");
        var request = new RestRequest(url, Method.POST);
        request.RequestFormat = DataFormat.Json;
        request.AddParameter("message", message, ParameterType.QueryString);
        request.AddParameter("uerId", uerId, ParameterType.QueryString);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Content-Type", "application/json; charset=utf-8");
        var response = _stDeveloperApi.Execute(request);

        Clients.All.stoptyping(name);
        Clients.All.broadcastMessage(name, message);
        return true;
    }

    public async Task<bool> Login(string name)
    {
        var returnValue = false;

        var _stDeveloperApi = new RestClient(endpoint);
        var url = string.Format("isexist");
        var request = new RestRequest(url, Method.POST);
        request.RequestFormat = DataFormat.Json;
        request.AddParameter("name", name, ParameterType.QueryString);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Content-Type", "application/json; charset=utf-8");
        var response = _stDeveloperApi.Execute(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            //To deserialize into a simple variable, use the <Dictionary<string, string>> type
            var loginResponse = deserial.Deserialize<List<int>>(response);

            if (loginResponse==null||loginResponse[0] == 0 || loginResponse[0] == -1)
                Clients.All.newuserloggedinfailed(name, loginResponse[0]);
            else
            {
                List<UserViewModel> users = new List<UserViewModel>();
                List<LastMessageViewModel> messages = new List<LastMessageViewModel>();
                request = new RestRequest("getall", Method.GET);
                request.RequestFormat = DataFormat.Json;
                response = _stDeveloperApi.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                    users = deserial.Deserialize<List<UserViewModel>>(response);

                request = new RestRequest("getlastmessages", Method.GET);
                request.RequestFormat = DataFormat.Json;
                response = _stDeveloperApi.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                    messages = deserial.Deserialize<List<LastMessageViewModel>>(response);

                Clients.All.newuserloggedinsuccess(users, name, messages);

                returnValue = true;
            }
        }

        //// Call the broadcastMessage method to update clients.
        return returnValue;
    }

    public async Task<bool> Logout(int id, string name)
    {
        var returnValue = false;
        var user = new UserViewModel
        {
            Id = id,
            Username = name,
            IsLoggedIn = false
        };

        var _stDeveloperApi = new RestClient(endpoint);
        var request = new RestRequest("update", Method.POST);
        request.RequestFormat = DataFormat.Json;
        request.AddBody(user);
        var response = _stDeveloperApi.Execute(request);

        request = new RestRequest("getall", Method.GET);
        request.RequestFormat = DataFormat.Json;
        response = _stDeveloperApi.Execute<UserViewModel>(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var users = deserial.Deserialize<List<UserViewModel>>(response);
            Clients.All.SuccessfullLogout(users, name);
            returnValue = true;
        }
        return returnValue;
    }

    public async Task<bool> Userlist()
    {
        var returnValue = false;

        var _stDeveloperApi = new RestClient(endpoint);
        var url = string.Format("getall");
        var request = new RestRequest(url, Method.GET);
        request.RequestFormat = DataFormat.Json;
        var response = _stDeveloperApi.Execute<UserViewModel>(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var users = deserial.Deserialize<List<UserViewModel>>(response);
            Clients.All.InitializeChat(users, "");
            returnValue = true;
        }
        return returnValue;
    }

    public async Task<bool> LoadCurrentStatus(string name)
    {
        List<UserViewModel> users = new List<UserViewModel>();
        List<LastMessageViewModel> messages = new List<LastMessageViewModel>();
        var _stDeveloperApi = new RestClient(endpoint);
        var url = string.Format("getall");
        var request = new RestRequest(url, Method.GET);
        request.RequestFormat = DataFormat.Json;
        request.AddParameter("name", name, ParameterType.QueryString);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Content-Type", "application/json; charset=utf-8");
        request.RequestFormat = DataFormat.Json;
        var response = _stDeveloperApi.Execute(request);

        if (response.StatusCode == HttpStatusCode.OK)
            users = deserial.Deserialize<List<UserViewModel>>(response);

        request = new RestRequest("getlastmessages", Method.GET);
        request.RequestFormat = DataFormat.Json;
        response = _stDeveloperApi.Execute(request);

        if (response.StatusCode == HttpStatusCode.OK)
            messages = deserial.Deserialize<List<LastMessageViewModel>>(response);

        Clients.All.newuserloggedinsuccess(users, name, messages);

        return true;
    }

    public void typingstart(string user, string message)
    {
        Clients.All.starttyping(user, message);
    }

    public void typingstop(string user)
    {
        Clients.All.stoptyping(user);
    }
}