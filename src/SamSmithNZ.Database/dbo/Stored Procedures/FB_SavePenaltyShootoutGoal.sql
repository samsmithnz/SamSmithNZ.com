CREATE PROCEDURE [dbo].[FB_SavePenaltyShootoutGoal]
	@PenaltyCode INT,
	@GameCode INT,
	@PlayerCode INT,
	@PenaltyOrder INT,
	@Scored bit
AS

IF (@PenaltyCode = 0)
BEGIN 
	--Get the new primary key
	SELECT @PenaltyCode = MAX(penalty_code) + 1 FROM wc_penalty_shootout
	IF (@PenaltyCode is NULL)
	BEGIN
		SELECT @PenaltyCode = 1
	END
	--insert the new wc_penalty_shootout record
	INSERT INTO wc_penalty_shootout
	SELECT @PenaltyCode, @GameCode, @PlayerCode, @PenaltyOrder, @Scored
END
ELSE --We are updating an existing record
BEGIN
	UPDATE wc_penalty_shootout
	SET game_code = @GameCode, player_code = @PlayerCode, penalty_order = @PenaltyOrder, scored = @Scored
	WHERE penalty_code = @PenaltyCode
END