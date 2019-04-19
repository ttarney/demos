docker pull microsoft/mssql-server-windows-express


# check to see what has ran
docker ps


#check to see what is running 
docker ps -a

docker images
#start sql server express
docker run --name sqlexpress -t -v c:\docker-temp:c:\data -d -p 1433:1433 -e sa_password=P@ssw0rd123! -e ACCEPT_EULA=Y microsoft/mssql-server-windows-express

# point to the volume share on the image not the host
attach_dbs [{'dbName':'AdventureWorks2017','dbFiles':['C:\\data\\AdventureWorks2017.mdf','C:\\data\\AdventureWorks2017_log.ldf']}]
#get the ip addresses exposed
docker exec -it 341b953b11b8 ipconfig

#get cmd prompt in container
docker exec -it 83cea2e373d2 cmd

#stop the container
docker stop 83cea2e373d2

# or by name
docker start sqlexpress
docker stop sqlexpress

#add volume mapping
# another comment
docker run -t -v C:\docker-temp:c:\data <image>

Clear-Host

NET STOP SQL Server (instance)
