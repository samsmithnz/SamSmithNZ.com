CREATE VIEW [dbo].[vWC_PlayerCount]
AS
SELECT t.tournament_code, count(g.tournament_code) as player_count
FROM wc_tournament t
LEFT OUTER JOIN wc_player g ON g.tournament_code = t.tournament_code
GROUP BY t.tournament_code