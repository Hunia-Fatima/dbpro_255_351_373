USE [DB20]
GO
/****** Object:  Table [dbo].[Lookup]    Script Date: 03/06/2019 00:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lookup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](100) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Lookup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Lookup] ON
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (1, N'iOS', N'OS')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (2, N'Android', N'OS')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (3, N'Black', N'Color')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (4, N'White', N'Color')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (5, N'Blue', N'Color')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (6, N'Silver', N'Color')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (7, N'Golden', N'Color')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (8, N'Red', N'Color')
SET IDENTITY_INSERT [dbo].[Lookup] OFF
/****** Object:  Table [dbo].[Project]    Script Date: 03/06/2019 00:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Group]    Script Date: 03/06/2019 00:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mobile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OS] [int] NOT NULL,
	[Color] [int] NOT NULL,
	[Category] [int] NOT NULL,
	[Dimensions] [float] NOT NULL,
	[Weight] [float] NOT NULL,
	[Display] [float] NOT NULL,
	[Memory] [int] NOT NULL,
	[RAM] [int] NOT NULL,
	[FrontCamerPx] [int] NOT NULL,
	[BackCamerPx] [int] NOT NULL,
	[Networks] [int] NOT NULL,
 CONSTRAINT [PK_Mobile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Administrator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar] NOT NULL,
	[Email] [varchar] NOT NULL,
	[Password] [varchar] NOT NULL,
 CONSTRAINT [PK_Administrator] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Evaluation]    Script Date: 03/06/2019 00:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MobileId] [int] NOT NULL,
	[SideViewImage] [image] NOT NULL,
	[FrontViewImage] [image] NOT NULL,
	[BackViewImage] [image] NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_Advisor_Lookup]    Script Date: 03/06/2019 00:49:31 ******/
ALTER TABLE [dbo].[Mobile]  WITH CHECK ADD  CONSTRAINT [FK_Mobile_Lookup_OS] FOREIGN KEY([OS])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Mobile] CHECK CONSTRAINT [FK_Mobile_Lookup_OS]
GO
/****** Object:  ForeignKey [FK_Advisor_Lookup]    Script Date: 03/06/2019 00:49:31 ******/
ALTER TABLE [dbo].[Mobile]  WITH CHECK ADD  CONSTRAINT [FK_Mobile_Lookup_Color] FOREIGN KEY([Color])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Mobile] CHECK CONSTRAINT [FK_Mobile_Lookup_Color]
GO
/****** Object:  ForeignKey [FK_Advisor_Lookup]    Script Date: 03/06/2019 00:49:31 ******/
ALTER TABLE [dbo].[Mobile]  WITH CHECK ADD  CONSTRAINT [FK_Mobile_Category] FOREIGN KEY([Category])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Mobile] CHECK CONSTRAINT [FK_Mobile_Category]
GO
/****** Object:  ForeignKey [FK_Advisor_Lookup]    Script Date: 03/06/2019 00:49:31 ******/
ALTER TABLE [dbo].[Images]  WITH CHECK ADD  CONSTRAINT [FK_Images_Mobile] FOREIGN KEY([MobileId])
REFERENCES [dbo].[Mobile] ([Id])
GO
ALTER TABLE [dbo].[Images] CHECK CONSTRAINT [FK_Images_Mobile]
GO

