USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateQuestion]    Script Date: 6/23/2023 9:04:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_UpdateQuestion]
@Id int,
@IdType int,
@IdLevel int,
@Content ntext,
@Point int,
@Options ntext,
@CorrectAnwser ntext,
@IsMultiAnwser bit
as
begin
	update Question
	set TypeId = @IdType,LevelId = @IdLevel,
		Content = @Content,Point = @Point,
		Options = @Options,
		CorrectAnwser = @CorrectAnwser,
		IsMultiAnwser = @IsMultiAnwser
	where Id = @Id

	select * from GetQuestion()
	where Id = @Id
end