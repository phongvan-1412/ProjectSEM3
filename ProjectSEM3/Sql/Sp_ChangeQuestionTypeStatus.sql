create proc Sp_ChangeQuestionTypeStatus
@Id int,
@Status bit
as
begin
	Update QuestionType
	set [Status] = @Status
	where Id = @Id

	select * from QuestionType where Id = @Id
end