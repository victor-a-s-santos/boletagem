#!/bin/sh
service boletador-features stop
docker-compose build --no-cache --force-rm 
service boletador start

