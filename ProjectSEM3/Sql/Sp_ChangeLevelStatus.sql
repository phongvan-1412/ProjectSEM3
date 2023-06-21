create proc Sp_ChangeLevelStatus
@Id int,
@Status bit
as
begin
	Update [Level]
	set [Status] = @Status
	where Id = @Id

	select * from [Level] where Id = @Id
end