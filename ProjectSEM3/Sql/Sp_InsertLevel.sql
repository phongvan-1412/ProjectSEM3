create proc Sp_InsertLevel
@Name nvarchar(500)
as
begin
	Declare @LevelTmp TABLE(
		Id int
	)
	insert into [Level]([Name],[Status])
	OUTPUT  inserted.Id INTO @LevelTmp
	values (@Name,1)

	declare @Id int = (select top 1 Id from @LevelTmp)
	select top 1 * from [Level] where Id = @Id
end