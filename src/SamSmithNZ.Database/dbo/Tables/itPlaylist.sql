CREATE TABLE [dbo].[itPlaylist] (
    [playlist_code] INT NOT NULL,
    [playlist_date] DATETIME NOT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_itPlaylist]
    ON [dbo].[itPlaylist]([playlist_code] ASC);

