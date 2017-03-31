CREATE PROCEDURE [dbo].[spWC_SaveGoal]
	@goal_code smallint,
	@game_code smallint,
	@player_code smallint,
	@goal_time smallint,
	@injury_time smallint,
	@is_penalty bit, 
	@is_own_goal bit
AS

IF (@goal_code = 0)
BEGIN
	--Get the new primary key
	SELECT @goal_code = max(goal_code) + 1 FROM wc_goal
	--insert the new goal record
	INSERT INTO wc_goal
	SELECT @goal_code, @game_code, @player_code, 
		@goal_time, CASE WHEN @injury_time = 0 THEN null ELSE @injury_time END, 
		@is_penalty, @is_own_goal
END
ELSE --We are updating an existing record
BEGIN
	UPDATE wc_goal
	SET game_code = @game_code, player_code = @player_code, goal_time = @goal_time, injury_time = CASE WHEN @injury_time = 0 THEN null ELSE @injury_time END, is_penalty = @is_penalty, is_own_goal = @is_own_goal
	WHERE goal_code = @goal_code
END