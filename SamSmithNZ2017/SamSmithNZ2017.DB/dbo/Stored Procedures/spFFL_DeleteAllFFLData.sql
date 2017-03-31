CREATE PROCEDURE [dbo].[spFFL_DeleteAllFFLData]
AS

DELETE FROM ffl_show_song
DELETE FROM ffl_show 
DELETE FROM ffl_unknown_song
DELETE FROM ffl_show_recording