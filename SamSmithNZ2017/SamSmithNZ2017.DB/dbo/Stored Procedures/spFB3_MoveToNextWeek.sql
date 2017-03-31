


CREATE PROCEDURE [dbo].[spFB3_MoveToNextWeek]
	@year_code smallint
AS

BEGIN TRAN

DECLARE @current_week_code smallint
DECLARE @new_week_code smallint

SELECT @current_week_code = current_week_code
FROM FBSettings

--Move to next week
UPDATE FBSettings 
SET current_week_code = current_week_code + 1 
FROM FBSettings

SELECT @new_week_code = current_week_code 
FROM FBSettings

SELECT @current_week_code, player_code, weekly_dollars_won
FROM FBSummary
WHERE year_code = @year_code and weekly_dollars_won > 0

--Clear Summary Table.
--UPDATE FBSummary 
--SET wins_this_week = 0, weekly_dollars_won = 0, ytd_dollars_won = 0

--Create the new week template
DECLARE @new_date smalldatetime
SET @new_date = getdate()

exec spFB3_AddNewWeekTemplate @year_code, @new_week_code, @new_date

DECLARE @player_code smallint

DECLARE Cursor1 CURSOR LOCAL FOR
	SELECT DISTINCT player_code
	FROM FBPlayer
	WHERE year_code = @year_code

OPEN Cursor1

--loop through all the items
FETCH NEXT FROM Cursor1 INTO @player_code
WHILE (@@FETCH_STATUS <> -1)
BEGIN
	--Add the new weeks records.
	INSERT INTO FBWeek
	SELECT @year_code, @player_code, @new_week_code, record_id, -1, 0, null --where -1 is nothing selected
	FROM FBWeekTemplate 
	WHERE year_code = @year_code and week_code = @new_week_code
	ORDER BY game_time

	FETCH NEXT FROM Cursor1 INTO @player_code
END

CLOSE Cursor1
DEALLOCATE Cursor1

exec spFB3_UpdateStatus @year_code, @new_week_code, 25 --The Donut cost

COMMIT TRAN