USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateHr]    Script Date: 6/14/2023 10:12:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_UpdateHr]
@Id int,
@Name nvarchar(500),
@Email varchar(500),
@Contact varchar(500) = '', 
@Address nvarchar(500) = '', 
@Education nvarchar(500) = '', 
@Experience nvarchar(2000) = ''
as
begin
	update Hr
	set [Name] = @Name, Email = @Email,
		Contact = @Contact, [Address] = @Address,
		Education = @Education, Experience = @Experience
	where Id = @Id
	select * from Hr where Id = @Id
end