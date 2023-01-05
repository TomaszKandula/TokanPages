CREATE USER AppAccessDb FROM LOGIN AppAccessDb

EXEC sp_addrolemember 'db_datareader', AppAccessDb
EXEC sp_addrolemember 'db_datawriter', AppAccessDb

GRANT CREATE TABLE TO AppAccessDb
GRANT ALTER ON SCHEMA::dbo TO AppAccessDb

GRANT DELETE ON SCHEMA::dbo TO AppAccessDb
GRANT INSERT ON SCHEMA::dbo TO AppAccessDb
GRANT SELECT ON SCHEMA::dbo TO AppAccessDb
GRANT UPDATE ON SCHEMA::dbo TO AppAccessDb
GRANT REFERENCES ON SCHEMA::dbo TO AppAccessDb
