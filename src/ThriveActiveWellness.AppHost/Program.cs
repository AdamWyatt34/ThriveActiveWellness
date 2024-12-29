IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresServerResource> postgres = builder.AddPostgres("thriveactivewellness-db")
    .WithDataVolume()
    .WithPgAdmin();

IResourceBuilder<PostgresDatabaseResource> db = postgres.AddDatabase("ThriveActiveWellness");

IResourceBuilder<RedisResource> redis = builder.AddRedis("thriveactivewellness-redis");

IResourceBuilder<RabbitMQServerResource> queue = builder.AddRabbitMQ("thriveactivewellness-queue")
    .WithManagementPlugin();

builder.AddProject<Projects.ThriveActiveWellness_Api>("thriveactivewellness-api")
    .WithReference(db)
    .WithReference(queue)
    .WithReference(redis)
    .WaitFor(db)
    .WaitFor(redis)
    .WaitFor(queue);

await builder.Build().RunAsync();
