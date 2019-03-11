# check to see what has ran
docker ps


#check to see what is running 
docker ps -a

#start sql server express
docker run -t -v c:\docker-temp:c:\data -d -p 1433:1433 -e sa_password=P@ssw0rd123! -e ACCEPT_EULA=Y microsoft/mssql-server-windows-express

#get the ip addresses exposed
docker exec -it 83cea2e373d2 ipconfig

#get cmd prompt in container
docker exec -it 83cea2e373d2 cmd

#stop the container
docker stop 83cea2e373d2

#add volume mapping
docker run -t -v C:\docker-temp:c:\data <image>

Clear-Host