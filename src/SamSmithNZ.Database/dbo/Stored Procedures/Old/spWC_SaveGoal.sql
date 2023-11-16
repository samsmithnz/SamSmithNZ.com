CREATE PROCEDURE [dbo].[spWC_SaveGoal]
	@goal_code INT,
	@game_code INT,
	@player_code INT,
	@goal_time INT,
	@injury_time INT,
	@is_penalty bit, 
	@is_own_goal bit
AS

IF (@goal_code = 0)
BEGIN
	--Get the new primary key
	SELECT @goal_code = MAX(goal_code) + 1 FROM wc_goal
	--insert the new goal record
	INSERT INTO wc_goal
	SELECT @goal_code, @game_code, @player_code, 
		@goal_time, CASE WHEN @injury_time = 0 THEN NULL ELSE @injury_time END, 
		@is_penalty, @is_own_goal
END
ELSE --We are updating an existing record
BEGIN
	UPDATE wc_goal
	SET game_code = @game_code, player_code = @player_code, goal_time = @goal_time, injury_time = CASE WHEN @injury_time = 0 THEN NULL ELSE @injury_time END, is_penalty = @is_penalty, is_own_goal = @is_own_goal
	WHERE goal_code = @goal_code
END