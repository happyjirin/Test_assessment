
/****** Object:  Database [TestAssessment]    Script Date: 7/27/2021 10:49:07 AM ******/
CREATE DATABASE [TestAssessment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TestAssessment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TestAssessment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TestAssessment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TestAssessment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

USE [TestAssessment]
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestAssessment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [TestAssessment] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [TestAssessment] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [TestAssessment] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [TestAssessment] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [TestAssessment] SET ARITHABORT OFF 
GO

ALTER DATABASE [TestAssessment] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [TestAssessment] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [TestAssessment] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [TestAssessment] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [TestAssessment] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [TestAssessment] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [TestAssessment] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [TestAssessment] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [TestAssessment] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [TestAssessment] SET  DISABLE_BROKER 
GO

ALTER DATABASE [TestAssessment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [TestAssessment] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [TestAssessment] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [TestAssessment] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [TestAssessment] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [TestAssessment] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [TestAssessment] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [TestAssessment] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [TestAssessment] SET  MULTI_USER 
GO

ALTER DATABASE [TestAssessment] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [TestAssessment] SET DB_CHAINING OFF 
GO

ALTER DATABASE [TestAssessment] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [TestAssessment] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [TestAssessment] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [TestAssessment] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [TestAssessment] SET QUERY_STORE = OFF
GO

ALTER DATABASE [TestAssessment] SET  READ_WRITE 
GO

USE [TestAssessment]
GO

/****** Object:  Table [dbo].[Platform]    Script Date: 7/27/2021 11:02:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Platform](
	[Id] [int] NOT NULL,
	[UniqueName] [nvarchar](250) NULL,
	[Latitude] [decimal](9, 6) NULL,
	[Longitude] [decimal](9, 6) NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Well](
	[Id] [int] NOT NULL,
	[PlatformId] [int] NOT NULL,
	[UniqueName] [nvarchar](250) NULL,
	[Latitude] [decimal](9, 6) NULL,
	[Longitude] [decimal](9, 6) NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_Well] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Well]  WITH CHECK ADD  CONSTRAINT [FK_Well_Platform] FOREIGN KEY([PlatformId])
REFERENCES [dbo].[Platform] ([Id])
GO

ALTER TABLE [dbo].[Well] CHECK CONSTRAINT [FK_Well_Platform]
GO