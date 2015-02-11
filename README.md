# SkarplinePOC
SkarplinePOC is a Proof of concept for chat application. The primary objective of this application is to provide a common platform for all the
users to chat in a group.

Technologies
ASP.NET MVC 5
EF 6
AutoMapper
Twitter Bootstrap
Web API

Patterns & Practices
Repository Pattern & Generic Repository
Unit of Work
Dependency Injection

Prerequisites
1. Restore database script from App_Data folder in SQL Server.
1. Change connection string, in web.config file under Skarpline.API project and web.config file under Skarpline.Web project 

Running the Application
Open the solution in Visual Studio 2013. Build the solution to install Nuget packages. (This will automatically restore Nuget packages. Please ensure you have Nuget version 2.7 or higher)
Run the application. It will show index page

For Task 1,
Click on Start Chat button. It will take you to Chat application.
Login with any username. (Please note that, currently the application does not provide any pre-defined user or registration facility.)

For Task2,
Click on Object Deserialization button. It will take you to "DeserializedString.aspx" page.
If the deserialization of pre-provided text is successful, it will display true next to Object Conversion text

Assumptions and Considerations
We have stored user information in session, so that user will be logged in till the log out button is pressed. This way, we have tried to handle an alternate to the saving of user information in cookies by ASP.Net Identity.

Goals and Roadmap
Overall Project Goals
Create Registration Screen of Chat application
User should sign in before using chat window
Implement ASP.Net Identity for user management
Following Test-Driven Development (TDD) to ensure a bug free application.