create table Type(
	Id int identity(1,1) primary key,
	Name nvarchar(500) default '',
	Status bit default 1
	)

create table Level(
	Id int identity(1,1) primary key,
	Name nvarchar(500) default '',
	Status bit default 1
)
create table Contestant(
	Id int identity(1,1) primary key,
	Name nvarchar(500) default '',
	Email varchar(500) default '',
	Password varchar(1000) default '',
	Cv varchar(100) default '',
	Contact varchar(15) default '',
	StartTime datetime default getdate(),
	LateTime datetime default getdate(),
	EndTime datetime default getdate(),
	Status bit default 1
)

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

create table ContestantQuestion(
	Id int identity(1,1) primary key,
	IdQuestion int references Question(Id),
	IdContestant int references Contestant(Id),
	Anwser varchar(10) default '',
	Status bit default 1
)