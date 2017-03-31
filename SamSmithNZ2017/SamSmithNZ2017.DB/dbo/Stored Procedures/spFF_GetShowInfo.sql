CREATE PROCEDURE [dbo].[spFF_GetShowInfo]
	@show_key smallint,
	@show_date varchar(20)
AS

IF (@show_date <> '')
BEGIN
	SELECT * 
	FROM ff_show sh
	WHERE show_date = @show_date
END
ELSE
BEGIN
	SELECT * 
	FROM ff_show sh
	WHERE show_key = @show_key
END