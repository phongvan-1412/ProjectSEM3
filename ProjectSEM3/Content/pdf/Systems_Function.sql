use ProjectManagement

alter function IsNullOrEmpty(@text varchar(max)) returns bit
as
begin
	declare @check bit = 0
	if(@text is null or @text like '')
		begin
			set @check = 1
		end
	return @check
end

create function StringContains(@text varchar(max),@value varchar(max)) returns bit
as
begin
	declare @check bit = 0
	if(@text like '%' + @value + '%')
		begin
			set @check = 1
		end
	return @check
end

create function StringEqual(@text varchar(max),@value varchar(max)) returns bit
as
begin
	declare @check bit = 0
	if(@text like @value)
		begin
			set @check = 1
		end
	return @check
end