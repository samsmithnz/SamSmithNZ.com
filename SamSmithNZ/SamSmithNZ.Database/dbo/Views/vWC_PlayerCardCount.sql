CREATE VIEW [dbo].[vWC_PlayerCardCount]
AS
SELECT t.tournament_code, ISNULL(COUNT(c.game_code),0) AS player_card_count
FROM wc_tournament t
LEFT JOIN wc_game g ON g.tournament_code = t.tournament_code
LEFT JOIN wc_card c ON c.game_code = g.game_code
GROUP BY t.tournament_code