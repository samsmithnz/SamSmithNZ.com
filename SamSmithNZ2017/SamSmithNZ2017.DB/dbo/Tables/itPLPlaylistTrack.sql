CREATE TABLE [dbo].[itPLPlaylistTrack] (
    [playlist_code] SMALLINT NOT NULL,
    [track_code]    SMALLINT NOT NULL,
    [track_order]   SMALLINT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_itPLPlaylistTrack]
    ON [dbo].[itPLPlaylistTrack]([playlist_code] ASC);

