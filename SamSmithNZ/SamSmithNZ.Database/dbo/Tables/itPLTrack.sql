CREATE TABLE [dbo].[itPLTrack] (
    [track_code]  INT      NOT NULL,
    [track_name]  VARCHAR (250) NULL,
    [artist_name] VARCHAR (250) NULL,
    [album_name]  VARCHAR (250) NULL,
    CONSTRAINT [PK_itPLTrack] PRIMARY KEY CLUSTERED ([track_code] ASC)
);

