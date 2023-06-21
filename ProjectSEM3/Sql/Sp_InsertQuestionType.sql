create proc Sp_InsertQuestionType
@Name nvarchar(500)
as
begin
	Declare @QuestionTypeTmp TABLE(
		Id int
	)
	insert into QuestionType([Name],[Status])
	OUTPUT  inserted.Id INTO @QuestionTypeTmp
	values (@Name,1)

	declare @Id int = (select top 1 Id from @QuestionTypeTmp)
	select top 1 * from QuestionType where Id = @Id
end