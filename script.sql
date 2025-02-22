USE [master]
GO
/****** Object:  Database [WeatherAPP]    Script Date: 22.02.2025 22:00:42 ******/
CREATE DATABASE [WeatherAPP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WeatherAPP', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\WeatherAPP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WeatherAPP_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\WeatherAPP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [WeatherAPP] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WeatherAPP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WeatherAPP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WeatherAPP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WeatherAPP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WeatherAPP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WeatherAPP] SET ARITHABORT OFF 
GO
ALTER DATABASE [WeatherAPP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WeatherAPP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WeatherAPP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WeatherAPP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WeatherAPP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WeatherAPP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WeatherAPP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WeatherAPP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WeatherAPP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WeatherAPP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WeatherAPP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WeatherAPP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WeatherAPP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WeatherAPP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WeatherAPP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WeatherAPP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WeatherAPP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WeatherAPP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WeatherAPP] SET  MULTI_USER 
GO
ALTER DATABASE [WeatherAPP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WeatherAPP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WeatherAPP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WeatherAPP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WeatherAPP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WeatherAPP] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [WeatherAPP] SET QUERY_STORE = ON
GO
ALTER DATABASE [WeatherAPP] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [WeatherAPP]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 22.02.2025 22:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [uniqueidentifier] NOT NULL,
	[TelegramID] [bigint] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[FavoriteCity] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WeatherHistory]    Script Date: 22.02.2025 22:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeatherHistory](
	[HistoryID] [uniqueidentifier] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[RequestDate] [datetime] NOT NULL,
	[City] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[HistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[WeatherHistory] ADD  DEFAULT (newid()) FOR [HistoryID]
GO
USE [master]
GO
ALTER DATABASE [WeatherAPP] SET  READ_WRITE 
GO
