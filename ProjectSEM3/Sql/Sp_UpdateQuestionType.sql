create proc Sp_UpdateQuestionType
@Id int,
@Name nvarchar(500)
as 
begin
	Update QuestionType
	set [Name] = @Name
	where Id = @Id

	select * from QuestionType where Id = @Id
end