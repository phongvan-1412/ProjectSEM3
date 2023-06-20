alter proc  [dbo].[Sp_GetCVs]
@Name nvarchar(max) = null,
@Email varchar(max) = null,
@Contact varchar(50) = null,
@PostedDate datetime = null,
@JobTitle nvarchar(max) = null,
@LevelId int = 0
As
Begin
     select * from GetContestants()
	 where (dbo.IsNullOrEmpty(@Name) = 1  or [Name] like @Name)
	 and (dbo.IsNullOrEmpty(@Email) = 1  or Email like @Email)
	 and (dbo.IsNullOrEmpty(@Contact) = 1  or Contact like @Contact)
	 and (dbo.IsNullOrEmpty(@PostedDate) = 1  or PostedDate = @PostedDate)
	 and (dbo.IsNullOrEmpty(@JobTitle) = 1  or JobTitle like @JobTitle)
	 and (@LevelId = 0 or LevelId = @LevelId)
	 order by Id desc
End