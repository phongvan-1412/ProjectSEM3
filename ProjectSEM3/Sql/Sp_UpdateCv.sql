alter proc Sp_UpdateCv
@Id int,
@Status int
as 
begin
	Update CV
	set [Status] = @Status
	where Id = @Id

	select * from GetCvs() where Id = @Id
end