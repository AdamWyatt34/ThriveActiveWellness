IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresServerResource> postgres = builder.AddPostgres("thriveactivewellness-server")
    .WithPgAdmin()
    .WithDataVolume();

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
    .WaitFor(queue)
    .WaitFor(postgres);

await builder.Build().RunAsync();
