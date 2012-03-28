
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/25/2012 10:14:50
-- Generated from EDMX file: c:\users\rune.rystad\documents\visual studio 2010\Projects\DataContext\DataContext.Core\DataContextModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DataContextDemo];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Contexts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contexts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Contexts'
CREATE TABLE [dbo].[Contexts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [IsTest] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL
);
GO

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [ContextId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Contexts'
ALTER TABLE [dbo].[Contexts]
ADD CONSTRAINT [PK_Contexts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ContextId] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [FK_ContextPerson]
    FOREIGN KEY ([ContextId])
    REFERENCES [dbo].[Contexts]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContextPerson'
CREATE INDEX [IX_FK_ContextPerson]
ON [dbo].[People]
    ([ContextId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------