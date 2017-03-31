CREATE PROCEDURE dbo.spFB3_DeleteAccountingTransaction
	@year_code smallint,
	@week_code smallint,
	@player_code smallint,
	@transaction_type_code smallint
AS

DELETE FROM FBAccountingTransaction
WHERE year_code = @year_code
and week_code = @week_code
and player_code = @player_code
and transaction_type_code = @transaction_type_code