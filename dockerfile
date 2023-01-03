# 1 - Build .NET6 WebAPI
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore

# Build and test or migrate
ARG ENV_VALUE
RUN dotnet build -c Release --no-restore
RUN if [ $ENV_VALUE = Testing ] || [ $ENV_VALUE = Staging ]; \
then ASPNETCORE_ENVIRONMENT=${ENV_VALUE} dotnet test -c Release --no-build --no-restore; \
else cd /app/TokanPages.Persistence/TokanPages.Persistence.MigrationRunner/bin/Release/net6.0 && \
ASPNETCORE_ENVIRONMENT=${ENV_VALUE} dotnet TokanPages.Persistence.MigrationRunner.dll --next-prod; fi

# Publish build
RUN dotnet publish -c Release -o out

# 2 - Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:6.0

ARG ENV_VALUE
ENV ASPNETCORE_ENVIRONMENT=${ENV_VALUE}
ENV ASPNETCORE_URLS=http://+:80  

EXPOSE 80
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "TokanPages.WebApi.dll"]
