CREATE PROCEDURE [dbo].[spFB2_GetStatistics_Wins_Details_Extra_ForMondays]
AS

/*
SELECT convert(smallint,null) as spread,
	convert(smallint,null) as games_played,
	convert(smallint,null) as fav_team_at_home_wins,
 	convert(decimal(10,4),null) as fav_team_at_home_win_percent, 
	convert(smallint,null) as fav_team_away_wins,
	convert(decimal(10,4),null) as fav_team_away_win_percent, 
	convert(smallint,null) as under_team_at_home_wins,
	convert(decimal(10,4),null) as under_team_at_home_win_percent, 
	convert(smallint,null) as under_team_away_wins,
	convert(decimal(10,4),null) as under_team_away_win_percent 
*/

SET NOCOUNT ON

CREATE TABLE #tmp_stats(
	spread decimal(4,2),
	games_played smallint,
	fav_team_at_home_wins smallint,
	fav_team_at_home_win_percent decimal(10,4),
	fav_team_away_wins smallint,  
	fav_team_away_win_percent decimal(10,4),
	under_team_at_home_wins smallint, 
	under_team_at_home_win_percent decimal(10,4),
	under_team_away_wins smallint,
	under_team_away_win_percent decimal(10,4))

--Insert in the year & Number of Games Played
INSERT INTO #tmp_stats
SELECT DISTINCT wt.spread, 
		(SELECT count(*) FROM fbweektemplate wt2 
			WHERE wt.spread = wt2.spread
			and wt2.year_code < year(getdate())
			and DATENAME(dw,wt2.game_time) = 'Monday'), 
		null, null, null, null, 
		null, null, null, null
FROM fbweektemplate wt
WHERE wt.year_code < year(getdate())
and DATENAME(dw,wt.game_time) = 'Monday'
GROUP BY wt.spread, wt.game_time 
ORDER BY wt.spread DESC



--Fav/underdog vs home/away
UPDATE s
SET s.fav_team_at_home_wins = (SELECT count(*)
					FROM fbweektemplate wt 
					WHERE s.spread = wt.spread 
					and wt.home_team_code = wt.fav_team_code
					and wt.fav_team_won_game = 1
					and DATENAME(dw,wt.game_time) = 'Monday'),
	s.fav_team_away_wins = (SELECT count(*)
					FROM fbweektemplate wt 
					WHERE s.spread = wt.spread 
					and wt.away_team_code = wt.fav_team_code
					and wt.fav_team_won_game = 1
					and DATENAME(dw,wt.game_time) = 'Monday'),
	s.under_team_at_home_wins = (SELECT count(*)
					FROM fbweektemplate wt 
					WHERE s.spread = wt.spread 
					and wt.home_team_code <> wt.fav_team_code
					and wt.fav_team_won_game = 0
					and DATENAME(dw,wt.game_time) = 'Monday'),
	s.under_team_away_wins = (SELECT count(*)
					FROM fbweektemplate wt 
					WHERE s.spread = wt.spread 
					and wt.away_team_code <> wt.fav_team_code
					and wt.fav_team_won_game = 0
					and DATENAME(dw,wt.game_time) = 'Monday')
FROM #tmp_stats s

--Fav/underdog vs home/away percemt
UPDATE s
SET s.fav_team_at_home_win_percent = isnull((SELECT convert(decimal(10,4),s.fav_team_at_home_wins) / convert(decimal(10,4),s2.games_played)
					FROM #tmp_stats s2 WHERE s.spread = s2.spread and s2.games_played > 0),0),
	s.fav_team_away_win_percent = isnull((SELECT convert(decimal(10,4),s.fav_team_away_wins) / convert(decimal(10,4),s2.games_played)
					FROM #tmp_stats s2 WHERE s.spread = s2.spread and s2.games_played > 0),0),
	s.under_team_at_home_win_percent = isnull((SELECT convert(decimal(10,4),s.under_team_at_home_wins) / convert(decimal(10,4),s2.games_played)
					FROM #tmp_stats s2 WHERE s.spread = s2.spread and s2.games_played > 0),0),
	s.under_team_away_win_percent = isnull((SELECT convert(decimal(10,4),s.under_team_away_wins) / convert(decimal(10,4),s2.games_played)
					FROM #tmp_stats s2 WHERE s.spread = s2.spread and s2.games_played > 0),0)
FROM #tmp_stats s

SELECT * 
FROM #tmp_stats 
ORDER BY spread DESC

DROP TABLE #tmp_stats