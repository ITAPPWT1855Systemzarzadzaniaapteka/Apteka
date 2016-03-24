
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/24/2016 12:21:49
-- Generated from EDMX file: C:\Users\zellu_000\documents\visual studio 2015\Projects\Apteka\Apteka\Models\AptekaDbContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [apteka];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_Faktura_ToHurtownia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Fakturas] DROP CONSTRAINT [FK_Faktura_ToHurtownia];
GO
IF OBJECT_ID(N'[dbo].[FK_Operacja_ToLek]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Operacjas] DROP CONSTRAINT [FK_Operacja_ToLek];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Faktura_operacja_Faktura]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Faktura_operacja] DROP CONSTRAINT [FK_Faktura_operacja_Faktura];
GO
IF OBJECT_ID(N'[dbo].[FK_Faktura_operacja_Operacja]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Faktura_operacja] DROP CONSTRAINT [FK_Faktura_operacja_Operacja];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[C__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[C__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Fakturas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fakturas];
GO
IF OBJECT_ID(N'[dbo].[Hurtownias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Hurtownias];
GO
IF OBJECT_ID(N'[dbo].[Leks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Leks];
GO
IF OBJECT_ID(N'[dbo].[Operacjas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Operacjas];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Sprawdz_faktury]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sprawdz_faktury];
GO
IF OBJECT_ID(N'[dbo].[Sprawdz_zawartosc_faktury]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sprawdz_zawartosc_faktury];
GO
IF OBJECT_ID(N'[dbo].[Stan_magazynu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stan_magazynu];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[Faktura_operacja]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Faktura_operacja];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Fakturas'
CREATE TABLE [dbo].[Fakturas] (
    [Id_faktura] int IDENTITY(1,1) NOT NULL,
    [Id_hurtownia] int  NULL,
    [Numer] varchar(50)  NULL,
    [Netto] float  NULL,
    [Brutto] float  NULL
);
GO

-- Creating table 'Hurtownias'
CREATE TABLE [dbo].[Hurtownias] (
    [ID_hurtownia] int IDENTITY(1,1) NOT NULL,
    [Nazwa] varchar(50)  NULL,
    [NIP] int  NULL
);
GO

-- Creating table 'Leks'
CREATE TABLE [dbo].[Leks] (
    [Id_lek] int IDENTITY(1,1) NOT NULL,
    [Nazwa] varchar(200)  NULL,
    [Postac] varchar(200)  NULL,
    [Opakowanie] int  NULL,
    [Dawka] varchar(100)  NULL
);
GO

-- Creating table 'Operacjas'
CREATE TABLE [dbo].[Operacjas] (
    [ID_operacja] int IDENTITY(1,1) NOT NULL,
    [ID_lek] int  NOT NULL,
    [Data] varchar(12)  NULL,
    [ID_user] int  NOT NULL,
    [Przychod] int  NULL,
    [Rozchod] int  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Sprawdz_faktury'
CREATE TABLE [dbo].[Sprawdz_faktury] (
    [ID_faktura] int  NOT NULL,
    [Data] varchar(12)  NULL,
    [Numer] varchar(50)  NULL,
    [Nazwa] varchar(50)  NULL,
    [Netto] float  NULL,
    [Brutto] float  NULL
);
GO

-- Creating table 'Sprawdz_zawartosc_faktury'
CREATE TABLE [dbo].[Sprawdz_zawartosc_faktury] (
    [Id_faktura] int  NOT NULL,
    [id_lek] int  NULL,
    [Nazwa] varchar(200)  NULL,
    [Dawka] varchar(100)  NULL,
    [Opakowanie] int  NULL,
    [Postac] varchar(200)  NULL,
    [Ilość_zakupionego_leku] int  NULL
);
GO

-- Creating table 'Stan_magazynu'
CREATE TABLE [dbo].[Stan_magazynu] (
    [ID_lek] int  NOT NULL,
    [Nazwa] varchar(200)  NULL,
    [Dawka] varchar(100)  NULL,
    [Postac] varchar(200)  NULL,
    [Opakowanie] int  NULL,
    [Obecny_Stan_Magazynu] int  NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Faktura_operacja'
CREATE TABLE [dbo].[Faktura_operacja] (
    [Fakturas_Id_faktura] int  NOT NULL,
    [Operacjas_ID_operacja] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id_faktura] in table 'Fakturas'
ALTER TABLE [dbo].[Fakturas]
ADD CONSTRAINT [PK_Fakturas]
    PRIMARY KEY CLUSTERED ([Id_faktura] ASC);
GO

-- Creating primary key on [ID_hurtownia] in table 'Hurtownias'
ALTER TABLE [dbo].[Hurtownias]
ADD CONSTRAINT [PK_Hurtownias]
    PRIMARY KEY CLUSTERED ([ID_hurtownia] ASC);
GO

-- Creating primary key on [Id_lek] in table 'Leks'
ALTER TABLE [dbo].[Leks]
ADD CONSTRAINT [PK_Leks]
    PRIMARY KEY CLUSTERED ([Id_lek] ASC);
GO

-- Creating primary key on [ID_operacja] in table 'Operacjas'
ALTER TABLE [dbo].[Operacjas]
ADD CONSTRAINT [PK_Operacjas]
    PRIMARY KEY CLUSTERED ([ID_operacja] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [ID_faktura] in table 'Sprawdz_faktury'
ALTER TABLE [dbo].[Sprawdz_faktury]
ADD CONSTRAINT [PK_Sprawdz_faktury]
    PRIMARY KEY CLUSTERED ([ID_faktura] ASC);
GO

-- Creating primary key on [Id_faktura] in table 'Sprawdz_zawartosc_faktury'
ALTER TABLE [dbo].[Sprawdz_zawartosc_faktury]
ADD CONSTRAINT [PK_Sprawdz_zawartosc_faktury]
    PRIMARY KEY CLUSTERED ([Id_faktura] ASC);
GO

-- Creating primary key on [ID_lek] in table 'Stan_magazynu'
ALTER TABLE [dbo].[Stan_magazynu]
ADD CONSTRAINT [PK_Stan_magazynu]
    PRIMARY KEY CLUSTERED ([ID_lek] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- Creating primary key on [Fakturas_Id_faktura], [Operacjas_ID_operacja] in table 'Faktura_operacja'
ALTER TABLE [dbo].[Faktura_operacja]
ADD CONSTRAINT [PK_Faktura_operacja]
    PRIMARY KEY CLUSTERED ([Fakturas_Id_faktura], [Operacjas_ID_operacja] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [Id_hurtownia] in table 'Fakturas'
ALTER TABLE [dbo].[Fakturas]
ADD CONSTRAINT [FK_Faktura_ToHurtownia]
    FOREIGN KEY ([Id_hurtownia])
    REFERENCES [dbo].[Hurtownias]
        ([ID_hurtownia])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Faktura_ToHurtownia'
CREATE INDEX [IX_FK_Faktura_ToHurtownia]
ON [dbo].[Fakturas]
    ([Id_hurtownia]);
GO

-- Creating foreign key on [ID_lek] in table 'Operacjas'
ALTER TABLE [dbo].[Operacjas]
ADD CONSTRAINT [FK_Operacja_ToLek]
    FOREIGN KEY ([ID_lek])
    REFERENCES [dbo].[Leks]
        ([Id_lek])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Operacja_ToLek'
CREATE INDEX [IX_FK_Operacja_ToLek]
ON [dbo].[Operacjas]
    ([ID_lek]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- Creating foreign key on [Fakturas_Id_faktura] in table 'Faktura_operacja'
ALTER TABLE [dbo].[Faktura_operacja]
ADD CONSTRAINT [FK_Faktura_operacja_Faktura]
    FOREIGN KEY ([Fakturas_Id_faktura])
    REFERENCES [dbo].[Fakturas]
        ([Id_faktura])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Operacjas_ID_operacja] in table 'Faktura_operacja'
ALTER TABLE [dbo].[Faktura_operacja]
ADD CONSTRAINT [FK_Faktura_operacja_Operacja]
    FOREIGN KEY ([Operacjas_ID_operacja])
    REFERENCES [dbo].[Operacjas]
        ([ID_operacja])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Faktura_operacja_Operacja'
CREATE INDEX [IX_FK_Faktura_operacja_Operacja]
ON [dbo].[Faktura_operacja]
    ([Operacjas_ID_operacja]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------