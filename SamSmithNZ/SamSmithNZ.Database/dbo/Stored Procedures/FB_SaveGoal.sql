CREATE PROCEDURE [dbo].[FB_SaveGoal]
	@GoalCode INT,
	@GameCode INT,
	@PlayerCode INT,
	@GoalTime INT,
	@InjuryTime INT,
	@IsPenalty BIT, 
	@IsOwnGoal BIT,
	@IsGoldenGoal BIT
AS

IF (@GoalCode = 0)
BEGIN
	--Get the new primary key
	SELECT @GoalCode = MAX(goal_code) + 1 FROM wc_goal
	--insert the new goal record
	INSERT INTO wc_goal
	SELECT @GoalCode, @GameCode, @PlayerCode, 
		@GoalTime, CASE WHEN @InjuryTime = 0 THEN NULL ELSE @InjuryTime END, 
		@IsPenalty, @IsOwnGoal, @IsGoldenGoal
END
ELSE --We are updating an existing record
BEGIN
	UPDATE wc_goal
	SET game_code = @GameCode, 
		player_code = @PlayerCode, 
		goal_time = @GoalTime, 
		injury_time = CASE WHEN @InjuryTime = 0 THEN NULL ELSE @InjuryTime END, 
		is_penalty = @IsPenalty, 
		is_own_goal = @IsOwnGoal,
		is_golden_goal = @IsGoldenGoal
	WHERE goal_code = @GoalCode
END