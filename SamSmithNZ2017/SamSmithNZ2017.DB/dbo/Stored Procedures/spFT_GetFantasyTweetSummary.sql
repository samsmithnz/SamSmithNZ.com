CREATE PROCEDURE [dbo].[spFT_GetFantasyTweetSummary]
AS

SELECT word, count(word) as word_count
FROM fantasy_tweet
WHERE len(word) > 3
GROUP BY word
ORDER BY word_count DESC, word