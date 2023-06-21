alter proc Sp_UpdateType
@Id int,
@Name nvarchar(500)
as 
begin
	Update [Type]
	set [Name] = @Name
	where Id = @Id

	select * from [Type] where Id = @Id
end