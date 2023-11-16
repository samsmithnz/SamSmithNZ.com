CREATE PROCEDURE [dbo].[FB_DeleteGoal]
	@GoalCode INT
AS
BEGIN
	DELETE g 
	FROM wc_goal g
	WHERE goal_code = @GoalCode
END