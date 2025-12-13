#!/bin/bash

# Stop all docker containers
docker stop $(docker ps -a -q)

# Delete all docker containers
docker rm $(docker ps -a -q)

# Delete all docker images
docker rmi $(docker images -q)

# Docker clear
docker system prune -f

# Build and run all docker containers
sh docker-compose-up-prod.sh

# Show container status
docker ps -a
