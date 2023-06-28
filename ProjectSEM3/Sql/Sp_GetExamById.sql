alter proc Sp_GetExamById
@ContestId int
as
begin
	select * from Exam where ContestId = @ContestId 
end