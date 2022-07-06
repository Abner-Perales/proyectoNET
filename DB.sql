CREATE DATABASE Examen;

USE Examen;

CREATE TABLE [dbo].[Cat_Proveedores]
(
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Nombre VARCHAR(100) NOT NULL,
RFC VARCHAR(13) NOT NULL
);

CREATE TABLE Cat_TipoProducto
(
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Nombre VARCHAR(30) NOT NULL
);

CREATE TABLE Productos
(
Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
Nombre VARCHAR(30) NOT NULL,
IDProveedor INT NOT NULL,
IDTipo INT NOT NULL,
Cantidad INT NOT NULL,
FechaAlta DATETIME NOT NULL,
Modelo VARCHAR(20),
Marca VARCHAR(20),

CONSTRAINT fk_proveedores FOREIGN KEY (IDProveedor) REFERENCES Cat_Proveedores (ID) ON DELETE CASCADE,
CONSTRAINT fk_tipoProducto FOREIGN KEY (IDTipo) REFERENCES Cat_TipoProducto (ID) ON DELETE CASCADE
);

SELECT * FROM Cat_Proveedores;
SELECT * FROM Cat_TipoProducto;
SELECT * FROM Productos;

ALTER PROCEDURE [dbo].[sp_MostrarProductos]
AS
BEGIN
	SELECT p.Id as IDProducto, p.Nombre as NombreProducto, p.Marca as Marca, p.Modelo as Modelo, cp.Nombre as Proveedor, ctp.Nombre as TipoProducto, p.Cantidad as Cantidad, p.FechaAlta as FechaAlta
	FROM Productos p
	INNER JOIN Cat_Proveedores cp on p.IDProveedor = cp.ID
	INNER JOIN Cat_TipoProducto ctp on p.IDTipo = ctp.ID
	order by p.Id
END
GO

ALTER PROCEDURE [dbo].[sp_EliminarProducto]
	@Id INT
AS
BEGIN
	DELETE FROM Productos WHERE Id = @Id;
END
GO

ALTER PROCEDURE [dbo].[sp_MostrarFiltro]
	@FechaInicio DATETIME,
	@FechaFin DATETIME,
	@TodosProductos INT
AS
BEGIN	
	IF @TodosProductos = 0 --Productos con existencia 0
	BEGIN
		SELECT p.Id as IDProducto, p.Nombre as NombreProducto, p.Marca as Marca, p.Modelo as Modelo, cp.Nombre as Proveedor, ctp.Nombre as TipoProducto, p.Cantidad as Cantidad, p.FechaAlta as FechaAlta
		FROM Productos p
		INNER JOIN Cat_Proveedores cp on p.IDProveedor = cp.ID
		INNER JOIN Cat_TipoProducto ctp on p.IDTipo = ctp.ID
		WHERE p.Cantidad = 0 AND (CONVERT(DATETIME, p.FechaAlta, 120) BETWEEN @FechaInicio AND @FechaFin)
		ORDER BY p.Id
	END;
	ELSE IF @TodosProductos = 1 --Productos con existencia
	BEGIN
		SELECT p.Id as IDProducto, p.Nombre as NombreProducto, p.Marca as Marca, p.Modelo as Modelo, cp.Nombre as Proveedor, ctp.Nombre as TipoProducto, p.Cantidad as Cantidad, p.FechaAlta as FechaAlta
		FROM Productos p
		INNER JOIN Cat_Proveedores cp on p.IDProveedor = cp.ID
		INNER JOIN Cat_TipoProducto ctp on p.IDTipo = ctp.ID
		WHERE p.Cantidad > 0 AND (CONVERT(DATETIME, p.FechaAlta, 120) BETWEEN @FechaInicio AND @FechaFin)
		ORDER BY p.Id
	END;
	ELSE --Todos los productos
	BEGIN
		SELECT p.Id as IDProducto, p.Nombre as NombreProducto, p.Marca as Marca, p.Modelo as Modelo, cp.Nombre as Proveedor, ctp.Nombre as TipoProducto, p.Cantidad as Cantidad, p.FechaAlta as FechaAlta
		FROM Productos p
		INNER JOIN Cat_Proveedores cp on p.IDProveedor = cp.ID
		INNER JOIN Cat_TipoProducto ctp on p.IDTipo = ctp.ID
		WHERE CONVERT(DATETIME, p.FechaAlta, 120) BETWEEN @FechaInicio AND @FechaFin
		ORDER BY p.Id
	END
END
GO

ALTER PROCEDURE [dbo].[sp_ActualizarCantidad]
	@Id INT,
	@Cantidad INT
AS
BEGIN
	UPDATE [dbo].[Productos]
	SET Cantidad = @Cantidad 
	WHERE Id = @Id;
END
GO

ALTER PROCEDURE [dbo].[sp_AgregarProducto]
	@Nombre VARCHAR(30),
	@IDProveedor INT,
	@IDTipo INT,
	@Cantidad INT,
	@Modelo VARCHAR(20),
	@Marca VARCHAR(20)
AS
BEGIN
	INSERT INTO [dbo].[Productos] (Nombre, IDProveedor, IDTipo, Cantidad, FechaAlta, Modelo, Marca)
	VALUES (@Nombre, @IDProveedor, @IDTipo, @Cantidad, GETDATE(), @Modelo, @Marca);
END
GO


INSERT INTO Cat_Proveedores (Nombre, RFC) VALUES ('Dell', 'RFC123');
INSERT INTO Cat_Proveedores (Nombre, RFC) VALUES ('HP', 'RFC123');
INSERT INTO Cat_Proveedores (Nombre, RFC) VALUES ('EXPO', 'RFC123');

INSERT INTO Cat_TipoProducto (Nombre) VALUES ('Electronica');
INSERT INTO Cat_TipoProducto (Nombre) VALUES ('Cocina');
INSERT INTO Cat_TipoProducto (Nombre) VALUES ('Ropa');