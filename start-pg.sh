#!/usr/bin/env bash

docker run \
    --name acos-db \
    -d \
    -e POSTGRES_DB=acos \
    -e POSTGRES_USER=acos \
    -e POSTGRES_PASSWORD=acos \
    -p \
    5432:5432 postgres

