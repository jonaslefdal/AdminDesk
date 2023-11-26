docker network create AdminDeskNetwork
docker container run --rm --env "TZ=Europe/Oslo" --name mariadb -p 3308:3306/tcp -v "$(pwd)/database":/var/lib/mysql -e MYSQL_ROOT_PASSWORD=54321 -d --network=AdminDeskNetwork mariadb:latest
docker container run --rm -it -d --name admindesk --publish 80:80 --network=AdminDeskNetwork admindesk
