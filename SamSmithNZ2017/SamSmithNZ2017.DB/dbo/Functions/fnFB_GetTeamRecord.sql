CREATE FUNCTION [dbo].[fnFB_GetTeamRecord](@team_code smallint, @year_code smallint, @week_code smallint)
	RETURNS varchar(10)
AS
BEGIN
	DECLARE @home_win smallint, @away_win smallint
	DECLARE @home_loss smallint, @away_loss smallint

	SELECT @home_win = count(*)
	FROM FBWeekTemplate
	WHERE year_code = @year_code
	and week_code <= @week_code
	and home_team_code = @team_code
	and home_team_result > away_team_result

	SELECT @home_loss = count(*)
	FROM FBWeekTemplate
	WHERE year_code = @year_code
	and week_code <= @week_code
	and home_team_code = @team_code
	and home_team_result < away_team_result

	SELECT @away_win = count(*)
	FROM FBWeekTemplate
	WHERE year_code = @year_code
	and week_code <= @week_code
	and away_team_code = @team_code
	and home_team_result < away_team_result

	SELECT @away_loss = count(*)
	FROM FBWeekTemplate
	WHERE year_code = @year_code
	and week_code <= @week_code
	and away_team_code = @team_code
	and home_team_result > away_team_result
	--SELECT * from FBTeam

	RETURN ' (' + CONVERT(varchar(2),@home_win+@away_win) + '-' + CONVERT(varchar(2),@home_loss+@away_loss) + ')'
END