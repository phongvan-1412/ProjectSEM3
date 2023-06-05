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
	A ntext default '',
	B ntext default '',
	C ntext default '',
	D ntext default '',
	CorrectAnwser varchar(10) default '',
	IsMultiAnwser bit default 1,
	Status bit default  1
)

select * from Question for json path
insert into Question(IdType,IdLevel,Content,Point,A,B,C,D,CorrectAnwser,IsMultiAnwser) values
(1,1,'Question content 1',4,'Question Anwser A1','Question Anwser B1','Question Anwser C1','Question Anwser D1','A',0),
(1,1,'Question content 2',3,'Question Anwser A2','Question Anwser B2','Question Anwser C2','Question Anwser D2','D',0)

create table ContestantQuestion(
	Id int identity(1,1) primary key,
	IdQuestion int references Question(Id),
	IdContestant int references Contestant(Id),
	Anwser varchar(10) default '',
	Status bit default 1
)

select * from Hr for json path