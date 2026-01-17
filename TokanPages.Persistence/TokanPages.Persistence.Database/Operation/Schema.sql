CREATE SCHEMA [Operation] AUTHORIZATION [dbo]

IF EXISTS (SELECT * FROM sys.database_principals WHERE NAME = 'AppAccessDb')
BEGIN
    GRANT ALTER ON SCHEMA::Operation TO AppAccessDb
    GRANT DELETE ON SCHEMA::Operation TO AppAccessDb
    GRANT INSERT ON SCHEMA::Operation TO AppAccessDb
    GRANT SELECT ON SCHEMA::Operation TO AppAccessDb
    GRANT UPDATE ON SCHEMA::Operation TO AppAccessDb
    GRANT REFERENCES ON SCHEMA::Operation TO AppAccessDb
END
