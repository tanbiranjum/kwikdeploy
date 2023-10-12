# KwikDeploy Backend

## How to run locally

### Start a Postgres instance for Development
Start an instance using Docker.

    docker run --name postgres -e POSTGRES_PASSWORD=Password123! -v postgres-data:/var/lib/postgresql -p 5432:5432 -d postgres:16.0
    
Create a database named 'kwikdeploy'. You can use https://dbeaver.io if you prefer a GUI.

### Migrate the database
    dotnet ef database update --project src\Infrastructure --startup-project src\Api

### Run the project
    dotnet run --project src/Api

## How to add a new EF migration
dotnet ef migrations add "NewMigrationName" --project src\Infrastructure --startup-project src\Api --output-dir Persistence\Migrations
