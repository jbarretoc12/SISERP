create database bd_siserp;
use bd_siserp;
create table tb_adm_empresas(
	coEmp varchar(20) primary key,
    razSocial varchar(100),
    nomComerial varchar(20),
    dirFiscal varchar(250),
    telefono varchar(15),
    paginaWeb varchar(50),
    estado char(1),
    st_registro bit,
    co_usua_crea varchar(30),
    fe_usua_crea datetime,
    co_usua_modi varchar(30),
    fe_usua_modi datetime
);

create table tb_adm_perfiles(
	coPerfil char(3),
    dePerfil varchar(30)
);
insert into tb_adm_perfiles (coPerfil,dePerfil) values ('001','ADMINISTRADOR'),('002','COMERCIAL');

create table tb_adm_usuarios(
	coUsu varchar(30) primary key,
    nroDoc varchar(20),
    noUsu varchar(100),
    noClave varchar(30),
    coPerfil char(3),
    estado char(1),
    st_registro bit,
    co_usua_crea varchar(30),
    fe_usua_crea datetime,
    co_usua_modi varchar(30),
    fe_usua_modi datetime
);

insert into tb_adm_usuarios (coUsu,nroDoc,noUsu,noClave,coPerfil,estado,st_registro,co_usua_crea,fe_usua_crea)
values ('JBARRETO','44720315','JEFFER BARRETO CARHUAZ','123','001','A',1,'JBARRETO',localtime());

select * from tb_adm_usuarios;

delimiter //
create procedure tb_adm_usuarios_ls(
	in opcion int,
	in criterio varchar(30))
begin
	if opcion=1 then
		select coUsu,nroDoc,noUsu,coPerfil,estado from tb_adm_usuarios
        where estado='A' and st_registro=1 and concat_ws(coUsu, nroDoc, noUsu) like concat('%', criterio, '%');
	end if;
end; //
delimiter ;


