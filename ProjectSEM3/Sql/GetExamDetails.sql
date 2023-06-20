alter function GetExamDetails() returns table
as
	return
		(
			select ed.Id,ed.ExamId,ed.Answer,ed.[Status],
					q.Content,q.Point,q.Options,q.CorrectAnwser,
					q.IsMultiAnwser,q.TypeId,qt.[Name] as TypeName,
					q.LevelId,l.[Name] as LevelName,
					q.ExamTypeId,t.[Name] as ExamTypeName
			from ExamDetail ed,Question q,QuestionType qt,
				[Level] l,[Type] t
			where 
			ed.QuestionId = q.Id and q.TypeId = qt.Id and
			q.LevelId = l.id and q.ExamTypeId = t.Id
		)
