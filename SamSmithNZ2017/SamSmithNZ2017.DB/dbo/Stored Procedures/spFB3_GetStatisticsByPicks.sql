CREATE PROCEDURE [dbo].[spFB3_GetStatisticsByPicks]
AS

SET NOCOUNT ON

/*
SELECT convert(smallint,null) as year_code,
	convert(smallint,null) as games_played,
	convert(smallint,null) as total_picks, 
	convert(smallint,null) as total_picks_won, 
	convert(decimal(10,2),null) as total_picks_won_percent, 
	convert(smallint,null) as total_favs_picked, 
	convert(decimal(10,2),null) as total_favs_picked_percent, 
	convert(smallint,null) as total_underdogs_picked, 
	convert(decimal(10,2),null) as total_underdogs_picked_percent, 
	convert(smallint,null) as total_fav_picks_won, 
	convert(decimal(10,2),null) as total_fav_picks_won_percent, 
	convert(smallint,null) as total_underdog_picks_won,
	convert(decimal(10,2),null) as total_underdog_picks_won_percent
*/

CREATE TABLE #tmp_stats(
	year_code smallint,
	games_played smallint,
	total_picks smallint, 
	total_picks_won smallint, 
	total_picks_won_percent decimal(10,2), 
	total_favs_picked smallint, 
	total_favs_picked_percent decimal(10,2), 
	total_underdogs_picked smallint, 
	total_underdogs_picked_percent decimal(10,2), 
	total_fav_picks_won smallint, 
	total_fav_picks_won_percent decimal(10,2), 
	total_underdog_picks_won smallint,
	total_underdog_picks_won_percent decimal(10,2))

DECLARE @current_year_code smallint
DECLARE @current_week_code smallint

SELECT @current_year_code = current_year_code, @current_week_code = current_week_code
FROM FBSettings

--Insert in the year
INSERT INTO #tmp_stats
SELECT distinct s.year_code, null, null, null, null, 
	null, null, null, null, null, null, null, null
FROM FBsummary s
GROUP BY s.year_code

--Number of Games Played
UPDATE s
SET games_played = (SELECT count(*) FROM fbweektemplate wt 
						WHERE s.year_code = wt.year_code
						and wt.game_time < getdate())
FROM #tmp_stats s

--Win/ Fav/ Underdog results
UPDATE s
SET total_picks = (SELECT count(*) 
					FROM fbweek w 
					INNER JOIN fbweektemplate wt ON w.record_id = wt.record_id
					WHERE s.year_code = w.year_code 
					and w.fav_team_picked >= 0
					and wt.game_time < getdate()),
	total_picks_won = (SELECT count(*) 
					FROM fbweek w 
					INNER JOIN fbweektemplate wt ON w.record_id = wt.record_id
					WHERE s.year_code = w.year_code 
					and w.won_pick = 1
					and wt.game_time < getdate()),
	total_favs_picked = (SELECT count(*) 
					FROM fbweek w 
					INNER JOIN fbweektemplate wt ON w.record_id = wt.record_id
					WHERE s.year_code = w.year_code 
					and w.fav_team_picked = 1
					and wt.game_time < getdate()),
	total_underdogs_picked = (SELECT count(*) 
					FROM fbweek w 
					INNER JOIN fbweektemplate wt ON w.record_id = wt.record_id
					WHERE s.year_code = w.year_code 
					and w.fav_team_picked = 0
					and wt.game_time < getdate()),
	total_fav_picks_won = (SELECT count(*) 
					FROM fbweek w 
					INNER JOIN fbweektemplate wt ON w.record_id = wt.record_id
					WHERE s.year_code = w.year_code 
					and w.fav_team_picked = 1 
					and w.won_pick = 1
					and wt.game_time < getdate()),
	total_underdog_picks_won = (SELECT count(*) 
					FROM fbweek w 
					INNER JOIN fbweektemplate wt ON w.record_id = wt.record_id
					WHERE s.year_code = w.year_code 
					and w.fav_team_picked = 0 
					and w.won_pick = 1
					and wt.game_time < getdate())
FROM #tmp_stats s

--Win/ Fav/ Underdog Percents
UPDATE s
SET s.total_picks_won_percent = isnull((SELECT convert(decimal(10,2),s.total_picks_won) / convert(decimal(10,2),s2.total_picks)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.total_picks > 0),0)*100,
	s.total_favs_picked_percent = isnull((SELECT convert(decimal(10,2),s.total_favs_picked) / convert(decimal(10,2),s2.total_picks)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.total_picks > 0),0)*100,
	s.total_underdogs_picked_percent = isnull((SELECT convert(decimal(10,2),s.total_underdogs_picked) / convert(decimal(10,2),s2.total_picks)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.total_picks > 0),0)*100,
	s.total_fav_picks_won_percent = isnull((SELECT convert(decimal(10,2),s.total_fav_picks_won) / convert(decimal(10,2),s2.total_picks_won)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.total_picks > 0),0)*100,
	s.total_underdog_picks_won_percent = isnull((SELECT convert(decimal(10,2),s.total_underdog_picks_won) / convert(decimal(10,2),s2.total_picks_won)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.total_picks > 0),0)*100
FROM #tmp_stats s

SELECT * 
FROM #tmp_stats 
ORDER BY year_code DESC

DROP TABLE #tmp_stats