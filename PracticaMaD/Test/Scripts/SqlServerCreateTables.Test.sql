/* 
 * SQL Server Script
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *     Configure the @Default_DB_Path variable with the path where 
 *     database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */


 /******************************************************************************/
 /*** PATH to store the db files. This path must exists in the local system. ***/
 /******************************************************************************/
 DECLARE @Default_DB_Path as VARCHAR(64)  
 SET @Default_DB_Path = N'C:\SourceCode\DataBase\'
 
USE [master]


/***** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'practicaMaD_test')
DROP DATABASE [practicaMaD_test]


USE [master]


/* DataBase Creation */

	                              
DECLARE @sql nvarchar(500)

SET @sql = 
  N'CREATE DATABASE [practicaMaD_test] 
    ON PRIMARY ( NAME = practicaMaD_test, FILENAME = "' + @Default_DB_Path + N'practicaMaD_test.mdf")
    LOG ON ( NAME = practicaMaD_test_log, FILENAME = "' + @Default_DB_Path + N'practicaMaD_test_log.ldf")'

EXEC(@sql)
PRINT N'Database [practicaMaD_test] created.'
GO


USE [practicaMaD_test]


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
	eventDate DATETIME NOT NULL,
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
	recommendationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
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
	joinDate DATETIME DEFAULT CURRENT_TIMESTAMP,

	CONSTRAINT [PK_Membership] PRIMARY KEY (userId ASC , groupId ASC),
	CONSTRAINT [FK_GroupMembership] FOREIGN KEY (groupId) REFERENCES GroupTable(groupId),
	CONSTRAINT [FK_UserMembership] FOREIGN KEY (userId) REFERENCES UserProfile(usrId) 
)

