
CREATE PROCEDURE [dbo].[spFB3_AddNewPlayer]
	@player_name varchar(50),
	@employee_code char(5)
AS

DECLARE @current_year_code smallint
DECLARE @current_week_code smallint
DECLARE @new_player_code smallint

IF (@employee_code = '') 
	SELECT @employee_code = null

SELECT @current_year_code = current_year_code, @current_week_code = current_week_code
FROM fbsettings

SELECT @new_player_code = isnull(max(player_code) + 1, 1)
FROM FBPlayer 
WHERE year_code = @current_year_code

INSERT INTO FBPlayer
SELECT @current_year_code, @new_player_code, @player_name, null, @employee_code, 0

INSERT INTO FBWeek
SELECT @current_year_code, @new_player_code, @current_week_code, record_id, -1, -1, getdate()
FROM FBWeekTemplate
WHERE year_code = @current_year_code 
and week_code = @current_week_code