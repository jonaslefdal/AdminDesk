@echo off

:: Kill running instance of container
docker kill AdminDesk

:: Builds image specified in Dockerfile
docker image build -t AdminDesk .

:: Starts container with web application and maps port 80 (ext) to 80 (internal)
docker container run --rm -it -d --name AdminDesk --publish 80:80 AdminDesk

echo.
echo "Link: http://localhost:80/"
echo.

pause