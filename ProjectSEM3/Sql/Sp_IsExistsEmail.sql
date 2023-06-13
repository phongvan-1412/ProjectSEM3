USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_IsEmailIsExsists]    Script Date: 6/11/2023 9:17:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_IsEmailIsExsists]
@Email varchar(500)
as
begin
	declare @IsExists bit = 1
	if
		(
			not exists (select Email from Hr where Email like @Email and [Status] = 1)
			and not exists (select Email from Contestant where Email like @Email and [Status] = 1)
		)
		begin
			set @IsExists = 0
		end
	select @IsExists as IsExists
end