

CREATE PROCEDURE [dbo].[spFB_GetStatistics]
	@year_code smallint, 
	@current_week_code smallint
AS

--Get all the games that have started
DECLARE @games_played smallint

SELECT @games_played = count(*)
FROM fbweektemplate
WHERE year_code = @year_code and week_code <= @current_week_code
and game_time < getdate()

DECLARE @total_picks decimal(10,4), @total_favs_picked decimal(10,4), @total_picks_won decimal(10,4)
DECLARE @total_fav_picks_won decimal(10,4), @total_underdog_picks_won decimal(10,4)

select @total_picks = count(*) 
from fbweek
where year_code = @year_code and week_code <= @current_week_code and fav_team_picked >= 0

select @total_favs_picked = count(*) 
from fbweek
where year_code = @year_code and week_code <= @current_week_code and fav_team_picked = 1

select @total_picks_won = count(*) 
from fbweek
where year_code = @year_code and week_code <= @current_week_code and won_pick = 1

select @total_fav_picks_won = count(*) 
from fbweek
where year_code = @year_code and week_code <= @current_week_code and fav_team_picked = 1 and won_pick = 1

select @total_underdog_picks_won = count(*) 
from fbweek
where year_code = @year_code and week_code <= @current_week_code and fav_team_picked = 0 and won_pick = 1

/*
SELECT @total_picks as total_picks, @total_favs_picked as total_favs_picked, @total_picks_won as total_picks_won, 
@total_fav_picks_won as total_fav_picks_won, @total_underdog_picks_won as total_underdog_picks_won

SELECT convert(decimal(10,4),(@total_picks/@total_picks)) as total_picks_percent, 
	convert(decimal(10,4),(@total_favs_picked/@total_picks)) as total_favs_picked_percent, 
	convert(decimal(10,4),(@total_picks_won/@total_picks)) as total_picks_won_percent, 
	convert(decimal(10,4),(@total_fav_picks_won/@total_picks_won)) as total_fav_picks_won_percent, 
	convert(decimal(10,4),(1-@total_fav_picks_won/@total_picks_won)) as total_underdog_picks_won_percent
*/

DECLARE @fav_team_at_home_win decimal(10,4), @fav_team_away_win decimal(10,4)
DECLARE @under_team_at_home_win decimal(10,4), @under_team_away_win decimal(10,4)

select @fav_team_at_home_win = count(*)
from fbweektemplate wt 
where home_team_code = fav_team_code and fav_team_won_game = 1
and wt.year_code = @year_code and wt.week_code <= @current_week_code

select @fav_team_away_win = count(*)
from fbweektemplate wt
where away_team_code = fav_team_code and fav_team_won_game = 1
and wt.year_code = @year_code and wt.week_code <= @current_week_code

select @under_team_at_home_win = count(*) 
from fbweektemplate wt
where home_team_code <> fav_team_code and fav_team_won_game = 0
and wt.year_code = @year_code and wt.week_code <= @current_week_code

select @under_team_away_win = count(*)
from fbweektemplate wt
where away_team_code <> fav_team_code and fav_team_won_game = 0
and wt.year_code = @year_code and wt.week_code <= @current_week_code

/*
SELECT @fav_team_at_home_win_percent as fav_team_at_home_win, @fav_team_away_win_percent as fav_team_away_win, 
	@under_team_at_home_win_percent as under_team_at_home_win, @under_team_away_win_percent as under_team_away_win

SELECT convert(decimal(10,4),(@fav_team_at_home_win_percent/16)) as fav_team_at_home_win_percent, 
	convert(decimal(10,4),(@fav_team_away_win_percent/16)) as fav_team_away_win_percent, 
	convert(decimal(10,4),(@under_team_at_home_win_percent/16)) as under_team_at_home_win_percent, 
	convert(decimal(10,4),(@under_team_away_win_percent/16)) as under_team_away_win_percent, 
	convert(decimal(10,4),((@fav_team_at_home_win_percent+@under_team_at_home_win_percent)/16)) as home_win_percent

--		 fav team 	| underdog
--home	|			|
--away	|			|
*/
DECLARE @ytd_amount money, @donut_cost money

SELECT @ytd_amount = sum(year_end_amount), @donut_cost = sum(donut_cost) 
FROM fbstatus
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
 	convert(decimal(10,4),(@total_picks/@total_picks)) as total_picks_percent, 
	convert(decimal(10,4),(@total_favs_picked/@total_picks)) as total_favs_picked_percent, 
	convert(decimal(10,4),(@total_picks_won/@total_picks)) as total_picks_won_percent, 
	convert(decimal(10,4),(@total_fav_picks_won/@total_picks_won)) as total_fav_picks_won_percent, 
	convert(decimal(10,4),(1-@total_fav_picks_won/@total_picks_won)) as total_underdog_picks_won_percent,
 	@fav_team_at_home_win as fav_team_at_home_win, 
	@fav_team_away_win as fav_team_away_win, 
	@under_team_at_home_win as under_team_at_home_win, 
	@under_team_away_win as under_team_away_win,
 	convert(decimal(10,4),(@fav_team_at_home_win/@games_played)) as fav_team_at_home_win_percent, 
	convert(decimal(10,4),(@fav_team_away_win/@games_played)) as fav_team_away_win_percent, 
	convert(decimal(10,4),(@under_team_at_home_win/@games_played)) as under_team_at_home_win_percent, 
	convert(decimal(10,4),(@under_team_away_win/@games_played)) as under_team_away_win_percent, 
	convert(decimal(10,4),((@fav_team_at_home_win+@fav_team_away_win+@under_team_at_home_win+@under_team_away_win))) as total_wins,
	convert(decimal(10,4),((@fav_team_at_home_win+@under_team_at_home_win))) as home_wins,
	convert(decimal(10,4),((@fav_team_at_home_win+@under_team_at_home_win)/@games_played)) as home_win_percent,
	convert(decimal(10,4),((@fav_team_at_home_win+@fav_team_away_win))) as fav_wins,
	convert(decimal(10,4),((@fav_team_at_home_win+@fav_team_away_win)/@games_played)) as fav_win_percent,
	isnull(@ytd_amount,0) as ytd_amount,
	isnull(@donut_cost,0) as donut_cost

--Team most picked as fav.
--Team most picked as underdog