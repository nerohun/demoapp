
/****** Object:  Database [demo]    Script Date: 26/02/2025 20:25:48 ******/
CREATE DATABASE [demo]
 CONTAINMENT = NONE
 

/****** Object:  Table [dbo].[Patient]    Script Date: 26/02/2025 20:25:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE [demo]
GO
CREATE TABLE [dbo].[Patient](
	[Name] [nchar](100) NULL,
	[Address] [nchar](200) NULL,
	[TajNumber] [nchar](11) NULL,
	[Complaint] [nchar](255) NULL,
	[Diagnosis] [nchar](255) NULL,
	[ArrivedAt] [datetime] NOT NULL,
	[LastModifiedAt] [datetime] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
