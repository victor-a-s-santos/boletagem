#!/bin/sh
DBNAME=Boletador_Features
SQLCMD=/opt/mssql-tools/bin/sqlcmd
CMDPARAMS="-S mssql -U sa -P sA123456"

$SQLCMD $CMDPARAMS -q "USE master; ALTER DATABASE [$DBNAME] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$DBNAME]; CREATE DATABASE [$DBNAME];"
for f in *.sql; do echo "Processing $f file.."; $SQLCMD $CMDPARAMS -d $DBNAME -i $f; done
