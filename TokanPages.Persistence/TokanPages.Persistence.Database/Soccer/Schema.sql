CREATE SCHEMA [Soccer] AUTHORIZATION [dbo]

IF EXISTS (SELECT * FROM sys.database_principals WHERE NAME = 'AppAccessDb')
BEGIN
    GRANT ALTER ON SCHEMA::Soccer TO AppAccessDb
    GRANT DELETE ON SCHEMA::Soccer TO AppAccessDb
    GRANT INSERT ON SCHEMA::Soccer TO AppAccessDb
    GRANT SELECT ON SCHEMA::Soccer TO AppAccessDb
    GRANT UPDATE ON SCHEMA::Soccer TO AppAccessDb
    GRANT REFERENCES ON SCHEMA::Soccer TO AppAccessDb
END
