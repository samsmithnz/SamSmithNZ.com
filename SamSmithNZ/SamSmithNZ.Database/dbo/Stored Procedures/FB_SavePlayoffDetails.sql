CREATE PROCEDURE [dbo].[FB_SavePlayoffDetails]
	@TournamentCode INT,
	@RoundNumber INT,
	@RoundCode VARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @number_of_teams_in_group INT

	SELECT @number_of_teams_in_group = tfr.number_of_teams_in_group 
	FROM wc_tournament t
	JOIN wc_tournament_format tf ON t.format_code = tf.format_code
	JOIN wc_tournament_format_round tfr ON tfr.format_round_code = tf.round_2_format_code
	WHERE tournament_code = @TournamentCode

	DECLARE @game_count INT
	SELECT @game_count = COUNT(*) 
	FROM wc_game
	WHERE tournament_code = @TournamentCode
	and round_number = @RoundNumber
	and round_code = @RoundCode

	--If the number of games doesn't audit correctly, add some new games
	IF (@number_of_teams_in_group / 2 <> @game_count)
	BEGIN
		SELECT g.tournament_code,
			g.game_code,
			g.game_number,
			g.game_time,
			g.[location],
			g.round_code,
			g.round_number,
			g.team_1_code,
			g.team_1_extra_time_score,
			g.team_1_normal_time_score,
			g.team_1_penalties_score,
			g.team_2_code,
			g.team_2_extra_time_score,
			g.team_2_normal_time_score,
			g.team_2_penalties_score
		FROM wc_game g
		WHERE tournament_code = @TournamentCode
		and round_number = @RoundNumber
		and round_code = @RoundCode
	END

	IF (@TournamentCode = 21)
	BEGIN
		--Update the next games
		--Update the playoff's if the group is done.
		DECLARE @Team1Score INT
		DECLARE @Team2Score INT
		DECLARE @GameNumber INT

		DECLARE Cursor1 CURSOR LOCAL FOR
			SELECT g.round_code, 
				SUM(g.team_1_normal_time_score) + SUM(ISNULL(g.team_1_extra_time_score,0)) + SUM(ISNULL(g.team_1_penalties_score,0)),
				SUM(g.team_2_normal_time_score) + SUM(ISNULL(g.team_2_extra_time_score,0)) + SUM(ISNULL(g.team_2_penalties_score,0)), 
				g.game_number
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber
			GROUP BY g.round_code, g.game_number
			ORDER BY g.game_number

		OPEN Cursor1

		--loop through all the items
		FETCH NEXT FROM Cursor1 INTO @RoundCode, @Team1Score, @Team2Score, @GameNumber
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			
			DECLARE @WinningTeamCode INT
			DECLARE @LosingTeamCode INT
			
			IF (@Team1Score > @Team2Score)
			BEGIN
				SELECT @WinningTeamCode = g.team_1_code, @LosingTeamCode = g.team_2_code
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = @GameNumber

			END
			ELSE IF (@Team1Score < @Team2Score)
			BEGIN
				SELECT @WinningTeamCode = g.team_2_code, @LosingTeamCode = g.team_1_code
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = @GameNumber
			END
			ELSE 
			BEGIN
				SELECT @WinningTeamCode = 0, @LosingTeamCode = 0
			END

			--Keep the winning team in the competition
			UPDATE te
			SET te.is_active = 1
			FROM wc_tournament_team_entry te
			WHERE te.tournament_code = @TournamentCode
			AND te.team_code = @WinningTeamCode

			--If the 3rd place and final games are done, mark them as done.
			IF ((@RoundCode = '3P' OR @RoundCode = 'FF') AND @WinningTeamCode > 0 AND @LosingTeamCode > 0)
			BEGIN
				UPDATE te
				SET te.is_active = 0
				FROM wc_tournament_team_entry te
				WHERE te.tournament_code = @TournamentCode
				AND te.team_code = @WinningTeamCode
			END

			--Remove the losing team	
			UPDATE te
			SET te.is_active = 0
			FROM wc_tournament_team_entry te
			WHERE te.tournament_code = @TournamentCode
			AND te.team_code = @LosingTeamCode

			UPDATE c
			SET c.chance_to_win = CONVERT(DECIMAL(6,4), 0)
			FROM wc_tournament_team_chance_to_win c
			WHERE c.tournament_code = @TournamentCode
			AND c.team_code = @LosingTeamCode

			--Quarter-finals
			--Winners Match 49	Match 57	Winners Match 50		
			IF (@GameNumber = 49) 
			BEGIN
				--select 'game 57', @WinningTeamCode, @LosingTeamCode
				UPDATE g
				SET team_1_code = @WinningTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 57
			END	
			ELSE IF (@GameNumber = 50) 
			BEGIN
				UPDATE g
				SET team_2_code = @WinningTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 57
			END
			--Winners Match 53	Match 58	Winners Match 54		
			IF (@GameNumber = 53) 
			BEGIN
				UPDATE g
				SET team_1_code = @WinningTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 58
			END	
			ELSE IF (@GameNumber = 54) 
			BEGIN
				UPDATE g
				SET team_2_code = @WinningTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 58
			END
			--Winners Match 55	Match 60	Winners Match 56		
			IF (@GameNumber = 55) 
			BEGIN
				UPDATE g
				SET team_1_code = @WinningTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 60
			END	
			ELSE IF (@GameNumber = 56) 
			BEGIN
				UPDATE g
				SET team_2_code = @WinningTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 60
			END
			--Winners Match 51	Match 59	Winners Match 52		
			IF (@GameNumber = 51) 
			BEGIN
				UPDATE g
				SET team_1_code = @WinningTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 59
			END	
			ELSE IF (@GameNumber = 52) 
			BEGIN
				UPDATE g
				SET team_2_code = @WinningTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 59
			END

			--Semi-finals
			--Winners Match 57	Match 61	Winners Match 58		
			IF (@GameNumber = 57) 
			BEGIN
				UPDATE g
				SET team_1_code = @WinningTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 61
			END	
			ELSE IF (@GameNumber = 58) 
			BEGIN
				UPDATE g
				SET team_2_code = @WinningTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 61
			END
			--Winners Match 59	Match 62	Winners Match 60		
			IF (@GameNumber = 59) 
			BEGIN
				UPDATE g
				SET team_1_code = @WinningTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 62
			END	
			ELSE IF (@GameNumber = 60) 
			BEGIN
				UPDATE g
				SET team_2_code = @WinningTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 62
			END

			--Final
			--Winners Match 61	Match 64	Winners Match 62
			--Third place play-off
			--Losers Match 61	Match 63	Losers Match 62					
			IF (@GameNumber = 61) 
			BEGIN
				UPDATE g
				SET team_1_code = @WinningTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 64

				UPDATE g
				SET team_1_code = @LosingTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 63
			END	
			ELSE IF (@GameNumber = 62) 
			BEGIN
				UPDATE g
				SET team_2_code = @WinningTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 64

				UPDATE g
				SET team_2_code = @LosingTeamCode
				FROM wc_game g
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = 63
			END
	
			FETCH NEXT FROM Cursor1 INTO @RoundCode, @Team1Score, @Team2Score, @GameNumber
		END

		CLOSE Cursor1
		DEALLOCATE Cursor1

	END

END