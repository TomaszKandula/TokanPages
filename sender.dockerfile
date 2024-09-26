# ======================================================================================================================
# 1 - BUILD PROJECTS AND RUN TESTS
# ======================================================================================================================
FROM mcr.microsoft.com/dotnet/sdk:6.0.416-alpine3.18 AS projects

WORKDIR /app
COPY . ./

RUN dotnet restore --disable-parallel
RUN dotnet build -c Release --force

# ======================================================================================================================
# 2 - BUILD DOCKER IMAGE
# ======================================================================================================================
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.18
WORKDIR /app

# INSTALL ICU FULL SUPPORT
RUN apk add icu-libs --no-cache
RUN apk add icu-data-full --no-cache

COPY --from=projects "/app/TokanPages.Backend/TokanPages.Backend.Configuration/bin/Release/net6.0" .
COPY --from=projects "/app/TokanPages.Persistence/TokanPages.Persistence.Caching/bin/Release/net6.0" .
COPY --from=projects "/app/TokanPages.Services/TokanPages.Services.AzureBusService/bin/Release/net6.0" .
COPY --from=projects "/app/TokanPages.Services/TokanPages.Services.BehaviourService/bin/Release/net6.0" .
COPY --from=projects "/app/TokanPages.WebApi/TokanPages.Sender/bin/Release/net6.0" .
COPY --from=projects "/app/TokanPages.WebApi/TokanPages.Sender.Dto/bin/Release/net6.0" .

ARG ENV_VALUE
ENV ASPNETCORE_ENVIRONMENT=${ENV_VALUE}
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "TokanPages.Sender.dll"]
