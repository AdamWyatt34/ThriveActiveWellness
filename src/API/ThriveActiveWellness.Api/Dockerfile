FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API/ThriveActiveWellness.Api/ThriveActiveWellness.Api.csproj", "API/ThriveActiveWellness.Api/"]
COPY ["Common/ThriveActiveWellness.Common.Application/Evently.Common.Application.csproj", "Common/ThriveActiveWellness.Common.Application/"]
COPY ["Common/ThriveActiveWellness.Common.Domain/Evently.Common.Domain.csproj", "Common/ThriveActiveWellness.Common.Domain/"]
COPY ["Common/ThriveActiveWellness.Common.Infrastructure/Evently.Common.Infrastructure.csproj", "Common/ThriveActiveWellness.Common.Infrastructure/"]
COPY ["Common/ThriveActiveWellness.Common.Presentation/Evently.Common.Presentation.csproj", "Common/ThriveActiveWellness.Common.Presentation/"]
COPY ["Modules/Exercises/ThriveActiveWellness.Modules.Exercises.Application/ThriveActiveWellness.Modules.Exercises.Application.csproj", "Modules/Exercises/ThriveActiveWellness.Modules.Exercises.Application/"]
COPY ["Modules/Exercises/ThriveActiveWellness.Modules.Exercises.Domain/ThriveActiveWellness.Modules.Exercises.Domain.csproj", "Modules/Exercises/ThriveActiveWellness.Modules.Exercises.Domain/"]
COPY ["Modules/Exercises/ThriveActiveWellness.Modules.Exercises.Infrastructure/ThriveActiveWellness.Modules.Exercises.Infrastructure.csproj", "Modules/Exercises/ThriveActiveWellness.Modules.Exercises.Infrastructure/"]
COPY ["Modules/Exercises/ThriveActiveWellness.Modules.Exercises.IntegrationEvents/ThriveActiveWellness.Modules.Exercises.IntegrationEvents.csproj", "Modules/Exercises/ThriveActiveWellness.Modules.Exercises.IntegrationEvents/"]
COPY ["Modules/Exercises/ThriveActiveWellness.Modules.Exercises.Presentation/ThriveActiveWellness.Modules.Exercises.Presentation.csproj", "Modules/Exercises/ThriveActiveWellness.Modules.Exercises.Presentation/"]
COPY ["Modules/Notifications/ThriveActiveWellness.Modules.Notifications.Application/ThriveActiveWellness.Modules.Notifications.Application.csproj", "Modules/Notifications/ThriveActiveWellness.Modules.Notifications.Application/"]
COPY ["Modules/Notifications/ThriveActiveWellness.Modules.Notifications.Domain/ThriveActiveWellness.Modules.Notifications.Domain.csproj", "Modules/Notifications/ThriveActiveWellness.Modules.Notifications.Domain/"]
COPY ["Modules/Notifications/ThriveActiveWellness.Modules.Notifications.Infrastructure/ThriveActiveWellness.Modules.Notifications.Infrastructure.csproj", "Modules/Notifications/ThriveActiveWellness.Modules.Notifications.Infrastructure/"]
COPY ["Modules/Notifications/ThriveActiveWellness.Modules.Notifications.Presentation/ThriveActiveWellness.Modules.Notifications.Presentation.csproj", "Modules/Notifications/ThriveActiveWellness.Modules.Notifications.Presentation/"]
COPY ["Modules/Users/ThriveActiveWellness.Modules.Users.Application/ThriveActiveWellness.Modules.Users.Application.csproj", "Modules/Users/ThriveActiveWellness.Modules.Users.Application/"]
COPY ["Modules/Users/ThriveActiveWellness.Modules.Users.Domain/ThriveActiveWellness.Modules.Users.Domain.csproj", "Modules/Users/ThriveActiveWellness.Modules.Users.Domain/"]
COPY ["Modules/Users/ThriveActiveWellness.Modules.Users.Infrastructure/ThriveActiveWellness.Modules.Users.Infrastructure.csproj", "Modules/Users/ThriveActiveWellness.Modules.Users.Infrastructure/"]
COPY ["Modules/Users/ThriveActiveWellness.Modules.Users.IntegrationEvents/ThriveActiveWellness.Modules.Users.IntegrationEvents.csproj", "Modules/Users/ThriveActiveWellness.Modules.Users.IntegrationEvents/"]
COPY ["Modules/Users/ThriveActiveWellness.Modules.Users.Presentation/ThriveActiveWellness.Modules.Users.Presentation.csproj", "Modules/Users/ThriveActiveWellness.Modules.Users.Presentation/"]
RUN dotnet restore "./API/ThriveActiveWellness.Api/ThriveActiveWellness.Api.csproj"
COPY . .
WORKDIR "/src/API/ThriveActiveWellness.Api"
RUN dotnet build "./ThriveActiveWellness.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ThriveActiveWellness.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThriveActiveWellness.Api.dll"]