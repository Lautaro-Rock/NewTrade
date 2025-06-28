USE ComercioDB
GO

INSERT INTO Usuario (Nombre, Apellido, DNI, Email, Password, Rol)
VALUES ('Admin', 'McAdmin', '12345678', 'pruebaAdmin', 'pruebaAdmin', 'Administrador')
GO

INSERT INTO Usuario (Nombre, Apellido, DNI, Email, Password, Rol)
VALUES ('Test', 'Testing', '12345478', 'prueba', 'prueba', 'Vendedor')
GO

INSERT INTO Marca (Nombre)
VALUES ('Nike'),
       ('Adidas'),
	   ('Reebok'),
	   ('Adidas'),
	   ('Puma');
GO


INSERT INTO TipoProducto(Nombre)
VALUES ('Zapatilla'),
       ('Accesorio'),
	   ('Remera'),
	   ('Pantalon'),
	   ('Bal�n');
GO



INSERT INTO Producto (Nombre,Precio,Stock,StockMinimo,UrlImgProducto,PorcentajeGanancia,IdMarca,IdTipoProducto)
VALUES ('Remera Oversize',15000,10,5,'https://acdn.mitiendanube.com/stores/709/396/products/remeronnegro1-e6479308c432784ad416727764008590-1024-1024.png',2.5,2,3),
('Zapatillas Nike Air Max', 60000, 15, 5, 'https://image-cdn.hypb.st/https://hypebeast.com/image/2023/01/nike-air-max-90-burgundy-crush-DQ4071-004-release-info-000.jpg?fit=max&cbr=1&q=90&w=750&h=500', 2.0, 1, 1),
('Zapatillas Puma RS-X', 58000, 12, 4, 'https://images.puma.com/image/upload/f_auto,q_auto,b_rgb:fafafa/global/369579/01/fnd/CHL/w/1000/h/1000/fmt/png', 2.3, 5, 1),
('Gorra Adidas', 12000, 20, 5, 'https://th.bing.com/th/id/R.b45e4826e1ad49c3754195636035745b?rik=cXSRDOU2qll8mA&pid=ImgRaw&r=0', 1.8, 2, 2),
('Mochila Nike', 25000, 8, 3, 'https://www.digitalsport.com.ar/files/products/5c59bf14aa3f4-461294-1200x1200.jpg', 2.1, 1, 2),
('Bal�n de f�tbol Adidas', 30000, 10, 3, 'https://static.sprintercdn.com/products/0341924/adidas-starlancer-plus_0341924_00_4_3844583287.jpg', 2.0, 2, 4),
('Bal�n Puma F�tbol Sala', 27000, 9, 2, 'https://th.bing.com/th/id/OIP.PBAf3J-gPkURzuZMhfOG3AHaHa?rs=1&pid=ImgDetMain', 2.2, 5, 4);

