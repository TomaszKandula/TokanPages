# 1 - Build ClientApp
FROM node:15 AS node
WORKDIR /app

COPY ./TokanPages/ClientApp/*.* ./
RUN yarn install

COPY ./TokanPages/ClientApp/public ./public
COPY ./TokanPages/ClientApp/src ./src
RUN yarn app-test --ci --coverage
RUN yarn build

# 2 - Build .NET Core app
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore

# Build and run all tests
RUN dotnet build -c Release --no-restore
RUN dotnet test -c Release --no-build --no-restore

# Publish build
RUN dotnet publish -c Release -o out

# 3 - Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=node /app/build ./ClientApp/build
ENTRYPOINT ["dotnet", "TokanPages.dll"]
