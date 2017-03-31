CREATE PROCEDURE [dbo].[spFT_AddFantasyTweet]
	@date datetime,
	@tweet varchar(140)
AS

INSERT INTO fantasy_tweet
SELECT @date, @tweet