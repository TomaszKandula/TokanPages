# PREPARE DOCKER COMPOSE FILE
cp docker-compose-test.yml docker-compose-run.yml

# SET ENVIRONMENT VARIABLES
API_VERSION="1.0"
APP_VERSION="0.0.1"
APP_BUILD_TEST="false"
APP_DATE_TIME=$(date +"%Y-%m-%d at %T")
SONAR_TOKEN=""
SONAR_KEY=""
SONAR_HOST=""

# APPLY ENVIRONMENT VARIABLES
sed -i "" \
-e "s/\${API_VERSION}/${API_VERSION}/" \
-e "s/\${APP_VERSION}/${APP_VERSION}/" \
-e "s/\${APP_BUILD_TEST}/${APP_BUILD_TEST}/" \
-e "s/\${APP_DATE_TIME}/${APP_DATE_TIME}/" \
-e "s/\${SONAR_TOKEN}/${SONAR_TOKEN}/" \
-e "s/\${SONAR_KEY}/${SONAR_KEY}/" \
-e "s/\${SONAR_HOST}/${SONAR_HOST}/" \
docker-compose-run.yml 

# RUN DOCKER COMPOSE
docker compose -f docker-compose-run.yml up
