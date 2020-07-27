#!/bin/sh
service boletador stop
docker-compose build --no-cache --force-rm 
service boletador start

