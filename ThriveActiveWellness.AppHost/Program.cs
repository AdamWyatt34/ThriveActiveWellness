IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresServerResource> db = builder.AddPostgres("thriveactivewellness-db")
    .WithDataVolume()
    .WithPgAdmin();

IResourceBuilder<RedisResource> redis = builder.AddRedis("thriveactivewellness-redis");

IResourceBuilder<RabbitMQServerResource> queue = builder.AddRabbitMQ("thriveactivewellness-queue")
    .WithManagementPlugin();

builder.AddProject<Projects.ThriveActiveWellness_Api>("thriveactivewellness-api")
    .WithReference(db)
    .WithReference(queue)
    .WithReference(redis);

builder.Build().Run();
