CREATE PROCEDURE [dbo].[spFB_GetWeekTemplate]
	@year_code smallint,
	@week_code smallint
AS

DECLARE @week_count smallint

SELECT @week_count = count(*)
FROM FBWeekTemplate wt
WHERE year_code = @year_code and week_code = @week_code 

IF (@week_count = 0)
BEGIN
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	IF (@week_code <= 2 or @week_code >= 11)
	BEGIN
		INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
		INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,getdate(),0,-1,-1, -1)
	END
END

SELECT record_id, wt.home_team_code, t1.team_name as home_team_name, wt.away_team_code, t2.team_name as away_team_name, 
wt.fav_team_code, wt.game_time, wt.spread, wt.home_team_result, wt.away_team_result, wt.fav_team_won_game
FROM FBWeekTemplate wt
INNER JOIN FBTeam t1 ON wt.home_team_code = t1.team_code
INNER JOIN FBTeam t2 ON wt.away_team_code = t2.team_code
WHERE year_code = @year_code and week_code = @week_code
ORDER BY game_time