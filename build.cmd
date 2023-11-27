@echo off

:: Kill running instance of container
docker kill admindesk

:: Builds image specified in Dockerfile
docker image build -t admindesk .

:: Starts container with web application and maps port 80 (ext) to 80 (internal)
docker container run --rm -it -d --name admindesk --network=admindesknetwork --publish 80:80 admindesk

echo.
echo "Link: http://localhost:80/"
echo.

pause
