# ======================================================================================================================
# 1 - BUILD PROJECTS AND RUN TESTS
# ======================================================================================================================

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS PROJECTS

WORKDIR /app
COPY . ./
ARG ENV_VALUE

# RESTORE & BUILD & TEST
RUN dotnet restore --disable-parallel
RUN dotnet build -c Release --no-restore
RUN if [ $ENV_VALUE = Testing ] || [ $ENV_VALUE = Staging ]; \
then ASPNETCORE_ENVIRONMENT=${ENV_VALUE} dotnet test -c Release --no-build --no-restore; \
else cd /app/TokanPages.Persistence/TokanPages.Persistence.MigrationRunner/bin/Release/net6.0 && \
ASPNETCORE_ENVIRONMENT=${ENV_VALUE} dotnet TokanPages.Persistence.MigrationRunner.dll --next-prod; fi

# ======================================================================================================================
# 2 - BUILD DOCKER IMAGE
# ======================================================================================================================

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

# BACKEND
COPY --from=PROJECTS "/app/TokanPages.Backend/TokanPages.Backend.Application/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Backend/TokanPages.Backend.Core/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Backend/TokanPages.Backend.Configuration/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Backend/TokanPages.Backend.Domain/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Backend/TokanPages.Backend.Shared/bin/Release/net6.0" .

# PERSISTENCE
COPY --from=PROJECTS "/app/TokanPages.Persistence/TokanPages.Persistence.Caching/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Persistence/TokanPages.Persistence.Database/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Persistence/TokanPages.Persistence.MigrationRunner/bin/Release/net6.0" .

# SERVICES
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.AzureBusService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.AzureStorageService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.BehaviourService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.CipheringService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.EmailSenderService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.HttpClientService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.RedisCacheService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.UserService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.WebSocketService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.WebTokenService/bin/Release/net6.0" .

# WEBAPI
COPY --from=PROJECTS "/app/TokanPages.WebApi/TokanPages.Gateway/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.WebApi/TokanPages.Gateway.Dto/bin/Release/net6.0" .

ARG ENV_VALUE
ENV ASPNETCORE_ENVIRONMENT=${ENV_VALUE}
ENV ASPNETCORE_URLS=http://+:80  
EXPOSE 80
ENTRYPOINT ["dotnet", "TokanPages.Gateway.dll"]
