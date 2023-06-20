alter proc Sp_GetCvByStatus
@Status int = -1,
@IsViewed int = -1
as
begin
	select * from dbo.GetContestants()
	where 
		(@Status = -1 or [Status] = @Status)
	and (@IsViewed = -1 or IsViewed = @IsViewed)
end

