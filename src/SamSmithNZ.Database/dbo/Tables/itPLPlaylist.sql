CREATE TABLE [dbo].[itPLPlaylist] (
    [playlist_code] INT      NOT NULL,
    [playlist_name] VARCHAR (250) NULL,
    [is_selected]   BIT           CONSTRAINT [DF_itPLPlaylist_is_selected] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_itPLPlaylist] PRIMARY KEY CLUSTERED ([playlist_code] ASC)
);

