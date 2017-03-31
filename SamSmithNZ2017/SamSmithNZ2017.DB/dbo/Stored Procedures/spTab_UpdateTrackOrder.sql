CREATE PROCEDURE [dbo].[spTab_UpdateTrackOrder]
	@Track1Key int,
	@Track2Key int
AS
SELECT 0
--UPDATE Tabtrack SET TrackOrder = TrackOrder - 1 WHERE TrackKey = @Track1Key
--UPDATE Tabtrack SET TrackOrder = TrackOrder + 1 WHERE TrackKey = @Track2Key