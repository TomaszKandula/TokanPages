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
COPY --from=PROJECTS "/app/TokanPages.Backend/TokanPages.Backend.Core/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Backend/TokanPages.Backend.Configuration/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Backend/TokanPages.Backend.Shared/bin/Release/net6.0" .

# SERVICES
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.AzureBusService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.HttpClientService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.VideoConverterService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/TokanPages.Services/TokanPages.Services.VideoProcessingService/bin/Release/net6.0" .

# HOSTED-SERVICES API
COPY --from=PROJECTS "/app/TokanPages.WebApi/TokanPages.HostedServices/bin/Release/net6.0" .

# INSTALL FFMPEG FOR ALPINE
RUN apk upgrade -U
RUN apk add ffmpeg

# CONFIGURATION
ARG ENV_VALUE
ENV ASPNETCORE_ENVIRONMENT=${ENV_VALUE}
ENV ASPNETCORE_URLS=http://+:80  

ENTRYPOINT ["dotnet", "TokanPages.HostedServices.dll"]
