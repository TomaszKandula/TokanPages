APP_NAME="tokanpages-test"
docker build . -t "$APP_NAME"
docker run --rm -it -p 5009:80 "$APP_NAME"