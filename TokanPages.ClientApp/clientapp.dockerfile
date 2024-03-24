# 1 - Build ClientApp
FROM node:20 AS node
WORKDIR /app

COPY ./*.* ./
COPY ./public ./public
COPY ./src ./src

ARG ENV_VALUE
ARG API_VERSION
ARG APP_VERSION
ARG APP_BUILD_TEST
ARG APP_DATE_TIME
ARG APP_BACKEND
ARG SONAR_TOKEN
ARG SONAR_KEY
ARG SONAR_HOST

ENV REACT_APP_API_VER=${API_VERSION}
ENV REACT_APP_VERSION_NUMBER=${APP_VERSION}
ENV REACT_APP_VERSION_DATE_TIME=${APP_DATE_TIME}
ENV REACT_APP_BACKEND="http://${APP_BACKEND}"

RUN yarn install
RUN if [ $ENV_VALUE = Testing ] && [ APP_BUILD_TEST = true ]; then  \
    yarn global add sonarqube-scanner; fi
RUN if [ $ENV_VALUE = Testing ] || [ $ENV_VALUE = Staging ] && [ APP_BUILD_TEST = true ]; then  \
    yarn app-test --ci --coverage; fi
RUN if [ $ENV_VALUE = Testing ] && [ APP_BUILD_TEST = true ]; then  \
    yarn sonar -Dsonar.login=${SONAR_TOKEN} -Dsonar.projectKey=${SONAR_KEY} -Dsonar.host.url=${SONAR_HOST}; fi
RUN yarn build

# 2 - Build NGINX 
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

RUN rm -rf ./* && apk update && apk add --no-cache bash
COPY --from=node /app/build .
CMD /bin/bash -c "nginx -g 'daemon off;'"
