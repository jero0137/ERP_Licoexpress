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

create table proveedores(
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	nombre varchar(30) NOT NULL,
	numero_contacto varchar(11) not null,
	correo varchar(20) not null,
	CONSTRAINT usuarios_pk PRIMARY KEY (id)	
);


create or replace procedure core.p_inserta_sesion(
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
