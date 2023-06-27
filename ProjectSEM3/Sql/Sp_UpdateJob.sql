USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateJob]    Script Date: 6/16/2023 10:20:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_UpdateJob]
@Id int,
@Title nvarchar(500),
@Location nvarchar(500),
@Content ntext, 
@Qualification ntext, 
@EndDate datetime, 
@SalaryMin decimal, 
@SalaryMax decimal, 
@LevelId int
as
begin
	update Job
	set Title = @Title, [Location] = @Location,
		Content = @Content, Qualification = @Qualification,
		EndDate = @EndDate, SalaryMin = @SalaryMin,
		SalaryMax = @SalaryMax, LevelId = @LevelId
	where Id = @Id

	select top 1 * from GetJobs() where Id = @Id
end