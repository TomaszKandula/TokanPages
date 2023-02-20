# Use script file "__testrun.dev.sh" for local development. 
# You can provide SonarQube keys to perform static analysis during development. 
ENV_VALUE="Testing"
API_VERSION="1.0"
APP_VERSION="0.0.1-local-dev"
BUILD_TIMESTAMP=$(date +"%Y-%m-%d at %T")
ALLOWED_ORIGINS="http://localnode:5000/;"
APP_FRONTEND="http://localhost:3000"
APP_BACKEND="http://localhost:5000"
SONAR_TOKEN=""
SONAR_KEY=""
SONAR_HOST=""

docker build . \
  --build-arg "ENV_VALUE=$ENV_VALUE" \
  --build-arg "API_VERSION=$API_VERSION" \
  --build-arg "APP_VERSION=$APP_VERSION" \
  --build-arg "APP_DATE_TIME=$BUILD_TIMESTAMP" \
  --build-arg "APP_FRONTEND=$APP_FRONTEND" \
  --build-arg "APP_BACKEND=$APP_BACKEND" \
  --build-arg "APP_SENTRY=$APP_SENTRY" \
  --build-arg "ALLOWED_ORIGINS=$ALLOWED_ORIGINS" \
  --build-arg "SONAR_TOKEN=$SONAR_TOKEN" \
  --build-arg "SONAR_KEY=$SONAR_KEY" \
  --build-arg "SONAR_HOST=$SONAR_HOST" \
  -t nginx-clientapp

MACHINE_IP=$(ifconfig en0 | grep inet | grep -v inet6 | awk '{print $2}')

docker run \
  --add-host localnode:"$MACHINE_IP" \
  --rm -it -p 3000:80 nginx-clientapp
