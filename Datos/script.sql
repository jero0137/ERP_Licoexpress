create table usuarios(
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	correo varchar(30) NOT NULL,
	contrasena varchar(200) not null,
	rol varchar(30) not null,
	sede_id int not null,
	CONSTRAINT usuarios_pk PRIMARY KEY (id)
);




create table proveedores (
	id int4 not null generated always as identity,
	nombre_empresa varchar not null,
	responsable varchar not null,
	correo varchar not null,
	numero_registro int not null,
	numero_contacto varchar not null,
	direccion_empresa varchar not null,
	ciudad varchar not null,
	constraint proveedores_pk primary key (id)
);

create table sedes (
	id int4 not null generated always as identity,
	nombre varchar not null,
	direccion varchar not null,
	nombre_admin varchar not null,
	contacto_admin varchar not null,
	telefono_admin varchar not null,
	ciudad varchar not null,
	constraint sedes_pk primary key (id)
);

create table tipos (
	id int4 not null generated always as identity,
	descripcion varchar not null,
	constraint tipos_pk primary key (id)
);

create table productos (
	id int4 not null generated always as identity,
	nombre varchar not null,
	tipo_id int4 not null,
	tamaño varchar not null,
	imagen varchar not null,
	precio_base real not null,
	precio_venta real not null,
	proveedor_id int4 not null,
	constraint productos_pk primary key (id)
);


create table public.inventarios (
	id int4 not null generated always as identity,
	sede_id int4 not null,
	producto_id int4 not null,
	fecha_vencimiento varchar not null,
	lote int not null,
	stock int not null,
	constraint inventarios_pk primary key (id)
);

alter table public.inventarios add constraint sedes_inventario_fk foreign key (sede_id) references public.sedes(id);
alter table public.inventarios add constraint producto_inventario_fk foreign key (producto_id) references public.productos(id);


create table public.productosxproveedor (
	id int4 not null generated always as identity,
	producto_id int4 not null,
	proveedor_id int4 not null,
	constraint productosxproveedor_pk primary key (id)
);

create or replace procedure p_inserta_usuario(
                    in p_correo varchar,
                    in p_contrasena varchar,
                    in p_rol varchar,
                    in p_sede_id int)
    language plpgsql
as
$$
    begin
        insert into usuarios(correo, contrasena, rol, sede_id)
        values(p_correo, p_contrasena, p_rol, p_sede_id);
    end;
$$;

create or replace procedure p_elimina_supplier(in p_id integer)
language plpgsql 
as $$
begin
    delete from proveedores p
   	where p.id=p_id;
end;
$$;


create or replace procedure p_inserta_supplier(in p_nombre_empresa varchar, in p_responsable varchar,
in p_correo varchar, in p_numero_registro int, in p_numero_contacto varchar, in p_direccion_empresa varchar,
in p_ciudad varchar)
language plpgsql    
as $$
begin
    insert into proveedores(nombre_empresa, responsable, correo, numero_registro, numero_contacto, direccion_empresa,
    ciudad)
    values (p_nombre_empresa, p_responsable, p_correo, p_numero_registro, p_numero_contacto,
    p_direccion_empresa, p_ciudad);
end;
$$;


create or replace procedure p_inserta_location(in p_nombre varchar, in p_direccion varchar,
in p_nombre_admin varchar, in p_contacto_admin varchar, in p_telefono_admin varchar, in p_ciudad varchar)
language plpgsql    
as $$
begin
    insert into sedes(nombre, direccion, nombre_admin, contacto_admin, telefono_admin, ciudad)
    values (p_nombre, p_direccion, p_nombre_admin, p_contacto_admin, p_telefono_admin, p_ciudad);
end;
$$;


create or replace procedure p_inserta_product(in p_nombre varchar, in p_tipo_id integer,
in p_tamaño varchar, in p_imagen varchar, in p_precio_base real, in p_precio_venta real, in p_proveedor_id integer)
language plpgsql    
as $$
begin
    insert into productos(nombre, tipo_id, tamaño, imagen, precio_base, precio_venta, proveedor_id)
    values (p_nombre, p_tipo_id, p_tamaño, p_imagen, p_precio_base, p_precio_venta, p_proveedor_id);
end;
$$;



create or replace procedure p_elimina_location(in p_id integer)
language plpgsql 
as $$
begin
    delete from sedes s
   	where s.id=p_id;
end;
$$;


create or replace procedure p_elimina_product(in p_id integer)
language plpgsql 
as $$
begin
    delete from productos p
   	where p.id=p_id;
end;
$$;

----------------------------------------------------------------------------------------

-- Insertar datos en la tabla de sedes
INSERT INTO sedes (nombre, direccion, nombre_admin, contacto_admin, telefono_admin, ciudad)
VALUES
  ('Sede Principal', 'Calle de los licores #123', 'Javier Gómez', 'javier@example.com', '1234567890', 'Bogotá'),
  ('Sede Norte', 'Avenida del Ron #789', 'Ana Martínez', 'ana@example.com', '0987654321', 'Medellín'),
  ('Sede Sur', 'Carrera del Tequila #456', 'Carlos González', 'carlos@example.com', '1122334455', 'Cali');

-- Insertar datos en la tabla de tipos
INSERT INTO tipos (descripcion)
VALUES
  ('Whisky'),
  ('Vodka'),
  ('Ron'),
  ('Tequila'),
  ('Ginebra');

-- Insertar datos en la tabla de proveedores
INSERT INTO proveedores (nombre_empresa, responsable, correo, numero_registro, numero_contacto, direccion_empresa, ciudad)
VALUES
  ('WhiskyWorld', 'Luis Hernández', 'luis@whiskyworld.com', 123456, '0987654321', 'Carrera 20 #45-67', 'Bogotá'),
  ('RonExquisite', 'María Rodríguez', 'maria@ronexquisite.com', 789012, '1122334455', 'Calle 80 #12-34', 'Medellín'),
  ('TequilaLandia', 'Pedro Gómez', 'pedro@tequilalandia.com', 345678, '5432167890', 'Avenida 40 #98-76', 'Cali');

-- Insertar datos en la tabla de usuarios
INSERT INTO usuarios (correo, contrasena, rol, sede_id)
VALUES
  ('admin@licores.com', 'contraseña123', 'Administrador', 1),
  ('usuario1@licores.com', 'pass123', 'Usuario', 2),
  ('usuario2@licores.com', 'password123', 'Usuario', 3);

-- Insertar datos en la tabla de productos
INSERT INTO productos (nombre, tipo_id, tamaño, imagen, precio_base, precio_venta, proveedor_id)
VALUES
  ('Johnnie Walker Black Label', 1, '750ml', 'https://static.wixstatic.com/media/477dc5_f3e8aa2c49de486f8b6b6a0e0c736842~mv2.png/v1/fill/w_560,h_560,al_c,q_85,usm_0.66_1.00_0.01,enc_auto/477dc5_f3e8aa2c49de486f8b6b6a0e0c736842~mv2.png', 150000, 175000, 1),
  ('Havana Club Añejo 7 Años', 3, '700ml', 'https://d2j6dbq0eux0bg.cloudfront.net/images/30491376/1617549085.jpg', 80000, 95000, 2),
  ('Patrón Reposado', 4, '750ml', 'https://licoresmedellin.com/cdn/shop/products/tequila-patron-reposado-botella-700mltequila-patron-reposado-botella-700mlpatronlicores-medellin-658858.png?crop=center&height=600&v=1681662091&width=600', 120000, 140000, 3);
