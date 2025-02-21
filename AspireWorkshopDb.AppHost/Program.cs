var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.ExecutionContext.IsPublishMode ?
    builder.AddConnectionString("mydatabasename") :
    builder.AddSqlServer("mysqlservername")
            .WithLifetime(ContainerLifetime.Persistent)
            .AddDatabase("mydatabasename");

var migrator = builder.AddProject<Projects.AspireWorkshopDb_Migrator>("aspireworkshopdb-migrator")
    .WithReference(sql);


var api = builder.AddProject<Projects.AspireWorkshopDb_Api>("aspireworkshopdb-api")
    .WithReference(sql)
    .WithReference(migrator)
    .WaitForCompletion(migrator);

builder.AddProject<Projects.AspireWorkshopDb_Web>("aspireworkshopdb-web")
    .WithReference(api)
    .WithExternalHttpEndpoints();

builder.Build().Run();
