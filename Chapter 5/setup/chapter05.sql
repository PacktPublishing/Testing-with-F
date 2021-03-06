USE [master]
GO
/****** Object:  Database [Chapter05]    Script Date: 2/15/2015 2:33:32 PM ******/
CREATE DATABASE [Chapter05]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Chapter05', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Chapter05.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Chapter05_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Chapter05_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Chapter05] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Chapter05].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Chapter05] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Chapter05] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Chapter05] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Chapter05] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Chapter05] SET ARITHABORT OFF 
GO
ALTER DATABASE [Chapter05] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Chapter05] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Chapter05] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Chapter05] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Chapter05] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Chapter05] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Chapter05] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Chapter05] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Chapter05] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Chapter05] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Chapter05] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Chapter05] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Chapter05] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Chapter05] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Chapter05] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Chapter05] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Chapter05] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Chapter05] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Chapter05] SET  MULTI_USER 
GO
ALTER DATABASE [Chapter05] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Chapter05] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Chapter05] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Chapter05] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Chapter05] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Chapter05]
GO
/****** Object:  Table [dbo].[DirectoryTree]    Script Date: 2/15/2015 2:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DirectoryTree](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NULL,
	[IsFile] [bit] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CheckSum] [nvarchar](50) NULL,
 CONSTRAINT [PK_DirectoryTree] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Page]    Script Date: 2/15/2015 2:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[PageType_FK] [int] NOT NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PageType]    Script Date: 2/15/2015 2:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PageType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Property]    Script Date: 2/15/2015 2:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Property](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PageType_FK] [int] NOT NULL,
	[PropertyType_FK] [int] NOT NULL,
 CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropertyType]    Script Date: 2/15/2015 2:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PropertyType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropertyValue]    Script Date: 2/15/2015 2:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyValue](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](50) NOT NULL,
	[Property_FK] [int] NOT NULL,
	[Page_FK] [int] NOT NULL,
 CONSTRAINT [PK_PropertyValue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 2/15/2015 2:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_PropertyType] FOREIGN KEY([PageType_FK])
REFERENCES [dbo].[PageType] ([ID])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_PropertyType]
GO
ALTER TABLE [dbo].[Property]  WITH CHECK ADD  CONSTRAINT [FK_Property_PageType] FOREIGN KEY([PageType_FK])
REFERENCES [dbo].[PageType] ([ID])
GO
ALTER TABLE [dbo].[Property] CHECK CONSTRAINT [FK_Property_PageType]
GO
ALTER TABLE [dbo].[Property]  WITH CHECK ADD  CONSTRAINT [FK_Property_PropertyType] FOREIGN KEY([PropertyType_FK])
REFERENCES [dbo].[PropertyType] ([ID])
GO
ALTER TABLE [dbo].[Property] CHECK CONSTRAINT [FK_Property_PropertyType]
GO
ALTER TABLE [dbo].[PropertyValue]  WITH CHECK ADD  CONSTRAINT [FK_PropertyValue_Page] FOREIGN KEY([Page_FK])
REFERENCES [dbo].[Page] ([ID])
GO
ALTER TABLE [dbo].[PropertyValue] CHECK CONSTRAINT [FK_PropertyValue_Page]
GO
ALTER TABLE [dbo].[PropertyValue]  WITH CHECK ADD  CONSTRAINT [FK_PropertyValue_Property] FOREIGN KEY([Property_FK])
REFERENCES [dbo].[Property] ([ID])
GO
ALTER TABLE [dbo].[PropertyValue] CHECK CONSTRAINT [FK_PropertyValue_Property]
GO
/****** Object:  StoredProcedure [dbo].[GetPagesOfPageType]    Script Date: 2/15/2015 2:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPagesOfPageType]
	@PageType varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SET FMTONLY OFF;

	DECLARE @Properties varchar(MAX),
			@Query AS NVARCHAR(MAX)


	-- extract columns as comma separated string from page type
	SELECT @Properties = STUFF(
		(SELECT ',' + property.[Name]
         FROM [Property] property
		 INNER JOIN [PageType] pageType ON property.PageType_FK = pageType.ID
	     WHERE pageType.Name = @PageType
         FOR XML PATH('')), 1, 1, '')

SET @Query = N'SELECT pageID, ' + @Properties + N' FROM
	(
		SELECT [page].ID as pageID,
			   property.[Name] as name,
			   propertyValue.[Value] as value
		FROM [Property] property
			INNER JOIN [PageType] pageType ON property.PageType_FK = pageType.ID
			INNER JOIN [Page] [page] ON [page].PageType_FK = pageType.ID
			INNER JOIN [PropertyValue] propertyValue ON propertyValue.Property_FK = property.ID AND propertyValue.Page_FK = [page].ID
		WHERE pageType.Name = ''' + @PageType + N'''
	) x

	PIVOT
	(
		max(value)
		FOR name IN (' + @Properties + N')
		) p'

exec sp_executesql @query
END


GO
/****** Object:  StoredProcedure [dbo].[Truncate]    Script Date: 2/15/2015 2:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Truncate] AS
BEGIN
	DELETE FROM [PropertyValue]
	DELETE FROM [Page]
	DELETE FROM [Property]
	DELETE FROM [PropertyType]
	DELETE FROM [PageType]
END


GO
USE [master]
GO
ALTER DATABASE [Chapter05] SET  READ_WRITE 
GO
