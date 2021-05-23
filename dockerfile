# 1 - Build ClientApp
FROM node:15 AS node
WORKDIR /app

COPY ./TokanPages/ClientApp/*.* ./
COPY ./TokanPages/ClientApp/public ./public
COPY ./TokanPages/ClientApp/src ./src

ARG APP_VER
RUN date +"(%Y-%d-%m at %T)" > CURRENT_DATE_TIME 
RUN echo "Version $APP_VER $(cat CURRENT_DATE_TIME)" > VERSION_STRING
RUN sed "s/{VERSION}/$(cat VERSION_STRING)/" $PWD/.env

RUN yarn install
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
ENV ASPNETCORE_URLS=http://+:80  
EXPOSE 80
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=node /app/build ./ClientApp/build
ENTRYPOINT ["dotnet", "TokanPages.dll"]
