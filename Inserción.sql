use bd_cine_swc
go
-- Inserci�n a tabla sexo:
insert into tb_Sexo values('Masculino')
insert into tb_Sexo values('Femenino')
go
-- Inserci�n a tabla tipo de trabajador:
insert into tb_tipotrabajador values('Administrador')
insert into tb_tipotrabajador values('Cajero')
go
--Inserci�n a la tabla censura
insert into tb_censura values('Apta para todos')
insert into tb_censura values('+14')
insert into tb_censura values('+18')
go
-- Inserci�n a la tabla genero
insert into tb_genero values('Drama')
insert into tb_genero values('Comedia')
insert into tb_genero values('Acci�n')
insert into tb_genero values('Ciencia Ficci�n')
insert into tb_genero values('Fantas�a')
insert into tb_genero values('Terror')
insert into tb_genero values('Romance')
insert into tb_genero values('Musical')
insert into tb_genero values('Melodrama')
insert into tb_genero values('Suspenso')
go


--Inserci�n a la tabla sala
insert into tb_sala values('VIP')
insert into tb_sala values('4DX')
insert into tb_sala values('IMAX')
insert into tb_sala values('MACROXE')
go

--Inserci�n a la tabla tama�o de producto
insert into tb_tama�o values('Peque�o')
insert into tb_tama�o values('Mediano')
insert into tb_tama�o values('Grande')
go

--Inserci�n a la tabla de tipo de producto
insert into tb_tipoProducto values('Bebidas')
insert into tb_tipoProducto values('Dulces')
insert into tb_tipoProducto values('Confiteria')
insert into tb_tipoProducto values('Salados')
insert into tb_tipoProducto values('Sandwiches')
go
--Inserci�n de un administrador por defecto en la tabla de empleados
insert into tb_empleado values('Administrador', 'Administrar', '12345678', 0000.00, '01/01/2017','admin', 'admin', 1, 1, 0)
go