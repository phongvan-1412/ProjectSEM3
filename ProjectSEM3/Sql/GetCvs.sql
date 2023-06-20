USE [PVsOilGas]
GO
/****** Object:  UserDefinedFunction [dbo].[GetCvs]    Script Date: 6/13/2023 7:46:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER function [dbo].[GetCvs]() returns table
as 
	return
	(
		select c.Id,c.[Name],c.Email,c.Contact,c.Cv,c.[Status],c.PostedDate,c.IsViewed,
			   c.JobId,j.Title as JobTitle,l.Id as LevelId,l.[Name] as LevelName
		from Contestant c,Job j,[Level] l
		where c.JobId = j.Id and j.LevelId = l.Id
	)