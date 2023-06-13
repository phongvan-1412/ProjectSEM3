create proc Sp_UpdateViewedCv
@id int
as
begin
	update CV
	set IsViewed = 1 
	where Id = @Id

	select top 1 * from GetCvs() where Id = @Id
end