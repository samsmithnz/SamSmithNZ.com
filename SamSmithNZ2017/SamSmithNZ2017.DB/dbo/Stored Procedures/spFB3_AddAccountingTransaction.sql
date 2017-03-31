CREATE PROCEDURE dbo.spFB3_AddAccountingTransaction
	@year_code smallint,
	@week_code smallint,
	@player_code smallint,
	@amount money,
	@transaction_type_code smallint
AS

INSERT INTO FBAccountingTransaction
SELECT @year_code, @week_code, @player_code, @amount, @transaction_type_code, getdate()