CREATE PROCEDURE [dbo].spFB3_GetTeamHeadToHeadStats
 @fav_team_code int, 
 @underdog_team_code int
AS

CREATE TABLE #tmp_teams (fav_team_code int, underdog_team_code int, fav_sum_scores int, underdog_sum_scores int, fav_record int, underdog_record int, )

--home team favorites
INSERT INTO #tmp_teams
SELECT home_team_code, away_team_code, sum(home_team_result), sum(away_team_result), 
 --sum(fav_team_won_game), count(fav_team_won_game) - sum(fav_team_won_game)
 sum(case when wt.home_team_result > wt.away_team_result then 1 else 0 end) as fav_wins,
 sum(case when wt.away_team_result > wt.home_team_result then 1 else 0 end) as underdog_wins 
FROM FBWeekTemplate wt
WHERE (home_team_code = @fav_team_code
and away_team_code = @underdog_team_code)
and fav_team_won_game >= 0
GROUP BY home_team_code, away_team_code

--away team favorites
INSERT INTO #tmp_teams
SELECT away_team_code, home_team_code, sum(away_team_result), sum(home_team_result), 
 --sum(fav_team_won_game), count(fav_team_won_game) - sum(fav_team_won_game)
 sum(case when wt.away_team_result > wt.home_team_result then 1 else 0 end) as fav_wins,
 sum(case when wt.home_team_result > wt.away_team_result then 1 else 0 end) as underdog_wins 
FROM FBWeekTemplate wt
WHERE (home_team_code = @underdog_team_code
and away_team_code = @fav_team_code)
and fav_team_won_game >= 0
GROUP BY away_team_code, home_team_code

SELECT th.team_name as fav_team, ta.team_name as under_team, fav_team_code, underdog_team_code, 
 sum(fav_sum_scores) as fav_sum_scores, sum(underdog_sum_scores) as underdog_sum_scores, 
 sum(fav_record) as fav_record_sum, sum(underdog_record) as underdog_record_sum
 --,newid() as record_id, 0 as week_code, 0 as player_code, 
 --0 as home_team_code, 0 as away_team_code, 0 as fav_team_code,
 --'' as home_team_name, '' as away_team_name,
 --'' as home_image_name, '' as away_image_name,
 --0 as fav_team_picked, getdate() as game_time, 0 as spread,
 --0 as home_team_result, 0 as away_team_result
FROM #tmp_teams wt
INNER JOIN FBTeam th ON wt.fav_team_code = th.team_code
INNER JOIN FBTeam ta ON wt.underdog_team_code = ta.team_code
GROUP BY th.team_name, ta.team_name, fav_team_code, underdog_team_code