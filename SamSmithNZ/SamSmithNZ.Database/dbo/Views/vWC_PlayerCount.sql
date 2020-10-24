CREATE VIEW [dbo].[vWC_PlayerCount]
AS
SELECT t.tournament_code, COUNT(g.tournament_code) AS player_count
FROM wc_tournament t
LEFT JOIN wc_player g ON g.tournament_code = t.tournament_code
GROUP BY t.tournament_code