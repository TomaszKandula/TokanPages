#!/bin/bash
# PREPARE DOCKER COMPOSE FILE
cp docker-compose-prod.yml docker-compose-run.yml
cp TokanPages.ClientApp/nginx/nginx-template.conf TokanPages.ClientApp/nginx/nginx.conf

# SET ENVIRONMENT VARIABLES
API_VERSION="1.0"
APP_VERSION=${GIT_APP_VERSION} # THIS IS SET BY SEMANTIC RELEASE
APP_BUILD_TEST="false"
APP_DATE_TIME=$(date +"%Y-%m-%d at %T")
APP_BACKEND="tomkandula.com:6100"
SERVER_NAME="tomkandula.com"
SONAR_TOKEN=""
SONAR_KEY=""
SONAR_HOST=""

# APPLY ENVIRONMENT VARIABLES
if [ -f "/etc/debian_version" ]; then
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
  -e "s/\${APP_BACKEND}/${APP_BACKEND}/" \
  TokanPages.ClientApp/nginx/nginx.conf 
else
  sed -i "" \
  -e "s/\${API_VERSION}/${API_VERSION}/" \
  -e "s/\${APP_VERSION}/${APP_VERSION}/" \
  -e "s/\${APP_BUILD_TEST}/${APP_BUILD_TEST}/" \
  -e "s/\${APP_DATE_TIME}/${APP_DATE_TIME}/" \
  -e "s/\${APP_BACKEND}/${APP_BACKEND}/" \
  -e "s/\${SONAR_TOKEN}/${SONAR_TOKEN}/" \
  -e "s/\${SONAR_KEY}/${SONAR_KEY}/" \
  -e "s/\${SONAR_HOST}/${SONAR_HOST}/" \
  docker-compose-run.yml 
  sed -i "" \
  -e "s/\${SERVER_NAME}/${SERVER_NAME}/" \
  -e "s/\${APP_BACKEND}/${APP_BACKEND}/" \
  TokanPages.ClientApp/nginx/nginx.conf 
fi

# RUN DOCKER COMPOSE
docker compose -f docker-compose-run.yml up -d
