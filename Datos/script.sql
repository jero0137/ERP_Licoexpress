create table usuarios(
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	correo varchar(30) NOT NULL,
	contrasena varchar(200) not null,
	CONSTRAINT usuarios_pk PRIMARY KEY (id)
);

create table sesiones(
	token_sesion varchar(200),
	fecha_creacion date,
	fecha_fin date
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

create table public.sedes (
	id int4 not null generated always as identity,
	nombre varchar not null,
	direccion varchar not null,
	nombre_admin varchar not null,
	contacto_admin varchar not null,
	telefono_admin varchar not null,
	ciudad varchar not null,
	constraint sedes_pk primary key (id)
);

create table public.tipos (
	id int4 not null generated always as identity,
	descripcion varchar not null,
	constraint tipos_pk primary key (id)
);

create table public.productos (
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

alter table public.productos add constraint tipo_producto_fk foreign key (tipo_id) references public.tipos(id);
alter table public.productos add constraint proveedor_producto_fk foreign key (proveedor_id) references public.proveedores(id);


create or replace procedure p_inserta_sesion(
                    in p_token varchar)
    language plpgsql
as
$$
    begin
        insert into sesiones(token_sesion, fecha_creacion, fecha_fin)
        values(p_token, now(), now());
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