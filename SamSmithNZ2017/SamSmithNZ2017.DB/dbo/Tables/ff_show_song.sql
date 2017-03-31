CREATE TABLE [dbo].[ff_show_song] (
    [showtrack_key]   SMALLINT NOT NULL,
    [show_key]        SMALLINT NULL,
    [song_key]        SMALLINT NULL,
    [show_song_order] SMALLINT NULL,
    CONSTRAINT [PK_ff_show_track] PRIMARY KEY CLUSTERED ([showtrack_key] ASC)
);

