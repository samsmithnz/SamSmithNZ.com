CREATE PROCEDURE [dbo].[spTab_AddTrackToExistingAlbum]
	@TrackName varchar(75), 
	@TrackPath varchar(100), 
	@TrackOrder int, 
	@AlbumKey int
AS
Exec spTab_InsertTrack @TrackName, @TrackPath, @TrackOrder, @AlbumKey
Exec spTab_ReIndexTracks