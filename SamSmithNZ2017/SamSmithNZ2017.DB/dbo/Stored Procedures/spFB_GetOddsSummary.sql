CREATE PROCEDURE [dbo].[spFB_GetOddsSummary]
	@odds_limit DECIMAL(18, 4) = null,
	@show_alive_teams bit = null,
	@show_eliminated_teams bit = null,
	@year_code int = null,
	@odds_date datetime = null,
	@team_name varchar(100) = null,
	@top_10_items bit = null
AS
 IF (@year_code is null)
 BEGIN
  SELECT @year_code = 2014
 END

 CREATE TABLE #tmp_team (team_name varchar(100), odds_limit DECIMAL(18, 4))

 IF (@odds_limit is null)
 BEGIN
  INSERT INTO #tmp_team
  SELECT distinct team_name, 0 
  FROM fbteam
  WHERE team_name <> ''
 END
 ELSE
 BEGIN
  INSERT INTO #tmp_team
  SELECT distinct team_name, max(odds_probability)
  FROM fb_odds 
  WHERE year_code = @year_code
  GROUP BY team_name
  HAVING max(odds_probability) <= @odds_limit
 END

 --Show teams that are still in the world cup
 IF (@show_alive_teams is null)
 BEGIN
  SELECT @show_alive_teams = 1
 END
 --Show eliminated teams
 IF (@show_eliminated_teams is null)
 BEGIN
  SELECT @show_eliminated_teams = 1
 END

 IF (@show_alive_teams = 0)
 BEGIN
  DELETE t 
  FROM #tmp_team t
  WHERE t.team_name in (SELECT o.team_name 
        FROM fb_odds o   
        WHERE year_code = @year_code      
        GROUP BY o.team_name
        HAVING min(o.odds_probability) > 0)  
 END
 
 IF (@show_eliminated_teams = 0)
 BEGIN
  DELETE t 
  FROM #tmp_team t
  WHERE t.team_name in (SELECT o.team_name 
        FROM fb_odds o  
        WHERE year_code = @year_code       
        GROUP BY o.team_name
        HAVING min(o.odds_probability) = 0)   
 END

DELETE FROM fb_odds
WHERE datename(dw,odds_date) != 'Tuesday'

if (not @top_10_items is null and @top_10_items = 1)
BEGIN
	CREATE TABLE #tmp_top_team (team_name varchar(100))

	DECLARE @top_date datetime
	SELECT @top_date = max(odds_date)
	FROM fb_odds

	INSERT INTO #tmp_top_team
	SELECT top 8 t.team_name
	FROM fb_odds t
	WHERE t.odds_date = @top_date
	ORDER BY odds_probability DESC
	--select * from #tmp_top_team
	--INSERT INTO #tmp_top_team
	--SELECT DISTINCT top 10 t.team_name 
	--FROM fb_odds t
	--WHERE t.year_code = @year_code
	--and (@odds_date is null or t.odds_date = @odds_date)
	--and team_name = (SELECT top 10 t.team_name
	--					FROM fb_odds
	--					GROUP BY max(odds_date)
	--					ORDER BY odds_probability DESC)
	--ORDER BY t.team_name 
	

	SELECT t.team_name, 
	o.odds_probability,
	o.odds_date,
	o.odds_mean,
	o.odds_max,
	o.odds_min,
	o.odds_stdDev,
	o.odds_sample_size,
	o.year_code
	FROM fb_odds o
	INNER JOIN #tmp_team t ON o.team_name = t.team_name     
	INNER JOIN #tmp_top_team t2 on t.team_name = t2.team_name
	WHERE year_code = @year_code
	and (@odds_date is null or o.odds_date = @odds_date)
	and (@team_name is null or t.team_name = @team_name)
	ORDER BY o.odds_date, o.team_name

END
ELSE
BEGIN
	SELECT t.team_name, 
	o.odds_probability,
	o.odds_date,
	o.odds_mean,
	o.odds_max,
	o.odds_min,
	o.odds_stdDev,
	o.odds_sample_size,
	o.year_code
	FROM fb_odds o
	INNER JOIN #tmp_team t ON o.team_name = t.team_name     
	WHERE year_code = @year_code
	and (@odds_date is null or o.odds_date = @odds_date)
	and (@team_name is null or t.team_name = @team_name)
	ORDER BY o.odds_date, o.team_name
END