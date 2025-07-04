USE [DVLD]
GO
/****** Object:  Table [dbo].[TestAppointments]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestAppointments](
	[TestAppointmentID] [int] IDENTITY(1,1) NOT NULL,
	[TestTypeID] [int] NOT NULL,
	[LocalDrivingLicenseApplicationID] [int] NOT NULL,
	[AppointmentDate] [smalldatetime] NOT NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
	[IsLocked] [bit] NOT NULL,
 CONSTRAINT [PK_TestAppointments] PRIMARY KEY CLUSTERED 
(
	[TestAppointmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestTypes]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestTypes](
	[TestTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TestTypeTitle] [nvarchar](100) NOT NULL,
	[TestTypeDescription] [nvarchar](500) NOT NULL,
	[TestTypeFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_TestTypes] PRIMARY KEY CLUSTERED 
(
	[TestTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tests]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[TestID] [int] IDENTITY(1,1) NOT NULL,
	[TestAppointmentID] [int] NOT NULL,
	[TestResult] [bit] NOT NULL,
	[Notes] [nvarchar](500) NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[TestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocalDrivingLicenseApplications]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalDrivingLicenseApplications](
	[LocalDrivingLicenseApplicationID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[LicenseClassID] [int] NOT NULL,
 CONSTRAINT [PK_DrivingLicsenseApplications] PRIMARY KEY CLUSTERED 
(
	[LocalDrivingLicenseApplicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[PassedTests]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[PassedTests] as

SELECT LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID,
LocalDrivingLicenseApplications.ApplicationID, TestTypes.TestTypeTitle, Tests.TestResult
FROM LocalDrivingLicenseApplications INNER JOIN TestAppointments
ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID INNER JOIN
TestTypes ON TestAppointments.TestTypeID = TestTypes.TestTypeID
GO
/****** Object:  Table [dbo].[People]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[NationalNo] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[SecondName] [nvarchar](20) NOT NULL,
	[ThirdName] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[Gendor] [tinyint] NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[NationalityCountryID] [int] NOT NULL,
	[ImagePath] [nvarchar](250) NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Applications]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[ApplicationID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationPersonID] [int] NOT NULL,
	[ApplicationDate] [datetime] NOT NULL,
	[ApplicationTypeID] [int] NOT NULL,
	[ApplicationStatus] [tinyint] NOT NULL,
	[LastStatusDate] [datetime] NOT NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[ApplicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LicenseClasses]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicenseClasses](
	[LicenseClassID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NOT NULL,
	[ClassDescription] [nvarchar](500) NOT NULL,
	[MinimumAllowedAge] [tinyint] NOT NULL,
	[DefaultValidityLength] [tinyint] NOT NULL,
	[ClassFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_LicenseClasses] PRIMARY KEY CLUSTERED 
(
	[LicenseClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[LocalDrivingLicensesApplications]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[LocalDrivingLicensesApplications] as
select R.LocalDrivingLicenseApplicationID as 'L.D.L.AppID', R.ClassName as 'Driving Class', R.NationalNo , R.FullName as 'FullName',
R.ApplicationDate as 'Application Date', (select Count(*) from PassedTests
where LocalDrivingLicenseApplicationID = R.LocalDrivingLicenseApplicationID and TestResult = 1) as 'Passed Tests', 
case R.ApplicationStatus
when 1 then 'new'
when 2 then 'Canceled'
when 3 then 'Completed'
end
as 'Status' from
(
SELECT LocalDrivingLicenseApplicationID , LicenseClasses.ClassName, People.NationalNo, 
FullName = People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName,
Applications.ApplicationDate, Applications.ApplicationStatus
FROM Applications INNER JOIN People ON Applications.ApplicationPersonID = People.PersonID INNER JOIN
LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN
LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
)R
GO
/****** Object:  Table [dbo].[Licenses]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Licenses](
	[LicenseID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[DriverID] [int] NOT NULL,
	[LicenseClass] [int] NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[Notes] [nvarchar](500) NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IssueReason] [tinyint] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_Licenses] PRIMARY KEY CLUSTERED 
(
	[LicenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[LicensesInShortDetails]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[LicensesInShortDetails] as
SELECT Licenses.LicenseID, Licenses.ApplicationID, Licenses.DriverID, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive, LicenseClasses.ClassName
FROM            LicenseClasses INNER JOIN
                         Licenses ON LicenseClasses.LicenseClassID = Licenses.LicenseClass
GO
/****** Object:  Table [dbo].[InternationalLicenses]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternationalLicenses](
	[InternationalLicenseID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[DriverID] [int] NOT NULL,
	[IssuedUsingLocalLicenseID] [int] NOT NULL,
	[IssueDate] [smalldatetime] NOT NULL,
	[ExpirationDate] [smalldatetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_InternationalLicenses] PRIMARY KEY CLUSTERED 
(
	[InternationalLicenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drivers]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drivers](
	[DriverID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
	[CreatedDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Drivers_1] PRIMARY KEY CLUSTERED 
(
	[DriverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ListDrivers]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[ListDrivers] as
select * , ((select Count(*) from Licenses where DriverID = R.DriverID and IsActive = 1) + (select Count(*) 
from InternationalLicenses where DriverID = R.DriverID and IsActive = 1)) as ActiveLicneses
from
(
SELECT Drivers.DriverID, People.PersonID, People.NationalNo, 
FullName = People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName, Drivers.CreatedDate
FROM Drivers INNER JOIN People 
ON Drivers.PersonID = People.PersonID
)R
GO
/****** Object:  Table [dbo].[DetainedLicenses]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetainedLicenses](
	[DetainID] [int] IDENTITY(1,1) NOT NULL,
	[LicenseID] [int] NOT NULL,
	[DetainDate] [smalldatetime] NOT NULL,
	[FineFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
	[IsReleased] [bit] NOT NULL,
	[ReleaseDate] [smalldatetime] NULL,
	[ReleasedByUserID] [int] NULL,
	[ReleaseApplicationID] [int] NULL,
 CONSTRAINT [PK_DetainedLicenses] PRIMARY KEY CLUSTERED 
(
	[DetainID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ListDetainedLicenses]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[ListDetainedLicenses] as
SELECT        DetainedLicenses.DetainID, DetainedLicenses.LicenseID, DetainedLicenses.DetainDate, DetainedLicenses.IsReleased, DetainedLicenses.FineFees, DetainedLicenses.ReleaseDate, People.NationalNo,  

FullName = People.FirstName + ' '+  People.SecondName + ' '+ People.ThirdName + ' '+ People.LastName, DetainedLicenses.ReleaseApplicationID
FROM            DetainedLicenses INNER JOIN
                         Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID INNER JOIN
                         Drivers ON Licenses.DriverID = Drivers.DriverID INNER JOIN
                         People ON Drivers.PersonID = People.PersonID
GO
/****** Object:  Table [dbo].[ApplicationTypes]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationTypes](
	[ApplicationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationTypeTitle] [nvarchar](150) NOT NULL,
	[ApplicationFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_ApplicationTypes] PRIMARY KEY CLUSTERED 
(
	[ApplicationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Countrie__10D160BFDBD6933F] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/29/2025 7:58:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Applications] ON 

INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (43, 1, CAST(N'2023-09-23T20:17:32.483' AS DateTime), 1, 2, CAST(N'2023-09-23T20:17:59.753' AS DateTime), 20.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (45, 1, CAST(N'2023-09-23T20:32:17.010' AS DateTime), 1, 3, CAST(N'2023-09-23T20:44:05.453' AS DateTime), 20.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (46, 1, CAST(N'2023-09-23T20:39:42.570' AS DateTime), 1, 1, CAST(N'2023-09-23T20:39:42.570' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (47, 1, CAST(N'2023-09-23T20:45:14.393' AS DateTime), 6, 3, CAST(N'2023-09-23T20:45:14.393' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (48, 1024, CAST(N'2023-09-23T21:06:37.867' AS DateTime), 1, 1, CAST(N'2023-09-23T21:06:37.867' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (49, 1, CAST(N'2023-09-23T21:09:00.460' AS DateTime), 3, 3, CAST(N'2023-09-23T21:09:00.460' AS DateTime), 10.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (50, 1, CAST(N'2023-09-23T21:15:50.123' AS DateTime), 3, 3, CAST(N'2023-09-23T21:15:50.123' AS DateTime), 10.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (51, 1, CAST(N'2023-09-23T21:16:23.963' AS DateTime), 4, 3, CAST(N'2023-09-23T21:16:23.963' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (52, 1, CAST(N'2023-09-24T03:24:20.553' AS DateTime), 1, 3, CAST(N'2023-09-24T11:08:27.533' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (61, 1, CAST(N'2023-09-24T11:10:04.557' AS DateTime), 2, 3, CAST(N'2023-09-24T11:10:04.557' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (62, 1, CAST(N'2023-09-24T11:10:46.160' AS DateTime), 4, 3, CAST(N'2023-09-24T11:10:46.160' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (63, 1, CAST(N'2023-09-24T11:11:00.693' AS DateTime), 4, 3, CAST(N'2023-09-24T11:11:00.693' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (64, 1, CAST(N'2023-09-24T11:24:21.113' AS DateTime), 3, 3, CAST(N'2023-09-24T11:24:21.113' AS DateTime), 10.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (65, 1025, CAST(N'2023-09-24T13:44:30.513' AS DateTime), 1, 3, CAST(N'2023-09-24T13:52:51.067' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (66, 1025, CAST(N'2023-09-24T13:56:34.560' AS DateTime), 3, 3, CAST(N'2023-09-24T13:56:34.560' AS DateTime), 10.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (67, 1025, CAST(N'2023-09-24T13:58:43.560' AS DateTime), 4, 3, CAST(N'2023-09-24T13:58:43.560' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (69, 1025, CAST(N'2023-09-24T14:02:25.850' AS DateTime), 6, 3, CAST(N'2023-09-24T14:02:25.850' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (77, 1, CAST(N'2023-09-28T11:57:59.607' AS DateTime), 5, 3, CAST(N'2023-09-28T11:57:59.607' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (78, 1025, CAST(N'2023-09-28T16:59:14.463' AS DateTime), 5, 3, CAST(N'2023-09-28T16:59:14.463' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (79, 1, CAST(N'2023-09-28T17:05:19.327' AS DateTime), 5, 3, CAST(N'2023-09-28T17:05:19.327' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (80, 1, CAST(N'2023-09-28T17:09:00.387' AS DateTime), 5, 3, CAST(N'2023-09-28T17:09:00.387' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (81, 1, CAST(N'2023-09-28T17:12:33.063' AS DateTime), 6, 3, CAST(N'2023-09-28T17:12:33.063' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (128, 2062, CAST(N'2025-06-11T07:25:19.653' AS DateTime), 1, 3, CAST(N'2025-06-11T07:25:19.653' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (130, 2062, CAST(N'2025-06-11T07:32:58.433' AS DateTime), 1, 3, CAST(N'2025-06-11T07:32:58.433' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (131, 2062, CAST(N'2025-06-11T07:35:02.460' AS DateTime), 1, 3, CAST(N'2025-06-11T07:35:02.460' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (153, 2062, CAST(N'2025-06-20T09:58:53.723' AS DateTime), 7, 3, CAST(N'2025-06-20T09:58:53.723' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (154, 2062, CAST(N'2025-06-20T10:01:59.520' AS DateTime), 7, 3, CAST(N'2025-06-20T10:01:59.520' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (155, 2062, CAST(N'2025-06-20T10:02:15.897' AS DateTime), 7, 3, CAST(N'2025-06-20T10:02:15.897' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (156, 2062, CAST(N'2025-06-20T11:41:29.540' AS DateTime), 7, 3, CAST(N'2025-06-20T11:41:29.540' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (157, 2062, CAST(N'2025-06-20T11:42:01.927' AS DateTime), 7, 3, CAST(N'2025-06-20T11:42:01.927' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (158, 2062, CAST(N'2025-06-20T11:42:14.810' AS DateTime), 7, 3, CAST(N'2025-06-20T11:42:14.810' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (159, 2062, CAST(N'2025-06-20T11:42:52.677' AS DateTime), 7, 3, CAST(N'2025-06-20T11:42:52.677' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (160, 2062, CAST(N'2025-06-21T06:22:27.613' AS DateTime), 7, 3, CAST(N'2025-06-21T06:22:27.613' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (161, 2064, CAST(N'2025-06-22T18:03:28.230' AS DateTime), 1, 2, CAST(N'2025-06-22T18:03:28.230' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (163, 2064, CAST(N'2025-06-22T18:06:06.873' AS DateTime), 1, 3, CAST(N'2025-06-22T18:06:06.873' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (165, 2064, CAST(N'2025-06-22T18:20:41.150' AS DateTime), 7, 3, CAST(N'2025-06-22T18:20:41.150' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (166, 2064, CAST(N'2025-06-22T18:31:14.307' AS DateTime), 7, 3, CAST(N'2025-06-22T18:31:14.307' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (167, 2064, CAST(N'2025-06-22T18:31:29.033' AS DateTime), 7, 3, CAST(N'2025-06-22T18:31:29.033' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (170, 2065, CAST(N'2025-06-22T21:48:30.523' AS DateTime), 1, 3, CAST(N'2025-06-22T21:48:30.523' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (171, 2065, CAST(N'2025-06-22T21:49:33.587' AS DateTime), 1, 3, CAST(N'2025-06-22T21:49:33.587' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (172, 2065, CAST(N'2025-06-22T21:52:04.167' AS DateTime), 7, 3, CAST(N'2025-06-22T21:52:04.167' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (173, 2065, CAST(N'2025-06-22T21:53:39.150' AS DateTime), 7, 3, CAST(N'2025-06-22T21:53:39.150' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (174, 2065, CAST(N'2025-06-22T21:53:49.947' AS DateTime), 7, 3, CAST(N'2025-06-22T21:53:49.947' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (175, 2066, CAST(N'2025-06-22T22:02:37.560' AS DateTime), 1, 3, CAST(N'2025-06-22T22:02:37.560' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (177, 2066, CAST(N'2025-06-22T22:06:23.823' AS DateTime), 7, 3, CAST(N'2025-06-22T22:06:23.823' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (178, 2066, CAST(N'2025-06-22T22:08:03.100' AS DateTime), 7, 3, CAST(N'2025-06-22T22:08:03.100' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (179, 2065, CAST(N'2025-06-22T22:44:45.650' AS DateTime), 1, 3, CAST(N'2025-06-22T22:44:45.650' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (180, 2065, CAST(N'2025-06-22T22:47:45.277' AS DateTime), 7, 3, CAST(N'2025-06-22T22:47:45.277' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (181, 2065, CAST(N'2025-06-24T11:27:36.970' AS DateTime), 6, 3, CAST(N'2025-06-24T11:27:36.970' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (182, 2062, CAST(N'2025-06-24T15:47:15.483' AS DateTime), 6, 3, CAST(N'2025-06-24T15:47:15.483' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (183, 1041, CAST(N'2025-06-24T22:42:00.357' AS DateTime), 1, 3, CAST(N'2025-06-24T22:42:00.357' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (184, 1041, CAST(N'2025-06-24T22:47:48.180' AS DateTime), 6, 3, CAST(N'2025-06-24T22:47:48.180' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (185, 2066, CAST(N'2025-06-25T11:10:31.437' AS DateTime), 1, 3, CAST(N'2025-06-25T11:10:31.437' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (186, 1047, CAST(N'2025-06-25T11:25:07.750' AS DateTime), 1, 3, CAST(N'2025-06-25T11:25:07.750' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (188, 2066, CAST(N'2025-06-25T21:40:36.090' AS DateTime), 2, 3, CAST(N'2025-06-25T21:40:36.090' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (189, 1047, CAST(N'2025-06-25T21:53:12.763' AS DateTime), 2, 3, CAST(N'2025-06-25T21:53:12.763' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (190, 1047, CAST(N'2025-06-25T22:26:45.537' AS DateTime), 6, 3, CAST(N'2025-06-25T22:26:45.537' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (191, 1047, CAST(N'2025-06-25T22:29:17.027' AS DateTime), 2, 3, CAST(N'2025-06-25T22:29:17.027' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (192, 2066, CAST(N'2025-06-25T22:36:11.713' AS DateTime), 6, 3, CAST(N'2025-06-25T22:36:11.713' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (193, 2066, CAST(N'2025-06-25T23:38:18.503' AS DateTime), 6, 3, CAST(N'2025-06-25T23:38:18.503' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (194, 2066, CAST(N'2025-06-26T06:37:20.483' AS DateTime), 6, 3, CAST(N'2025-06-26T06:37:21.243' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (195, 1, CAST(N'2025-06-26T07:00:41.310' AS DateTime), 6, 3, CAST(N'2025-06-26T07:00:41.310' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (196, 1025, CAST(N'2025-06-26T07:28:53.923' AS DateTime), 6, 3, CAST(N'2025-06-26T07:28:53.923' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (197, 2065, CAST(N'2025-06-26T07:33:59.573' AS DateTime), 6, 3, CAST(N'2025-06-26T07:33:59.573' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (198, 1064, CAST(N'2025-06-26T09:23:09.340' AS DateTime), 1, 3, CAST(N'2025-06-26T09:23:09.340' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (199, 1064, CAST(N'2025-06-26T09:26:06.223' AS DateTime), 2, 3, CAST(N'2025-06-26T09:26:06.223' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (200, 1064, CAST(N'2025-06-26T09:26:25.827' AS DateTime), 6, 3, CAST(N'2025-06-26T09:26:25.827' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (201, 1064, CAST(N'2025-06-26T09:37:24.810' AS DateTime), 2, 3, CAST(N'2025-06-26T09:37:24.810' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (202, 1064, CAST(N'2025-06-26T09:40:03.920' AS DateTime), 6, 3, CAST(N'2025-06-26T09:40:03.920' AS DateTime), 50.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (203, 1047, CAST(N'2025-06-26T22:03:26.597' AS DateTime), 3, 3, CAST(N'2025-06-26T22:03:26.597' AS DateTime), 10.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (204, 1047, CAST(N'2025-06-26T22:08:44.093' AS DateTime), 3, 3, CAST(N'2025-06-26T22:08:44.093' AS DateTime), 10.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (205, 1064, CAST(N'2025-06-26T22:14:59.370' AS DateTime), 3, 3, CAST(N'2025-06-26T22:14:59.370' AS DateTime), 10.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (206, 1064, CAST(N'2025-06-26T22:16:02.403' AS DateTime), 3, 3, CAST(N'2025-06-26T22:16:02.403' AS DateTime), 10.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (207, 2066, CAST(N'2025-06-26T22:18:14.563' AS DateTime), 3, 3, CAST(N'2025-06-26T22:18:14.563' AS DateTime), 10.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (208, 2066, CAST(N'2025-06-26T22:18:52.630' AS DateTime), 3, 3, CAST(N'2025-06-26T22:18:52.630' AS DateTime), 10.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (209, 2066, CAST(N'2025-06-26T22:20:36.373' AS DateTime), 2, 3, CAST(N'2025-06-26T22:20:36.373' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (210, 1064, CAST(N'2025-06-27T22:20:17.497' AS DateTime), 5, 3, CAST(N'2025-06-27T22:20:17.497' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (211, 1047, CAST(N'2025-06-27T22:24:00.647' AS DateTime), 5, 3, CAST(N'2025-06-27T22:24:00.647' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (212, 2066, CAST(N'2025-06-27T22:30:09.937' AS DateTime), 5, 2, CAST(N'2025-06-27T22:30:09.937' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (213, 2066, CAST(N'2025-06-27T22:32:04.410' AS DateTime), 5, 3, CAST(N'2025-06-27T22:32:04.410' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (214, 1, CAST(N'2025-06-28T06:50:41.500' AS DateTime), 5, 3, CAST(N'2025-06-28T06:50:41.500' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (215, 2068, CAST(N'2025-06-28T11:13:32.620' AS DateTime), 1, 3, CAST(N'2025-06-28T11:13:32.620' AS DateTime), 15.0000, 1033)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (216, 2068, CAST(N'2025-06-28T11:14:38.290' AS DateTime), 7, 3, CAST(N'2025-06-28T11:14:38.290' AS DateTime), 5.0000, 1033)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (218, 2068, CAST(N'2025-06-28T11:17:42.060' AS DateTime), 6, 3, CAST(N'2025-06-28T11:17:42.060' AS DateTime), 50.0000, 1033)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (219, 2068, CAST(N'2025-06-28T11:19:04.893' AS DateTime), 3, 3, CAST(N'2025-06-28T11:19:04.893' AS DateTime), 10.0000, 1033)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (220, 2068, CAST(N'2025-06-28T11:19:46.060' AS DateTime), 3, 3, CAST(N'2025-06-28T11:19:46.060' AS DateTime), 10.0000, 1033)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (221, 2068, CAST(N'2025-06-28T11:21:34.160' AS DateTime), 5, 3, CAST(N'2025-06-28T11:21:34.160' AS DateTime), 15.0000, 1033)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (222, 1064, CAST(N'2025-06-28T11:22:08.667' AS DateTime), 5, 3, CAST(N'2025-06-28T11:22:08.667' AS DateTime), 15.0000, 1033)
SET IDENTITY_INSERT [dbo].[Applications] OFF
GO
SET IDENTITY_INSERT [dbo].[ApplicationTypes] ON 

INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (1, N'New Local Driving License Service', 15.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (2, N'Renew Driving License Service', 5.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (3, N'Replacement for a Lost Driving License', 10.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (4, N'Replacement for a Damaged Driving License', 5.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (5, N'Release Detained Driving Licsense', 15.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (6, N'New International License', 50.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (7, N'Retake Test', 5.0000)
SET IDENTITY_INSERT [dbo].[ApplicationTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (1, N'Afghanistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (2, N'Albania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (3, N'Algeria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (4, N'Andorra')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (5, N'Angola')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (6, N'Antigua and Barbuda')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (7, N'Argentina')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (8, N'Armenia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (9, N'Austria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (10, N'Azerbaijan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (11, N'Bahrain')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (12, N'Bangladesh')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (13, N'Barbados')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (14, N'Belarus')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (15, N'Belgium')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (16, N'Belize')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (17, N'Benin')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (18, N'Bhutan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (19, N'Bolivia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (20, N'Bosnia and Herzegovina')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (21, N'Botswana')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (22, N'Brazil')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (23, N'Brunei')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (24, N'Bulgaria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (25, N'Burkina Faso')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (26, N'Burundi')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (27, N'Cabo Verde')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (28, N'Cambodia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (29, N'Cameroon')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (30, N'Canada')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (31, N'Central African Republic')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (32, N'Chad')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (33, N'Channel Islands')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (34, N'Chile')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (35, N'China')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (36, N'Colombia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (37, N'Comoros')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (38, N'Congo')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (39, N'Costa Rica')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (40, N'Côte d''Ivoire')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (41, N'Croatia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (42, N'Cuba')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (43, N'Cyprus')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (44, N'Czech Republic')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (45, N'Denmark')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (46, N'Djibouti')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (47, N'Dominica')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (48, N'Dominican Republic')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (49, N'DR Congo')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (50, N'Ecuador')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (51, N'Egypt')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (52, N'El Salvador')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (53, N'Equatorial Guinea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (54, N'Eritrea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (55, N'Estonia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (56, N'Eswatini')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (57, N'Ethiopia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (58, N'Faeroe Islands')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (59, N'Finland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (60, N'France')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (61, N'French Guiana')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (62, N'Gabon')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (63, N'Gambia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (64, N'Georgia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (65, N'Germany')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (66, N'Ghana')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (67, N'Gibraltar')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (68, N'Greece')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (69, N'Grenada')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (70, N'Guatemala')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (71, N'Guinea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (72, N'Guinea-Bissau')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (73, N'Guyana')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (74, N'Haiti')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (75, N'Holy See')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (76, N'Honduras')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (77, N'Hong Kong')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (78, N'Hungary')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (79, N'Iceland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (80, N'India')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (81, N'Indonesia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (82, N'Iran')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (83, N'Iraq')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (84, N'Ireland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (85, N'Isle of Man')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (86, N'Israel')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (87, N'Italy')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (88, N'Jamaica')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (89, N'Japan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (90, N'Jordan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (91, N'Kazakhstan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (92, N'Kenya')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (93, N'Kuwait')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (94, N'Kyrgyzstan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (95, N'Laos')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (96, N'Latvia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (97, N'Lebanon')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (98, N'Lesotho')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (99, N'Liberia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (100, N'Libya')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (101, N'Liechtenstein')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (102, N'Lithuania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (103, N'Luxembourg')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (104, N'Macao')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (105, N'Madagascar')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (106, N'Malawi')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (107, N'Malaysia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (108, N'Maldives')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (109, N'Mali')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (110, N'Malta')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (111, N'Mauritania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (112, N'Mauritius')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (113, N'Mayotte')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (114, N'Mexico')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (115, N'Moldova')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (116, N'Monaco')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (117, N'Mongolia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (118, N'Montenegro')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (119, N'Morocco')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (120, N'Mozambique')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (121, N'Myanmar')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (122, N'Namibia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (123, N'Nepal')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (124, N'Netherlands')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (125, N'Nicaragua')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (126, N'Niger')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (127, N'Nigeria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (128, N'North Korea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (129, N'North Macedonia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (130, N'Norway')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (131, N'Oman')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (132, N'Pakistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (133, N'Panama')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (134, N'Paraguay')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (135, N'Peru')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (136, N'Philippines')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (137, N'Poland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (138, N'Portugal')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (139, N'Qatar')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (140, N'Réunion')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (141, N'Romania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (142, N'Russia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (143, N'Rwanda')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (144, N'Saint Helena')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (145, N'Saint Kitts and Nevis')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (146, N'Saint Lucia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (147, N'Saint Vincent and the Grenadines')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (148, N'San Marino')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (149, N'Sao Tome & Principe')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (150, N'Saudi Arabia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (151, N'Senegal')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (152, N'Serbia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (153, N'Seychelles')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (154, N'Sierra Leone')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (155, N'Singapore')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (156, N'Slovakia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (157, N'Slovenia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (158, N'Somalia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (159, N'South Africa')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (160, N'South Korea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (161, N'South Sudan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (162, N'Spain')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (163, N'Sri Lanka')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (164, N'State of Palestine')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (165, N'Sudan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (166, N'Suriname')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (167, N'Sweden')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (168, N'Switzerland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (169, N'Syria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (170, N'Taiwan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (171, N'Tajikistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (172, N'Tanzania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (173, N'Thailand')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (174, N'The Bahamas')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (175, N'Timor-Leste')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (176, N'Togo')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (177, N'Trinidad and Tobago')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (178, N'Tunisia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (179, N'Turkey')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (180, N'Turkmenistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (181, N'Uganda')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (182, N'Ukraine')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (183, N'United Arab Emirates')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (184, N'United Kingdom')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (185, N'United States')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (186, N'Uruguay')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (187, N'Uzbekistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (188, N'Venezuela')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (189, N'Vietnam')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (190, N'Western Sahara')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (191, N'Yemen')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (192, N'Zambia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (193, N'Zimbabwe')
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[DetainedLicenses] ON 

INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (5, 14, CAST(N'2023-09-25T08:53:00' AS SmallDateTime), 56.0000, 1, 1, CAST(N'2025-06-28T06:51:00' AS SmallDateTime), 1, 214)
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (6, 15, CAST(N'2023-09-25T08:54:00' AS SmallDateTime), 60.0000, 1, 1, CAST(N'2023-09-28T16:59:00' AS SmallDateTime), NULL, 78)
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (7, 14, CAST(N'2023-09-28T16:46:00' AS SmallDateTime), 50.0000, 1, 1, CAST(N'2025-06-28T06:51:00' AS SmallDateTime), 1, 214)
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (8, 14, CAST(N'2023-09-28T17:06:00' AS SmallDateTime), 100.0000, 1, 1, CAST(N'2025-06-28T06:51:00' AS SmallDateTime), 1, 214)
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (9, 14, CAST(N'2023-09-28T17:10:00' AS SmallDateTime), 54.0000, 1, 1, CAST(N'2025-06-28T06:51:00' AS SmallDateTime), 1, 214)
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (10, 39, CAST(N'2025-06-27T15:30:00' AS SmallDateTime), 150.0000, 1, 1, CAST(N'2025-06-28T11:22:00' AS SmallDateTime), 1033, 222)
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (11, 37, CAST(N'2025-06-27T15:44:00' AS SmallDateTime), 500.0000, 1, 1, CAST(N'2025-06-27T22:24:00' AS SmallDateTime), 1, 211)
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (12, 42, CAST(N'2025-06-27T22:29:00' AS SmallDateTime), 1000.0000, 1, 1, CAST(N'2025-06-27T22:32:00' AS SmallDateTime), 1, 213)
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (13, 39, CAST(N'2025-06-28T07:02:00' AS SmallDateTime), 200.0000, 1, 1, CAST(N'2025-06-28T11:22:00' AS SmallDateTime), 1033, 222)
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (14, 45, CAST(N'2025-06-28T11:21:00' AS SmallDateTime), 1000.0000, 1033, 1, CAST(N'2025-06-28T11:22:00' AS SmallDateTime), 1033, 221)
SET IDENTITY_INSERT [dbo].[DetainedLicenses] OFF
GO
SET IDENTITY_INSERT [dbo].[Drivers] ON 

INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (8, 1, 1, CAST(N'2023-09-24T03:26:00' AS SmallDateTime))
INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (9, 1025, 1, CAST(N'2023-09-24T13:53:00' AS SmallDateTime))
INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (10, 2062, 1, CAST(N'2025-06-21T05:31:00' AS SmallDateTime))
INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (11, 2064, 1, CAST(N'2025-06-22T18:35:00' AS SmallDateTime))
INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (12, 2065, 1, CAST(N'2025-06-22T21:54:00' AS SmallDateTime))
INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (13, 2066, 1, CAST(N'2025-06-22T22:09:00' AS SmallDateTime))
INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (14, 1041, 1, CAST(N'2025-06-24T22:43:00' AS SmallDateTime))
INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (15, 1047, 1, CAST(N'2025-06-25T11:26:00' AS SmallDateTime))
INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (16, 1064, 1, CAST(N'2025-06-26T09:24:00' AS SmallDateTime))
INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (17, 2068, 1033, CAST(N'2025-06-28T11:16:00' AS SmallDateTime))
SET IDENTITY_INSERT [dbo].[Drivers] OFF
GO
SET IDENTITY_INSERT [dbo].[InternationalLicenses] ON 

INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (12, 69, 9, 17, CAST(N'2023-09-24T14:02:00' AS SmallDateTime), CAST(N'2024-09-24T14:02:00' AS SmallDateTime), 0, 1)
INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (13, 81, 8, 14, CAST(N'2023-09-28T17:13:00' AS SmallDateTime), CAST(N'2024-09-28T17:13:00' AS SmallDateTime), 0, 1)
INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (14, 181, 12, 22, CAST(N'2023-09-24T14:02:00' AS SmallDateTime), CAST(N'2024-09-28T17:13:00' AS SmallDateTime), 0, 1)
INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (15, 182, 10, 18, CAST(N'2025-06-24T15:47:00' AS SmallDateTime), CAST(N'2026-06-24T15:47:00' AS SmallDateTime), 1, 1)
INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (16, 184, 14, 26, CAST(N'2025-06-24T22:48:00' AS SmallDateTime), CAST(N'2026-06-24T22:48:00' AS SmallDateTime), 1, 1)
INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (17, 190, 15, 31, CAST(N'2025-06-25T22:27:00' AS SmallDateTime), CAST(N'2026-06-25T22:27:00' AS SmallDateTime), 1, 1)
INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (18, 192, 13, 30, CAST(N'2023-06-25T22:36:00' AS SmallDateTime), CAST(N'2024-06-25T22:36:00' AS SmallDateTime), 0, 1)
INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (20, 194, 13, 30, CAST(N'2025-06-26T06:37:00' AS SmallDateTime), CAST(N'2026-06-26T06:38:00' AS SmallDateTime), 1, 1)
INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (21, 195, 8, 14, CAST(N'2025-06-26T07:01:00' AS SmallDateTime), CAST(N'2026-06-26T07:01:00' AS SmallDateTime), 1, 1)
INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (22, 196, 9, 17, CAST(N'2025-06-26T07:29:00' AS SmallDateTime), CAST(N'2026-06-26T07:29:00' AS SmallDateTime), 1, 1)
INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (23, 197, 12, 22, CAST(N'2025-06-26T07:34:00' AS SmallDateTime), CAST(N'2026-06-26T07:34:00' AS SmallDateTime), 1, 1)
INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (24, 200, 16, 34, CAST(N'2025-06-26T09:26:00' AS SmallDateTime), CAST(N'2024-09-24T14:02:00' AS SmallDateTime), 0, 1)
INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (25, 202, 16, 35, CAST(N'2025-06-26T09:40:00' AS SmallDateTime), CAST(N'2026-06-26T09:40:00' AS SmallDateTime), 1, 1)
INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (26, 218, 17, 43, CAST(N'2025-06-28T11:18:00' AS SmallDateTime), CAST(N'2026-06-28T11:18:00' AS SmallDateTime), 1, 1033)
SET IDENTITY_INSERT [dbo].[InternationalLicenses] OFF
GO
SET IDENTITY_INSERT [dbo].[LicenseClasses] ON 

INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (1, N'Class 1 - Small Motorcycle', N'It allows the driver to drive small motorcycles, It is suitable for motorcycles with small capacity and limited power.', 18, 5, 15.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (2, N'Class 2 - Heavy Motorcycle License', N'Heavy Motorcycle License (Large Motorcycle License)', 21, 5, 30.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (3, N'Class 3 - Ordinary driving license', N'Ordinary driving license (car licence)', 18, 10, 20.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (4, N'Class 4 - Commercial', N'Commercial driving license (taxi/limousine)', 21, 10, 200.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (5, N'Class 5 - Agricultural', N'Agricultural and work vehicles used in farming or construction, (tractors / tillage machinery)', 21, 10, 50.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (6, N'Class 6 - Small and medium bus', N'Small and medium bus license', 21, 10, 250.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (7, N'Class 7 - Truck and heavy vehicle', N'Truck and heavy vehicle license', 21, 10, 300.0000)
SET IDENTITY_INSERT [dbo].[LicenseClasses] OFF
GO
SET IDENTITY_INSERT [dbo].[Licenses] ON 

INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (10, 52, 8, 3, CAST(N'2020-09-24T11:08:27.533' AS DateTime), CAST(N'2021-09-24T11:08:27.533' AS DateTime), NULL, 20.0000, 0, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (11, 61, 8, 3, CAST(N'2023-09-24T11:10:04.567' AS DateTime), CAST(N'2021-09-24T11:08:27.533' AS DateTime), NULL, 20.0000, 0, 2, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (12, 62, 8, 3, CAST(N'2023-09-24T11:10:46.170' AS DateTime), CAST(N'2033-09-24T11:08:27.533' AS DateTime), NULL, 0.0000, 0, 3, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (13, 63, 8, 3, CAST(N'2023-09-24T11:11:00.703' AS DateTime), CAST(N'2033-09-24T11:08:27.533' AS DateTime), NULL, 0.0000, 0, 3, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (14, 64, 8, 3, CAST(N'2023-09-24T11:24:21.117' AS DateTime), CAST(N'2033-09-24T11:10:04.567' AS DateTime), NULL, 0.0000, 1, 4, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (15, 65, 9, 3, CAST(N'2023-09-24T13:52:51.063' AS DateTime), CAST(N'2021-09-24T11:08:27.533' AS DateTime), NULL, 20.0000, 0, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (16, 66, 9, 3, CAST(N'2023-09-24T13:56:34.573' AS DateTime), CAST(N'2021-09-24T11:08:27.533' AS DateTime), NULL, 0.0000, 0, 4, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (17, 67, 9, 3, CAST(N'2023-09-24T13:58:43.570' AS DateTime), CAST(N'2033-09-24T13:52:51.063' AS DateTime), NULL, 0.0000, 1, 3, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (18, 128, 10, 3, CAST(N'2025-06-21T05:31:09.080' AS DateTime), CAST(N'2035-06-21T05:31:09.080' AS DateTime), NULL, 20.0000, 1, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (19, 130, 10, 1, CAST(N'2025-06-21T06:21:11.287' AS DateTime), CAST(N'2030-06-21T06:21:11.287' AS DateTime), NULL, 15.0000, 1, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (20, 131, 10, 7, CAST(N'2025-06-21T06:23:18.030' AS DateTime), CAST(N'2035-06-21T06:23:18.030' AS DateTime), NULL, 300.0000, 1, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (21, 163, 11, 3, CAST(N'2025-06-22T18:35:12.253' AS DateTime), CAST(N'2035-06-22T18:35:12.253' AS DateTime), NULL, 20.0000, 1, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (22, 171, 12, 3, CAST(N'2025-06-22T21:54:21.127' AS DateTime), CAST(N'2035-06-22T21:54:21.127' AS DateTime), NULL, 20.0000, 1, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (23, 175, 13, 6, CAST(N'2025-06-22T22:08:45.693' AS DateTime), CAST(N'2035-06-22T22:08:45.693' AS DateTime), NULL, 250.0000, 0, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (24, 170, 12, 5, CAST(N'2025-06-22T22:26:10.617' AS DateTime), CAST(N'2035-06-22T22:26:10.617' AS DateTime), NULL, 50.0000, 1, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (25, 179, 12, 4, CAST(N'2025-06-22T22:48:04.510' AS DateTime), CAST(N'2035-06-22T22:48:04.510' AS DateTime), NULL, 200.0000, 1, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (26, 183, 14, 3, CAST(N'2025-06-24T22:42:50.490' AS DateTime), CAST(N'2035-06-24T22:42:50.490' AS DateTime), NULL, 20.0000, 1, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (27, 185, 13, 3, CAST(N'2014-06-25T11:14:08.070' AS DateTime), CAST(N'2024-06-25T11:14:08.070' AS DateTime), NULL, 20.0000, 0, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (28, 186, 15, 3, CAST(N'2014-06-25T11:14:08.070' AS DateTime), CAST(N'2024-06-25T11:14:08.070' AS DateTime), NULL, 20.0000, 0, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (30, 188, 13, 3, CAST(N'2025-06-25T21:40:36.097' AS DateTime), CAST(N'2035-06-25T21:40:36.097' AS DateTime), NULL, 20.0000, 1, 2, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (31, 189, 15, 3, CAST(N'2014-06-25T11:14:08.070' AS DateTime), CAST(N'2024-06-25T11:14:08.070' AS DateTime), NULL, 20.0000, 0, 2, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (32, 191, 15, 3, CAST(N'2025-06-25T22:29:17.030' AS DateTime), CAST(N'2035-06-25T22:29:17.030' AS DateTime), NULL, 20.0000, 0, 2, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (33, 198, 16, 3, CAST(N'2025-06-26T09:23:58.750' AS DateTime), CAST(N'2021-09-24T11:08:27.533' AS DateTime), NULL, 20.0000, 0, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (34, 199, 16, 3, CAST(N'2025-06-26T09:26:06.227' AS DateTime), CAST(N'2021-09-24T11:08:27.533' AS DateTime), NULL, 20.0000, 0, 2, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (35, 201, 16, 3, CAST(N'2025-06-26T09:37:24.833' AS DateTime), CAST(N'2035-06-26T09:37:24.833' AS DateTime), NULL, 20.0000, 0, 2, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (36, 203, 15, 3, CAST(N'2025-06-26T22:03:26.827' AS DateTime), CAST(N'2035-06-25T22:29:17.030' AS DateTime), N'No Notes', 20.0000, 0, 3, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (37, 204, 15, 3, CAST(N'2025-06-26T22:08:44.323' AS DateTime), CAST(N'2035-06-25T22:29:17.030' AS DateTime), N'No Notes', 20.0000, 1, 4, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (38, 205, 16, 3, CAST(N'2025-06-26T22:14:59.437' AS DateTime), CAST(N'2035-06-26T09:37:24.833' AS DateTime), N'No Notes', 20.0000, 0, 3, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (39, 206, 16, 3, CAST(N'2025-06-26T22:16:02.470' AS DateTime), CAST(N'2035-06-26T09:37:24.833' AS DateTime), N'No Notes', 20.0000, 1, 4, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (40, 207, 13, 6, CAST(N'2025-06-26T22:18:19.947' AS DateTime), CAST(N'2035-06-22T22:08:45.693' AS DateTime), N'No Notes', 250.0000, 0, 3, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (41, 208, 13, 6, CAST(N'2025-06-26T22:18:55.693' AS DateTime), CAST(N'2024-06-22T22:08:45.693' AS DateTime), N'No Notes', 250.0000, 0, 4, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (42, 209, 13, 6, CAST(N'2025-06-26T22:20:36.373' AS DateTime), CAST(N'2035-06-26T22:20:36.373' AS DateTime), NULL, 250.0000, 1, 2, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (43, 215, 17, 3, CAST(N'2025-06-28T11:15:43.973' AS DateTime), CAST(N'2035-06-28T11:15:43.973' AS DateTime), NULL, 20.0000, 0, 1, 1033)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (44, 219, 17, 3, CAST(N'2025-06-28T11:19:06.997' AS DateTime), CAST(N'2035-06-28T11:15:43.973' AS DateTime), N'No Notes', 20.0000, 0, 3, 1033)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (45, 220, 17, 3, CAST(N'2025-06-28T11:19:48.953' AS DateTime), CAST(N'2035-06-28T11:15:43.973' AS DateTime), N'No Notes', 20.0000, 1, 4, 1033)
SET IDENTITY_INSERT [dbo].[Licenses] OFF
GO
SET IDENTITY_INSERT [dbo].[LocalDrivingLicenseApplications] ON 

INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (30, 52, 3)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (31, 65, 3)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (76, 128, 3)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (78, 130, 1)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (79, 131, 7)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (80, 161, 3)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (82, 163, 3)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (86, 170, 5)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (87, 171, 3)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (88, 175, 6)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (90, 179, 4)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (91, 183, 3)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (92, 185, 3)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (93, 186, 3)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (94, 198, 3)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (95, 215, 3)
SET IDENTITY_INSERT [dbo].[LocalDrivingLicenseApplications] OFF
GO
SET IDENTITY_INSERT [dbo].[People] ON 

INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (1, N'N1', N'Mohammed', N'Saqer', N'Mussa', N'Abu-Hadhoud', CAST(N'1977-11-06T00:00:00.000' AS DateTime), 0, N'Amman Jubaiha1', N'999876', N'Msaqer@gmail.com', 90, N'aa6a1aaa-1a2d-4814-bc4f-ac16f7a9c906.jpg')
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (1023, N'N2', N'Omar', N'Mohammed', N'Saqer', N'Abu-Hadhoud', CAST(N'2005-06-01T20:13:44.000' AS DateTime), 0, N'Amman 20091-Street', N'07992992', N'Omar@g.com', 90, N'7bfc1053-643e-4b2b-a06a-c92622b59a9a.jpg')
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (1024, N'N3', N'Hamzeh', N'Mohammed', N'Saqer', N'Abu-Hadhoud', CAST(N'2005-09-23T21:05:06.000' AS DateTime), 0, N'Amman', N'234566', N'H@H.com', 90, N'247182b1-a4a0-4765-ba94-01baf1cb8e4d.jpg')
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (1025, N'n4', N'Salma', N'ALi', N'Maher', N'hamed', CAST(N'2005-09-24T13:32:14.000' AS DateTime), 1, N'Amman - Uni street 8938', N'566543', N'Kh@k.com', 90, N'5d857651-d151-4f38-b58f-601098103a25.jpeg')
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (1041, N'N6', N'Mohammed', N'Farok', N'Ibrahim', N'Ahmad', CAST(N'2006-01-01T10:40:36.000' AS DateTime), 0, N'Syria Tartous', N'54253544', N'mohammed@gmail.com', 139, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (1047, N'N10', N'Salwa', N'Farok', N'Ibrahim', N'Ahmad', CAST(N'2004-09-20T22:01:50.000' AS DateTime), 1, N'Flower Street', N'257275', N'my@gmail.com', 185, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (1051, N'N7', N'Farok', N'Ibrahim', N'Mohammed', N'Ahmad', CAST(N'1973-08-01T10:34:51.000' AS DateTime), 0, N'Syria street 505', N'4252445', N'farok@gmail.com', 169, N'99300b3c-9abe-4f88-aedb-9a5563de4410.jpg')
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (1057, N'N11', N'Ahmad', N'ali', N'Khalil', N'Yaser', CAST(N'2007-05-25T15:51:39.000' AS DateTime), 0, N'Amman street 500', N'0465465', N'ahmad@gmail.com', 90, N'b4e6632a-06d4-4313-8726-f7c2e5098773.jpg')
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (1064, N'N13', N'Farah', N'Mohammmed', N'Ibrahim', N'Ahmad', CAST(N'2007-05-26T04:57:52.000' AS DateTime), 1, N'Damascus', N'04545645', N'', 169, N'984bf54e-bf9b-4808-b928-d9da311e6124.jpeg')
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (2062, N'n12', N'Ahmad', N'Adeal', N'Fadi', N'Ali', CAST(N'2007-06-10T12:13:08.000' AS DateTime), 0, N'Amman street 500', N'561654', N'', 90, N'51c0db57-87da-42f3-b407-6fdcf77493f5.jpg')
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (2063, N'N14', N'Alia', N'Sami', N'Ali', N'Ahmad', CAST(N'2007-06-10T21:56:45.857' AS DateTime), 1, N'Amman 5004', N'45465463', N'', 90, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (2064, N'n15', N'Samer', N'Ali', N'Ahmad', N'Fayaz', CAST(N'2005-06-22T17:57:20.000' AS DateTime), 0, N'Amman streer 1001', N'24163546', N'samer@gmail.com', 90, N'deae021c-ab34-4c3a-acb5-5a3c352f6bba.jpg')
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (2065, N'N16', N'Sarah ', N'Ali ', N'Ahmad ', N'Mohammed', CAST(N'2007-06-22T21:45:37.000' AS DateTime), 1, N'Amman', N'5465465', N'sa@gmail.com', 90, N'37321dbd-ef79-470a-b56c-e8ece6c16a4e.jpeg')
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (2066, N'N012', N'Talen', N'Ahmad', N'Ahmed', N'Ali', CAST(N'1952-06-22T21:58:44.000' AS DateTime), 1, N'Amman', N'0999852555', N'', 90, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (2068, N'N20', N'Ghader', N'Farok', N'Ibrhim', N'Ahmad', CAST(N'2002-09-24T11:07:17.000' AS DateTime), 0, N'Street 2001', N'5465465456', N'ghader@gmail.com', 169, N'ba12d1f1-7405-4110-ac18-91416b16d921.jpg')
SET IDENTITY_INSERT [dbo].[People] OFF
GO
SET IDENTITY_INSERT [dbo].[TestAppointments] ON 

INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (65, 1, 30, CAST(N'2023-09-24T03:25:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (66, 2, 30, CAST(N'2023-09-24T03:25:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (67, 3, 30, CAST(N'2023-09-24T03:25:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (68, 1, 31, CAST(N'2023-09-24T13:49:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (69, 2, 31, CAST(N'2023-09-24T13:50:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (70, 2, 31, CAST(N'2023-09-25T13:51:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (71, 3, 31, CAST(N'2023-09-28T13:52:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (130, 1, 76, CAST(N'2025-06-21T09:56:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (131, 1, 76, CAST(N'2025-06-20T09:58:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (132, 2, 76, CAST(N'2025-06-20T10:01:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (133, 3, 76, CAST(N'2025-06-20T10:02:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (134, 3, 76, CAST(N'2025-06-20T10:02:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (135, 3, 76, CAST(N'2025-06-20T10:02:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (136, 1, 78, CAST(N'2025-06-20T11:41:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (137, 1, 78, CAST(N'2025-06-20T11:41:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (138, 2, 78, CAST(N'2025-06-20T11:42:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (139, 2, 78, CAST(N'2025-06-20T11:42:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (140, 2, 78, CAST(N'2025-06-20T11:42:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (141, 3, 78, CAST(N'2025-06-20T11:43:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (142, 3, 78, CAST(N'2025-06-20T11:43:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (143, 1, 79, CAST(N'2025-06-22T06:22:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (144, 1, 79, CAST(N'2025-06-21T06:22:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (145, 2, 79, CAST(N'2025-06-21T06:23:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (146, 3, 79, CAST(N'2025-06-21T06:23:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (148, 1, 80, CAST(N'2025-06-23T18:13:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (149, 1, 82, CAST(N'2025-06-23T18:15:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (150, 1, 82, CAST(N'2025-06-22T18:20:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (151, 2, 82, CAST(N'2025-06-22T18:30:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (152, 3, 82, CAST(N'2025-06-22T18:31:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (153, 3, 82, CAST(N'2025-06-22T18:31:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (154, 3, 82, CAST(N'2025-06-22T18:31:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (155, 1, 87, CAST(N'2025-06-23T21:51:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (156, 1, 87, CAST(N'2025-06-22T21:52:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (157, 2, 87, CAST(N'2025-06-22T21:53:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (158, 3, 87, CAST(N'2025-06-22T21:53:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (159, 3, 87, CAST(N'2025-06-22T21:54:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (160, 3, 87, CAST(N'2025-06-22T21:54:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (161, 1, 88, CAST(N'2025-06-22T22:05:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (162, 1, 88, CAST(N'2025-06-22T22:06:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (163, 2, 88, CAST(N'2025-06-22T22:07:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (164, 3, 88, CAST(N'2025-06-22T22:08:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (165, 3, 88, CAST(N'2025-06-22T22:08:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (166, 1, 86, CAST(N'2025-06-22T22:17:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (167, 2, 86, CAST(N'2025-06-22T22:22:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (168, 3, 86, CAST(N'2025-06-22T22:24:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (169, 1, 90, CAST(N'2025-06-22T22:45:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (170, 2, 90, CAST(N'2025-06-22T22:47:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (171, 3, 90, CAST(N'2025-06-22T22:48:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (172, 3, 90, CAST(N'2025-06-22T22:48:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (173, 1, 91, CAST(N'2025-06-24T22:42:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (174, 2, 91, CAST(N'2025-06-24T22:42:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (175, 3, 91, CAST(N'2025-06-24T22:43:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (176, 1, 92, CAST(N'2025-06-25T11:11:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (177, 2, 92, CAST(N'2025-06-25T11:14:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (178, 3, 92, CAST(N'2025-06-25T11:14:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (179, 1, 93, CAST(N'2025-06-25T11:25:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (180, 2, 93, CAST(N'2025-06-25T11:26:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (181, 3, 93, CAST(N'2025-06-25T11:26:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (182, 1, 94, CAST(N'2025-06-26T09:23:00' AS SmallDateTime), 10.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (183, 2, 94, CAST(N'2025-06-26T09:24:00' AS SmallDateTime), 20.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (184, 3, 94, CAST(N'2025-06-26T09:24:00' AS SmallDateTime), 30.0000, 1, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (185, 1, 95, CAST(N'2025-06-29T11:14:00' AS SmallDateTime), 10.0000, 1033, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (186, 1, 95, CAST(N'2025-06-28T11:15:00' AS SmallDateTime), 10.0000, 1033, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (187, 2, 95, CAST(N'2025-06-28T11:15:00' AS SmallDateTime), 20.0000, 1033, 1)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (188, 3, 95, CAST(N'2025-06-28T11:15:00' AS SmallDateTime), 30.0000, 1033, 1)
SET IDENTITY_INSERT [dbo].[TestAppointments] OFF
GO
SET IDENTITY_INSERT [dbo].[Tests] ON 

INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (29, 65, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (30, 66, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (31, 67, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (32, 68, 1, N'with Glasses', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (33, 69, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (34, 70, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (35, 71, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (84, 130, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (85, 131, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (86, 132, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (87, 133, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (88, 134, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (89, 135, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (90, 136, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (91, 137, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (92, 138, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (93, 139, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (94, 140, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (95, 141, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (96, 142, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (97, 143, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (98, 144, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (99, 145, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (100, 146, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (102, 148, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (103, 149, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (104, 150, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (105, 151, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (106, 152, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (107, 153, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (108, 154, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (109, 155, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (110, 156, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (111, 157, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (112, 158, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (113, 159, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (114, 160, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (115, 161, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (116, 162, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (117, 163, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (118, 164, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (119, 165, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (120, 166, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (121, 167, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (122, 168, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (123, 169, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (124, 170, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (125, 171, 0, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (126, 172, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (127, 173, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (128, 174, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (129, 175, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (130, 176, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (131, 177, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (132, 178, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (133, 179, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (134, 180, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (135, 181, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (136, 182, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (137, 183, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (138, 184, 1, NULL, 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (139, 185, 0, NULL, 1033)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (140, 186, 1, NULL, 1033)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (141, 187, 1, NULL, 1033)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (142, 188, 1, NULL, 1033)
SET IDENTITY_INSERT [dbo].[Tests] OFF
GO
SET IDENTITY_INSERT [dbo].[TestTypes] ON 

INSERT [dbo].[TestTypes] ([TestTypeID], [TestTypeTitle], [TestTypeDescription], [TestTypeFees]) VALUES (1, N'Vision Test ', N'This assesses the applicant''s visual acuity to ensure they have sufficient vision to drive safely.', 10.0000)
INSERT [dbo].[TestTypes] ([TestTypeID], [TestTypeTitle], [TestTypeDescription], [TestTypeFees]) VALUES (2, N'Written (Theory) Test', N'This test assesses the applicant''s knowledge of traffic rules, road signs, and driving regulations. It typically consists of multiple-choice questions, and the applicant must select the correct answer(s). The written test aims to ensure that the applicant understands the rules of the road and can apply them in various driving scenarios.', 20.0000)
INSERT [dbo].[TestTypes] ([TestTypeID], [TestTypeTitle], [TestTypeDescription], [TestTypeFees]) VALUES (3, N'Practical (Street) Test', N'This test evaluates the applicant''s driving skills and ability to operate a motor vehicle safely on public roads. A licensed examiner accompanies the applicant in the vehicle and observes their driving performance.', 30.0000)
SET IDENTITY_INSERT [dbo].[TestTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (1, 1, N'Msaqer77', N'5678', 1)
INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (15, 1025, N'user41', N'1234', 1)
INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (22, 1041, N'Mohammed1', N'4321', 0)
INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (23, 1023, N'Omar1', N'1234', 0)
INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (24, 1064, N'Abeer1', N'1234', 1)
INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (1033, 2068, N'GhaderUser', N'4321', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_ApplicationStatus]  DEFAULT ((1)) FOR [ApplicationStatus]
GO
ALTER TABLE [dbo].[ApplicationTypes] ADD  CONSTRAINT [DF_ApplicationTypes_Fees]  DEFAULT ((0)) FOR [ApplicationFees]
GO
ALTER TABLE [dbo].[DetainedLicenses] ADD  CONSTRAINT [DF_DetainedLicenses_IsReleased]  DEFAULT ((0)) FOR [IsReleased]
GO
ALTER TABLE [dbo].[LicenseClasses] ADD  CONSTRAINT [DF_LicenseClasses_Age]  DEFAULT ((18)) FOR [MinimumAllowedAge]
GO
ALTER TABLE [dbo].[LicenseClasses] ADD  CONSTRAINT [DF_LicenseClasses_DefaultPeriodLength]  DEFAULT ((1)) FOR [DefaultValidityLength]
GO
ALTER TABLE [dbo].[LicenseClasses] ADD  CONSTRAINT [DF_LicenseClasses_ClassFees]  DEFAULT ((0)) FOR [ClassFees]
GO
ALTER TABLE [dbo].[Licenses] ADD  CONSTRAINT [DF_Licenses_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Licenses] ADD  CONSTRAINT [DF_Licenses_IssueReason]  DEFAULT ((1)) FOR [IssueReason]
GO
ALTER TABLE [dbo].[People] ADD  CONSTRAINT [DF_People_Gendor]  DEFAULT ((0)) FOR [Gendor]
GO
ALTER TABLE [dbo].[TestAppointments] ADD  CONSTRAINT [DF_TestAppointments_AppointmentLocked]  DEFAULT ((0)) FOR [IsLocked]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_ApplicationTypes] FOREIGN KEY([ApplicationTypeID])
REFERENCES [dbo].[ApplicationTypes] ([ApplicationTypeID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_ApplicationTypes]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_People] FOREIGN KEY([ApplicationPersonID])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_People]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_Users]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Applications] FOREIGN KEY([ReleaseApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Applications]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Licenses] FOREIGN KEY([LicenseID])
REFERENCES [dbo].[Licenses] ([LicenseID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Licenses]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Users]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Users1] FOREIGN KEY([ReleasedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Users1]
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK_Drivers_People] FOREIGN KEY([PersonID])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK_Drivers_People]
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK_Drivers_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK_Drivers_Users]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Applications]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Drivers] FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Drivers]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Licenses] FOREIGN KEY([IssuedUsingLocalLicenseID])
REFERENCES [dbo].[Licenses] ([LicenseID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Licenses]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Users]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_Applications]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_Drivers] FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_Drivers]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_LicenseClasses] FOREIGN KEY([LicenseClass])
REFERENCES [dbo].[LicenseClasses] ([LicenseClassID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_LicenseClasses]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_Users]
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications]  WITH CHECK ADD  CONSTRAINT [FK_DrivingLicsenseApplications_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications] CHECK CONSTRAINT [FK_DrivingLicsenseApplications_Applications]
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications]  WITH CHECK ADD  CONSTRAINT [FK_DrivingLicsenseApplications_LicenseClasses] FOREIGN KEY([LicenseClassID])
REFERENCES [dbo].[LicenseClasses] ([LicenseClassID])
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications] CHECK CONSTRAINT [FK_DrivingLicsenseApplications_LicenseClasses]
GO
ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Countries1] FOREIGN KEY([NationalityCountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Countries1]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_LocalDrivingLicenseApplications] FOREIGN KEY([LocalDrivingLicenseApplicationID])
REFERENCES [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_LocalDrivingLicenseApplications]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_TestTypes] FOREIGN KEY([TestTypeID])
REFERENCES [dbo].[TestTypes] ([TestTypeID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_TestTypes]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_Users]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_TestAppointments] FOREIGN KEY([TestAppointmentID])
REFERENCES [dbo].[TestAppointments] ([TestAppointmentID])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_TestAppointments]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_People] FOREIGN KEY([PersonID])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_People]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-New 2-Cancelled 3-Completed' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Applications', @level2type=N'COLUMN',@level2name=N'ApplicationStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Minmum age allowed to apply for this license' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LicenseClasses', @level2type=N'COLUMN',@level2name=N'MinimumAllowedAge'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'How many years the licesnse will be valid.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LicenseClasses', @level2type=N'COLUMN',@level2name=N'DefaultValidityLength'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Licenses', @level2type=N'COLUMN',@level2name=N'IssueReason'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 Male , 1 Femail' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'People', @level2type=N'COLUMN',@level2name=N'Gendor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 - Fail 1-Pass' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tests', @level2type=N'COLUMN',@level2name=N'TestResult'
GO
