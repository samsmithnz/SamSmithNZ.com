CREATE TABLE [dbo].[ffl_show_song] (
    [showtrack_key]    SMALLINT     NOT NULL,
    [show_key]         SMALLINT     NULL,
    [song_key]         SMALLINT     NULL,
    [unknown_song_key] SMALLINT     NULL,
    [show_song_order]  SMALLINT     NULL,
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

