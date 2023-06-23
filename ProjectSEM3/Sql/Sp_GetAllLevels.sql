USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetAllLevels]    Script Date: 6/24/2023 1:35:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_GetAllLevels]
@Status int = -1
as
begin
	select * from [Level]
	where @Status = -1
	or [Status] = @Status
	order by Id desc
end