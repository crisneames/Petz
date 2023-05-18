USE [master]
GO

IF db_id('Petz') IS NULL
  CREATE DATABASE [Petz]
GO
USE [Petz]
GO

DROP TABLE IF EXISTS [users];
DROP TABLE IF EXISTS [pets];
DROP TABLE IF EXISTS [posts];
DROP TABLE IF EXISTS [comments];
DROP TABLE IF EXISTS [reactions];
DROP TABLE IF EXISTS [postReactions];
DROP TABLE IF EXISTS [petPosts];

CREATE TABLE [users] (
  [Id] int PRIMARY KEY identity NOT NULL,
  [FirebaseId] nvarchar(255),
  [FullName] nvarchar(255),
  [Email] nvarchar(255),
  [Username] nvarchar(255),
  [Password] nvarchar(255)
)
GO

CREATE TABLE [pets] (
  [Id] int PRIMARY KEY identity NOT NULL,
  [Name] nvarchar(255),
  [KindOfPet] nvarchar(255),
  [ImageUrl] nvarchar(255),
  [UserId] int
)
GO

CREATE TABLE [posts] (
  [Id] int PRIMARY KEY identity NOT NULL,
  [Post] nvarchar(255),
  [Date] date,
  [ImageUrl] time,
  [UserId] int
)
GO

CREATE TABLE [comments] (
  [Id] int PRIMARY KEY identity NOT NULL,
  [Comment] nvarchar(255),
  [UserId] int,
  [PostId] int,
  [Date] date
)
GO

CREATE TABLE [reactions] (
  [Id] int PRIMARY KEY identity NOT NULL,
  [Text] nvarchar(255)
)
GO

CREATE TABLE [postReactions] (
  [Id] int PRIMARY KEY identity NOT NULL,
  [PostId] int,
  [ReactionId] int,
  [UserId] int
)
GO

CREATE TABLE [petPosts] (
  [Id] int PRIMARY KEY identity NOT NULL,
  [PetId] int,
  [PostId] int
)
GO

ALTER TABLE [pets] ADD FOREIGN KEY ([UserId]) REFERENCES [users] ([Id])
GO

ALTER TABLE [posts] ADD FOREIGN KEY ([UserId]) REFERENCES [users] ([Id])
GO

ALTER TABLE [postReactions] ADD FOREIGN KEY ([UserId]) REFERENCES [users] ([Id])
GO

ALTER TABLE [postReactions] ADD FOREIGN KEY ([ReactionId]) REFERENCES [reactions] ([Id])
GO

ALTER TABLE [postReactions] ADD FOREIGN KEY ([PostId]) REFERENCES [posts] ([Id])
GO

ALTER TABLE [comments] ADD FOREIGN KEY ([PostId]) REFERENCES [posts] ([Id])
GO

ALTER TABLE [comments] ADD FOREIGN KEY ([UserId]) REFERENCES [users] ([Id])
GO

ALTER TABLE [petPosts] ADD FOREIGN KEY ([PetId]) REFERENCES [pets] ([Id])
GO

ALTER TABLE [petPosts] ADD FOREIGN KEY ([PostId]) REFERENCES [posts] ([Id])
GO
