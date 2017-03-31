CREATE PROCEDURE [dbo].[spValor_AddClaim]
	@world_num int,
	@x int,
	@y int,
	@owner varchar(100),
	@added_by varchar(100),
	@guild_code int = 0,
	@last_updated datetime

AS
INSERT INTO valor_claim
SELECT isnull((SELECT max(claim_code) + 1 FROM valor_claim),1) as claim_code,
	@world_num, @x, @y, @owner, @added_by, @last_updated, @guild_code