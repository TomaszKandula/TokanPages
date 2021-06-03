APP_VERSION="0.0.1-local-dev"
BUILD_TIMESTAMP=$(date +"%Y-%m-%d at %T")
ALLOW_CORS="http://localnode:5000/;"
APP_FRONTEND="http://localhost:3000"
APP_BACKEND="http://localhost:5000"
APP_STORAGE="https://maindbstorage.blob.core.windows.net/tokanpages"
APP_SENTRY="https://f6e140265f624d8188ccb9dff4e6e995@o479380.ingest.sentry.io/5524546"

docker build . \
  --build-arg "APP_VERSION=$APP_VERSION" \
  --build-arg "APP_DATE_TIME=$BUILD_TIMESTAMP" \
  --build-arg "APP_FRONTEND=$APP_FRONTEND" \
  --build-arg "APP_BACKEND=$APP_BACKEND" \
  --build-arg "APP_STORAGE=$APP_STORAGE" \
  --build-arg "APP_SENTRY=$APP_SENTRY" \
  --build-arg "ALLOW_CORS=$ALLOW_CORS" \
  -t nginx-clientapp

MACHINE_IP=$(ifconfig en0 | grep inet | grep -v inet6 | awk '{print $2}')

docker run \
  --add-host localnode:"$MACHINE_IP" \
  --rm -it -p 3000:80 nginx-clientapp
