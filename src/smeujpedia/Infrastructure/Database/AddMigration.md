**Adding a migration**

We use an alternative location for our ef core migrations in the infrastructure project.
You can use the following command to add a new migration in the correct location.

Before executing the command please check that your ef core tool version is the 
same as that of the *Microsoft.EntityFrameworkCore.Sqlite* package

Replace <MigrationName> with the name of your migration.

```
dotnet ef migrations add <MigrationName> -o "./Database/Migrations"
```