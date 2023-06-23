USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetAllTypes]    Script Date: 6/24/2023 1:34:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_GetAllTypes]
@Status int = -1
as
begin
select * from [Type]
where @Status = -1
	or [Status] = @Status
order by Id desc
end