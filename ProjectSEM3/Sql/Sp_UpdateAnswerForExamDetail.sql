alter proc Sp_UpdateAnswerForExamDetail
@ExamId int,
@QuestionId int,
@Answer ntext

as
begin
	update ExamDetail set Answer = @Answer where ExamId = @ExamId and QuestionId = @QuestionId
	select top 1 * from ExamDetail where ExamId = @ExamId 
end