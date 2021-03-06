USE [DBDemo]
GO
/****** Object:  Table [dbo].[Platezh]    Script Date: 26.04.2021 12:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Platezh](
	[Nomer_zakaza] [int] NOT NULL,
	[Nomer_Prihod] [int] NOT NULL,
	[Plat_Sum] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prihod_deneg]    Script Date: 26.04.2021 12:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prihod_deneg](
	[Nomer_prihoda] [int] IDENTITY(1,1) NOT NULL,
	[Data] [datetime] NULL,
	[Sum] [int] NOT NULL,
	[Ostatok] [int] NOT NULL,
 CONSTRAINT [PK_Prihod_deneg] PRIMARY KEY CLUSTERED 
(
	[Nomer_prihoda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Zakaz]    Script Date: 26.04.2021 12:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zakaz](
	[Nomer] [int] IDENTITY(1,1) NOT NULL,
	[Data] [date] NULL,
	[Sum] [int] NULL,
	[PaymentSum] [int] NULL,
 CONSTRAINT [PK_Zakaz] PRIMARY KEY CLUSTERED 
(
	[Nomer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Platezh]  WITH CHECK ADD  CONSTRAINT [FK_Platezh_Prihod_deneg] FOREIGN KEY([Nomer_Prihod])
REFERENCES [dbo].[Prihod_deneg] ([Nomer_prihoda])
GO
ALTER TABLE [dbo].[Platezh] CHECK CONSTRAINT [FK_Platezh_Prihod_deneg]
GO
ALTER TABLE [dbo].[Platezh]  WITH CHECK ADD  CONSTRAINT [FK_Platezh_Zakaz] FOREIGN KEY([Nomer_zakaza])
REFERENCES [dbo].[Zakaz] ([Nomer])
GO
ALTER TABLE [dbo].[Platezh] CHECK CONSTRAINT [FK_Platezh_Zakaz]
GO
/****** Object:  Trigger [dbo].[Accepted_Platezh_Prihod]    Script Date: 26.04.2021 12:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Trigger [dbo].[Accepted_Platezh_Prihod]
on [dbo].[Platezh] AFTER Insert
As Update Prihod_deneg
Set Ostatok=Ostatok-Platezh.Plat_Sum From Platezh
Where Nomer_prihoda = Nomer_Prihod
GO
ALTER TABLE [dbo].[Platezh] ENABLE TRIGGER [Accepted_Platezh_Prihod]
GO
/****** Object:  Trigger [dbo].[Accepted_Platezh_Zakaz]    Script Date: 26.04.2021 12:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Trigger [dbo].[Accepted_Platezh_Zakaz] 
On [dbo].[Platezh] After INSERT
As Update Zakaz
Set PaymentSum=PaymentSum+Platezh.Plat_Sum From Platezh
Where Nomer = Nomer_zakaza

GO
ALTER TABLE [dbo].[Platezh] ENABLE TRIGGER [Accepted_Platezh_Zakaz]
GO
