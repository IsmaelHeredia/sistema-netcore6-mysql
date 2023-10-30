create table tipos_usuarios(
	id int not null auto_increment,
	nombre varchar(100) unique,
	primary key(id)
);

create table usuarios(
	id int not null auto_increment,
	nombre varchar(100) unique,
	clave varchar(100),
	id_tipo int,
	fecha_registro varchar(100),
	primary key(id),
	foreign key (id_tipo) references tipos_usuarios(id)
);

create table proveedores(
	id int not null auto_increment,
	nombre varchar(100) unique,
	direccion varchar(100),
	telefono int,
	fecha_registro varchar(100),
	primary key(id)
);

create table productos(
	id int not null auto_increment,
	nombre varchar(100) unique,
	descripcion varchar(100),
	precio double,
	id_proveedor int,
	fecha_registro varchar(100),
	primary key(id),
	foreign key (id_proveedor) references proveedores(id) 
);

insert into tipos_usuarios(nombre) values('Administrador');
insert into tipos_usuarios(nombre) values('Usuario');

insert into usuarios(nombre,clave,id_tipo,fecha_registro) 
	values('supervisor','$2a$11$n3wJp8J589XUawMW2tdk4eghkRqMys.NA7YfaoMq6.jLzOHF8QjBa',1,'2023-04-02');

insert into proveedores(nombre,direccion,telefono,fecha_registro) 
	values('empresa 1','calle 1',4975034,'2023-04-02');

insert into proveedores(nombre,direccion,telefono,fecha_registro) 
	values('empresa 2','calle 2',4646891,'2023-03-06');

insert into proveedores(nombre,direccion,telefono,fecha_registro) 
	values('empresa 3','calle 3',4646891,'2023-08-21');

insert into productos(nombre,descripcion,precio,id_proveedor,fecha_registro) 
	values('producto 1','descripción 1',200.20,1,'2023-03-01');

insert into productos(nombre,descripcion,precio,id_proveedor,fecha_registro) 
	values('producto 2','descripción 2',400,2,'2023-01-06');

insert into productos(nombre,descripcion,precio,id_proveedor,fecha_registro) 
	values('producto 3','descripción 3',500.55,3,'2023-08-02');

insert into productos(nombre,descripcion,precio,id_proveedor,fecha_registro)
	values('producto 4','descripción 4',250,2,'2023-04-20');

insert into productos(nombre,descripcion,precio,id_proveedor,fecha_registro) 
	values('producto 5','descripción 5',750,3,'2023-8-23');