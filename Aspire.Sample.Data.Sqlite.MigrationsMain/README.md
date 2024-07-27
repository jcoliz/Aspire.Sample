# Migrations Main for Data.Sqlite

In order to run migrations, EF Core needs a "main" app to run. To keep this simple,
we dedicate a whole app, with very minimal things going on, so we don't have to worry
about configuration, secrets, etc, which have nothing to do with the migration.

See: https://erwinstaal.nl/posts/db-per-tenant-catalog-database-ef-core-migrations/

## Make a migration

After making changes to the `ApplicationDbContext`, we need to add a migration
to describe how those changes will show up in the database. Migrations do need
to be added separately for Sql Server and Postgres.

```Powershell
PS Aspire.Sample> dotnet ef migrations add $env:MIGRATION -o .\Migrations\ -n Aspire.Sample.Data.Sqlite.Migrations --project .\Aspire.Sample.Data.Sqlite\ --startup-project .\Aspire.Sample.Data.Sqlite.MigrationsMain\ --context ApplicationDbContext
```

If you make a mistake and need to re-do it, be sure to remove the `ApplicationDbContextModelSnapshot.cs` file.

## Update database automatically

For Postgres database, no further action is needed. Application Main() is expected
to call `Database.Migrate()` at launch, to automatically apply the latest
migrations.

This is allowed because Postgres is only used in development and testing scenarios.

## Generate a migrations SQL script to update database

Again, we don't normally need to migrate the database manually.
That said, if we want to see what the migrations script looks like, we can
create one:

```Powershell
PS Aspire.Sample> dotnet ef migrations script --project .\Aspire.Sample.Data.Sqlite\ --startup-project .\Aspire.Sample.Data.Sqlite.MigrationsMain\ --context ApplicationDbContext -i -o out\postgres-migration.sql
```

## Never EnsureCreated

Note that databases created with "EnsureCreated" can never be migrated. That's definitely not OK
for production, so "EnsureCreated" is explicitly forbidden for code headed to production.

Calling "Migrate" is allowed on Postgres databases, because these are always only used in
development or functional testing.
