ENV_VALUE="Testing"
APP_NAME="tokanpages-test"

docker build . --build-arg "ENV_VALUE=$ENV_VALUE" -t "$APP_NAME"
docker run --rm -it -p 5009:80 "$APP_NAME"
