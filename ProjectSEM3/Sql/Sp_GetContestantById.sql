create proc Sp_GetContestantById
@Id int
as
begin
	select * from dbo.GetContestants() where Id = @Id
end