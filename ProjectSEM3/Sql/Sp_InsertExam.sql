USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_InsertExam]    Script Date: 6/11/2023 11:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_InsertExam]
@ContestantId int,
@StartTime datetime,
@EndTime datetime,
@LateTime datetime
as
begin
	Declare @ExamTmp TABLE(
		Id int
	)
	insert into Exam(ContestId,StartTime,EndTime,LateTime,[Status])
	OUTPUT  inserted.Id INTO @ExamTmp
	values (@ContestantId,@StartTime,@EndTime,@LateTime,1)

	declare @Id int = (select top 1 Id from @ExamTmp)
	select top 1 * from Exam where Id = @Id
end