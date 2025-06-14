CREATE DATABASE ComercioDB;
GO

USE ComercioDB;
GO

-- Tabla de Usuarios (incluye Cliente, Vendedor, Administrador)
CREATE TABLE Usuario (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    DNI NVARCHAR(20),
    Email NVARCHAR(150) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    Rol NVARCHAR(50) NOT NULL -- 'Cliente', 'Vendedor', 'Administrador'
);
GO

-- Tabla de Marcas
CREATE TABLE Marca (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);
GO

-- Tabla de Tipos de Producto
CREATE TABLE TipoProducto (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);
GO

-- Tabla de Proveedores
CREATE TABLE Proveedor (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RazonSocial NVARCHAR(150) NOT NULL,
    Cuit NVARCHAR(20) NOT NULL,
    Email NVARCHAR(150),
    Telefono NVARCHAR(50),
    Direccion NVARCHAR(200)
);
GO

-- Tabla de Productos
CREATE TABLE Producto (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(150) NOT NULL,
    Precio DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL,
    StockMinimo INT NOT NULL,
    UrlImgProducto NVARCHAR(1000) NOT NULL,
	PorcentajeGanancia DECIMAL(5,2) NOT NULL,
    IdMarca INT NOT NULL,
    IdTipoProducto INT NOT NULL,
    FOREIGN KEY (IdMarca) REFERENCES Marca(Id),
    FOREIGN KEY (IdTipoProducto) REFERENCES TipoProducto(Id)
);
GO

-- Tabla intermedia Producto-Proveedor
CREATE TABLE ProductoProveedor (
    IdProducto INT NOT NULL,
    IdProveedor INT NOT NULL,
    PRIMARY KEY (IdProducto, IdProveedor),
    FOREIGN KEY (IdProducto) REFERENCES Producto(Id),
    FOREIGN KEY (IdProveedor) REFERENCES Proveedor(Id)
);
GO

-- Tabla de Compras
CREATE TABLE Compra (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdProveedor INT NOT NULL,
    Fecha DATETIME NOT NULL,
    FOREIGN KEY (IdProveedor) REFERENCES Proveedor(Id)
);
GO

-- Detalle de Compras
CREATE TABLE DetalleCompra (
    IdDetalleCompra INT PRIMARY KEY IDENTITY(1,1),
    IdCompra INT NOT NULL,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (IdCompra) REFERENCES Compra(Id),
    FOREIGN KEY (IdProducto) REFERENCES Producto(Id)
);
GO

-- Tabla de Ventas
CREATE TABLE Venta (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdCliente INT NOT NULL,
    IdVendedor INT NOT NULL,
    Fecha DATETIME NOT NULL,
    NumeroFactura NVARCHAR(50) NOT NULL UNIQUE,
    FOREIGN KEY (IdCliente) REFERENCES Usuario(Id),
    FOREIGN KEY (IdVendedor) REFERENCES Usuario(Id)
);
GO

-- Detalle de Ventas
CREATE TABLE DetalleVenta (
    IdDetalleVenta INT PRIMARY KEY IDENTITY(1,1),
    IdVenta INT NOT NULL,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (IdVenta) REFERENCES Venta(Id),
    FOREIGN KEY (IdProducto) REFERENCES Producto(Id)
);