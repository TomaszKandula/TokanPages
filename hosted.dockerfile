# ======================================================================================================================
# 1 - BUILD PROJECTS AND RUN TESTS
# ======================================================================================================================
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS projects

WORKDIR /app
COPY . ./

RUN dotnet restore --disable-parallel
RUN dotnet build -c Release --force

# ======================================================================================================================
# 2 - BUILD DOCKER IMAGE
# ======================================================================================================================
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# INSTALL FFMPEG FOR ALPINE
RUN apk upgrade -U
RUN apk add ffmpeg

# INSTALL ICU FULL SUPPORT
RUN apk add icu-libs --no-cache
RUN apk add icu-data-full --no-cache

COPY --from=projects "/app/TokanPages.Backend/TokanPages.Backend.Configuration/bin/Release/net6.0" .
COPY --from=projects "/app/TokanPages.Services/TokanPages.Services.AzureBusService/bin/Release/net6.0" .
COPY --from=projects "/app/TokanPages.Services/TokanPages.Services.BatchService/bin/Release/net6.0" .
COPY --from=projects "/app/TokanPages.Services/TokanPages.Services.EmailSenderService/bin/Release/net6.0" .
COPY --from=projects "/app/TokanPages.Services/TokanPages.Services.HttpClientService/bin/Release/net6.0" .
COPY --from=projects "/app/TokanPages.Services/TokanPages.Services.VideoProcessingService/bin/Release/net6.0" .
COPY --from=projects "/app/TokanPages.Services/TokanPages.Services.SpaCachingService/bin/Release/net6.0" .
COPY --from=projects "/app/TokanPages.WebApi/TokanPages.HostedServices/bin/Release/net6.0" .

ARG ENV_VALUE
ENV ASPNETCORE_ENVIRONMENT=${ENV_VALUE}
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "TokanPages.HostedServices.dll"]
