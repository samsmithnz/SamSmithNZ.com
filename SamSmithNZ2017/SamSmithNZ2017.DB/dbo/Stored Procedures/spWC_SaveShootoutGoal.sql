CREATE PROCEDURE [dbo].[spWC_SaveShootoutGoal]
	@penalty_code smallint,
	@game_code smallint,
	@player_code smallint,
	@penalty_order smallint,
	@scored bit
AS

IF (@penalty_code = 0)
BEGIN 
	--Get the new primary key
	SELECT @penalty_code = max(penalty_code) + 1 FROM wc_penalty_shootout --WHERE game_code = @game_code
	IF (@penalty_code is null)
	BEGIN
		SELECT @penalty_code = 1
	END
	--insert the new wc_penalty_shootout record
	INSERT INTO wc_penalty_shootout
	SELECT @penalty_code, @game_code, @player_code, 
		@penalty_order, @scored
END
ELSE --We are updating an existing record
BEGIN
	UPDATE wc_penalty_shootout
	SET game_code = @game_code, player_code = @player_code, penalty_order = @penalty_order, scored = @scored
	WHERE penalty_code = @penalty_code
END