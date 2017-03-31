CREATE PROCEDURE dbo.spFB3_GetAccountingTransactionList
	@year_code smallint,
	@player_code smallint = null
AS
SELECT at.*, p.player_name, att.accounting_transaction_type_name
FROM FBAccountingTransaction at
INNER JOIN FBPlayer p ON at.year_code = p.year_code and at.player_code = p.player_code
INNER JOIN FBAccountingTransactionType att ON att.accounting_transaction_type_code = at.transaction_type_code
WHERE at.year_code = @year_code
and (at.player_code = @player_code or @player_code is null)
ORDER BY year_code, last_updated desc