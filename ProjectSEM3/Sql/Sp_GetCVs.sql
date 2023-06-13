select * from examDetail
select * from cv
alter table cv
add IsViewed bit default 0
update  CV 
set IsViewed = 0
select * from dbo.GetCvs()