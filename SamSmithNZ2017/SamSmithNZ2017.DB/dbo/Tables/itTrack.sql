CREATE TABLE [dbo].[itTrack] (
    [playlist_code]       INT              NULL,
    [track_name]          VARCHAR (75)     NULL,
    [album_name]          VARCHAR (50)     NULL,
    [artist_name]         VARCHAR (50)     NULL,
    [play_count]          SMALLINT         NULL,
    [previous_play_count] SMALLINT         NULL,
    [ranking]             SMALLINT         NULL,
    [previous_ranking]    SMALLINT         NULL,
    [is_new_entry]        BIT              NULL,
    [rating]              SMALLINT         NULL,
    [record_id]           UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_itTrack] PRIMARY KEY CLUSTERED ([record_id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [nci_wi_itTrack_2F9FA3C0-454E-4B2C-B349-9A27271F647A]
    ON [dbo].[itTrack]([rating] ASC, [playlist_code] ASC, [ranking] ASC)
    INCLUDE([album_name], [artist_name], [is_new_entry], [play_count], [previous_play_count], [previous_ranking], [record_id], [track_name]);

