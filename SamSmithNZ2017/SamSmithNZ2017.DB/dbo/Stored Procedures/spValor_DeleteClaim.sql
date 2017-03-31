CREATE PROCEDURE [dbo].[spValor_DeleteClaim]
	@claim_code int
AS
DELETE
FROM valor_claim
WHERE (claim_code = @claim_code)