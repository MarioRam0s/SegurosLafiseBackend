CREATE database SegurosLafiseBD
go

use SegurosLafiseBD
go

CREATE TABLE CoverageCategory(
	id int identity(1,1) primary key,
	nameCategory varchar(65) NOT NULL,

	active bit default(1) not null,
	createdAt datetime not null default  sysdatetime(),
	updateAt datetime  null,
	deleteAt datetime  null
)
go

CREATE TABLE Client(
	id int identity (1,1) primary key,
	nameClient nvarchar(100) not null,
	identification nvarchar(50) not null unique,
	email nvarchar(50) not null,

	active bit default(1) not null,
	createdAt datetime not null default  sysdatetime(),
	updateAt datetime  null,
	deleteAt datetime  null
)
go

CREATE TABLE Vehicle(
	id int identity(1,1) primary key,
	licensePlate nvarchar(25) not null unique,
	brand nvarchar(50) not null,
	model nvarchar(50) not null,
	manufacturingYear Int not null,
	commercialValue  decimal not null,

	active bit default(1) not null,
	createdAt datetime not null default  sysdatetime(),
	updateAt datetime  null,
	deleteAt datetime  null
)
go

CREATE TABLE Coverage(
	id int identity (1,1) primary key,
	idCoverageCategory int not null,
	rate decimal(5,2) not null,

	active bit default(1) not null,
	createdAt datetime not null default  sysdatetime(),
	updateAt datetime  null,
	deleteAt datetime  null

	constraint FK_Coverage_CoverageCategory 
	foreign key(idCoverageCategory) references CoverageCategory(id)
)
go



CREATE TABLE InsurancePolicy(
	id int identity(1,1) primary key,
	insurancePolicy nvarchar(50) not null unique,
	idClient int not null,
	idVehicle int not null,
	issueDate date not null,
	coverageAmount decimal(18,2) not null,
	totalPremium decimal(5,2) not null,

	active bit default(1) not null,
	createdAt datetime not null default  sysdatetime(),
	updateAt datetime  null,
	deleteAt datetime  null

	constraint FK_InsurancePolicy_Client foreign key (idClient) references Client(id),
	constraint FK_InsurancePolicy_Vehicle foreign key(idVehicle) references Vehicle(id)
)
go

CREATE TABLE InsurancePolicyCoverage(
	id int identity (1,1) primary key,
	idPolicy int not null,
	idCoverage  int not null,
	appliedRate decimal(5,2) not null,
	appliedCoverageAmount decimal(18,2) not null,

	active bit default(1) not null,
	createdAt datetime not null default  sysdatetime(),
	updateAt datetime  null,
	deleteAt datetime  null

	constraint FK_InsurancePolicyCoverage_InsurancePolicy
	foreign key (idPolicy) references InsurancePolicy(id),
	constraint FK_InsurancePolicyCoverage_Coverage 
	foreign key(idCoverage) references Coverage(id),

	constraint UQ_InsurancePolicy_Coverage unique (idPolicy,idCoverage)
)