CREATE PROCEDURE [dbo].[spFB3_GetStatistics_By_Spread]
AS

SET NOCOUNT ON

CREATE TABLE #tmp_stats(
	spread decimal(4,1),
	total_games_played smallint,
	fav_home_games smallint,
	fav_team_at_home_wins smallint,
	fav_team_at_home_win_percent decimal(10,2),
	under_team_away_wins smallint,
	under_team_away_win_percent decimal(10,2),
	under_home_games smallint,
	under_team_at_home_wins smallint, 
	under_team_at_home_win_percent decimal(10,2),
	fav_team_away_wins smallint,  
	fav_team_away_win_percent decimal(10,2))

--Insert in the year & Number of Games Played
INSERT INTO #tmp_stats
SELECT wt.spread, 
		(SELECT count(*) FROM fbweektemplate wt2 
			WHERE wt.spread = wt2.spread
			--and wt2.year_code < year(getdate())
			and wt2.fav_team_won_game > -1),
		null, null, null, null, null, 
		null, null, null, null, null
FROM fbweektemplate wt
WHERE wt.fav_team_won_game > -1
--and wt.year_code < year(getdate())
GROUP BY wt.spread
ORDER BY wt.spread DESC

--Fav/underdog vs home/away
UPDATE s
SET s.fav_team_at_home_wins = (SELECT count(*)
					FROM fbweektemplate wt 
					WHERE s.spread = wt.spread 
					and wt.home_team_code = wt.fav_team_code
					and wt.fav_team_won_game = 1),
	s.fav_team_away_wins = (SELECT count(*)
					FROM fbweektemplate wt 
					WHERE s.spread = wt.spread 
					and wt.away_team_code = wt.fav_team_code
					and wt.fav_team_won_game = 1),
	s.under_team_at_home_wins = (SELECT count(*)
					FROM fbweektemplate wt 
					WHERE s.spread = wt.spread 
					and wt.home_team_code <> wt.fav_team_code
					and wt.fav_team_won_game = 0),
	s.under_team_away_wins = (SELECT count(*)
					FROM fbweektemplate wt 
					WHERE s.spread = wt.spread 
					and wt.away_team_code <> wt.fav_team_code
					and wt.fav_team_won_game = 0)
FROM #tmp_stats s

--Update the Fav Home and Under Home Win Columsn
UPDATE s
SET s.fav_home_games = (s.fav_team_at_home_wins + s.under_team_away_wins),
	s.under_home_games = (s.under_team_at_home_wins + s.fav_team_away_wins)
FROM #tmp_stats s

--Fav/underdog vs home/away percemt
UPDATE s
SET s.fav_team_at_home_win_percent = isnull((SELECT convert(decimal(10,4),s.fav_team_at_home_wins) / convert(decimal(10,4),s2.fav_home_games)
					FROM #tmp_stats s2 WHERE s.spread = s2.spread and s2.fav_home_games > 0),0)*100,
	s.under_team_away_win_percent = isnull((SELECT convert(decimal(10,4),s.under_team_away_wins) / convert(decimal(10,4),s2.fav_home_games)
					FROM #tmp_stats s2 WHERE s.spread = s2.spread and s2.fav_home_games > 0),0)*100,
	s.under_team_at_home_win_percent = isnull((SELECT convert(decimal(10,4),s.under_team_at_home_wins) / convert(decimal(10,4),s2.under_home_games)
					FROM #tmp_stats s2 WHERE s.spread = s2.spread and s2.under_home_games > 0),0)*100,
	s.fav_team_away_win_percent = isnull((SELECT convert(decimal(10,4),s.fav_team_away_wins) / convert(decimal(10,4),s2.under_home_games)
					FROM #tmp_stats s2 WHERE s.spread = s2.spread and s2.under_home_games > 0),0)*100
FROM #tmp_stats s

SELECT * 
FROM #tmp_stats 
ORDER BY spread DESC

DROP TABLE #tmp_stats