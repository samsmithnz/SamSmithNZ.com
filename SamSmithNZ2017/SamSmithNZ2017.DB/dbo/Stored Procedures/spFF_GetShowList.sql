
CREATE PROCEDURE [dbo].[spFF_GetShowList]
	@show_year smallint
AS

IF (@show_year = 0)
BEGIN
	SELECT @show_year = max(year(show_date))
	FROM ff_show
END

IF (@show_year = -1)
BEGIN
	SELECT sh.show_key, sh.show_date, sh.show_location, sh.show_city, song_count
	FROM vFF_Show sh
	ORDER BY sh.show_date desc, sh.show_key desc
END
ELSE
BEGIN
	SELECT sh.show_key, sh.show_date, sh.show_location, sh.show_city, song_count
	FROM vFF_Show sh
	WHERE year(show_date) = @show_year
	ORDER BY sh.show_date desc, sh.show_key desc
END