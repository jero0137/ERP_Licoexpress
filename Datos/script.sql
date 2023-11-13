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
	ciudad varchar not null
);

create table sedes (
	id int4 not null generated always as identity,
	nombre varchar not null,
	direccion varchar not null,
	nombre_admin varchar not null,
	contacto_admin varchar not null,
	telefono_admin varchar not null,
	ciudad varchar not null
);

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


create or replace procedure p_elimina_location(in p_id integer)
language plpgsql 
as $$
begin
    delete from sedes s
   	where s.id=p_id;
end;
$$;

