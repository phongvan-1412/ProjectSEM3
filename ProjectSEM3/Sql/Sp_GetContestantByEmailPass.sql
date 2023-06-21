USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetContestantByEmailPass]    Script Date: 6/21/2023 1:40:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_GetContestantByEmailPass]
@Email varchar(500),
@Password varchar(500)
as
begin
	select * from Contestant 
	where Email like @Email and [Password] like @Password and [Status] = 2
end