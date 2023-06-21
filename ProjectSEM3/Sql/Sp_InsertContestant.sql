USE [PVsOilGas]
GO
/****** Object:  StoredProcedure [dbo].[Sp_InsertContestant]    Script Date: 6/20/2023 10:45:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_InsertContestant]
@Id int,
@Password varchar(1000),
@Name nvarchar(500),
@Email varchar(1000),
@Contact varchar(100),
@Address varchar(1000),
@Status int
as
begin
	Update Contestant
	set [Password] = @Password,[Status] = @Status, IsViewed = 1,
		[Name] = @Name, Email = @Email, Contact = @Contact,
		@Address = @Address
	where Id = @Id

	select * from GetCvs() where Id = @Id
end