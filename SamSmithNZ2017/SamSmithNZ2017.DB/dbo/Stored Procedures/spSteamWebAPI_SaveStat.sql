CREATE PROCEDURE dbo.spSteamWebAPI_SaveStat
	@batch_id uniqueidentifier,
	@api_url varchar(1000)
AS
INSERT INTO steamAPI_stat
SELECT @batch_id, @api_url, getdate()