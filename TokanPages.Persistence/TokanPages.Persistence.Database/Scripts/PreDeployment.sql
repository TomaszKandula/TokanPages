USE MASTER

IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE NAME = 'AppAccessDb')
BEGIN
    CREATE LOGIN [AppAccessDb] WITH PASSWORD = N'Password001', DEFAULT_LANGUAGE = 'us_english', CHECK_POLICY = OFF;
END

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE NAME = 'AppAccessDb')
BEGIN
    CREATE USER [AppAccessDb] FOR LOGIN [AppAccessDb];
    EXEC sp_addrolemember 'db_datareader', AppAccessDb
    EXEC sp_addrolemember 'db_datawriter', AppAccessDb
END
