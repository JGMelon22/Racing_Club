-- Creates the DB
CREATE DATABASE RacingGroups;
GO

USE RacingGroups;


-- Manually Creates the tables
-- RacingGroups.dbo.Addresses definition

-- Drop table

-- DROP TABLE RacingGroups.dbo.Addresses;

CREATE TABLE RacingGroups.dbo.Addresses (
	Id int IDENTITY(1,1) NOT NULL,
	Street nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	City nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	State nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK_Addresses PRIMARY KEY (Id)
);


-- RacingGroups.dbo.AspNetRoles definition

-- Drop table

-- DROP TABLE RacingGroups.dbo.AspNetRoles;

CREATE TABLE RacingGroups.dbo.AspNetRoles (
	Id nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Name nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	NormalizedName nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ConcurrencyStamp nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_AspNetRoles PRIMARY KEY (Id)
);
 CREATE  UNIQUE NONCLUSTERED INDEX RoleNameIndex ON dbo.AspNetRoles (  NormalizedName ASC  )  
	 WHERE  ([NormalizedName] IS NOT NULL)
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- RacingGroups.dbo.AspNetRoleClaims definition

-- Drop table

-- DROP TABLE RacingGroups.dbo.AspNetRoleClaims;

CREATE TABLE RacingGroups.dbo.AspNetRoleClaims (
	Id int IDENTITY(1,1) NOT NULL,
	RoleId nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ClaimType nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ClaimValue nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_AspNetRoleClaims PRIMARY KEY (Id),
	CONSTRAINT FK_AspNetRoleClaims_AspNetRoles_RoleId FOREIGN KEY (RoleId) REFERENCES RacingGroups.dbo.AspNetRoles(Id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_AspNetRoleClaims_RoleId ON dbo.AspNetRoleClaims (  RoleId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- RacingGroups.dbo.AspNetUsers definition

-- Drop table

-- DROP TABLE RacingGroups.dbo.AspNetUsers;

CREATE TABLE RacingGroups.dbo.AspNetUsers (
	Id nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Pace int NULL,
	Mileage int NULL,
	AddressId int NULL,
	AccessFailedCount int DEFAULT 0 NOT NULL,
	ConcurrencyStamp nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Email nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	EmailConfirmed bit DEFAULT CONVERT([bit],(0)) NOT NULL,
	LockoutEnabled bit DEFAULT CONVERT([bit],(0)) NOT NULL,
	LockoutEnd datetimeoffset NULL,
	NormalizedEmail nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	NormalizedUserName nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PasswordHash nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PhoneNumber nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PhoneNumberConfirmed bit DEFAULT CONVERT([bit],(0)) NOT NULL,
	SecurityStamp nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	TwoFactorEnabled bit DEFAULT CONVERT([bit],(0)) NOT NULL,
	UserName nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	City nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ProfileImageUrl nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	State nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_AspNetUsers PRIMARY KEY (Id),
	CONSTRAINT FK_AspNetUsers_Addresses_AddressId FOREIGN KEY (AddressId) REFERENCES RacingGroups.dbo.Addresses(Id)
);
 CREATE NONCLUSTERED INDEX EmailIndex ON dbo.AspNetUsers (  NormalizedEmail ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX IX_AspNetUsers_AddressId ON dbo.AspNetUsers (  AddressId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE  UNIQUE NONCLUSTERED INDEX UserNameIndex ON dbo.AspNetUsers (  NormalizedUserName ASC  )  
	 WHERE  ([NormalizedUserName] IS NOT NULL)
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- RacingGroups.dbo.Clubs definition

-- Drop table

-- DROP TABLE RacingGroups.dbo.Clubs;

CREATE TABLE RacingGroups.dbo.Clubs (
	Id int IDENTITY(1,1) NOT NULL,
	Title nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Description nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Image] nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	AddressId int NOT NULL,
	ClubCategory int NOT NULL,
	AppuserId nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Clubs PRIMARY KEY (Id),
	CONSTRAINT FK_Clubs_Addresses_AddressId FOREIGN KEY (AddressId) REFERENCES RacingGroups.dbo.Addresses(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Clubs_AspNetUsers_AppuserId FOREIGN KEY (AppuserId) REFERENCES RacingGroups.dbo.AspNetUsers(Id)
);
 CREATE NONCLUSTERED INDEX IX_Clubs_AddressId ON dbo.Clubs (  AddressId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX IX_Clubs_AppuserId ON dbo.Clubs (  AppuserId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- RacingGroups.dbo.Races definition

-- Drop table

-- DROP TABLE RacingGroups.dbo.Races;

CREATE TABLE RacingGroups.dbo.Races (
	Id int IDENTITY(1,1) NOT NULL,
	Title nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Description nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Image] nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	AddressId int NOT NULL,
	RaceCategory int NOT NULL,
	AppUserId nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Races PRIMARY KEY (Id),
	CONSTRAINT FK_Races_Addresses_AddressId FOREIGN KEY (AddressId) REFERENCES RacingGroups.dbo.Addresses(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Races_AspNetUsers_AppUserId FOREIGN KEY (AppUserId) REFERENCES RacingGroups.dbo.AspNetUsers(Id)
);
 CREATE NONCLUSTERED INDEX IX_Races_AddressId ON dbo.Races (  AddressId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX IX_Races_AppUserId ON dbo.Races (  AppUserId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- RacingGroups.dbo.AspNetUserClaims definition

-- Drop table

-- DROP TABLE RacingGroups.dbo.AspNetUserClaims;

CREATE TABLE RacingGroups.dbo.AspNetUserClaims (
	Id int IDENTITY(1,1) NOT NULL,
	UserId nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ClaimType nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ClaimValue nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_AspNetUserClaims PRIMARY KEY (Id),
	CONSTRAINT FK_AspNetUserClaims_AspNetUsers_UserId FOREIGN KEY (UserId) REFERENCES RacingGroups.dbo.AspNetUsers(Id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_AspNetUserClaims_UserId ON dbo.AspNetUserClaims (  UserId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- RacingGroups.dbo.AspNetUserLogins definition

-- Drop table

-- DROP TABLE RacingGroups.dbo.AspNetUserLogins;

CREATE TABLE RacingGroups.dbo.AspNetUserLogins (
	LoginProvider nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ProviderKey nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ProviderDisplayName nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserId nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK_AspNetUserLogins PRIMARY KEY (LoginProvider,ProviderKey),
	CONSTRAINT FK_AspNetUserLogins_AspNetUsers_UserId FOREIGN KEY (UserId) REFERENCES RacingGroups.dbo.AspNetUsers(Id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_AspNetUserLogins_UserId ON dbo.AspNetUserLogins (  UserId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- RacingGroups.dbo.AspNetUserRoles definition

-- Drop table

-- DROP TABLE RacingGroups.dbo.AspNetUserRoles;

CREATE TABLE RacingGroups.dbo.AspNetUserRoles (
	UserId nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	RoleId nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK_AspNetUserRoles PRIMARY KEY (UserId,RoleId),
	CONSTRAINT FK_AspNetUserRoles_AspNetRoles_RoleId FOREIGN KEY (RoleId) REFERENCES RacingGroups.dbo.AspNetRoles(Id) ON DELETE CASCADE,
	CONSTRAINT FK_AspNetUserRoles_AspNetUsers_UserId FOREIGN KEY (UserId) REFERENCES RacingGroups.dbo.AspNetUsers(Id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_AspNetUserRoles_RoleId ON dbo.AspNetUserRoles (  RoleId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- RacingGroups.dbo.AspNetUserTokens definition

-- Drop table

-- DROP TABLE RacingGroups.dbo.AspNetUserTokens;

CREATE TABLE RacingGroups.dbo.AspNetUserTokens (
	UserId nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	LoginProvider nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Name nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Value nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_AspNetUserTokens PRIMARY KEY (UserId,LoginProvider,Name),
	CONSTRAINT FK_AspNetUserTokens_AspNetUsers_UserId FOREIGN KEY (UserId) REFERENCES RacingGroups.dbo.AspNetUsers(Id) ON DELETE CASCADE
);
