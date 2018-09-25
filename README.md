# Zirk
Simple ASP.NET Core Webserver application

Modify the `appsettings.json` file to specify a valid connection string. The server you connect to should have a table on the master db called "data".
This "data" table should have keys which reflect the fields specified in the Datum class (Datum.cs).

To run, open a terminal/Command Prompt in the project directory and run `dotnet run Zirk`. You must have the .NET Core runtime installed for this application to function.

By default, the application will begin listening on `http://localhost:5000` and `https://localhost:5001`. If you wish to specify your own server URL, you can do so with command line parameters, but you must instead run the .NET application .dll directly. Navigate to `bin/<Debug, Release>/netcoreapp2.1/` and run `dotnet Zirk.dll --serverurls "x;y;z"` where x;y;z is a semicolon-delimited list of URLs (of any number 1 or greater) you would like to listen on. Please also specify the port as well: `--server.urls "http://0.0.0.0:8000"` The application will fail to start if it cannot initialize a connection on the specified URL.
