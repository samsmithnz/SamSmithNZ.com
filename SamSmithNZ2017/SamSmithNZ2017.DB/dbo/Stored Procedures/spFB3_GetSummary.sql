CREATE PROCEDURE [dbo].[spFB3_GetSummary]
 @year_code smallint,
 @week_code smallint,
 @order_by smallint 
AS

IF (@order_by = 0)
BEGIN
 DECLARE @max_weeks_played smallint
 SELECT @max_weeks_played = max(weeks_played) 
 FROM FBSummary
 WHERE year_code = @year_code and week_code = @week_code

 --IF (@week_code > 1)
 --BEGIN
 SELECT p.player_name, p.player_code, s.weeks_played, s.wins_this_week, s.YTD_wins, s.YTD_losses, s.YTD_win_percentage, 
 --isnull(m.ytd_total,0) 
 --0 as weekly_dollars_won,
 --isnull(sum(isnull(m2.ytd_total,0)),0) as ytd_dollars_won, 
 isnull(s.weekly_dollars_won,0) as weekly_dollars_won, s.ytd_dollars_won,
 s.ranking, isnull(s2.ranking,0) as previous_ranking, --s.ranking as previous_ranking 
 isnull(p.employee_code,'') as employee_code, p.is_celebrity, s.last_updated,
  STUFF(
   (SELECT ', ' + convert(varchar(3), ssp.ranking)
   FROM FBSummary ssp 
   WHERE ssp.year_code = @year_code
   and ssp.player_code = p.player_code
   ORDER BY ssp.week_code
   FOR XML PATH ('')), 1, 1, '') AS ranking_sparkline,
  STUFF(
   (SELECT ', ' + convert(varchar(6), ssp.YTD_win_percentage)
   FROM FBSummary ssp 
   WHERE ssp.year_code = @year_code
   and ssp.player_code = p.player_code
   ORDER BY ssp.week_code
   FOR XML PATH ('')), 1, 1, '') AS YTD_sparkline
 FROM FBSummary s
 INNER JOIN FBPlayer p ON s.player_code = p.player_code 
 --LEFT JOIN FBMoney m ON s.player_code = m.player_code and m.week_code = @week_code
 --LEFT JOIN FBMoney m2 ON s.player_code = m2.player_code and m2.week_code <= @week_code
 LEFT JOIN FBSummary s2 ON s.player_code = s2.player_code and s2.week_code = (s.week_code - 1) and s2.year_code = @year_code
 WHERE s.year_code = @year_code and p.year_code = @year_code and s.week_code = @week_code --and p.player_code = 53
 GROUP BY p.player_name, p.player_code, s.ytd_dollars_won, s.weeks_played, s.wins_this_week, s.YTD_wins, s.YTD_losses, 
 s.YTD_win_percentage, s.weekly_dollars_won, s.ranking, s2.ranking, p.employee_code, p.is_celebrity, s.last_updated
 ORDER BY case when s.weeks_played >= (@max_weeks_played-1) then 0 else 1 end, --To filter out people who have missed 2 or more games.
 s.YTD_win_percentage DESC, s.wins_this_week DESC, s.weeks_played DESC, s.YTD_wins DESC, p.player_name
 /*END
 ELSE
 BEGIN
 SELECT p.player_name, weeks_played, wins_this_week, YTD_wins, YTD_losses, YTD_win_percentage, 
 isnull(m.ytd_total,0) as weekly_dollars_won, 
 isnull(sum(isnull(m2.ytd_total,0)),0) as ytd_dollars_won, 
 ranking, 0 as previous_ranking
 FROM FBSummary s
 INNER JOIN FBPlayer p ON s.player_code = p.player_code
 LEFT JOIN FBMoney m ON s.player_code = m.player_code and m.week_code = @week_code
 LEFT JOIN FBMoney m2 ON s.player_code = m2.player_code and m2.week_code <= @week_code
 WHERE s.week_code = @week_code
 GROUP BY p.player_name, ytd_dollars_won, weeks_played, wins_this_week, YTD_wins, YTD_losses, YTD_win_percentage, weekly_dollars_won, s.ranking, m.ytd_total
 ORDER BY case when weeks_played >= (@max_weeks_played-1) then 0 else 1 end, --To filter out people who have missed 2 or more games.
 YTD_win_percentage DESC, wins_this_week DESC, weeks_played DESC, YTD_wins DESC, player_name
 END*/
END
ELSE
BEGIN
 SELECT p.player_name, s.player_code, weeks_played, wins_this_week, YTD_wins, YTD_losses, YTD_win_percentage, 
 --isnull(m.ytd_total,0)  
 --0 as weekly_dollars_won, 
 --isnull(sum(isnull(m2.ytd_total,0)),0) as ytd_dollars_won, 
 isnull(s.weekly_dollars_won,0) as weekly_dollars_won, s.ytd_dollars_won,
 --s.ranking, 
 Rank() over (ORDER BY s.wins_this_week DESC) as ranking,
 0 as previous_ranking, isnull(p.employee_code,'') as employee_code, p.is_celebrity, s.last_updated,
  STUFF(
   (SELECT ', ' + convert(varchar(3), ssp.ranking)
   FROM FBSummary ssp 
   WHERE ssp.year_code = @year_code
   and ssp.player_code = s.player_code
   ORDER BY ssp.week_code
   FOR XML PATH ('')), 1, 1, '') AS ranking_sparkline,
  STUFF(
   (SELECT ', ' + convert(varchar(5), ssp.YTD_win_percentage)
   FROM FBSummary ssp 
   WHERE ssp.year_code = @year_code
   and ssp.player_code = s.player_code
   ORDER BY ssp.week_code
   FOR XML PATH ('')), 1, 1, '') AS YTD_sparkline
 FROM FBSummary s
 INNER JOIN FBPlayer p ON s.player_code = p.player_code
 --LEFT JOIN FBMoney m ON s.player_code = m.player_code and m.week_code = @week_code
 --LEFT JOIN FBMoney m2 ON s.player_code = m2.player_code and m2.week_code <= @week_code
 WHERE s.year_code = @year_code and s.week_code = @week_code and p.year_code = @year_code
 GROUP BY p.player_name, s.player_code, ytd_dollars_won, weeks_played, wins_this_week, YTD_wins, YTD_losses, 
 YTD_win_percentage, weekly_dollars_won, s.ranking, p.employee_code, p.is_celebrity, s.last_updated --, m.ytd_total
 ORDER BY wins_this_week DESC, p.player_name, YTD_win_percentage DESC, weeks_played DESC, YTD_wins DESC
END