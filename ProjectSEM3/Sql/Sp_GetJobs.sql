create proc Sp_GetJobs
@Title nvarchar(500) = null,
@Location nvarchar(500) = null,
@Content ntext = null,
@PostedDate datetime = null,
@EndDate datetime = null,
@SalaryMin decimal = 0,
@SalaryMax decimal = 0,
@LevelId int = 0
as
begin
	select * from dbo.GetJobs()
	where (dbo.IsNullOrEmpty(@Title) = 1  or Title like @Title)
	 and (dbo.IsNullOrEmpty(@Location) = 1  or [Location] like @Location)
	 and (dbo.IsNullOrEmpty(@Content) = 1  or Content like @Content)
	 and (@PostedDate is null  or PostedDate = @PostedDate)
	 and (@EndDate is null  or EndDate = @EndDate)
	 and (@SalaryMin = 0 or SalaryMin = @SalaryMin)
	 and (@SalaryMax = 0 or SalaryMax = @SalaryMax)
	 and (@LevelId = 0 or LevelId = @LevelId)
	 order by Id desc
end