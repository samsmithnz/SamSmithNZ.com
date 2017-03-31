
CREATE VIEW [dbo].[vwWC_FinalPlacing]
AS

--1st Place
SELECT g1.tournament_code, 1 as placing, CASE WHEN gss1.goals_with_penalties > gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END as team_code
FROM wc_game g1
INNER JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
INNER JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
INNER JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
WHERE g1.round_code = 'FF'

UNION
 
--2nd Place
SELECT g1.tournament_code, 2 as placing, CASE WHEN gss1.goals_with_penalties < gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END as team_code
FROM wc_game g1
INNER JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
INNER JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
INNER JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
WHERE g1.round_code = 'FF'

UNION

--3rd Place
SELECT g1.tournament_code, 3 as placing, CASE WHEN gss1.goals_with_penalties > gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END as team_code
FROM wc_game g1
INNER JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
INNER JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
INNER JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
WHERE g1.round_code = '3P'

UNION

--4th Place
SELECT g1.tournament_code, 4 as placing, CASE WHEN gss1.goals_with_penalties < gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END as team_code
FROM wc_game g1
INNER JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
INNER JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
INNER JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
WHERE g1.round_code = '3P'

UNION

--5th - 8th Place
SELECT g1.tournament_code, 5 as placing, CASE WHEN gss1.goals_with_penalties < gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END as team_code
FROM wc_game g1
INNER JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
INNER JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
INNER JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
WHERE g1.round_code = 'QF' and g1.round_code not in ('3P', 'FF')

UNION

--9th - 16th Place
SELECT g1.tournament_code, 9 as placing, CASE WHEN gss1.goals_with_penalties < gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END as team_code
FROM wc_game g1
INNER JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
INNER JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
INNER JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
WHERE g1.round_code = '16' and g1.round_code not in ('QF', '3P', 'FF')

UNION

--17th - 32nd Place: Part 1 (Team 1)
SELECT DISTINCT g1.tournament_code, 17 as placing, g1.team_1_code as team_code
FROM wc_game g1
WHERE g1.round_code not in ('16', 'QF', '3P', 'FF')

UNION

--17th - 32nd Place: Part 2 (Team 2)
SELECT DISTINCT g2.tournament_code, 17 as placing, g2.team_2_code as team_code
FROM wc_game g2
WHERE g2.round_code not in ('16', 'QF', '3P', 'FF')