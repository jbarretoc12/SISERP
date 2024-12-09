create database bd_siserp;
use bd_siserp;

create table tb_adm_empresas(
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
    fe_usua_modi datetime,
    logoEmpresa longblob
);

select * from tb_adm_empresas;

insert into tb_adm_empresas (coEmp,razSocial,nomComercial,dirFiscal,telefono,paginaWeb,estado,st_registro,co_usua_crea,fe_usua_crea,co_usua_modi,fe_usua_modi) 
values ('10707832717','ED Store E.I.R.L.','El Davis','Av. Lima #234 Piso 10','963258741','eldavis.com.pe','A',1,'DCONDORI',localtime(),null,null);
insert into tb_adm_empresas (coEmp,razSocial,nomComercial,dirFiscal,telefono,paginaWeb,estado,st_registro,co_usua_crea,fe_usua_crea,co_usua_modi,fe_usua_modi) 
values ('10288107909','El Halcón S.A.C.','HalcónSoft','Av. Mangomarca #666 Piso 5','987456321','halconsoft.com.pe','A',1,'DCONDORI',localtime(),null,null);

/*---------Procedimientos Almacenados Listar y Grabar Empresas---------*/
drop procedure if exists sp_tb_adm_empresas_ls;
delimiter //
create procedure sp_tb_adm_empresas_ls(
	in p_opcion int,
	in p_criterio varchar(50))
begin
	if p_opcion=1 then
		select coEmp,razSocial,nomComercial,dirFiscal,telefono,paginaWeb,
        estado,st_registro,co_usua_crea,fe_usua_crea,co_usua_modi,fe_usua_modi,logoEmpresa from tb_adm_empresas
        where estado='A' and st_registro=1 and concat_ws('', coEmp, razSocial) like concat('%', p_criterio, '%');
	end if;
end; //
delimiter ;

drop procedure if exists sp_tb_adm_empresas_gr;
delimiter //
create procedure sp_tb_adm_empresas_gr(
	p_opc int,
	p_coEmp varchar(20),
    p_razSocial varchar(100),
    p_nomComercial varchar(20),
    p_dirFiscal varchar(250),
    p_telefono varchar(15),
    p_paginaWeb varchar(50),
    p_co_usua_crea varchar(30),
    p_logoEmpresa longblob)
begin
	if p_opc=1 then
		insert into tb_adm_empresas (coEmp,razSocial,nomComercial,dirFiscal,telefono,paginaWeb,estado,st_registro,co_usua_crea,fe_usua_crea,co_usua_modi,fe_usua_modi,logoEmpresa) 
		values (p_coEmp,p_razSocial,p_nomComercial,p_dirFiscal,p_telefono,p_paginaWeb,'A',1,p_co_usua_crea,localtime(),null,null,p_logoEmpresa);
	elseif p_opc=2 then
		update tb_adm_empresas set 
        coEmp=p_coEmp,
        razSocial=p_razSocial,
        nomComercial=p_nomComercial,
        dirFiscal=p_dirFiscal,
        telefono=p_telefono,
        paginaWeb=p_paginaWeb,
        co_usua_modi=p_co_usua_crea,
        fe_usua_modi=localtime(),
        logoEmpresa=p_logoEmpresa
        where coEmp=p_coEmp;
	elseif p_opc=3 then
		delete from tb_adm_empresas where coEmp=p_coEmp;
    end if;
end //
delimiter ;

/* exec a los procedimientos */
call sp_tb_adm_empresas_ls(1, 'halcon');
call sp_tb_adm_empresas_gr(1,'10852963587','KinVaz S.A.C.','KV Store','Av. Huascar #10 Piso 1','985632564','kinvaz.com.pe','DCONDORI');

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
insert into tb_adm_usuarios (coUsu,nroDoc,noUsu,noClave,coPerfil,estado,st_registro,co_usua_crea,fe_usua_crea)
values ('DCONDORI','70783271','DAVIS CONDORI LLACZA','123','001','A',1,'DCONDORI',localtime());

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

/************************************************************************************************************************************/
/*--------  --------*/
create table tb_con_plan_contable_tipo_cuenta(
	coTipo int not null,
    deTipo varchar(50),
    naturaleza char(1),/*D=Débito // C=Crédigo*/
    primary key (coTipo)
);

insert into tb_con_plan_contable_tipo_cuenta (coTipo,deTipo,naturaleza) values 
(1,'Activo Circulante','D'),(2,'Activo No Circulante','D'),
(3,'Pasivo Circulante','C'),(4,'Pasivo No Circulante','C'),
(5,'Patrimonio','C'),(6,'Ingresos','C'),(7,'Gastos','D');

select * from tb_con_plan_contable_tipo_cuenta;

drop procedure if exists sp_tb_con_plan_contable_tipo_cuenta_ls;
delimiter //
create procedure sp_tb_con_plan_contable_tipo_cuenta_ls(
	in p_opcion int,
	in p_criterio varchar(50))
begin
	if p_opcion=1 then
		select coTipo,deTipo,naturaleza from tb_con_plan_contable_tipo_cuenta
        where concat_ws('', deTipo, naturaleza) like concat('%', p_criterio, '%');
	end if;
end; //
delimiter ;

drop procedure if exists sp_tb_con_plan_contable_tipo_cuenta_gr;
delimiter //
create procedure sp_tb_con_plan_contable_tipo_cuenta_gr(
	p_opc int,
	p_coTipo int,
    p_deTipo varchar(50),
    p_naturaleza char(1)/*D=Débito // C=Crédigo*/)
begin
	if p_opc=1 then
		insert into tb_con_plan_contable_tipo_cuenta (coTipo,deTipo,naturaleza) values (p_coTipo,p_deTipo,p_naturaleza);
	elseif p_opc=2 then
		update tb_con_plan_contable_tipo_cuenta set coTipo=p_coTipo,deTipo=p_deTipo,naturaleza=p_naturaleza where coTipo=p_coTipo;
	elseif p_opc=3 then
		delete from tb_con_plan_contable_tipo_cuenta where coTipo=p_coTipo;
    end if;
end //
delimiter ;
/************************************************************************************************************************************/
/************************************************************************************************************************************/
create table tb_con_plan_contable(
	coCuenta int NOT NULL,
    noCuenta varchar(50),
    deCuenta varchar(250),
    coTipo int,
    nivel int,
	primary key (coCuenta)
);

insert into tb_con_plan_contable (coCuenta,noCuenta,deCuenta,coTipo,nivel) values 
(1,'Activo Circulante','Bienes y derechos que se pueden convertir en efectivo en menos de un año',1,1),
(10,'Caja y Bancos','Dinero en efectivo y saldos bancarios',1,2),
(101,'Caja','Dinero en efectivo',1,3),
(102,'Banco - Cuenta Corriente','Fondos en cuentas bancarias',1,3);
/*(2,'Activo No Circulante','Bienes y derechos que no se convierten fácilmente en efectivo',coTipo,nivel),
(20,'Propiedad, Planta y Equipo','Inversiones en bienes tangibles de largo plazo',2,nivel),
(201,'Terrenos y Edificaciones','Terrenos y construcciones',2,nivel),
(3,'Pasivo Circulante','Obligaciones que se deben pagar en el corto plazo',coTipo,nivel),
(30,'Proveedores','Deudas a corto plazo con proveedores',coTipo,nivel),
(301,'Proveedores Nacionales','Deudas con proveedores locales',coTipo,nivel),
(4,'Pasivo No Circulante','Obligaciones que se deben pagar a largo plazo',coTipo,nivel),
(40,'Préstamos a Largo Plazo',' Plazo	Deudas con vencimiento a más de un año',coTipo,nivel),
(401,'Préstamos Bancarios','Préstamos bancarios a largo plazo',coTipo,nivel),
(5,'Patrimonio','Inversiones de los propietarios o accionistas',coTipo,nivel),
(50,'Capital Social','Aportes iniciales y adicionales de los accionistas',coTipo,nivel),
(51,'Utilidades Acumuladas','Ganancias retenidas de años anteriores',coTipo,nivel),
(6,'Ingresos','	Dinero recibido por ventas o servicios prestados',coTipo,nivel),
(60,'Ingresos por Ventas','Ingresos generados por la venta de productos',coTipo,nivel),
(7,'Gastos','Costos asociados a la operación de la empresa',coTipo,nivel),
(70,'Gastos de Administración','Costos relacionados con la gestión y administración de la empresa',coTipo,nivel),
(701,'Sueldos y Salarios','Pago a empleados por su trabajo',coTipo,nivel);*/

select * from tb_con_plan_contable;

drop procedure if exists sp_tb_con_plan_contable_ls;
delimiter //
create procedure sp_tb_con_plan_contable_ls(
	in p_opcion int,
	in p_criterio varchar(50))
begin
	if p_opcion=1 then
		select coCuenta,noCuenta,deCuenta,coTipo,nivel from tb_con_plan_contable where concat_ws('', coCuenta, noCuenta) like concat('%', p_criterio, '%');
	end if;
end; //
delimiter ;

drop procedure if exists sp_tb_con_plan_contable_gr;
delimiter //
create procedure sp_tb_con_plan_contable_gr(
	p_opc int,
	p_coCuenta int,
    p_noCuenta varchar(50),
    p_deCuenta varchar(250),
    p_coTipo int,
    p_nivel int)
begin
	if p_opc=1 then
		insert into tb_con_plan_contable (coCuenta,noCuenta,deCuenta,coTipo,nivel) values (p_coCuenta,p_noCuenta,p_deCuenta,p_coTipo,p_nivel);
	elseif p_opc=2 then
		update tb_con_plan_contable set coCuenta=p_coCuenta,noCuenta=p_noCuenta,deCuenta=p_deCuenta,coTipo=p_coTipo,nivel=p_nivel where coCuenta=p_coCuenta;
	elseif p_opc=3 then
		delete from tb_con_plan_contable where coCuenta=p_coCuenta;
    end if;
end //
delimiter ;

create table tb_con_plan_contable_sub_cuentas(
	coCuenta int,
	coSubCuenta int,
    noSubCuenta varchar(50)
);

insert into tb_con_plan_contable_sub_cuentas (coCuenta,coSubCuenta,noSubCuenta) values 
(101,'1011','Caja Chica'),
(101,'1012','Caja Grande'),
(102,'1021','Banco BBVA'),
(102,'1022','Banco Santander'),
(201,'2011','Terreno Oficina Principal');

select * from tb_con_plan_contable_sub_cuentas;

drop procedure if exists sp_tb_con_plan_contable_sub_cuentas_ls;
delimiter //
create procedure sp_tb_con_plan_contable_sub_cuentas_ls(
	in p_opcion int,
	in p_criterio varchar(50))
begin
	if p_opcion=1 then
		select coCuenta,coSubCuenta,noSubCuenta from tb_con_plan_contable_sub_cuentas where concat_ws('', coSubCuenta, noSubCuenta) like concat('%', p_criterio, '%');
	end if;
end; //
delimiter ;

drop procedure if exists sp_tb_con_plan_contable_sub_cuentas_gr;
delimiter //
create procedure sp_tb_con_plan_contable_sub_cuentas_gr(
	p_opc int,
	p_coCuenta int,
	p_coSubCuenta int,
    p_noSubCuenta varchar(50))
begin
	if p_opc=1 then
		insert into tb_con_plan_contable_sub_cuentas (coCuenta,coSubCuenta,noSubCuenta) values (p_coCuenta,p_coSubCuenta,p_noSubCuenta);
	elseif p_opc=2 then
		update tb_con_plan_contable_sub_cuentas set coCuenta=p_coCuenta,coSubCuenta=p_coSubCuenta,noSubCuenta=p_noSubCuenta where coSubCuenta=p_coSubCuenta;
	elseif p_opc=3 then
		delete from tb_con_plan_contable_sub_cuentas where coSubCuenta=p_coSubCuenta;
    end if;
end //
delimiter ;









































CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_tb_adm_empresas_ls`(
	in p_opcion int,
	in p_criterio varchar(50))
begin
	if p_opcion=1 then
		select coEmp,razSocial,nomComercial,dirFiscal,telefono,paginaWeb,
        estado,st_registro,co_usua_crea,fe_usua_crea,co_usua_modi,fe_usua_modi from tb_adm_empresas
        where estado='A' and st_registro=1 and concat_ws('', coEmp, razSocial) like concat('%', p_criterio, '%');
	end if;
end
go
…………………………….
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_tb_con_plan_contable_gr`(
	p_opc int,
	p_coCuenta int,
    p_noCuenta varchar(50),
    p_deCuenta varchar(250),
    p_coTipo int,
    p_nivel int)
begin
	if p_opc=1 then
		insert into tb_con_plan_contable (coCuenta,noCuenta,deCuenta,coTipo,nivel) values (p_coCuenta,p_noCuenta,p_deCuenta,p_coTipo,p_nivel);
	elseif p_opc=2 then
		update tb_con_plan_contable set coCuenta=p_coCuenta,noCuenta=p_noCuenta,deCuenta=p_deCuenta,coTipo=p_coTipo,nivel=p_nivel where coCuenta=p_coCuenta;
	elseif p_opc=3 then
		delete from tb_con_plan_contable where coCuenta=p_coCuenta;
    end if;
end
--------------
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_tb_con_plan_contable_ls`(
	in p_opcion int,
	in p_criterio varchar(50))
begin
	if p_opcion=1 then
		select coCuenta,noCuenta,deCuenta,coTipo,nivel from tb_con_plan_contable where concat_ws('', coCuenta, noCuenta) like concat('%', p_criterio, '%');
	end if;
end
----------------
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_tb_con_plan_contable_sub_cuentas_gr`(
	p_opc int,
	p_coCuenta int,
	p_coSubCuenta int,
    p_noSubCuenta varchar(50))
begin
	if p_opc=1 then
		insert into tb_con_plan_contable_sub_cuentas (coCuenta,coSubCuenta,noSubCuenta) values (p_coCuenta,p_coSubCuenta,p_noSubCuenta);
	elseif p_opc=2 then
		update tb_con_plan_contable_sub_cuentas set coCuenta=p_coCuenta,coSubCuenta=p_coSubCuenta,noSubCuenta=p_noSubCuenta where coSubCuenta=p_coSubCuenta;
	elseif p_opc=3 then
		delete from tb_con_plan_contable_sub_cuentas where coSubCuenta=p_coSubCuenta;
    end if;
end
------------------
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_tb_con_plan_contable_sub_cuentas_ls`(
	in p_opcion int,
	in p_criterio varchar(50))
begin
	if p_opcion=1 then
		select coCuenta,coSubCuenta,noSubCuenta from tb_con_plan_contable_sub_cuentas where concat_ws('', coSubCuenta, noSubCuenta) like concat('%', p_criterio, '%');
	end if;
end
----------------
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_tb_con_plan_contable_tipo_cuenta_gr`(
	p_opc int,
	p_coTipo int,
    p_deTipo varchar(50),
    p_naturaleza char(1)/*D=Débito // C=Crédigo*/)
begin
	if p_opc=1 then
		insert into tb_con_plan_contable_tipo_cuenta (coTipo,deTipo,naturaleza) values (p_coTipo,p_deTipo,p_naturaleza);
	elseif p_opc=2 then
		update tb_con_plan_contable_tipo_cuenta set coTipo=p_coTipo,deTipo=p_deTipo,naturaleza=p_naturaleza where coTipo=p_coTipo;
	elseif p_opc=3 then
		delete from tb_con_plan_contable_tipo_cuenta where coTipo=p_coTipo;
    end if;
end
--------------------
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_tb_con_plan_contable_tipo_cuenta_ls`(
	in p_opcion int,
	in p_criterio varchar(50))
begin
	if p_opcion=1 then
		select coTipo,deTipo,naturaleza from tb_con_plan_contable_tipo_cuenta
        where concat_ws('', deTipo, naturaleza) like concat('%', p_criterio, '%');
	end if;
end
-------------------
CREATE DEFINER=`root`@`localhost` PROCEDURE `tb_adm_usuarios_ls`(
	in opcion int,
	in criterio varchar(30))
begin
	if opcion=1 then
		select coUsu,nroDoc,noUsu,coPerfil,estado from tb_adm_usuarios
        where estado='A' and st_registro=1 and concat_ws(coUsu, nroDoc, noUsu) like concat('%', criterio, '%');
	end if;
end
