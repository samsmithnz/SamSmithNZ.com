CREATE TABLE [dbo].[itPlaylist] (
    [playlist_code] INT      NULL,
    [playlist_date] DATETIME NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_itPlaylist]
    ON [dbo].[itPlaylist]([playlist_code] ASC);

