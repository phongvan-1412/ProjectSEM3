create proc Sp_UpdatePointForExam
@ExamId int,
@KnowledgePoint int,
@MathPoint int,
@ComputerPoint int

as
begin
	update Exam set KnowledgePoint = @KnowledgePoint, MathPoint = @MathPoint, ComputerPoint = @ComputerPoint where Id = @ExamId
    select e.Id, e.ContestId, e.KnowledgePoint, e.MathPoint, e.ComputerPoint, c.Email from exam e join contestant c on e.ContestId = c.Id

end