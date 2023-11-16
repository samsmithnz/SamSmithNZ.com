CREATE VIEW [dbo].[vWC_GameCount]
AS
SELECT t.tournament_code, COUNT(g.tournament_code) AS game_count
FROM wc_tournament t
LEFT JOIN wc_game g ON g.tournament_code = t.tournament_code
GROUP BY t.tournament_code