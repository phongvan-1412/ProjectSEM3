create proc Sp_UpdateLevel
@Id int,
@Name nvarchar(500)
as 
begin
	Update [Level]
	set [Name] = @Name
	where Id = @Id

	select * from [Level] where Id = @Id
end