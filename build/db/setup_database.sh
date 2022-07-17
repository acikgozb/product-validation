#sqlcmd -S localhost -U SA -P Pass@word -i setup.sql 
for i in {1..50};
do
    /opt/mssql-tools/bin/sqlcmd -S "localhost" -U "SA" -P "Pass@word" -d master -i setup.sql
    if [ $? -eq 0 ]
    then
        echo "setup.sql completed"
        break
    else
        echo "not ready yet..."
        sleep 1
    fi
done