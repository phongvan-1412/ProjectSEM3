USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateViewedCv]    Script Date: 6/20/2023 9:18:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_UpdateViewedCv]
@id int
as
begin
	update Contestant
	set IsViewed = 1 
	where Id = @Id

	select top 1 * from GetCvs() where Id = @Id
end