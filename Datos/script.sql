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
	ciudad varchar not null
);

create or replace procedure p_inserta_user(
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

