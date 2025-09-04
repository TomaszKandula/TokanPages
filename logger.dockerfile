# ======================================================================================================================
# 1 - BUILD PROJECTS AND RUN TESTS
# ======================================================================================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0.413-alpine3.21 AS projects

WORKDIR /app
COPY . ./

RUN dotnet restore
RUN dotnet build -c Release

# ======================================================================================================================
# 2 - BUILD DOCKER IMAGE
# ======================================================================================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0.19-alpine3.22
WORKDIR /app

# INSTALL ICU FULL SUPPORT
RUN apk add icu-libs --no-cache
RUN apk add icu-data-full --no-cache

COPY --from=projects "/app/TokanPages.Backend/TokanPages.Backend.Configuration/bin/Release/net8.0" .
COPY --from=projects "/app/TokanPages.Services/TokanPages.Services.BehaviourService/bin/Release/net8.0" .
COPY --from=projects "/app/TokanPages.WebApi/TokanPages.Logger/bin/Release/net8.0" .
COPY --from=projects "/app/TokanPages.WebApi/TokanPages.Logger.Dto/bin/Release/net8.0" .

ARG ENV_VALUE
ENV ASPNETCORE_ENVIRONMENT=${ENV_VALUE}
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "TokanPages.Logger.dll"]
