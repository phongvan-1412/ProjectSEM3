alter proc Sp_GetExamDetailById
@ExamId int
as
begin
	select * from GetExamDetails() 
	where ExamId = @ExamId and Status=1
end

