use master
go

if db_id('bd_cine_swc') is not null
	drop database bd_cine_swc
go

create database bd_cine_swc
go

use bd_cine_swc
go

/*Combos Pelicula*/
if object_id('tb_censura') is not null
	drop table tb_censura
go

create table tb_censura(
idCensura int primary key identity(1,1),
desCensura varchar(200) null
)
go

if object_id('tb_genero') is not null
	drop table tb_genero
go

create table tb_genero(
idGenero int primary key identity(1,1),
desGenero varchar(200) null
)
go

/*Combos Empleados y Cliente*/
if object_id('tb_tipotrabajador') is not null
	drop table tb_tipotrabajador
go

create table tb_tipotrabajador(
idTipotrab int primary key identity(1,1),
desTipotrab varchar(200) null
)
go

if object_id('tb_Sexo') is not null
	drop table tb_Sexo
go

create table tb_Sexo(
idSexo int primary key identity(1,1),
desSexo varchar(200) null
)
go

/*Combos Producto*/
if object_id('tb_tipoProducto') is not null
	drop table tb_tipoProducto
go

create table tb_tipoProducto(
idTipopro int primary key identity(1,1),
desTipopro varchar(200) null
)
go

/*Combos Venta Producto*/
if object_id('tb_tamaño') is not null
	drop table tb_tamaño
go

create table tb_tamaño(
idTamaño int primary key identity(1,1),
desTamaño varchar(200) null
)
go

/*Combos Funcion*/
if object_id('tb_sala') is not null
	drop table tb_sala
go

create table tb_sala(
idSala int primary key identity(1,1),
desSala varchar(200) null
)
go

if object_id('tb_pelicula') is not null
	drop table tb_pelicula
go

create table tb_pelicula(
idPelicula int primary key identity(1,1),
nombre varchar(200),
foto varchar(200) null,
sinopsis varchar(max) null,
duracion time null,
trailer varchar(200)null,
pais varchar(200) null,
idGenero int references tb_genero,
idCensura int references tb_censura
)
go

create table tb_estadoempleado(
idEstadoEmpleado int primary key identity(1,1),
desEstadoEmpleado varchar(200) null
)
go

if object_id('tb_empleado') is not null
	drop table tb_empleado
go

create table tb_empleado(
idEmpleado int primary key identity(1,1),
nombre varchar(200)null,
apellidos varchar(200) null,
dni varchar(8) null,
sueldo decimal(10,2) null,
fecNac datetime null,
usuario varchar(200),
contra varchar(200),
idTipotrab int references tb_tipotrabajador,
idSexo int references tb_sexo,
idEstadoEmpleado int references tb_estadoempleado,
)
go

if object_id('tb_producto') is not null
	drop table tb_producto
go

create table tb_producto(
idProducto int primary key identity(1,1),
nombre varchar(200)null,
precio decimal(10,2) null,
idTipopro int references tb_tipoProducto,
)
go

if object_id('tb_funcion') is not null
	drop table tb_funcion
go

create table tb_funcion(
idFuncion int primary key identity(1,1),
nombre_funcion varchar(200) null,/*Concatenar NombrePelicula y hora Funcion*/
fecha_inicio datetime null,
fecha_fin datetime null,
nroSala int null,
hora_funcion time null,
idPelicula int references tb_Pelicula,
idSala int references tb_sala
)
go

if object_id('tb_VentaBoleto') is not null
	drop table tb_VentaBoleto
go

create table tb_VentaBoleto(
idVentaB int primary key identity(1,1),
fecha datetime null,
idFuncion int references tb_Funcion,
idEmpleado int references tb_empleado,
cantidad int null,
total decimal(10,2) null
)
go

if object_id('tb_VentaProducto') is not null
	drop table tb_VentaProducto
go

create table tb_VentaProducto(
idVentaP int primary key identity(1,1),
fecha datetime null,
idEmpleado int references tb_empleado,
total decimal(10,2) null
)
go

if object_id('tb_DetalleVentaProducto') is not null
	drop table tb_DetalleVentaProducto
go

create table tb_DetalleVentaProducto(
idDetalleVP int primary key identity(1,1),
idTipopro int references tb_tipoProducto,
idProducto int references tb_Producto,
idTamaño int references tb_Tamaño,
idVentaP int references tb_VentaProducto,
subtotal decimal(10,2) null
)
go