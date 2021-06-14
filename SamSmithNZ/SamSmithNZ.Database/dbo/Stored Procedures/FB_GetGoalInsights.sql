CREATE PROCEDURE [dbo].[FB_GetGoalInsights]
AS
BEGIN
	SELECT CASE WHEN NOT injury_time IS NULL THEN CONVERT(decimal(5,1),goal_time) + 0.5 ELSE CONVERT(decimal(5,1),goal_time) END AS goal_time,
		count(goal_code) AS goal_count
	FROM wc_goal
	GROUP BY CASE WHEN NOT injury_time IS NULL THEN CONVERT(decimal(5,1),goal_time) + 0.5 ELSE CONVERT(decimal(5,1),goal_time) END
	ORDER BY CASE WHEN NOT injury_time IS NULL THEN CONVERT(decimal(5,1),goal_time) + 0.5 ELSE CONVERT(decimal(5,1),goal_time) END
END
GO