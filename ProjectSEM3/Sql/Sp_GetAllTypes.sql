USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetAllTypes]    Script Date: 6/13/2023 6:23:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_GetAllTypes]
@Status int = -1
as
begin
select * from Type
where @Status = -1
	or [Status] = @Status
end