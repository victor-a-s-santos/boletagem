# Solve - Financial Market Integration - APIs

1. Table of conents
- [2. Guidelines](#2-guidelines)
- [3. Getting and Building the Code](#3-getting-and-building-the-code)
- [4. Deploy](#4-deploy)

## 2. Guidelines

[C# Guideline](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/) -- Microsoft C# Programming Guideline.

## 3. Getting and Building the Code

### Prerequisites

* **[Visual Studio Code](https://code.visualstudio.com/)**
* **[SQL Server 2016 or higher](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)**

### Clone the repository

```bash
git clone https://itlabbrasil@dev.azure.com/itlabbrasil/Solve.FinancialMarketIntegration.Web/_git/Solve.FinancialMarketIntegration.API
```

### Configure

Create a copy of _appsettings.json and rename to appsettings.json. After that configure the connection strings for your host:

```javascript

  // appsettings.json

  "ConnectionString": {
    "SecurityDataContext": "server=.\\sql2017;database=Solve.FinancialMarketIntegration;User ID=sa;password=sa;",
    "TicketsDataContext": "server=.\\sql2017;database=Solve.FinancialMarketIntegration;User ID=sa;password=sa;",
    "WorkflowDataContext": "server=.\\sql2017;database=Solve.FinancialMarketIntegration;User ID=sa;password=sa;",
    "FilesDataContext":    "server=.\\sql2017;database=Solve.FinancialMarketIntegration;User ID=sa;password=sa;"
  },
```

### Running Migrations

```batch
dotnet ef database update --context DataContext
```

```batch
dotnet ef database update --context WorkflowDataContext
```

```batch
dotnet ef database update --context TicketDataContext
```

```batch
dotnet ef database update --context FileDataContext
```

### Removing a Migration

With my last corret migration been 1.0.0.3 let's add a wrong migration:

```batch
dotnet ef database add wrong-migration --context TicketDataContext
dotnet ef database update --context TicketDataContext
```

To remove the wrong migration:

* Apply last corret migration

```batch
dotnet ef database add wrong-migration --context TicketDataContext
```

* Remove wrong migration

```batch
dotnet ef migrations remove -c TicketDataContext
```

> Don't remove a migration without using dotnet cli. This is let the snapshot file in a wrong state.

### Build and run

```batch
dotnet build
```

```batch
dotnet run
```


## 4. Deploy

To generate database script run the following code:
```batch
 dotnet ef migrations script {previous_migration_version} -c {datacontext} -o ../scripts/{file_version}.sql -i
```

* `{previous_migration_version}`: Lasted applied migration Ex.: If you are creating a 1.0.0.2 migration this parameter must be **1.0.0.2**;
* `{datacontext}`: Data context name Ex.: **TicketDataContext**;
* `{file_version}`: File version name Ex.: **tickets.1.0.0.7.sql**;

For example:
```batch
dotnet ef migrations script 20190704193000_1.0.0.4 -c TicketDataContext -o ../scripts/tickets.1.0.0.6.sql -i
```

To see more about ef migrations access: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/#generate-sql-scripts