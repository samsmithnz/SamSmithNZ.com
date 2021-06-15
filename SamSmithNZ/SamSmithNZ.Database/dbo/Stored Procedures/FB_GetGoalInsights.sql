CREATE PROCEDURE [dbo].[FB_GetGoalInsights]
	@AnalyzeExtraTime BIT = 0
AS
BEGIN
	DECLARE @totalCount DECIMAL(5,1)
	SELECT @totalCount = CONVERT(DECIMAL(5,1),COUNT(goal_code))
	FROM wc_goal
	WHERE (@AnalyzeExtraTime = 0 AND goal_time <= 90) OR (@AnalyzeExtraTime = 1 AND goal_time > 90)

	SELECT CASE WHEN NOT injury_time IS NULL THEN CONVERT(DECIMAL(5,1),goal_time) + 0.5 ELSE CONVERT(DECIMAL(5,1),goal_time) END AS GoalTime,
		COUNT(goal_code) AS GoalCount,
		CONVERT(DECIMAL(5,1),COUNT(goal_code)) / @totalCount * 100 AS GoalCountPercent
	FROM wc_goal
	WHERE (@AnalyzeExtraTime = 0 AND goal_time <= 90) OR (@AnalyzeExtraTime = 1 AND goal_time > 90)
	GROUP BY CASE WHEN NOT injury_time IS NULL THEN CONVERT(DECIMAL(5,1),goal_time) + 0.5 ELSE CONVERT(DECIMAL(5,1),goal_time) END
	ORDER BY CASE WHEN NOT injury_time IS NULL THEN CONVERT(DECIMAL(5,1),goal_time) + 0.5 ELSE CONVERT(DECIMAL(5,1),goal_time) END
END
GO