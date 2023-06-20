alter proc Sp_GetExamDetailById
@ExamId int
as
begin
	select * from GetExamDetails() 
	where ExamId = @ExamId
end

