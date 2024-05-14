export server="localhost"
export port="27017"
export connectionString="mongodb://localhost:27017/"
export database="userDB"
export collection="userCol"
export connAuk="http://localhost:5102"
echo $database $collection $connectionString
dotnet run server="$server" port="$port"