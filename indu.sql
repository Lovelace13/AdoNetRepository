USE [Indusur]
GO
/****** Object:  Table [dbo].[vh_vehiculosTest]    Script Date: 6/27/2024 1:33:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vh_vehiculosTest](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](20) NOT NULL,
	[chasis] [varchar](20) NULL,
	[marca] [varchar](50) NULL,
	[modelo] [varchar](50) NULL,
	[anio_modelo] [int] NULL,
	[color] [varchar](50) NULL,
	[estado] [varchar](50) NULL,
	[fecha_registro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[vh_vehiculosTest] ON 

INSERT [dbo].[vh_vehiculosTest] ([id], [codigo], [chasis], [marca], [modelo], [anio_modelo], [color], [estado], [fecha_registro]) VALUES (1, N'4545', N'c4040', N'Toyota', N'sail', 2010, N'blanco', N'vendido', CAST(N'2024-06-06T00:00:00.000' AS DateTime))
INSERT [dbo].[vh_vehiculosTest] ([id], [codigo], [chasis], [marca], [modelo], [anio_modelo], [color], [estado], [fecha_registro]) VALUES (2, N'3030', N'f5655f', N'chevrolet', N'camioneta', 2015, N'rojo', N'vendido', CAST(N'2024-07-06T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[vh_vehiculosTest] OFF
GO
/****** Object:  StoredProcedure [dbo].[DeleteVehiculo]    Script Date: 6/27/2024 1:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteVehiculo]
    @Id INT
AS
BEGIN
    DELETE FROM vh_vehiculosTest
    WHERE id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllCar]    Script Date: 6/27/2024 1:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllCar]
AS
BEGIN
    SET NOCOUNT ON; -- Para evitar contar las filas afectadas
    
    SELECT * FROM vh_vehiculosTest
END
GO
/****** Object:  StoredProcedure [dbo].[GetCarId]    Script Date: 6/27/2024 1:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCarId]
    @vehiculoId INT
AS
BEGIN
    SET NOCOUNT ON; -- Para evitar contar las filas afectadas
    
    SELECT *
    FROM vh_vehiculosTest
    WHERE Id = @vehiculoId;
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateCar]    Script Date: 6/27/2024 1:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateCar]
	@Id int,
	@cod varchar(20),
	@chasis varchar(20) null,
	@marca varchar(50) null,
	@modelo varchar(50) null,
	@anio int null,
	@color varchar(50) null,
	@estado varchar(50) null,
	@fecha datetime null
AS
BEGIN
    BEGIN TRANSACTION;

    -- Manejo de errores
    BEGIN TRY
        -- Inserta el nuevo registro en la tabla Personas
        UPDATE vh_vehiculosTest
		SET 
			codigo = @cod,
			chasis			= CASE WHEN @chasis IS NULL THEN chasis ELSE @chasis end,
			marca			= CASE WHEN @marca IS NULL then marca else @marca end,
			modelo			= CASE WHEN @modelo IS NULL then marca else @modelo end,
			anio_modelo		= CASE WHEN @anio IS NULL then marca else @anio end,
			color			= CASE WHEN @color IS NULL then marca else @color end,
			estado			= CASE WHEN @estado IS NULL then marca else @estado end,
			fecha_registro	= CASE WHEN @fecha IS NULL then marca else @fecha end
        WHERE
			id = @Id;
        -- Si la inserción fue exitosa, confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si hubo un error, revierte la transacción
        ROLLBACK TRANSACTION;

        -- Devuelve información del error
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO
