/* 
 * SQL Server Script
 * 
 * This script can be directly executed to configure the test database from
 * PCs located at CECAFI Lab. The database and the corresponding users are 
 * already created in the sql server, so it will create the tables needed 
 * in the samples. 
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *      Configure within the CREATE DATABASE sql-sentence the path where 
 *      database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */

 
USE [practicaMaD]


/* ********** Drop Table UserProfile if already exists *********** */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') AND type in ('U'))
DROP TABLE [UserProfile]
GO

/* ********** Drop Table Category if already exists *********** */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') AND type in ('C'))
DROP TABLE [Category]
GO

/* ********** Drop Table EventTable if already exists *********** */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[EventTable]') AND type in ('E'))
DROP TABLE [EventTable]
GO

/* ********** Drop Table CommentTable if already exists *********** */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[CommentTable]') AND type in ('O'))
DROP TABLE [CommentTable]
GO

/* ********** Drop Table GroupTable if already exists *********** */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[GroupTable]') AND type in ('G'))
DROP TABLE [GroupTable]
GO

/* ********** Drop Table GroupTable if already exists *********** */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Recommendation]') AND type in ('R'))
DROP TABLE [Recommendation]
GO

/*
 * Create tables.
 * UserProfile table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  UserProfile */

CREATE TABLE UserProfile (
	usrId bigint IDENTITY(1,1) NOT NULL,
	loginName varchar(30) NOT NULL,
	enPassword varchar(50) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(40) NOT NULL,
	email varchar(60) NOT NULL,
	language varchar(2) NOT NULL,
	country varchar(2) NOT NULL,

	CONSTRAINT [PK_UserProfile] PRIMARY KEY (usrId),
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName)
)


CREATE NONCLUSTERED INDEX [IX_UserProfileIndexByLoginName]
ON [UserProfile] ([loginName] ASC)

PRINT N'Table UserProfile created.'
GO


/*
 * Create tables.
 * Category table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  Category */

CREATE TABLE Category (
	categoryId bigint IDENTITY (1,1) NOT NULL,
	categoryName varchar (30) NOT NULL

	CONSTRAINT [PK_Category] PRIMARY KEY (categoryId),
	CONSTRAINT [UniqueKey_Category] UNIQUE (categoryName)
)

PRINT N'Table Category created.'
GO


/*
 * Create tables.
 * EventTable table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  EventTable */

CREATE TABLE EventTable (
	eventId bigint IDENTITY (1,1) NOT NULL,
	eventName varchar(30) NOT NULL,
	eventDate datetime NOT NULL,
	categoryId bigint NOT NULL,
	reseña VARCHAR(100) NOT NULL,
	hasComments bit NOT NULL


	CONSTRAINT [PK_EventTable] PRIMARY KEY (eventId),
	CONSTRAINT [FK_EventCategory] FOREIGN KEY (categoryId) REFERENCES Category(categoryId)
)

PRINT N'Table EventTable created.'
GO



/*
 * Create tables.
 * CommentTable table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  CommentTable */

CREATE TABLE CommentTable (
	commId bigint IDENTITY(1,1) NOT NULL,
	eventId bigint NOT NULL,
	userId bigint NOT NULL,
	userName varchar(30) NOT NULL,
	body varchar(300) NOT NULL,
	commDate DATE NOT NULL,

	CONSTRAINT [PK_CommentTable] PRIMARY KEY (commId),
	CONSTRAINT [FK_EventComment] FOREIGN KEY (eventId) REFERENCES EventTable(eventId),
	CONSTRAINT [FK_UserComment] FOREIGN KEY (userId) REFERENCES UserProfile(usrId)
)

CREATE NONCLUSTERED INDEX [IX_CommentTableIndexByCommDate]
ON [CommentTable] ([commDate] ASC)




/*
 * Create tables.
 * GroupTable table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  GroupTable */

CREATE TABLE GroupTable (
	groupId bigint IDENTITY(1,1) NOT NULL,
	groupName varchar(30) NOT NULL,
	body varchar(255),
	categoryId bigint NOT NULL,
	userId bigint NOT NULL,

	CONSTRAINT [PK_GroupTable] PRIMARY KEY (groupId),
	CONSTRAINT [FK_CategoryGroup] FOREIGN KEY (categoryId) REFERENCES Category(categoryId),
	CONSTRAINT [FK_UserGroup] FOREIGN KEY (userId) REFERENCES UserProfile(usrId) 
)



/*
 * Create tables.
 * Recommendation table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  Recommendation */

CREATE TABLE Recommendation(
	recomId bigint IDENTITY(1,1) NOT NULL,
	justification varchar(255),
	recommendationDate datetime NOT NULL,
	eventId bigint NOT NULL,
	groupId bigint NOT NULL,
	userId bigint NOT NULL,

	CONSTRAINT [PK_Recommendation] PRIMARY KEY (recomId),
	CONSTRAINT [FK_EventRecommendation] FOREIGN KEY (eventId) REFERENCES EventTable(eventId),
	CONSTRAINT [FK_GroupRecommendation] FOREIGN KEY (groupId) REFERENCES GroupTable(groupId),
	CONSTRAINT [FK_UserRecommendation] FOREIGN KEY (userId) REFERENCES UserProfile(usrId)

)

CREATE TABLE Membership(
	userId bigint NOT NULL,
	groupId bigint NOT NULL,
	joinDate datetime NOT NULL,

	CONSTRAINT [PK_Membership] PRIMARY KEY (userId ASC , groupId ASC),
	CONSTRAINT [FK_GroupMembership] FOREIGN KEY (groupId) REFERENCES GroupTable(groupId) ON DELETE CASCADE,
	CONSTRAINT [FK_UserMembership] FOREIGN KEY (userId) REFERENCES UserProfile(usrId) ON DELETE CASCADE
)

/*Inserts para la comprobación de la aplicacion*/
PRINT N'Creacion de categorias'
INSERT INTO Category (categoryName) VALUES ('Futbol')
INSERT INTO Category (categoryName) VALUES ('Baloncesto')
INSERT INTO Category (categoryName) VALUES ('Tenis')
INSERT INTO Category (categoryName) VALUES ('Voleyball')
INSERT INTO Category (categoryName) VALUES ('Rugby')


PRINT N'Creacion de eventos'
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 1 Futbol', '2019-12-1', (select[categoryId] from Category where categoryName = 'Futbol'), 'Partido de futbol', 1)
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 1 Baloncesto', '2019-12-2', (select[categoryId] from Category where categoryName = 'Baloncesto'), 'Partido de baloncesto', 1)
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 1 Tenis', '2019-12-3', (select[categoryId] from Category where categoryName = 'Tenis'), 'Partido de tenis', 1)
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 2 Futbol', '2019-12-4', (select[categoryId] from Category where categoryName = 'Futbol'), 'Partido de futbol 2', 1)
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 2 Baloncesto', '2019-12-5', (select[categoryId] from Category where categoryName = 'Baloncesto'), 'Partido de baloncesto 2', 1)
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 2 Tenis', '2019-12-6', (select[categoryId] from Category where categoryName = 'Tenis'), 'Partido de tenis 2', 1)
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 3 Futbol', '2019-12-7', (select[categoryId] from Category where categoryName = 'Futbol'), 'Partido de futbol 3', 1)
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 3 Baloncesto', '2019-12-8', (select[categoryId] from Category where categoryName = 'Baloncesto'), 'Partido de baloncesto 3', 1)
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 3 Tenis', '2020-01-5', (select[categoryId] from Category where categoryName = 'Tenis'), 'Partido de tenis 3', 1)
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 1 Voley', '2019-12-7', (select[categoryId] from Category where categoryName = 'Voleyball'), 'Partido de voley', 1)
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 2 Voley', '2019-12-7', (select[categoryId] from Category where categoryName = 'Voleyball'), 'Partido de voley 2', 1)
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 3 Voley', '2019-12-7', (select[categoryId] from Category where categoryName = 'Voleyball'), 'Partido de voley 3', 1)
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 1 Rugby', '2019-12-11', (select[categoryId] from Category where categoryName = 'Rugby'), 'Partido de rugby', 1)
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 2 Rugby', '2019-01-12', (select[categoryId] from Category where categoryName = 'Rugby'), 'Partido de rugby 2', 1)
INSERT INTO EventTable (eventName, eventDate, categoryId, reseña, hasComments) VALUES ('Partido 3 Rugby', '2019-12-09', (select[categoryId] from Category where categoryName = 'Rugby'), 'Partido de rugby 3', 1)