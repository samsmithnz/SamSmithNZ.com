CREATE TABLE [dbo].[ffl_show_song] (
    [showtrack_key]    INT     NOT NULL,
    [show_key]         INT     NULL,
    [song_key]         INT     NULL,
    [unknown_song_key] INT     NULL,
    [show_song_order]  INT     NULL,
    [is_partial]       BIT          NULL,
    [is_jam]           BIT          NULL,
    [song_notes]       VARCHAR (50) NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_FFLShowSong]
    ON [dbo].[ffl_show_song]([song_key] ASC);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_ffl_show_song]
    ON [dbo].[ffl_show_song]([showtrack_key] ASC);

