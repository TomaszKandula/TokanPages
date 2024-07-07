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
ENV REACT_APP_BACKEND=${APP_BACKEND}

RUN npm install
RUN if [ $ENV_VALUE = Testing ] && [ APP_BUILD_TEST = true ]; then  \
    npm install sonarqube-scanner -g; fi
RUN if [ $ENV_VALUE = Testing ] || [ $ENV_VALUE = Staging ] && [ APP_BUILD_TEST = true ]; then  \
    npm run app-test --ci --coverage; fi
RUN if [ $ENV_VALUE = Testing ] && [ APP_BUILD_TEST = true ]; then  \
    npm run sonar -Dsonar.login=${SONAR_TOKEN} -Dsonar.projectKey=${SONAR_KEY} -Dsonar.host.url=${SONAR_HOST}; fi
RUN npm run build

# 2 - Build Debian w/NGINX 
FROM debian:latest
WORKDIR /usr/share/nginx/html

RUN rm -rf ./*
RUN apt-get update
RUN apt-get -y upgrade
RUN apt-get -y install bash
RUN apt-get -y install nginx
RUN apt-get -y install nginx-full
RUN apt-get -y install nginx-extras
RUN adduser --system --no-create-home --shell /bin/false --group --disabled-login nginx
RUN chown -R nginx:nginx /var/www/html

COPY --from=node /app/build .

CMD /bin/bash -c "nginx -t"
CMD /bin/bash -c "nginx -g 'daemon off;'"
