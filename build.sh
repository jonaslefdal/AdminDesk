#!/bin/zsh

docker kill admindesk

docker image build -t admindesk .

docker container run --rm -it -d --name admindesk --network=admindesknetwork -p 80:80 -e ASPNETCORE_ENVIRONMENT=Development admindesk

echo
echo "Link: http://localhost:80/"
echo

