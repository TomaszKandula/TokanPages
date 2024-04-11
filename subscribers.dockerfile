# ======================================================================================================================
# 1 - BUILD PROJECTS AND RUN TESTS
# ======================================================================================================================

FROM mcr.microsoft.com/dotnet/sdk:6.0.416-alpine3.18 AS PROJECTS

WORKDIR /app
COPY . ./

# RESTORE & BUILD & TEST
RUN dotnet restore --disable-parallel
RUN dotnet build -c Release --force

# ======================================================================================================================
# 2 - BUILD DOCKER IMAGE
# ======================================================================================================================

FROM mcr.microsoft.com/dotnet/sdk:6.0.416-alpine3.18
WORKDIR /app

# BACKEND
COPY --from=PROJECTS "/app/TokanPages.Backend/TokanPages.Backend.Configuration/bin/Release/net6.0" .

# PERSISTENCE
COPY --from=PROJECTS "/app/TokanPages.Persistence/TokanPages.Persistence.Caching/bin/Release/net6.0" .

# SERVICES
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.BehaviourService/bin/Release/net6.0" .

# WEBAPI
COPY --from=PROJECTS "/app/TokanPages.WebApi/TokanPages.Subscribers/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.WebApi/TokanPages.Subscribers.Dto/bin/Release/net6.0" .

ARG ENV_VALUE
ENV ASPNETCORE_ENVIRONMENT=${ENV_VALUE}
ENV ASPNETCORE_URLS=http://+:80  

EXPOSE 80
ENTRYPOINT ["dotnet", "TokanPages.Subscribers.dll"]
