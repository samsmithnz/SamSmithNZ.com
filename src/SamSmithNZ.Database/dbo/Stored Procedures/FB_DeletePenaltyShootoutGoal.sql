CREATE PROCEDURE [dbo].[FB_DeletePenaltyShootoutGoal]
	@PenaltyCode INT
AS
BEGIN
	DELETE p
	FROM wc_penalty_shootout p
	WHERE penalty_code = @PenaltyCode
END