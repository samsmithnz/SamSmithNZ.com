CREATE PROCEDURE [dbo].[spFF_ImportCreateShow]
	@show_date varchar(50),
	@show_location varchar(100),
	@show_city varchar(50)
AS

DECLARE @show_key smallint

SELECT @show_key = sh.show_key
FROM ff_show sh
WHERE sh.show_date = @show_date

IF (@show_key is null)
BEGIN
	INSERT INTO ff_show
	SELECT (SELECT max(show_key)+1 FROM ff_show), @show_date, @show_location, @show_city

	SELECT @show_key = sh.show_key
	FROM ff_show sh
	WHERE sh.show_date = @show_date
END

SELECT @show_key as show_key