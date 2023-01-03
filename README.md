# DigitalWallet

### Migrations

##### Create migration

Go to the solution folder and write the below command to create a migation with the specified <b>"MigrationName"</b> and context.

```
dotnet ef migrations add "MigrationName" -s src/DigitalWallet.Api -p src/DigitalWallet.Infrastructure --context ApplicationDbContext --output-dir Persistence/Migrations
```

##### Create database and update

To apply migration changes use the below command.
Form solution level.

```
dotnet ef database update "MigrationName" -s src/DigitalWallet.Api -p src/DigitalWallet.Infrastructure --context ApplicationDbContext
```

##### Remove migrations

Use this command to remove latest migration form the specified context. Migrations can be removed once the database will not be affected with the changes. For already applied migration to the database you can also use <b>--force</b> in addition to the command.
From solution level.

```
dotnet ef migrations remove -s src/DigitalWallet.Api -p src/DigitalWallet.Infrastructure --context ApplicationDbContext
```
