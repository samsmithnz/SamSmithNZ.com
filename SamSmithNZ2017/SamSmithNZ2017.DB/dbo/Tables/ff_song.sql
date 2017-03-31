CREATE TABLE [dbo].[ff_song] (
    [song_key]    SMALLINT       NOT NULL,
    [song_name]   VARCHAR (100)  NULL,
    [album_key]   SMALLINT       NULL,
    [song_order]  SMALLINT       NULL,
    [song_lyrics] VARCHAR (8000) NULL,
    [song_notes]  VARCHAR (2000) NULL,
    [song_image]  VARCHAR (200)  NULL,
    CONSTRAINT [PK_ff_track] PRIMARY KEY CLUSTERED ([song_key] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_FFSong]
    ON [dbo].[ff_song]([song_key] ASC);

