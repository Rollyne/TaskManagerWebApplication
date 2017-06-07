# TaskManagerWebApplication
Task managing application made with ASP.NET Core 1.1

Basically a simple web application for task managing using basic text files as a data provider.
The application is being built on ASP.NET Core 1.1
It includes basic unit tests for the functionality of the application.


When being cloned the **ConnectionString** should be changed in the **TaskManagerASP.appsettings.json** and **DbDataProvider.TaskManagerContextFactory.Create()**, sorry about that. This is for the DbDataProvider.

**OR/AND**

When being cloned the **DataPath** in **appconfig.json** and **SetBasePath** in **Configuration.GetConfig()** should be changed according to the machine. This is for the FileDataProvider.

Data provider types can be configured in **TaskManagerASP.Tools.RepositoryClient** by changing the repositoryType.
