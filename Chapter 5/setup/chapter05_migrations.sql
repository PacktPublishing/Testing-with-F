USE [master]
GO
/****** Object:  Database [Chapter05_Migrations]    Script Date: 2/15/2015 2:34:06 PM ******/
CREATE DATABASE [Chapter05_Migrations]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Chapter05_Migrations', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Chapter05_Migrations.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Chapter05_Migrations_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Chapter05_Migrations_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Chapter05_Migrations] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Chapter05_Migrations].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Chapter05_Migrations] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET ARITHABORT OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Chapter05_Migrations] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Chapter05_Migrations] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Chapter05_Migrations] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Chapter05_Migrations] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Chapter05_Migrations] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Chapter05_Migrations] SET  MULTI_USER 
GO
ALTER DATABASE [Chapter05_Migrations] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Chapter05_Migrations] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Chapter05_Migrations] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Chapter05_Migrations] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Chapter05_Migrations] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Chapter05_Migrations]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 2/15/2015 2:34:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StreetName] [nvarchar](50) NOT NULL,
	[StreetNumber] [nvarchar](50) NULL,
	[StreetLetter] [nvarchar](50) NULL,
	[ApartmentNumber] [nvarchar](50) NULL,
	[Floor] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](50) NOT NULL,
	[Town] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Email]    Script Date: 2/15/2015 2:34:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Email](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FromAddress] [nvarchar](255) NOT NULL,
	[ToAddress] [nvarchar](255) NOT NULL,
	[Subject] [nvarchar](255) NOT NULL,
	[Body] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NationalRegistrationAddress]    Script Date: 2/15/2015 2:34:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NationalRegistrationAddress](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Person] [int] NOT NULL,
	[Address] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_NationalRegistrationAddress] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Person]    Script Date: 2/15/2015 2:34:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SSN] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[GivenName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[BirthDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VersionInfo]    Script Date: 2/15/2015 2:34:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VersionInfo](
	[Version] [bigint] NOT NULL,
	[AppliedOn] [datetime] NULL,
	[Description] [nvarchar](1024) NULL
) ON [PRIMARY]

GO
/****** Object:  Index [UC_Version]    Script Date: 2/15/2015 2:34:06 PM ******/
CREATE UNIQUE CLUSTERED INDEX [UC_Version] ON [dbo].[VersionInfo]
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[NationalRegistrationAddress]  WITH CHECK ADD  CONSTRAINT [FK_NationalRegistrationAddress_Address] FOREIGN KEY([Address])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[NationalRegistrationAddress] CHECK CONSTRAINT [FK_NationalRegistrationAddress_Address]
GO
ALTER TABLE [dbo].[NationalRegistrationAddress]  WITH CHECK ADD  CONSTRAINT [FK_NationalRegistrationAddress_Person] FOREIGN KEY([Person])
REFERENCES [dbo].[Person] ([ID])
GO
ALTER TABLE [dbo].[NationalRegistrationAddress] CHECK CONSTRAINT [FK_NationalRegistrationAddress_Person]
GO
/****** Object:  StoredProcedure [dbo].[SetupTestData]    Script Date: 2/15/2015 2:34:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SetupTestData]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- local variables

	DECLARE @PersonID1 int,
			@PersonID2 int,
			@PersonID3 int,
			@AddressID1 int,
			@AddressID2 int;

	-- Create test persons
	INSERT INTO dbo.Person (SSN, FirstName, GivenName, LastName, BirthDate)		
		VALUES ('193808209005', 'Ingela Ping', 'Ingela', 'Forsman', '1938-08-20');
	SET @PersonID1 = SCOPE_IDENTITY();
	
	INSERT INTO dbo.Person (SSN, FirstName, GivenName, LastName, BirthDate)
		VALUES ('194212259005', 'Inge Pong', 'Inge', 'Forsman', '1942-12-25');
	SET @PersonID2 = SCOPE_IDENTITY();
	
	INSERT INTO dbo.Person (SSN, FirstName, GivenName, LastName, BirthDate)
		VALUES ('196403063374', 'Jesper', 'Jesper', 'Forsman', '1964-03-06');
	SET @PersonID3 = SCOPE_IDENTITY();
	-- ... more test persons

	-- Create test addresses
	INSERT INTO dbo.[Address] (StreetName, StreetNumber, PostalCode, Town)
		VALUES ('Lantgatan', '38', '12559', 'SOLNA');
	SET @AddressID1 = SCOPE_IDENTITY();

	INSERT INTO dbo.[Address] (StreetName, StreetNumber, StreetLetter, ApartmentNumber, [Floor], PostalCode, Town)
		VALUES ('Stångmästarevägen', '11-13', 'A', '1201', '2', '15955', 'STOCKHOLM');
	SET @AddressID2 = SCOPE_IDENTITY();
	-- ... more test addresses

	-- Create relations
	INSERT INTO dbo.NationalRegistrationAddress (Person, [Address], [Date])
		VALUES (@PersonID1, @AddressID1, '1970-01-01');
	INSERT INTO dbo.NationalRegistrationAddress (Person, [Address], [Date])
		VALUES (@PersonID2, @AddressID1, '1970-01-01');
	INSERT INTO dbo.NationalRegistrationAddress (Person, [Address], [Date])
		VALUES (@PersonID3, @AddressID1, '1970-01-01');
	INSERT INTO dbo.NationalRegistrationAddress (Person, [Address], [Date])
		VALUES (@PersonID3, @AddressID2, '1982-03-06');
	-- ...more test relations

END

GO
USE [master]
GO
ALTER DATABASE [Chapter05_Migrations] SET  READ_WRITE 
GO
