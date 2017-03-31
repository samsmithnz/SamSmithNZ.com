CREATE TABLE [dbo].[TabAlbum_old] (
    [AlbumKey]             INT           NOT NULL,
    [ArtistName]           VARCHAR (50)  NULL,
    [AlbumName]            VARCHAR (50)  NULL,
    [Path]                 VARCHAR (500) NULL,
    [AlbumYear]            INT           CONSTRAINT [DF_Albums_AlbumYear] DEFAULT ((0)) NULL,
    [IsABassTabAlbum]      BIT           NULL,
    [IsNewAlbum]           BIT           CONSTRAINT [DF_Albums_IsNewFlag] DEFAULT ((1)) NULL,
    [IsUpdatedAlbum]       BIT           CONSTRAINT [DF_Albums_IsUpdatedFlag] DEFAULT ((0)) NULL,
    [IsAVariousSelection]  BIT           CONSTRAINT [DF_Albums_IsAVariousSelection] DEFAULT ((0)) NULL,
    [LastUpdated]          DATETIME      NULL,
    [IsIncludedInTheIndex] BIT           CONSTRAINT [DF_Albums_IsIncludedInTheIndex] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Albums] PRIMARY KEY CLUSTERED ([AlbumKey] ASC)
);

