# EF.Core Migrations freezing issue

Look at file [AppDbContext](./Entities/AppDbContext.cs) at line 35,
comment code below and run `dotnet ef migration add RenameFk`.

It should run without any problems and create empty migration.

Now, uncomment commented code and try to run it again.
It should freeze.
