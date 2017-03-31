CREATE PROCEDURE [dbo].[spFB2_GetStatistics]
AS

SET NOCOUNT ON

/*
SELECT convert(smallint,null) as year_code,
	convert(smallint,null) as games_played,
	convert(smallint,null) as total_picks, 
	convert(smallint,null) as total_favs_picked, 
	convert(smallint,null) as total_picks_won, 
	convert(smallint,null) as total_fav_picks_won, 
	convert(smallint,null) as total_underdog_picks_won,
 	convert(decimal(10,2),null) as total_picks_percent, 
	convert(decimal(10,2),null) as total_favs_picked_percent, 
	convert(decimal(10,2),null) as total_picks_won_percent, 
	convert(decimal(10,2),null) as total_fav_picks_won_percent, 
	convert(decimal(10,2),null) as total_underdog_picks_won_percent,
 	convert(smallint,null) as fav_team_at_home_win, 
	convert(smallint,null) as fav_team_away_win, 
	convert(smallint,null) as under_team_at_home_win, 
	convert(smallint,null) as under_team_away_win,
 	convert(decimal(10,2),null) as fav_team_at_home_win_percent, 
	convert(decimal(10,2),null) as fav_team_away_win_percent, 
	convert(decimal(10,2),null) as under_team_at_home_win_percent, 
	convert(decimal(10,2),null) as under_team_away_win_percent, 
	convert(decimal(10,2),null) as total_wins,
	convert(decimal(10,2),null) as home_wins,
	convert(decimal(10,2),null) as home_win_percent,
	convert(decimal(10,2),null) as fav_wins,
	convert(decimal(10,2),null) as fav_win_percent,
	convert(decimal(10,2),null) as ytd_amount,
	convert(decimal(10,2),null) as donut_cost
*/

CREATE TABLE #tmp_stats(
	year_code smallint,
	games_played smallint,
	total_picks smallint, 
	total_favs_picked smallint, 
	total_picks_won smallint, 
	total_fav_picks_won smallint, 
	total_underdog_picks_won smallint,
 	total_picks_percent decimal(10,2), 
	total_favs_picked_percent decimal(10,2), 
	total_picks_won_percent decimal(10,2), 
	total_fav_picks_won_percent decimal(10,2), 
	total_underdog_picks_won_percent decimal(10,2),
 	fav_team_at_home_win smallint, 
	fav_team_away_win smallint, 
	under_team_at_home_win smallint, 
	under_team_away_win smallint,
 	fav_team_at_home_win_percent decimal(10,2), 
	fav_team_away_win_percent decimal(10,2), 
	under_team_at_home_win_percent decimal(10,2), 
	under_team_away_win_percent decimal(10,2), 
	total_wins decimal(10,2),
	home_wins decimal(10,2),
	home_win_percent decimal(10,2),
	fav_wins decimal(10,2),
	fav_win_percent decimal(10,2),
	ytd_amount decimal(10,2),
	donut_cost decimal(10,2))

DECLARE @current_year_code smallint
DECLARE @current_week_code smallint

SELECT @current_year_code = current_year_code, @current_week_code = current_week_code
FROM FBSettings

--Insert in the year
INSERT INTO #tmp_stats
SELECT distinct s.year_code, null, null, null, null, null, null, null, null, 
	null, null, null, null, null, null, null, null, null, 
	null, null, null, null, null, null, null, null, null
FROM FBSummary s
GROUP BY s.year_code

--Number of Games Played
UPDATE s
SET games_played = (SELECT count(*) FROM FBWeekTemplate wt 
						WHERE s.year_code = wt.year_code
						and wt.game_time < getdate())
FROM #tmp_stats s

--Win/ Fav/ Underdog results
UPDATE s
SET total_picks = (SELECT count(*) 
					FROM FBWeek w WHERE s.year_code = w.year_code 
					and w.fav_team_picked >= 0),
	total_favs_picked = (SELECT count(*) 
					FROM FBWeek w WHERE s.year_code = w.year_code 
					and w.fav_team_picked = 1),
	total_picks_won = (SELECT count(*) 
					FROM FBWeek w WHERE s.year_code = w.year_code 
					and w.won_pick = 1),
	total_fav_picks_won = (SELECT count(*) 
					FROM FBWeek w WHERE s.year_code = w.year_code 
					and w.fav_team_picked = 1 
					and w.won_pick = 1),
	total_underdog_picks_won = (SELECT count(*) 
					FROM FBWeek w WHERE s.year_code = w.year_code 
					and w.fav_team_picked = 0 
					and w.won_pick = 1)
FROM #tmp_stats s

--Win/ Fav/ Underdog Percents
UPDATE s
SET s.total_picks_percent = isnull((SELECT convert(decimal(10,2),s.total_picks) / convert(decimal(10,2),s2.total_picks)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.total_picks > 0),0)*100,
	s.total_favs_picked_percent = isnull((SELECT convert(decimal(10,2),s.total_favs_picked) / convert(decimal(10,2),s2.total_picks)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.total_picks > 0),0)*100,
	s.total_picks_won_percent = isnull((SELECT convert(decimal(10,2),s.total_picks_won) / convert(decimal(10,2),s2.total_picks)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.total_picks > 0),0)*100,
	s.total_fav_picks_won_percent = isnull((SELECT convert(decimal(10,2),s.total_fav_picks_won) / convert(decimal(10,2),s2.total_picks_won)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.total_picks > 0),0)*100,
	s.total_underdog_picks_won_percent = isnull((SELECT convert(decimal(10,2),s.total_underdog_picks_won) / convert(decimal(10,2),s2.total_picks_won)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.total_picks > 0),0)*100
FROM #tmp_stats s

--Fav/underdog vs home/away
UPDATE s
SET s.fav_team_at_home_win = (SELECT count(*)
					FROM FBWeekTemplate wt 
					WHERE s.year_code = wt.year_code 
					and wt.home_team_code = wt.fav_team_code
					and wt.fav_team_won_game = 1),
	s.fav_team_away_win = (SELECT count(*)
					FROM FBWeekTemplate wt 
					WHERE s.year_code = wt.year_code 
					and wt.away_team_code = wt.fav_team_code
					and wt.fav_team_won_game = 1),
	s.under_team_at_home_win = (SELECT count(*)
					FROM FBWeekTemplate wt 
					WHERE s.year_code = wt.year_code 
					and wt.home_team_code <> wt.fav_team_code
					and wt.fav_team_won_game = 0),
	s.under_team_away_win = (SELECT count(*)
					FROM FBWeekTemplate wt 
					WHERE s.year_code = wt.year_code 
					and wt.away_team_code <> wt.fav_team_code
					and wt.fav_team_won_game = 0)
FROM #tmp_stats s

--Fav/underdog vs home/away percemt
UPDATE s
SET s.fav_team_at_home_win_percent = isnull((SELECT convert(decimal(10,2),s.fav_team_at_home_win) / convert(decimal(10,2),s2.games_played)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.total_picks > 0),0)*100,
	s.fav_team_away_win_percent = isnull((SELECT convert(decimal(10,2),s.fav_team_away_win) / convert(decimal(10,2),s2.games_played)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.total_picks > 0),0)*100,
	s.under_team_at_home_win_percent = isnull((SELECT convert(decimal(10,2),s.under_team_at_home_win) / convert(decimal(10,2),s2.games_played)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.total_picks > 0),0)*100,
	s.under_team_away_win_percent = isnull((SELECT convert(decimal(10,2),s.under_team_away_win) / convert(decimal(10,2),s2.games_played)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.total_picks > 0),0)*100
FROM #tmp_stats s

UPDATE s
SET s.total_wins = (SELECT fav_team_at_home_win+fav_team_away_win+under_team_at_home_win+under_team_away_win 
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code),
	s.home_wins = (SELECT fav_team_at_home_win+under_team_at_home_win
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code),
	s.home_win_percent = isnull((SELECT convert(decimal(10,2),(fav_team_at_home_win+under_team_at_home_win)) / convert(decimal(10,2),s2.games_played)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.games_played > 0),0)*100,
	s.fav_wins = (SELECT fav_team_at_home_win+fav_team_away_win 
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code),
	s.fav_win_percent = isnull((SELECT convert(decimal(10,2),(fav_team_at_home_win+fav_team_away_win)) / convert(decimal(10,2),s2.games_played)
					FROM #tmp_stats s2 WHERE s.year_code = s2.year_code and s2.games_played > 0),0)*100
FROM #tmp_stats s 

UPDATE s
SET ytd_amount = (SELECT sum(year_end_amount) 
					FROM FBStatus s2
					WHERE s.year_code = s2.year_code),
	donut_cost = (SELECT sum(donut_cost) 
					FROM FBStatus s2
					WHERE s.year_code = s2.year_code)
FROM #tmp_stats s 
/*
SELECT @ytd_amount = sum(year_end_amount), @donut_cost = sum(donut_cost) 
FROM FBStatus
WHERE not year_end_amount is null and not donut_cost is null and year_code = @year_code
*/

/*
--Get all the games that have started
DECLARE @games_played smallint

SELECT @games_played = count(*)
FROM FBWeekTemplate
WHERE year_code = @year_code and week_code <= @current_week_code
and game_time < getdate()

DECLARE @total_picks decimal(10,2), @total_favs_picked decimal(10,2), @total_picks_won decimal(10,2)
DECLARE @total_fav_picks_won decimal(10,2), @total_underdog_picks_won decimal(10,2)

select @total_picks = count(*) 
from FBWeek
where year_code = @year_code and week_code <= @current_week_code and fav_team_picked >= 0

select @total_favs_picked = count(*) 
from FBWeek
where year_code = @year_code and week_code <= @current_week_code and fav_team_picked = 1

select @total_picks_won = count(*) 
from FBWeek
where year_code = @year_code and week_code <= @current_week_code and won_pick = 1

select @total_fav_picks_won = count(*) 
from FBWeek
where year_code = @year_code and week_code <= @current_week_code and fav_team_picked = 1 and won_pick = 1

select @total_underdog_picks_won = count(*) 
from FBWeek
where year_code = @year_code and week_code <= @current_week_code and fav_team_picked = 0 and won_pick = 1

/*
SELECT @total_picks as total_picks, @total_favs_picked as total_favs_picked, @total_picks_won as total_picks_won, 
@total_fav_picks_won as total_fav_picks_won, @total_underdog_picks_won as total_underdog_picks_won

SELECT convert(decimal(10,2),(@total_picks/@total_picks)) as total_picks_percent, 
	convert(decimal(10,2),(@total_favs_picked/@total_picks)) as total_favs_picked_percent, 
	convert(decimal(10,2),(@total_picks_won/@total_picks)) as total_picks_won_percent, 
	convert(decimal(10,2),(@total_fav_picks_won/@total_picks_won)) as total_fav_picks_won_percent, 
	convert(decimal(10,2),(1-@total_fav_picks_won/@total_picks_won)) as total_underdog_picks_won_percent
*/

DECLARE @fav_team_at_home_win decimal(10,2), @fav_team_away_win decimal(10,2)
DECLARE @under_team_at_home_win decimal(10,2), @under_team_away_win decimal(10,2)

select @fav_team_at_home_win = count(*)
from FBWeekTemplate wt 
where home_team_code = fav_team_code and fav_team_won_game = 1
and wt.year_code = @year_code and wt.week_code <= @current_week_code

select @fav_team_away_win = count(*)
from FBWeekTemplate wt
where away_team_code = fav_team_code and fav_team_won_game = 1
and wt.year_code = @year_code and wt.week_code <= @current_week_code

select @under_team_at_home_win = count(*) 
from FBWeekTemplate wt
where home_team_code <> fav_team_code and fav_team_won_game = 0
and wt.year_code = @year_code and wt.week_code <= @current_week_code

select @under_team_away_win = count(*)
from FBWeekTemplate wt
where away_team_code <> fav_team_code and fav_team_won_game = 0
and wt.year_code = @year_code and wt.week_code <= @current_week_code

/*
SELECT @fav_team_at_home_win_percent as fav_team_at_home_win, @fav_team_away_win_percent as fav_team_away_win, 
	@under_team_at_home_win_percent as under_team_at_home_win, @under_team_away_win_percent as under_team_away_win

SELECT convert(decimal(10,2),(@fav_team_at_home_win_percent/16)) as fav_team_at_home_win_percent, 
	convert(decimal(10,2),(@fav_team_away_win_percent/16)) as fav_team_away_win_percent, 
	convert(decimal(10,2),(@under_team_at_home_win_percent/16)) as under_team_at_home_win_percent, 
	convert(decimal(10,2),(@under_team_away_win_percent/16)) as under_team_away_win_percent, 
	convert(decimal(10,2),((@fav_team_at_home_win_percent+@under_team_at_home_win_percent)/16)) as home_win_percent

--		 fav team 	| underdog
--home	|			|
--away	|			|
*/
DECLARE @ytd_amount money, @donut_cost money

SELECT @ytd_amount = sum(year_end_amount), @donut_cost = sum(donut_cost) 
FROM FBStatus
WHERE not year_end_amount is null and not donut_cost is null and year_code = @year_code

IF (@total_picks = 0)
	SELECT @total_picks = 1
IF (@total_picks_won = 0)
	SELECT @total_picks_won = 1
IF (@games_played = 0)
	SELECT @games_played = 1

SELECT @total_picks as total_picks, 
	@total_favs_picked as total_favs_picked, 
	@total_picks_won as total_picks_won, 
	@total_fav_picks_won as total_fav_picks_won, 
	@total_underdog_picks_won as total_underdog_picks_won,
 	convert(decimal(10,2),(@total_picks/@total_picks)) as total_picks_percent, 
	convert(decimal(10,2),(@total_favs_picked/@total_picks)) as total_favs_picked_percent, 
	convert(decimal(10,2),(@total_picks_won/@total_picks)) as total_picks_won_percent, 
	convert(decimal(10,2),(@total_fav_picks_won/@total_picks_won)) as total_fav_picks_won_percent, 
	convert(decimal(10,2),(1-@total_fav_picks_won/@total_picks_won)) as total_underdog_picks_won_percent,
 	@fav_team_at_home_win as fav_team_at_home_win, 
	@fav_team_away_win as fav_team_away_win, 
	@under_team_at_home_win as under_team_at_home_win, 
	@under_team_away_win as under_team_away_win,
 	convert(decimal(10,2),(@fav_team_at_home_win/@games_played)) as fav_team_at_home_win_percent, 
	convert(decimal(10,2),(@fav_team_away_win/@games_played)) as fav_team_away_win_percent, 
	convert(decimal(10,2),(@under_team_at_home_win/@games_played)) as under_team_at_home_win_percent, 
	convert(decimal(10,2),(@under_team_away_win/@games_played)) as under_team_away_win_percent, 
	convert(decimal(10,2),((@fav_team_at_home_win+@fav_team_away_win+@under_team_at_home_win+@under_team_away_win))) as total_wins,
	convert(decimal(10,2),((@fav_team_at_home_win+@under_team_at_home_win))) as home_wins,
	convert(decimal(10,2),((@fav_team_at_home_win+@under_team_at_home_win)/@games_played)) as home_win_percent,
	convert(decimal(10,2),((@fav_team_at_home_win+@fav_team_away_win))) as fav_wins,
	convert(decimal(10,2),((@fav_team_at_home_win+@fav_team_away_win)/@games_played)) as fav_win_percent,
	isnull(@ytd_amount,0) as ytd_amount,
	isnull(@donut_cost,0) as donut_cost

--Team most picked as fav.
--Team most picked as underdog

*/

SELECT * 
FROM #tmp_stats 
--where year_code = 2007
ORDER BY year_code DESC

DROP TABLE #tmp_stats