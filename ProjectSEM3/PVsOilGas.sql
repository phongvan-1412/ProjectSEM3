create table Type(
	Id int identity(1,1) primary key,
	Name nvarchar(500) default '',
	Status bit default 1
	)
insert into Type(name) values ('Type Test 1')

create table Level(
	Id int identity(1,1) primary key,
	Name nvarchar(500) default '',
	Status bit default 1
)
insert into Level(name) values ('Level Test 1')
create table Contestant(
	Id int identity(1,1) primary key,
	Name nvarchar(500) default '',
	Email varchar(500) default '',
	Password varchar(1000) default '',
	IpDevice varchar(1000) default '',
	Cv varchar(100) default '',
	Contact varchar(15) default '',
	Address nvarchar(500) default '',
	StartTime datetime default getdate(),
	LateTime datetime default getdate(),
	EndTime datetime default getdate(),
	CreatedDate datetime default getdate(),
	Status bit default 1
)
insert into Contestant(Name,Email,Password) values('Van','van@gmail.com','111111')
create table Hr(
	Id int identity(1,1) primary key,
	Name nvarchar(500) default '',
	Email varchar(500) default '',
	Password varchar(1000) default '',
	Contact varchar(500) default '',
	Address nvarchar(500) default '',
	Education nvarchar(500) default '',
	Experience nvarchar(2000) default '',
	Status bit default 1
)

insert into Hr(Name,Email,Password) values('Phuong','phuong@gmail.com','111111')

 create table Question(
	Id int identity(1,1) primary key,
	IdType int references Type(Id),
	IdLevel int references Level(Id),
	Content ntext default '',
	Point int default 1,
	Options ntext default '',
	CorrectAnwser ntext default '',
	IsMultiAnwser bit default 1,
	Status bit default  1
)
select * from Type 
select * from level 

select * from Question for json path
insert into Question(IdType,IdLevel,Content,Point,A,B,C,D,CorrectAnwser,IsMultiAnwser) values
(1,1,'Question content 1',4,'[{'A' : 'ádasdasdasdasd'},{'B' : 'ádasdasdasdasd'},{'C' : 'ádasdasdasdasd'},{'D' : 'ádasdasdasdasd'}]','A',0),
(1,1,'Question content 2',3,'[{'A' : 'ádasdasdasdasd'},{'B' : 'ádasdasdasdasd'},{'C' : 'ádasdasdasdasd'},{'D' : 'ádasdasdasdasd'}]','D',0)

create table ContestantQuestion(
	Id int identity(1,1) primary key,
	IdQuestion int references Question(Id),
	IdContestant int references Contestant(Id),
	Anwser varchar(10) default '',
	Status bit default 1
)

select * from ContestantQuestion
update hr
set status = 1