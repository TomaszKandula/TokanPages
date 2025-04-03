# ======================================================================================================================
# 1 - BUILD PROJECTS AND RUN TESTS
# ======================================================================================================================
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS projects

WORKDIR /app
COPY . ./

RUN dotnet restore
RUN dotnet build -c Release

# ======================================================================================================================
# 2 - BUILD DOCKER IMAGE
# ======================================================================================================================
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# INSTALL FFMPEG
RUN apt-get update -y
RUN apt-get install -y ffmpeg

# INSTALL CHROM DEPENDENCIES
RUN apt-get install -y wget \
    unzip \
    fontconfig \
    locales \
    gconf-service \
    libasound2 \
    libatk1.0-0 \
    libc6 \
    libcairo2 \
    libcups2 \
    libdbus-1-3 \
    libexpat1 \
    libfontconfig1 \
    libgcc1 \
    libgconf-2-4 \
    libgdk-pixbuf2.0-0 \
    libglib2.0-0 \
    libgtk-3-0 \
    libnspr4 \
    libpango-1.0-0 \
    libpangocairo-1.0-0 \
    libstdc++6 \
    libx11-6 \
    libx11-xcb1 \
    libxcb1 \
    libxcomposite1 \
    libxcursor1 \
    libxdamage1 \
    libxext6 \
    libxfixes3 \
    libxi6 \
    libxrandr2 \
    libxrender1 \
    libxss1 \
    libxtst6 \
    ca-certificates \
    fonts-liberation \
    libappindicator1 \
    libnss3 \
    lsb-release \
    xdg-utils

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
