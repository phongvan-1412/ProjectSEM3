USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_ChangeCvStatus]    Script Date: 6/22/2023 11:03:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_ChangeCvStatus]
@id int, @status bit
As
Begin
     update Contestant set [status] = @status where id = @id
     select * from GetContestants() where id = @id
End