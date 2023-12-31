USE [db_pedidos]
GO
/****** Object:  Table [dbo].[T_Clientes]    Script Date: 12/05/2022 00:51:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Clientes](
	[id_cliente] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[dni] [int] NOT NULL,
	[cod_postal] [int] NOT NULL,
 CONSTRAINT [PK_T_Clientes] PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[T_Clientes] ([id_cliente], [nombre], [apellido], [dni], [cod_postal]) VALUES (1, N'Cachilo', N'Sosa', 6448965, 5960)
INSERT [dbo].[T_Clientes] ([id_cliente], [nombre], [apellido], [dni], [cod_postal]) VALUES (2, N'Juan', N'Casas', 20698753, 5999)
/****** Object:  StoredProcedure [dbo].[SP_CONSULTAR_CLIENTES]    Script Date: 12/05/2022 00:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CONSULTAR_CLIENTES] 
AS
BEGIN
	SELECT *
	FROM T_Clientes
END
GO
/****** Object:  Table [dbo].[T_Pedidos]    Script Date: 12/05/2022 00:51:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Pedidos](
	[codigo] [int] NOT NULL,
	[fec_entrega] [date] NOT NULL,
	[dir_entrega] [varchar](50) NOT NULL,
	[id_cliente] [int] NOT NULL,
	[entregado] [varchar](1) NULL,
	[fecha_baja] [date] NULL,
 CONSTRAINT [PK_Pedidos] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[T_Pedidos] ([codigo], [fec_entrega], [dir_entrega], [id_cliente], [entregado], [fecha_baja]) VALUES (1, CAST(0x9B440B00 AS Date), N'Julio A. Roca 966 - Rosario', 1, N'S', NULL)
INSERT [dbo].[T_Pedidos] ([codigo], [fec_entrega], [dir_entrega], [id_cliente], [entregado], [fecha_baja]) VALUES (2, CAST(0xAF440B00 AS Date), N'Sarmiento 699 - Cba', 2, N'S', CAST(0xBD440B00 AS Date))
/****** Object:  StoredProcedure [dbo].[SP_REGISTRAR_ENTREGA]    Script Date: 12/05/2022 00:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_REGISTRAR_ENTREGA] 
	@codigo int
AS
BEGIN
	UPDATE T_Pedidos SET entregado = 'S' 
	WHERE codigo = @codigo
	AND entregado = 'N';
END
GO
/****** Object:  StoredProcedure [dbo].[SP_REGISTRAR_BAJA]    Script Date: 12/05/2022 00:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_REGISTRAR_BAJA] 
	@codigo int
AS
BEGIN
	UPDATE T_Pedidos SET fecha_baja = GETDATE() 
	WHERE codigo = @codigo
	AND fecha_baja is NULL;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CONSULTAR_PEDIDOS]    Script Date: 12/05/2022 00:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CONSULTAR_PEDIDOS] 
	@cliente int,
	@fecha_desde datetime,
	@fecha_hasta datetime
AS
BEGIN
	SELECT t.*, (t1.apellido + ', ' + t1.nombre) as cliente
	FROM T_Pedidos t, T_Clientes t1
	WHERE t.id_cliente = t1.id_cliente 
	 AND((@cliente = 0) OR (t.id_cliente = @cliente))
	 AND((@fecha_desde is null and @fecha_hasta is null) OR (fec_entrega between @fecha_desde and @fecha_hasta));
END
GO
/****** Object:  ForeignKey [FK_T_Pedidos_T_Clientes]    Script Date: 12/05/2022 00:51:57 ******/
ALTER TABLE [dbo].[T_Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_T_Pedidos_T_Clientes] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[T_Clientes] ([id_cliente])
GO
ALTER TABLE [dbo].[T_Pedidos] CHECK CONSTRAINT [FK_T_Pedidos_T_Clientes]
GO
/****** Object:  ForeignKey [FK_T_Pedidos_T_Pedidos]    Script Date: 12/05/2022 00:51:57 ******/
ALTER TABLE [dbo].[T_Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_T_Pedidos_T_Pedidos] FOREIGN KEY([codigo])
REFERENCES [dbo].[T_Pedidos] ([codigo])
GO
ALTER TABLE [dbo].[T_Pedidos] CHECK CONSTRAINT [FK_T_Pedidos_T_Pedidos]
GO
