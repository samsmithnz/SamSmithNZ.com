CREATE PROCEDURE [dbo].[spFFL_ImportCreateShow]
	@show_date varchar(50),
	@show_location varchar(100),
	@show_city varchar(50),
	@show_state varchar(50),
	@show_country varchar(100),
	@other_performers varchar(5000),
	@notes varchar(5000),
	@is_postponed_show bit,
	@is_cancelled_show bit
AS

DECLARE @show_key smallint

SELECT @show_key = sh.show_key
FROM ffl_show sh
WHERE sh.show_date = @show_date

IF (@show_key is null)
BEGIN
	INSERT INTO ffl_show
	SELECT isnull((SELECT max(show_key)+1 FROM ffl_show),1), @show_date, @show_location, @show_city, @show_state, @show_country, @other_performers, @notes, @is_postponed_show, @is_cancelled_show, GETDATE()

	SELECT @show_key = sh.show_key
	FROM ffl_show sh
	WHERE sh.show_date = @show_date
END

SELECT @show_key as show_key