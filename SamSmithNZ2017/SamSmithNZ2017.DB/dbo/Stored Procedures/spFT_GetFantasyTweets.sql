CREATE PROCEDURE [dbo].[spFT_GetFantasyTweets]
	@date datetime = null
AS

SELECT *
FROM fantasy_tweet
WHERE (@date is null or tweet_date = @date)
ORDER BY tweet_date DESC, word