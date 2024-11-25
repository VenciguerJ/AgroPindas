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

INSERT INTO Funcionarios(cpf,senha,nome,telefone,email,id_cargo,data_nascimento)
VALUES ('47441293820','123','Jean','11953398867','jeanpedrosilva24@gmail.com',1,getdate());

DROP TABLE IF EXISTS Fornecedor;

CREATE TABLE Fornecedor(
	Id int identity not null primary key,
	CNPJ varchar(14) unique not null,
	RazaoSocial varchar(45) not null,
	Endereco varchar(250) null,
	Fone varchar(11) null,
	Email varchar(45) null
);

DROP TABLE IF EXISTS Compra;

CREATE TABLE Compra(
	Id int identity not null primary key,
	IdFornecedor int not null,
	ValorTotal money not null,
	DataCompra Date not null,
	constraint fk_fornecedor foreign key (IdFornecedor) references Fornecedor(Id)
);

DROP TABLE IF EXISTS UnidadeCadastro;

CREATE TABLE UnidadeCadastro(
	Id int identity not null primary key,
	Nome varchar(50) not null
);

INSERT INTO UnidadeCadastro(Nome)
VALUES ('Grama'),('Quilo'),('Unidade');

DROP TABLE IF EXISTS TipoProduto;

CREATE TABLE TipoProduto(
	Id int identity not null primary key,
	Nome varchar(50) not null
);

INSERT INTO TipoProduto(Nome)
VALUES ('Embalagem'),('Materia Prima'), ('Fertilizante');

DROP TABLE IF EXISTS Produto;

CREATE TABLE Produto(
	Id int identity not null primary key,
	Nome varchar(45) not null,
	Descricao varchar(90) null,
	TemperaturaPlantio int not null,
	DiasColheita int not null,
	UnidadeCadastro int not null,
	TipoProduto int not null,
	ValorProduto money not null,
	constraint fk_unidade_cadastro foreign key (UnidadeCadastro) references UnidadeCadastro(Id),
	constraint fk_tipo_produto foreign key (TipoProduto) references TipoProduto(Id)
);

DROP TABLE IF EXISTS Lote;

CREATE TABLE Lote(
	IdCompra int  not null, --fk da tabela de compra
	IdProduto int not null,
	QuantidadeLote int not null,
	QuantidadeSaida int not null,
	
	constraint fk_IdCompra foreign key (IdCompra) references Compra(Id),
	constraint fk_estoque_produto foreign key (IdProduto) references Produto(Id)
);

DROP TABLE IF EXISTS Suporte_Calhas;

CREATE TABLE Suporte_Calhas(
	Id int identity not null primary key,
	CapacidadeMudas int not null,
	ocupada bit not null
);

DROP TABLE IF EXISTS Suporte_Calhas_Fertilizantes;

CREATE TABLE Suporte_Calhas_Fertilizantes(
	IdSuporte int not null,
	IdProduto int not null,
	QtdUtilizada float not null

	constraint Id_suporteCalha foreign key (IdSuporte) references Suporte_Calhas(Id),
	constraint Id_ProdutoCalha foreign key (IdProduto) references Produto(Id)
);

DROP TABLE IF EXISTS Producao;

CREATE TABLE Producao(
	Id int identity not null primary key,
	IdLoteUsado int not null,
	IdProdutoProduzido int not null,
	QuantidadeProduzido int not null,
	IdCalha int not null,
	DiaColheita datetime not null,
	finalizada bit not null
	constraint fk_producao_calha foreign key (IdCalha) references Suporte_Calhas(Id)
);

DROP TABLE IF EXISTS Estoque_Produto;

CREATE TABLE Estoque_Produto(
	id int not null identity primary key,	
	id_estoque_produto int not null,
	id_producao int not null,
	quantidade_inicial int not null,
	quantidade_vendido int not null
);

DROP TABLE IF EXISTS Venda;

CREATE TABLE Venda(
	id_venda int identity not null primary key,
	Id_Lote int not null,
	quantidade int not null,
	valor_venda money not null
	constraint fk_venda_estoque foreign key (Id_Lote) references Estoque_Produto(id)
);


DROP TABLE IF EXISTS Clientes

CREATE TABLE Clientes(
	Id int not null IDENTITY,
	Nome varchar(50) not null,
	Email varchar(50) not null,
	CPF varchar(11)not null,
	Senha varchar (20) not null
);

select * from Funcionarios;

DROP TABLE IF EXISTS Pedidos

CREATE TABLE Pedidos(
	Id int not null IDENTITY primary key,
	CPF varchar(11)not null,
	Produtos varchar (500) not null,
	DataPedido datetime not null
);