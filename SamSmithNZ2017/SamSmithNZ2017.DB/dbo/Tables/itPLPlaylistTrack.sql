CREATE TABLE [dbo].[itPLPlaylistTrack] (
    [playlist_code] INT NOT NULL,
    [track_code]    INT NOT NULL,
    [track_order]   INT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_itPLPlaylistTrack]
    ON [dbo].[itPLPlaylistTrack]([playlist_code] ASC);

