create proc Sp_ChangeJobStatus
@Id int,
@Status int 
as
begin
	update Job
	set [Status] = @Status
	where Id = @Id

	select * from dbo.GetJobs()
	where Id = @Id
end

