USE master;

DROP DATABASE IF EXISTS PINDUCAS_farm;

CREATE DATABASE PINDUCAS_farm;

USE PINDUCAS_farm;

DROP TABLE IF EXISTS Cargos;

CREATE TABLE Cargos(
	id_cargo int identity not null primary key,
	cargo varchar(45) not null,
	permissoes int not null
);

INSERT INTO Cargos(cargo,permissoes)
VALUES ('MASTER',1),('GERENTE',2),('OPERADOR',3);


DROP TABLE IF EXISTS Funcionarios;

CREATE TABLE Funcionarios(
	id_usuario int identity not null primary key,
	cpf varchar(11) unique not null,
	senha varchar(20) not null,
	nome varchar(45) not null,
	telefone varchar(11) not null,
	email varchar(45) not null,
	id_cargo int not null,
	superior int null,
	data_nascimento datetime not null,
	constraint fk_funcionarios_cargo foreign key (id_cargo) references Cargos(id_cargo)
);

INSERT INTO Funcionarios(cpf,senha,nome,telefone,email,id_cargo,superior,data_nascimento)
VALUES ('99999999999','master','master','99999999999','master',1,0,getdate());

DROP TABLE IF EXISTS Produto;

CREATE TABLE Produto(
	id_produto int identity not null primary key,
	nome_produto varchar(45) not null,
	descricao varchar(90) not null,
	temperatura_plantio int not null,
	dias_para_colheita int not null
);

DROP TABLE IF EXISTS Estoque;

CREATE TABLE Estoque(
	id_estoque int identity not null primary key,
	id_produto int not null,
	quantidade_entrada int null,
	quantidade_saida int not null,
	constraint fk_estoque_produto foreign key (id_produto) references Produto(id_produto)
);

DROP TABLE IF EXISTS Fornecedor;

CREATE TABLE Fornecedor(
	id_fornecedor int identity not null primary key,
	cnpj varchar(14) unique not null,
	nome varchar(45) not null,
	ddd varchar(2) not null,
	fone varchar(9) not null,
	email varchar(45) not null
);

DROP TABLE IF EXISTS Compra;

CREATE TABLE Compra(
	id_compra int identity not null primary key,
	id_produto int not null,
	id_fornecedor int not null,
	id_estoque int not null,
	quantidade_compra int null,
	valor_unitario money not null,
	constraint fk_compra_produto foreign key (id_produto) references Produto(id_produto),
	constraint fk_compra_fornecedor foreign key (id_fornecedor) references Fornecedor(id_fornecedor),
	constraint fk_compra_estoque foreign key (id_estoque) references Estoque(id_estoque)
);

DROP TABLE IF EXISTS Suporte_Calhas;

CREATE TABLE Suporte_Calhas(
	id_suporte int identity not null primary key,
	capacidade_calhas int not null,
	andares int not null,
	ocupada bit not null
);

DROP TABLE IF EXISTS Calhas;

CREATE TABLE Calhas(
	id_calha int identity not null primary key,
	quantidade_suportes int not null,
	ocupada bit not null,
	id_suporte int not null
	constraint fk_calhas_suporte foreign key (id_suporte) references Suporte_Calhas(id_suporte)
);

DROP TABLE IF EXISTS Producao;

CREATE TABLE Producao(
	id_producao int identity not null primary key,
	id_estoque int not null,
	quantidade_inicial int not null,
	quantidade_produzido int not null,
	id_calha int not null,
	constraint fk_producao_estoque foreign key (id_estoque) references Estoque(id_estoque),
	constraint fk_producao_calha foreign key (id_calha) references Calhas(id_calha)
);


DROP TABLE IF EXISTS Estoque_Produto;

CREATE TABLE Estoque_Produto(
	id_estoque_produto int identity not null primary key,
	id_producao int not null,
	quantidade_inicial int not null,
	quantidade_vendido int not null
	constraint fk_estoquep_producao foreign key (id_producao) references Producao(id_producao)
);


DROP TABLE IF EXISTS Venda;

CREATE TABLE Venda(
	id_venda int identity not null primary key,
	id_estoque_produto int not null,
	quantidade int not null,
	valor_venda money not null
	constraint fk_venda_estoque foreign key (id_estoque_produto) references Estoque_Produto(id_estoque_produto)
);


select * from Funcionarios;