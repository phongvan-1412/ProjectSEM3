USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_InsertQuestion]    Script Date: 6/11/2023 9:00:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_InsertQuestion]
@IdType int,
@IdLevel int,
@IdExamType int,
@Content ntext,
@Point int,
@Options ntext,
@CorrectAnwser ntext,
@IsMultiAnwser bit
as
begin
	
	insert into Question(TypeId,LevelId,ExamTypeId,Content,Point,
						Options,CorrectAnwser,IsMultiAnwser,[Status])
	values (@IdType,@IdLevel,@IdExamType,@Content,@Point,
			@Options,@CorrectAnwser,@IsMultiAnwser,1)

	select * from GetQuestion()
	where TypeId = @IdType and LevelId = @IdLevel and Content like @Content
		and Point = @Point and Options like @Options and CorrectAnwser like @CorrectAnwser 
		and IsMultiAnwser = @IsMultiAnwser and [Status] = 1
end