USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_InsertCV]    Script Date: 6/13/2023 12:50:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_InsertCV]
@job int, @name nvarchar(500), @email varchar(4000), @phone varchar(15), @filepath varchar(1000), @date_posted varchar(255)
As
Begin
	Declare @CvTmp TABLE(
		Id int
	)
    Insert into Cv(jobId, name, email, phone, filepath, dateposted,[status])
	OUTPUT  inserted.Id INTO @CvTmp
	values (@job, @name, @email, @phone, @filepath, @date_posted,1)

	declare @Id int = (select top 1 Id from @CvTmp)
	select top 1 * from Cv where Id = @Id
End