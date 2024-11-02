create database bd_siserp;
use bd_siserp;

create table tb_empresas(
	coEmp varchar(20) primary key,
    razSocial varchar(100),
    nomComercial varchar(20),
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

select * from tb_empresas;

insert into tb_empresas (coEmp,razSocial,nomComercial,dirFiscal,telefono,paginaWeb,estado,st_registro,co_usua_crea,fe_usua_crea,co_usua_modi,fe_usua_modi) 
values ('10707832717','ED Store E.I.R.L.','El Davis','Av. Lima #234 Piso 10','963258741','eldavis.com.pe','A',1,'DCONDORI',localtime(),null,null);
insert into tb_empresas (coEmp,razSocial,nomComercial,dirFiscal,telefono,paginaWeb,estado,st_registro,co_usua_crea,fe_usua_crea,co_usua_modi,fe_usua_modi) 
values ('10288107909','El Halcón S.A.C.','HalcónSoft','Av. Mangomarca #666 Piso 5','987456321','halconsoft.com.pe','A',1,'DCONDORI',localtime(),null,null);

/*---------Procedimientos Almacenados Listar y Grabar Empresas---------*/
drop procedure if exists sp_tb_empresas_ls;
delimiter //
create procedure sp_tb_empresas_ls(
	in p_opcion int,
	in p_criterio varchar(50))
begin
	if p_opcion=1 then
		select coEmp,razSocial,nomComercial,dirFiscal,telefono,paginaWeb,
        estado,st_registro,co_usua_crea,fe_usua_crea,co_usua_modi,fe_usua_modi from tb_empresas
        where estado='A' and st_registro=1 and concat_ws('', coEmp, razSocial) like concat('%', p_criterio, '%');
	end if;
end; //
delimiter ;

delimiter //
create procedure sp_tb_empresas_gr(
	p_opc int,
	p_coEmp varchar(20),
    p_razSocial varchar(100),
    p_nomComercial varchar(20),
    p_dirFiscal varchar(250),
    p_telefono varchar(15),
    p_paginaWeb varchar(50),
    p_co_usua_crea varchar(30))
begin
	if p_opc=1 then
		insert into tb_empresas (coEmp,razSocial,nomComercial,dirFiscal,telefono,paginaWeb,estado,st_registro,co_usua_crea,fe_usua_crea,co_usua_modi,fe_usua_modi) 
		values (p_coEmp,p_razSocial,p_nomComercial,p_dirFiscal,p_telefono,p_paginaWeb,'A',1,p_co_usua_crea,localtime(),null,null);
	elseif p_opc=2 then
		update tb_empresas set 
        coEmp=p_coEmp,
        razSocial=p_razSocial,
        nomComercial=p_nomComercial,
        dirFiscal=p_dirFiscal,
        telefono=p_telefono,
        paginaWeb=p_paginaWeb,
        co_usua_modi=p_co_usua_crea,
        fe_usua_modi=localtime() 
        where coEmp=p_coEmp;
	elseif p_opc=3 then
		delete from tb_empresas where coEmp=p_coEmp;
    end if;
end //
delimiter ;

/* exec a los procedimientos */
call sp_tb_empresas_ls(1, 'halcon');
call sp_tb_empresas_gr(1,'10852963587','KinVaz S.A.C.','KV Store','Av. Huascar #10 Piso 1','985632564','kinvaz.com.pe','DCONDORI');

