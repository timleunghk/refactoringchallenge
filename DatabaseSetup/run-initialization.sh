echo "Wait for 30s to setup SQL Server......"
sleep 30s
echo "Wait for 30s to setup SQL Server......Completed"
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd -d master -i northwind.sql
echo "Northwind database created"