CREATE TABLE [dbo].[ff_show_song] (
    [showtrack_key]   INT NOT NULL,
    [show_key]        INT NULL,
    [song_key]        INT NULL,
    [show_song_order] INT NULL,
    CONSTRAINT [PK_ff_show_track] PRIMARY KEY CLUSTERED ([showtrack_key] ASC)
);

