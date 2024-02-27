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
COPY --from=PROJECTS "/app/TokanPages.Backend/TokanPages.Backend.Core/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Backend/TokanPages.Backend.Configuration/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Backend/TokanPages.Backend.Shared/bin/Release/net6.0" .

# SERVICES
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.AzureBusService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.HttpClientService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/ForSportApp.Services/ForSportApp.Services.VideoConverterService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/ForSportApp.Services/ForSportApp.Services.VideoProcessingService/bin/Release/net6.0" .

# WEBAPI
COPY --from=PROJECTS "/app/TokanPages.WebApi/TokanPages.HostedServices/bin/Release/net6.0" .

# INSTALL FFMPEG FOR ALPINE
RUN apk upgrade -U
RUN apk add ffmpeg

# CONFIGURATION
ARG ENV_VALUE
ENV ASPNETCORE_ENVIRONMENT=${ENV_VALUE}
ENV ASPNETCORE_URLS=http://+:80  

ENTRYPOINT ["dotnet", "TokanPages.HostedServices.dll"]
