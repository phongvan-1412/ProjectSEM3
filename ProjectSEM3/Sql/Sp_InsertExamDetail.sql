USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_InsertExamDetail]    Script Date: 6/11/2023 11:58:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_InsertExamDetail]
@QuestionId int,
@ExamId int
as
begin
	Declare @ExamDetailTmp TABLE(
		Id int
	)
	if(dbo.IsExistsExamDetail(@QuestionId,@ExamId) = 0)
		begin
			insert into ExamDetail (QuestionId,ExamId,Answer,[Status])
			OUTPUT inserted.Id INTO @ExamDetailTmp
			values (@QuestionId,@ExamId,'',1)
		end
	declare @Id int = (select top 1 Id from @ExamDetailTmp)
	select * from ExamDetail where Id = @Id
end