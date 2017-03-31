CREATE TABLE [dbo].[TabTrack_old] (
    [TrackKey]   INT           NOT NULL,
    [TrackName]  VARCHAR (75)  NULL,
    [TrackPath]  VARCHAR (100) NULL,
    [TrackOrder] INT           NULL,
    [AlbumKey]   INT           NULL,
    [Rating]     SMALLINT      NULL,
    CONSTRAINT [PK_Tracks] PRIMARY KEY CLUSTERED ([TrackKey] ASC)
);

