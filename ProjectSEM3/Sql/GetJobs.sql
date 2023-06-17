create function GetJobs() returns table 
as
	return 
		(select j.Id,j.Title,j.[Location],j.Content,j.[Status],j.PostedDate,
			j.EndDate,j.SalaryMin,j.SalaryMax,l.Id as LevelId, l.[Name] as LevelName
		from Job j,[Level] l
		where j.LevelId = l.Id
		)

