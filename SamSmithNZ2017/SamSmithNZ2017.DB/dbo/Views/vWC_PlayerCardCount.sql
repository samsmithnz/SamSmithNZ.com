CREATE VIEW [dbo].[vWC_PlayerCardCount]
AS
SELECT t.tournament_code, isnull(count(c.game_code),0) as player_card_count
FROM wc_tournament t
LEFT OUTER JOIN wc_game g ON g.tournament_code = t.tournament_code
LEFT OUTER JOIN wc_card c ON c.game_code = g.game_code
GROUP BY t.tournament_code