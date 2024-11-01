create database bd_siserp;
use bd_siserp;
create table tb_empresas(
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