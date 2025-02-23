IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresServerResource> postgres = builder.AddPostgres("thriveactivewellness-server")
    .WithPgAdmin()
    .WithDataVolume();

IResourceBuilder<PostgresDatabaseResource> db = postgres.AddDatabase("ThriveActiveWellness");

IResourceBuilder<RedisResource> redis = builder.AddRedis("thriveactivewellness-redis");

IResourceBuilder<RabbitMQServerResource> queue = builder.AddRabbitMQ("thriveactivewellness-queue")
    .WithManagementPlugin();

IResourceBuilder<ProjectResource> migrations = builder.AddProject<Projects.ThriveActiveWellness_MigrationService>("migrations")
    .WithReference(db)
    .WaitFor(db);

IResourceBuilder<OllamaResource> ollama = builder.AddOllama("ollama");

IResourceBuilder<OllamaModelResource> phi35 = ollama.AddModel("phi3.5");

builder.AddProject<Projects.ThriveActiveWellness_Api>("thriveactivewellness-api")
    .WithReference(db)
    .WithReference(queue)
    .WithReference(redis)
    .WithReference(phi35)
    .WaitFor(db)
    .WaitFor(redis)
    .WaitFor(queue)
    .WaitFor(postgres)
    .WaitFor(phi35)
    .WaitFor(migrations);

await builder.Build().RunAsync();
