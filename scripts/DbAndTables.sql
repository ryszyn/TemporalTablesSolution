USE Master;
GO

IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'TemporalTablesSolution')
    DROP DATABASE TemporalTablesSolution;
GO

CREATE DATABASE TemporalTablesSolution;
GO

USE TemporalTablesSolution
GO

CREATE TABLE Product (
    RecordId bigint IDENTITY(1,1) NOT NULL,
    Id uniqueidentifier NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    ValidFrom DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    ValidTo DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo),
    CONSTRAINT PK_Product PRIMARY KEY (RecordId),
    CONSTRAINT UQ_Product_Id UNIQUE(Id)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.ProductHistory));
GO
