alter proc Sp_GetExamById
@ExamId int,
@ContestId int
as
begin
	select * from Exam where ContestId = @ContestId and Id = @ExamId
end