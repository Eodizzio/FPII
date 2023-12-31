USE [db_facturas]
GO
/****** Object:  Table [dbo].[T_PRODUCTOS]    Script Date: 11/23/2022 10:33:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_PRODUCTOS](
	[id_producto] [int] IDENTITY(1,1) NOT NULL,
	[n_producto] [varchar](255) NOT NULL,
	[precio] [numeric](8, 2) NOT NULL,
	[activo] [varchar](1) NOT NULL,
 CONSTRAINT [PK__T_PRODUC__FF341C0D7F60ED59] PRIMARY KEY CLUSTERED 
(
	[id_producto] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[T_PRODUCTOS] ON
INSERT [dbo].[T_PRODUCTOS] ([id_producto], [n_producto], [precio], [activo]) VALUES (1, N'Producto A', CAST(999.00 AS Numeric(8, 2)), N'S')
INSERT [dbo].[T_PRODUCTOS] ([id_producto], [n_producto], [precio], [activo]) VALUES (2, N'Producto B', CAST(111.00 AS Numeric(8, 2)), N'N')
SET IDENTITY_INSERT [dbo].[T_PRODUCTOS] OFF
/****** Object:  Table [dbo].[T_FACTURAS]    Script Date: 11/23/2022 10:33:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_FACTURAS](
	[nro] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [date] NOT NULL,
	[cliente] [varchar](100) NOT NULL,
	[forma_pago] [int] NOT NULL,
	[fecha_baja] [date] NULL,
	[total] [numeric](8, 2) NOT NULL,
 CONSTRAINT [PK__T_FACT__33BEB70E03317E3D] PRIMARY KEY CLUSTERED 
(
	[nro] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_DETALLES_FACTURA]    Script Date: 11/23/2022 10:33:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_DETALLES_FACTURA](
	[nro] [int] NOT NULL,
	[detalle_nro] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[nro] ASC,
	[detalle_nro] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_FACTURA]    Script Date: 11/23/2022 10:33:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INSERTAR_FACTURA] 
	@cliente varchar(255), 
	@forma int,
	@total numeric(8,2),
	@nro int output
AS
BEGIN
	INSERT INTO T_FACTURAS(fecha, cliente, forma_pago, total)
    VALUES (GETDATE(), @cliente, @forma, @total);
	SET @nro = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_DETALLES]    Script Date: 11/23/2022 10:33:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INSERTAR_DETALLES] 
	@nro int,
	@detalle int, 
	@id_producto int, 
	@cantidad int
AS
BEGIN
	INSERT INTO T_DETALLES_FACTURA(nro,detalle_nro, id_producto, cantidad)
    VALUES (@nro, @detalle, @id_producto, @cantidad);
  
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CONSULTAR_PRODUCTOS]    Script Date: 11/23/2022 10:33:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CONSULTAR_PRODUCTOS]
AS
BEGIN
	
	SELECT * from T_PRODUCTOS ORDER BY n_producto;
END
GO
