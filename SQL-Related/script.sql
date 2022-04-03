-- Creates the DB
CREATE DATABASE [RacingGroups];
GO

-- Manually Creates the tables

-- Address
CREATE TABLE RacingGroups.dbo.Addresses (
	Id int IDENTITY(1,1) NOT NULL,
	Street nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	City nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	State nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK_Addresses PRIMARY KEY (Id)
);

-- AppUser
CREATE TABLE RacingGroups.dbo.AppUser (
	Id nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Pace int NULL,
	Mileage int NULL,
	AddressId int NULL,
	CONSTRAINT PK_AppUser PRIMARY KEY (Id)
);

ALTER TABLE RacingGroups.dbo.AppUser ADD CONSTRAINT FK_AppUser_Addresses_AddressId FOREIGN KEY (AddressId) REFERENCES RacingGroups.dbo.Addresses(Id);

-- Clubs
CREATE TABLE RacingGroups.dbo.Clubs (
	Id int IDENTITY(1,1) NOT NULL,
	Title nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Description nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Image] nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	AddressId int NOT NULL,
	ClubCategory int NOT NULL,
	AppuserId nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Clubs PRIMARY KEY (Id)
);

ALTER TABLE RacingGroups.dbo.Clubs ADD CONSTRAINT FK_Clubs_Addresses_AddressId FOREIGN KEY (AddressId) REFERENCES RacingGroups.dbo.Addresses(Id) ON DELETE CASCADE;
ALTER TABLE RacingGroups.dbo.Clubs ADD CONSTRAINT FK_Clubs_AppUser_AppuserId FOREIGN KEY (AppuserId) REFERENCES RacingGroups.dbo.AppUser(Id);

-- Races
CREATE TABLE RacingGroups.dbo.Races (
	Id int IDENTITY(1,1) NOT NULL,
	Title nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Description nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Image] nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	AddressId int NOT NULL,
	RaceCategory int NOT NULL,
	AppUserId nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Races PRIMARY KEY (Id)
);

ALTER TABLE RacingGroups.dbo.Races ADD CONSTRAINT FK_Races_Addresses_AddressId FOREIGN KEY (AddressId) REFERENCES RacingGroups.dbo.Addresses(Id) ON DELETE CASCADE;
ALTER TABLE RacingGroups.dbo.Races ADD CONSTRAINT FK_Races_AppUser_AppUserId FOREIGN KEY (AppUserId) REFERENCES RacingGroups.dbo.AppUser(Id);

-- Manually populate Tables

-- Address
INSERT INTO RacingGroups.dbo.Addresses
(Street, City, State)
VALUES('123 Main St', 'Belo Horizonte', 'MG'),
	  ('694 Principal St', 'Niteroi', 'RJ'),
	  ('Lacemakers Court 22 St', 'Charllote', 'NC'),
	  ('123 Main St', 'Belo Horizonte', 'MG'),
	  ('Lacemakers Court 22 St', 'Charllote', 'NC');
	  
-- Clubs
INSERT INTO RacingGroups.dbo.Clubs
(Title, Description, [Image], AddressId, ClubCategory, AppuserId)
VALUES('Racing Club A', 'This is the description of the first club', 'https://media.istockphoto.com/vectors/checkered-flag-for-car-racing-or-rally-club-modern-illustration-vector-id1339105507', 1, 0, ''),
      ('Racing Club B', 'This is the description of the second club', 'https://media.istockphoto.com/vectors/racing-club-round-linear-logo-of-speed-racing-on-black-background-vector-id1185938729?s=612x612', 2, 0, ''),
	  ('Racing Club C', 'This is the description of the third club', 'https://media.istockphoto.com/vectors/design-of-racing-car-team-badge-vector-id1250970452?s=612x612', 3, 0, '');
	
-- Races
INSERT INTO RacingGroups.dbo.Races
(Title, Description, [Image], AddressId, RaceCategory, AppUserId)
VALUES('Racing 1', 'This is the description of the first race', 'https://images.pexels.com/photos/10342583/pexels-photo-10342583.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 4, 2, ''),
      ('Racing 2', 'This is the description of the first race', 'https://images.pexels.com/photos/9843281/pexels-photo-9843281.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 5, 1, '');
