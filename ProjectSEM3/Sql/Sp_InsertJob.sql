USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_InsertCV]    Script Date: 6/14/2023 10:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter proc [dbo].[Sp_InsertJob]
@Title nvarchar(500),
@Location nvarchar(500),
@Content ntext, 
@Qualification ntext, 
@PostedDate datetime, 
@EndDate datetime, 
@SalaryMin decimal, 
@SalaryMax decimal, 
@LevelId int
As
Begin
	Declare @JobTmp TABLE(
		Id int
	)
    Insert into Job(Title, [Location], Content, PostedDate, EndDate,SalaryMin, SalaryMax,[status],LevelId,Qualification)
	OUTPUT  inserted.Id INTO @JobTmp
	values (@Title, @Location, @Content, @PostedDate, @EndDate,@SalaryMin, @SalaryMax,1,@LevelId,@Qualification)

	declare @Id int = (select top 1 Id from @JobTmp)
	select top 1 * from GetJobs() where Id = @Id
End
