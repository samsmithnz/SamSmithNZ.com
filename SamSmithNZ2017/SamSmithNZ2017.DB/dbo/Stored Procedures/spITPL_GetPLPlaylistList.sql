CREATE PROCEDURE [dbo].[spITPL_GetPLPlaylistList]
	@include_all_playlists bit
AS
IF (@include_all_playlists = 1)
	SELECT * FROM itPLPlaylist
	WHERE playlist_name not in ('Library','Movies','Music','Party Shuffle','podcasts','purchased','recently played')
	ORDER BY playlist_name
ELSE
	SELECT * FROM itPLPlaylist
	WHERE playlist_name not in ('Library','Movies','Music','Party Shuffle','podcasts','purchased','recently played')
	and is_selected = 1
	ORDER BY playlist_name