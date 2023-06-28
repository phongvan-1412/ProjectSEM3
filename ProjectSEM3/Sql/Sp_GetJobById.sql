USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetQuestionById]    Script Date: 6/14/2023 10:11:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter proc [dbo].[Sp_GetJobById]
@Id int
as
begin
	select * from GetJobs() where Id = @Id and [Status] = 1
end