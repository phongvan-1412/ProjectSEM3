USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_InsertCV]    Script Date: 6/13/2023 12:50:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_InsertCV]
@JobId int, 
@Name nvarchar(500), 
@Email varchar(4000), 
@Contact varchar(15), 
@Cv varchar(1000), 
@PostedDate datetime
As
Begin
	Declare @CvTmp TABLE(
		Id int
	)
    Insert into Contestant(JobId, [name], email, Contact, Cv, PostedDate,[status],IsViewed)
	OUTPUT  inserted.Id INTO @CvTmp
	values (@JobId, @Name, @Email, @Contact, @Cv, @PostedDate,1,0)

	declare @Id int = (select top 1 Id from @CvTmp)
	select top 1 * from dbo.GetContestants() where Id = @Id
End


