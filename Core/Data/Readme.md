# Contexts

## DatabaseContext 
Used only for creating structure migrations. All db entities which are to be maintained by this application go here

## DataContext
To be used for normal CRUD but **not** for changes to the Database structure.

## creating / adding Migrations
Sample is for a Migration named `InitialCreate` all subsequent migrations need a diffrent name (Preferably referenceing the Issue / Task which spawned them)

- Package-Manager Console
```

Add-Migration InitialCreate -StartupProject MyFridgeList-Webapi -Context DatabaseContext  -v

```
- .net cli
´´´
Move to folder:
<ProjectRoot>

dotnet ef migrations add InitialCreate --startup-project ./

´´´


## Running/Applying Migrations
Applies migrations, please note to use them on the correct DbContext.

- Package-Manager Console
```

Update-Database -StartupProject MyFridgeList-Webapi  -Context DatabaseContext  -v

```
- .net cli
´´´
Move to folder:
<ProjectRoot>

dotnet ef database update --startup-project ./

´´´

## Links / Documentations
[https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet]
[https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/powershell]