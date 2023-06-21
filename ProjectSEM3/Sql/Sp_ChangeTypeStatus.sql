create proc Sp_ChangeTypeStatus
@Id int,
@Status bit
as
begin
	Update [Type]
	set [Status] = @Status
	where Id = @Id

	select * from [Type] where Id = @Id
end