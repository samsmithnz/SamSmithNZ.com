CREATE PROCEDURE [dbo].[FB_GetTournaments]
	@CompetitionCode INT = NULL,
	@TournamentCode INT = NULL
AS
BEGIN
	DECLARE @CurrentTime DATETIME 
	SELECT @CurrentTime = GETDATE()

	SELECT t.competition_code AS CompetitionCode, 
		t.tournament_code AS TournamentCode, 
		[year] AS TournamentYear, 
		[name] AS TournamentName, 
		ISNULL(t.host_team_code,0) AS HostTeamCode, 
		te.team_name AS HostTeamName, 
		ISNULL(te.flag_name,'') AS HostFlagName, 
		ISNULL(t.co_host_team_code,0) AS CoHostTeamCode, 
		te2.team_name AS CoHostTeamName, 
		ISNULL(te2.flag_name,'') AS CoHostFlagName,
		ISNULL(t.co_host_team_code2,0) AS CoHostTeamCode2, 
		te3.team_name AS CoHostTeamName2, 
		ISNULL(te3.flag_name,'') AS CoHostFlagName2,
		CONVERT(VARCHAR(8000),t.notes) AS Notes, 
		t.logo_image AS LogoImage, 
		t.qualification_image AS QualificationImage,	
		ISNULL((SELECT MIN(ga1.game_time) FROM wc_game ga1 WHERE ga1.tournament_code = t.tournament_code),NULL) AS MinGameTime,
		ISNULL((SELECT MAX(ga2.game_time) FROM wc_game ga2 WHERE ga2.tournament_code = t.tournament_code),NULL) AS MaxGameTime,
		tf.format_code AS FormatCode,
		r1_format_round_code AS R1FormatRoundCode, 
		r1_is_group_stage AS R1IsGroupStage, 
		r1_number_of_teams_in_group AS R1NumberOfTeamsInGroup, 
		r1_number_of_groups_in_round AS R1NumberOfGroupsInRound, 
		r1_number_of_teams_from_group_that_advance AS R1NumberOfTeamsFromGroupThatAdvance, 
		r1_total_number_of_teams_that_advance AS R1TotalNumberOfTeamsThatAdvance,
		ISNULL((SELECT TOP 1 gr1.round_code FROM wc_group_stage gr1 WHERE gr1.tournament_code = t.tournament_code AND gr1.round_number = 1 ORDER BY gr1.round_code),'') AS R1FirstGroupCode,
		r2_format_round_code AS R2FormatRoundCode, 
		r2_is_group_stage AS R2IsGroupStage, 
		r2_number_of_teams_in_group AS R2NumberOfTeamsInGroup, 
		r2_number_of_groups_in_round AS R2NumberOfGroupsInRound, 
		r2_number_of_teams_from_group_that_advance AS R2NumberOfTeamsFromGroupThatAdvance, 
		r2_total_number_of_teams_that_advance AS R2TotalNumberOfTeamsThatAdvance,
		ISNULL((SELECT TOP 1 gr2.round_code FROM wc_group_stage gr2 WHERE gr2.tournament_code = t.tournament_code AND gr2.round_number = 2 ORDER BY gr2.round_code),'') AS R2FirstGroupCode,
		r3_format_round_code AS R3FormatRoundCode, 
		r3_is_group_stage AS R3IsGroupStage, 
		r3_number_of_teams_in_group AS R3NumberOfTeamsInGroup, 
		r3_number_of_groups_in_round AS R3NumberOfGroupsInRound, 
		r3_number_of_teams_from_group_that_advance AS R3NumberOfTeamsFromGroupThatAdvance, 
		r3_total_number_of_teams_that_advance AS R3TotalNumberOfTeamsThatAdvance,
		ISNULL((SELECT TOP 1 gr3.round_code FROM wc_group_stage gr3 WHERE gr3.tournament_code = t.tournament_code AND gr3.round_number = 3 ORDER BY gr3.round_code),'') AS R3FirstGroupCode
	FROM wc_tournament t
	LEFT JOIN wc_team te ON te.team_code = t.host_team_code
	LEFT JOIN wc_team te2 ON te2.team_code = t.co_host_team_code
	LEFT JOIN wc_team te3 ON te3.team_code = t.co_host_team_code2
	LEFT JOIN wc_game g ON t.tournament_code = g.tournament_code
	JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
	WHERE (t.tournament_code = @TournamentCode OR @TournamentCode IS NULL)
	AND (t.competition_code = @CompetitionCode OR @CompetitionCode IS NULL)
	AND t.competition_code != 4
	GROUP BY t.competition_code, 
		t.tournament_code, 
		[year] , 
		[name], 
		ISNULL(t.host_team_code,0), 
		te.team_name, 
		ISNULL(te.flag_name,''), 
		ISNULL(t.co_host_team_code,0), 
		te2.team_name, 
		ISNULL(te2.flag_name,''),
		ISNULL(t.co_host_team_code2,0), 
		te3.team_name, 
		ISNULL(te3.flag_name,''),
		CONVERT(VARCHAR(8000),t.notes), 
		t.logo_image, 
		t.qualification_image,
		tf.format_code,
		r1_format_round_code, 
		r1_is_group_stage, 
		r1_number_of_teams_in_group, 
		r1_number_of_groups_in_round, 
		r1_number_of_teams_from_group_that_advance, 
		r1_total_number_of_teams_that_advance,
		r2_format_round_code, 
		r2_is_group_stage, 
		r2_number_of_teams_in_group, 
		r2_number_of_groups_in_round, 
		r2_number_of_teams_from_group_that_advance, 
		r2_total_number_of_teams_that_advance,
		r3_format_round_code, 
		r3_is_group_stage, 
		r3_number_of_teams_in_group, 
		r3_number_of_groups_in_round, 
		r3_number_of_teams_from_group_that_advance, 
		r3_total_number_of_teams_that_advance
	ORDER BY [year] DESC
END
GO