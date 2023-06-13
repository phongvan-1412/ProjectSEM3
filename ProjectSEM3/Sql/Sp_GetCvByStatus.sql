create proc Sp_GetCvByStatus
@Status int = -1
as
begin
	select * from dbo.GetCvs()
	where @Status = -1 or [Status] = @Status
end

