#!/bin/bash
# PREPARE CONFIGURATION
cp docker-compose-test-linux.yml docker-compose-run.yml
cp TokanPages.ClientApp/nginx/nginx-http.conf TokanPages.ClientApp/nginx/nginx.conf

# SET ENVIRONMENT VARIABLES
API_VERSION="1.0"
APP_VERSION="0.0.1"
APP_BUILD_TEST="false"
APP_DATE_TIME=$(date +"%Y-%m-%d at %T")
APP_BACKEND="localhost:3000"
SERVER_NAME="localhost"
SONAR_TOKEN=""
SONAR_KEY=""
SONAR_HOST=""

# APPLY ENVIRONMENT VARIABLES
sed -i \
-e "s/\${API_VERSION}/${API_VERSION}/" \
-e "s/\${APP_VERSION}/${APP_VERSION}/" \
-e "s/\${APP_BUILD_TEST}/${APP_BUILD_TEST}/" \
-e "s/\${APP_DATE_TIME}/${APP_DATE_TIME}/" \
-e "s/\${APP_BACKEND}/${APP_BACKEND}/" \
-e "s/\${SONAR_TOKEN}/${SONAR_TOKEN}/" \
-e "s/\${SONAR_KEY}/${SONAR_KEY}/" \
-e "s/\${SONAR_HOST}/${SONAR_HOST}/" \
docker-compose-run.yml 
sed -i \
-e "s/\${SERVER_NAME}/${SERVER_NAME}/" \
TokanPages.ClientApp/nginx/nginx.conf 

# RUN DOCKER COMPOSE
docker compose -f docker-compose-run.yml up -d
