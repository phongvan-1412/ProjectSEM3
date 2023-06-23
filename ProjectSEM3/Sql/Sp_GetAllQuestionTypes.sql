USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetAllQuestionTypes]    Script Date: 6/24/2023 1:34:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_GetAllQuestionTypes]
@Status int = -1
as
begin
select *from QuestionType
where @Status = -1
	or [Status] = @Status
	order by Id desc
end