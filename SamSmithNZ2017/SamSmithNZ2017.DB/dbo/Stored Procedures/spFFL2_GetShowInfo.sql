CREATE PROCEDURE [dbo].[spFFL2_GetShowInfo]
	@show_key smallint
AS
SELECT sl.show_date,
	sl.show_key, sl.show_location, isnull(sl.show_city + 
									CASE WHEN not sl.show_state is null and sl.show_state <> '' THEN ', ' + sl.show_state ELSE '' END + 
									CASE WHEN not sl.show_country is null and sl.show_country <> '' THEN ', ' + sl.show_country ELSE '' END,'') as city_state_country, 
	sl.other_performers, sl.notes
FROM ffl_show sl
WHERE sl.show_key = @show_key