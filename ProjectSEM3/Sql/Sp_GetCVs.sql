USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetCVs]    Script Date: 6/14/2023 10:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc  [dbo].[Sp_GetCVs]
@Name nvarchar(max) = null,
@Email varchar(max) = null,
@Phone varchar(50) = null,
@DatePosted datetime = null,
@JobTitle nvarchar(max) = null,
@LevelId int = 0
As
Begin
     select * from GetCvs()
	 where (dbo.IsNullOrEmpty(@Name) = 1  or [Name] like @Name)
	 and (dbo.IsNullOrEmpty(@Email) = 1  or Email like @Email)
	 and (dbo.IsNullOrEmpty(@Phone) = 1  or Phone like @Phone)
	 and (dbo.IsNullOrEmpty(@DatePosted) = 1  or DatePosted = @DatePosted)
	 and (dbo.IsNullOrEmpty(@JobTitle) = 1  or JobTitle like @JobTitle)
	 and (@LevelId = 0 or LevelId = @LevelId)
	 order by Id desc
End