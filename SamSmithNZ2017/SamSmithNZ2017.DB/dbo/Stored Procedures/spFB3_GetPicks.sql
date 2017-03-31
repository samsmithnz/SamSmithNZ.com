CREATE PROCEDURE [dbo].[spFB3_GetPicks]
	@year_code smallint,
	@week_code smallint,
	@player_code smallint,
	@show_team_records bit
AS

DECLARE @e_count smallint
DECLARE @pick_count smallint

--Get the number of picks
SELECT @pick_count = count(*) 
FROM FBWeek
WHERE year_code = @year_code and week_code = @week_code and player_code = @player_code

IF (@pick_count = 0)
BEGIN
	--Create the weeks records
	INSERT INTO FBWeek
	SELECT @year_code , @player_code, @week_code, record_id, -1, 0, null --where -1 is nothing selected
	FROM FBWeekTemplate
	WHERE year_code = @year_code and week_code = @week_code
	ORDER BY game_time
END

DECLARE @tmp_counts TABLE (record_id uniqueidentifier, fav_count decimal(10,4))

INSERT INTO @tmp_counts 
SELECT w.record_id, sum(w.fav_team_picked)
FROM FBWeek w
WHERE w.year_code = @year_code and w.week_code = @week_code and w.fav_team_picked >= 0 
GROUP BY w.record_id

DECLARE @week_player_total decimal(10,4)
SELECT @week_player_total = count(*)
FROM fbweektemplate wt 
INNER JOIN fbweek w ON wt.record_id = w.record_id
WHERE w.year_code = @year_code and w.week_code = @week_code and w.fav_team_picked >= 0 
group by wt.record_id

--Now get the weeks records
SELECT w.record_id, w.week_code, w.player_code, wt.home_team_code, t1.image_name as home_image_name, 
	case when @show_team_records = 1 then t1.team_name + dbo.fnFB_GetTeamRecord(wt.home_team_code, @year_code, @week_code) else t1.team_name end as home_team_name, 
	wt.away_team_code, t2.image_name as away_image_name,
	case when @show_team_records = 1 then t2.team_name + dbo.fnFB_GetTeamRecord(wt.away_team_code, @year_code, @week_code) else t2.team_name end as away_team_name, 
	wt.fav_team_code, wt.game_time, wt.spread, w.fav_team_picked, isnull(w.last_updated,'1900-1-1') as last_updated,
	wt.home_team_result, wt.away_team_result, wt.fav_team_won_game, w.won_pick, 
	convert(decimal(10,4),isnull((c.fav_count/@week_player_total),0)) as fav_picked_count
FROM FBWeek w
INNER JOIN FBWeekTemplate wt ON w.record_id = wt.record_id
INNER JOIN FBTeam t1 ON wt.home_team_code = t1.team_code
INNER JOIN FBTeam t2 ON wt.away_team_code = t2.team_code
LEFT JOIN @tmp_counts c ON w.record_id = c.record_id
WHERE w.year_code = @year_code and w.week_code = @week_code and w.player_code = @player_code
ORDER BY wt.game_time