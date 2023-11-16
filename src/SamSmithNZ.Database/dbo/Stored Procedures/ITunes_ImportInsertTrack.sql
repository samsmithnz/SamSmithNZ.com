CREATE PROCEDURE [dbo].[ITunes_ImportInsertTrack]
	@PlaylistCode INT,
	@TrackName VARCHAR(75),
	@AlbumName VARCHAR(50),
	@ArtistName VARCHAR(50),
	@PlayCount INT,
	@Ranking INT,
	@Rating INT
AS
BEGIN
	INSERT INTO itTrack
	SELECT @PlaylistCode, @TrackName, @AlbumName, @ArtistName, @PlayCount, NULL, @Ranking, NULL, 0, @Rating, NEWID()
END
