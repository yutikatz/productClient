USE [master]
GO
/****** Object:  Database [Products]    Script Date: 25/11/2022 02:42:50 ******/
CREATE DATABASE [Products]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Products', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Products.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Products_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Products_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Products].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Products] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Products] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Products] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Products] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Products] SET ARITHABORT OFF 
GO
ALTER DATABASE [Products] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Products] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Products] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Products] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Products] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Products] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Products] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Products] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Products] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Products] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Products] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Products] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Products] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Products] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Products] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Products] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Products] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Products] SET RECOVERY FULL 
GO
ALTER DATABASE [Products] SET  MULTI_USER 
GO
ALTER DATABASE [Products] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Products] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Products] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Products] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Products] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Products', N'ON'
GO
ALTER DATABASE [Products] SET QUERY_STORE = OFF
GO
USE [Products]
GO
ALTER DATABASE SCOPED CONFIGURATION SET ACCELERATED_PLAN_FORCING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_ADAPTIVE_JOINS = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_MEMORY_GRANT_FEEDBACK = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_ON_ROWSTORE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET DEFERRED_COMPILATION_TV = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_ONLINE = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_RESUMABLE = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET GLOBAL_TEMPORARY_TABLE_AUTO_DROP = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET INTERLEAVED_EXECUTION_TVF = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ISOLATE_SECURITY_POLICY_CARDINALITY = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LAST_QUERY_PLAN_STATS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LIGHTWEIGHT_QUERY_PROFILING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET OPTIMIZE_FOR_AD_HOC_WORKLOADS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ROW_MODE_MEMORY_GRANT_FEEDBACK = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET TSQL_SCALAR_UDF_INLINING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET VERBOSE_TRUNCATION_WARNINGS = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET XTP_PROCEDURE_EXECUTION_STATISTICS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET XTP_QUERY_EXECUTION_STATISTICS = OFF;
GO
USE [Products]
GO
/****** Object:  Table [dbo].[images]    Script Date: 25/11/2022 02:42:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[images](
	[code] [int] NOT NULL,
	[router] [nvarchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 25/11/2022 02:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[code] [int] IDENTITY(1000,1) NOT NULL,
	[name] [nvarchar](20) NULL,
	[description] [nvarchar](20) NULL,
	[startDate] [datetime] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[images] ([code], [router]) VALUES (1000, N'D:\1.png')
INSERT [dbo].[images] ([code], [router]) VALUES (1001, N'lllll')
INSERT [dbo].[images] ([code], [router]) VALUES (1015, N'יוטי תעודת זהות 001.')
INSERT [dbo].[images] ([code], [router]) VALUES (1065, N'logo.png')
INSERT [dbo].[images] ([code], [router]) VALUES (1108, N'mouse.bmp')
INSERT [dbo].[images] ([code], [router]) VALUES (1110, N'computer.bmp')
SET IDENTITY_INSERT [dbo].[products] ON 

INSERT [dbo].[products] ([code], [name], [description], [startDate]) VALUES (1108, N'עכבר', N'מיקימאוס אפור', CAST(N'2022-11-25T01:40:36.913' AS DateTime))
INSERT [dbo].[products] ([code], [name], [description], [startDate]) VALUES (1110, N'מחשב', N'חדיש ומקצועי', CAST(N'2022-11-25T01:48:12.707' AS DateTime))
SET IDENTITY_INSERT [dbo].[products] OFF
/****** Object:  StoredProcedure [dbo].[AddIMG]    Script Date: 25/11/2022 02:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddIMG]
@code int,
@router nvarchar(20)
AS 
INSERT INTO images (code,router)
VALUES (@code,@router)
GO
/****** Object:  StoredProcedure [dbo].[AddProduct]    Script Date: 25/11/2022 02:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddProduct]
@name nvarchar(20),
@description nvarchar(20)
AS 
INSERT INTO products (name,description,startDate)
VALUES (@name,@description,GETDATE())
GO
/****** Object:  StoredProcedure [dbo].[AddTABLE]    Script Date: 25/11/2022 02:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTABLE]
AS 
CREATE Table products(
[code][int] IDENTITY(1000,1) NOT NULL,
name [nvarchar](20),
description[nvarchar](20),
startDate datetime)
GO
/****** Object:  StoredProcedure [dbo].[AddTABLEIMG]    Script Date: 25/11/2022 02:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTABLEIMG]
AS 
CREATE Table images(
[code][int] NOT NULL,
router [nvarchar](20))
GO
/****** Object:  StoredProcedure [dbo].[DeleteIMG]    Script Date: 25/11/2022 02:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteIMG]
@code int
AS
DELETE FROM images WHERE code=@code;
GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 25/11/2022 02:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteProduct]
@code int
AS
DELETE FROM products WHERE code=@code;
GO
/****** Object:  StoredProcedure [dbo].[EditIMG]    Script Date: 25/11/2022 02:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EditIMG]
@code int,
@router nvarchar(20)
AS
UPDATE images
SET router = @router
WHERE code=@code 
GO
/****** Object:  StoredProcedure [dbo].[EditProduct]    Script Date: 25/11/2022 02:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EditProduct]
@code int,
@name nvarchar(20),
@description nvarchar(20)
AS
UPDATE products
SET name = @name, description = @description
WHERE code=@code 
GO
/****** Object:  StoredProcedure [dbo].[GetAllProducts]    Script Date: 25/11/2022 02:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProducts]
AS
SELECT P.code,P.name,P.description,convert(nvarchar(20),P.startdate,105) as startDate,I.router
 FROM products P
LEFT  JOIN images I
ON P.code= I.code
GO
/****** Object:  StoredProcedure [dbo].[GetByCode]    Script Date: 25/11/2022 02:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetByCode]
@code int
AS
SELECT * FROM products
WHERE code =@code
--קבלת כל המוצרים
GO
/****** Object:  StoredProcedure [dbo].[GetByName]    Script Date: 25/11/2022 02:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetByName]
@name nvarchar(20)
AS
SELECT * FROM products 
WHERE name LIKE @name
GO
/****** Object:  StoredProcedure [dbo].[SortByDescription]    Script Date: 25/11/2022 02:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SortByDescription]
AS
SELECT  P.code,P.name,P.description,convert(nvarchar(20),P.startdate,105) as startDate,I.router
 FROM products P
LEFT  JOIN images I
ON P.code= I.code
ORDER BY P.description  
GO
/****** Object:  StoredProcedure [dbo].[SortByName]    Script Date: 25/11/2022 02:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SortByName]
AS
SELECT  P.code,P.name,P.description,convert(nvarchar(20),P.startdate,105) as startDate,I.router
 FROM products  P
 LEFT  JOIN images I
 ON P.code= I.code
ORDER BY name 
GO
USE [master]
GO
ALTER DATABASE [Products] SET  READ_WRITE 
GO
