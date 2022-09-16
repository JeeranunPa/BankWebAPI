/****** Object:  Database [BankDB]    Script Date: 9/16/2022 3:46:58 PM ******/
CREATE DATABASE [BankDB]
ALTER DATABASE [BankDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BankDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BankDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BankDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BankDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BankDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BankDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BankDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BankDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BankDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BankDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BankDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BankDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BankDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BankDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BankDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BankDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BankDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BankDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BankDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BankDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BankDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BankDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BankDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BankDB] SET RECOVERY FULL 
GO
ALTER DATABASE [BankDB] SET  MULTI_USER 
GO
ALTER DATABASE [BankDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BankDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BankDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BankDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BankDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BankDB', N'ON'
GO
ALTER DATABASE [BankDB] SET QUERY_STORE = OFF
GO
USE [BankDB]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 9/16/2022 3:46:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[acountId] [int] IDENTITY(1,1) NOT NULL,
	[companyId] [int] NOT NULL,
	[officeId] [int] NOT NULL,
	[accountNo] [nvarchar](20) NOT NULL,
	[idno] [nvarchar](15) NOT NULL,
	[accountName] [nchar](200) NULL,
	[firstName] [nvarchar](100) NULL,
	[lastName] [nvarchar](100) NULL,
	[accountTypId] [int] NULL,
	[email] [nvarchar](255) NULL,
	[mobile] [nvarchar](20) NULL,
	[birthDate] [date] NULL,
	[issueDate] [date] NULL,
	[expiryDate] [date] NULL,
	[amount] [numeric](18, 2) NULL,
	[amountFee] [numeric](13, 2) NULL,
	[emailFlag] [nvarchar](1) NULL,
	[smsFlag] [nvarchar](1) NULL,
	[recStatus] [nvarchar](1) NULL,
	[createBy] [nvarchar](100) NULL,
	[createDate] [datetime] NULL,
	[updateBy] [nvarchar](100) NULL,
	[updateDate] [datetime] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[acountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountTransactions]    Script Date: 9/16/2022 3:46:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountTransactions](
	[transactionsId] [int] IDENTITY(1,1) NOT NULL,
	[companyId] [int] NOT NULL,
	[officeId] [int] NOT NULL,
	[transactionsTypeId] [int] NOT NULL,
	[accountNo] [nvarchar](20) NOT NULL,
	[trandte] [datetime] NULL,
	[tranferTo] [nvarchar](20) NULL,
	[tranferFrom] [nvarchar](20) NULL,
	[tranferFlag] [nvarchar](2) NULL,
	[amount] [numeric](13, 2) NULL,
	[amountFee] [numeric](13, 2) NULL,
	[recStatus] [nvarchar](1) NULL,
	[createBy] [nvarchar](100) NULL,
	[createDate] [datetime] NULL,
 CONSTRAINT [PK_AccountTransection] PRIMARY KEY CLUSTERED 
(
	[transactionsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountType]    Script Date: 9/16/2022 3:46:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountType](
	[accountTypId] [int] IDENTITY(1,1) NOT NULL,
	[accountCode] [nvarchar](5) NULL,
	[accountDesc] [nvarchar](500) NULL,
	[recStatus] [nvarchar](1) NULL,
	[createBy] [nvarchar](100) NULL,
	[createDate] [datetime] NULL,
	[updateBy] [nvarchar](100) NULL,
	[updateDate] [datetime] NULL,
 CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED 
(
	[accountTypId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConfigMaster]    Script Date: 9/16/2022 3:46:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConfigMaster](
	[configId] [int] IDENTITY(1,1) NOT NULL,
	[configCode] [nvarchar](5) NULL,
	[configDesc] [nvarchar](100) NULL,
	[configUrl] [nvarchar](100) NULL,
	[configValue1] [nvarchar](100) NULL,
	[configvalue2] [nvarchar](100) NULL,
	[recStatus] [nvarchar](1) NULL,
	[createBy] [nvarchar](100) NULL,
	[createDate] [datetime] NULL,
 CONSTRAINT [PK_IBANConfig] PRIMARY KEY CLUSTERED 
(
	[configId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionsType]    Script Date: 9/16/2022 3:46:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionsType](
	[transectionTypeId] [int] IDENTITY(1,1) NOT NULL,
	[transectionCode] [nvarchar](10) NULL,
	[transectionDesc] [nvarchar](200) NULL,
	[recStatus] [nvarchar](1) NULL,
	[createBy] [nvarchar](100) NULL,
	[createDate] [datetime] NULL,
 CONSTRAINT [PK_TransectionType] PRIMARY KEY CLUSTERED 
(
	[transectionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AccountType] ON 

INSERT [dbo].[AccountType] ([accountTypId], [accountCode], [accountDesc], [recStatus], [createBy], [createDate], [updateBy], [updateDate]) VALUES (1, N'001', N'Savings account', N'A', N'Admin     ', NULL, NULL, NULL)
INSERT [dbo].[AccountType] ([accountTypId], [accountCode], [accountDesc], [recStatus], [createBy], [createDate], [updateBy], [updateDate]) VALUES (2, N'002', N'Salary account', N'A', N'Admin     ', NULL, NULL, NULL)
INSERT [dbo].[AccountType] ([accountTypId], [accountCode], [accountDesc], [recStatus], [createBy], [createDate], [updateBy], [updateDate]) VALUES (3, N'003', N'Fixed deposit account', N'A', N'Admin     ', NULL, NULL, NULL)
INSERT [dbo].[AccountType] ([accountTypId], [accountCode], [accountDesc], [recStatus], [createBy], [createDate], [updateBy], [updateDate]) VALUES (4, N'004', N'Recurring deposit account', N'A', N'Admin     ', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[AccountType] OFF
SET IDENTITY_INSERT [dbo].[ConfigMaster] ON 

INSERT [dbo].[ConfigMaster] ([configId], [configCode], [configDesc], [configUrl], [configValue1], [configvalue2], [recStatus], [createBy], [createDate]) VALUES (1, N'001', N'Generate IBAN', N'http://randomiban.com/?country=Netherlands', N'[class^=ibandisplay]', NULL, N'A', N'Admin', NULL)
SET IDENTITY_INSERT [dbo].[ConfigMaster] OFF
SET IDENTITY_INSERT [dbo].[TransactionsType] ON 

INSERT [dbo].[TransactionsType] ([transectionTypeId], [transectionCode], [transectionDesc], [recStatus], [createBy], [createDate]) VALUES (1, N'001', N'Deposit', N'A', N'Admin', NULL)
INSERT [dbo].[TransactionsType] ([transectionTypeId], [transectionCode], [transectionDesc], [recStatus], [createBy], [createDate]) VALUES (2, N'002', N'Transfer', N'A', N'Admin', NULL)
INSERT [dbo].[TransactionsType] ([transectionTypeId], [transectionCode], [transectionDesc], [recStatus], [createBy], [createDate]) VALUES (3, N'003', N'Withdraw', N'A', N'Admin', NULL)
SET IDENTITY_INSERT [dbo].[TransactionsType] OFF
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_recStatus]  DEFAULT (N'A') FOR [recStatus]
GO
ALTER TABLE [dbo].[AccountTransactions] ADD  CONSTRAINT [DF_AccountTransection_recStatus]  DEFAULT (N'A') FOR [recStatus]
GO
ALTER TABLE [dbo].[AccountType] ADD  CONSTRAINT [DF_AccountType_recStatus]  DEFAULT (N'A') FOR [recStatus]
GO
ALTER TABLE [dbo].[ConfigMaster] ADD  CONSTRAINT [DF_IBANConfig_recStatus]  DEFAULT (N'A') FOR [recStatus]
GO
ALTER TABLE [dbo].[TransactionsType] ADD  CONSTRAINT [DF_TransectionType_recStatus]  DEFAULT (N'A') FOR [recStatus]
GO
USE [master]
GO
ALTER DATABASE [BankDB] SET  READ_WRITE 
GO
