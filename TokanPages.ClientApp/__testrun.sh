APP_VERSION="0.0.1-local-dev"
BUILD_TIMESTAMP=$(date +"%Y-%m-%d at %T")
ALLOWED_ORIGINS="http://localnode:5000/;"
APP_FRONTEND="http://localhost:3000"
APP_BACKEND="http://localhost:5000"
APP_STORAGE="https://maindbstorage.blob.core.windows.net/tokanpages"
APP_SENTRY="https://d689c23e973449e696af516279e92ffe@o479380.ingest.sentry.io/5816109"

docker build . \
  --build-arg "APP_VERSION=$APP_VERSION" \
  --build-arg "APP_DATE_TIME=$BUILD_TIMESTAMP" \
  --build-arg "APP_FRONTEND=$APP_FRONTEND" \
  --build-arg "APP_BACKEND=$APP_BACKEND" \
  --build-arg "APP_STORAGE=$APP_STORAGE" \
  --build-arg "APP_SENTRY=$APP_SENTRY" \
  --build-arg "ALLOWED_ORIGINS=$ALLOWED_ORIGINS" \
  -t nginx-clientapp

MACHINE_IP=$(ifconfig en0 | grep inet | grep -v inet6 | awk '{print $2}')

docker run \
  --add-host localnode:"$MACHINE_IP" \
  --rm -it -p 3000:80 nginx-clientapp
