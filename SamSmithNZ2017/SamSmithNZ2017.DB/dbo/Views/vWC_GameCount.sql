CREATE VIEW [dbo].[vWC_GameCount]
AS
SELECT t.tournament_code, count(g.tournament_code) as game_count
FROM wc_tournament t
LEFT OUTER JOIN wc_game g ON g.tournament_code = t.tournament_code
GROUP BY t.tournament_code