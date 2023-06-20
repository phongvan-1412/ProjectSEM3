USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetCvByStatus]    Script Date: 6/20/2023 9:12:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_GetCvByStatus]
@Status int = -1,
@IsViewed int = -1
as
begin
	select * from dbo.GetCvs()
	where 
		(@Status = -1 or [Status] = @Status)
	and (@IsViewed = -1 or IsViewed = @IsViewed)
end
