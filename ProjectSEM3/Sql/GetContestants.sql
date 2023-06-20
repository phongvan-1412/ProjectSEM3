alter function GetContestants() returns table
as
	return
		(
			select c.Id,c.[Name],c.[Password],c.Email,c.Cv,C.Contact,c.[Address],c.[Status],c.PostedDate,c.IsViewed,c.JobId,
					j.Title as JobTitle,j.LevelId,l.[Name] as LevelName,e.Id as ExamId
			from Contestant c,Job j,[Level] l, Exam e
			where c.JobId = j.Id and j.LevelId = l.Id and c.Id = e.ContestId
		)

		