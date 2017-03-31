CREATE PROCEDURE [dbo].[spFB2_AddNewWeekTemplate]
	@year_code smallint,
	@week_code smallint,
	@start_date smalldatetime
AS

INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--1
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--2
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--3
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--4
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--5
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--6
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--7
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--8
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--9
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--10
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--11
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--12
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--13
--Extra Games
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--14
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--15
INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--16

/*IF (@week_code <> 7 and @week_code <> 8 and @week_code <> 9) --weeks with only 13 games
BEGIN
	INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--14

	IF (@week_code <> 4 and @week_code <> 5 and @week_code <> 6) --weeks with only 14 games
	BEGIN
		INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--15

		IF (@week_code <> 10) --weeks with only 15 games
		BEGIN
			INSERT INTO FBWeekTemplate VALUES (newid(), @year_code, @week_code,0,0,0,@start_date,0,-1,-1,-1)--16
		END
	END
END
*/