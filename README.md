# UserService

## Adding Entity Framework Core Migrations and Updating the Database using Visual Studio

To work with Entity Framework Core migrations and update the database, you can utilize Visual Studio's Package Manager Console. Follow these steps:

### Adding Migrations

1. **Access Package Manager Console:** In Visual Studio, go to `Tools` > `NuGet Package Manager` > `Package Manager Console`.

2. **Create a Migration:** Run the following command in the Package Manager Console:

    ```bash
    Add-Migration YourMigrationName
    ```
    Replace `YourMigrationName` with a descriptive name for your migration.

### Updating the Database

1. **Access Package Manager Console:** If not already open, go to `Tools` > `NuGet Package Manager` > `Package Manager Console`.

2. **Update the Database:** Execute the following command in the Package Manager Console:

    ```bash
    Update-Database
    ```

    This command will apply pending migrations and update the database schema based on your Entity Framework Core models.
