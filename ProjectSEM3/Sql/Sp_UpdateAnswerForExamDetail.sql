create proc Sp_UpdatePointForExam
@ContestId int,
@KnowledgePoint int,
@MathPoint int,
@ComputerPoint int

as
begin
	update Exam set KnowledgePoint = @KnowledgePoint, MathPoint=@MathPoint, ComputerPoint=@ComputerPoint where ContestId= @ContestId
end