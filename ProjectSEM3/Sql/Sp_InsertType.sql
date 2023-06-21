alter proc Sp_InsertType
@Name nvarchar(500)
as
begin
	Declare @TypeTmp TABLE(
		Id int
	)
	insert into [Type]([Name],[Status])
	OUTPUT  inserted.Id INTO @TypeTmp
	values (@Name,1)

	declare @Id int = (select top 1 Id from @TypeTmp)
	select top 1 * from [Type] where Id = @Id
end