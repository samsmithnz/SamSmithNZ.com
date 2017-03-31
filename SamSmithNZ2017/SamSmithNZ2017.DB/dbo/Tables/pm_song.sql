CREATE TABLE [dbo].[pm_song] (
    [song_id]    UNIQUEIDENTIFIER NOT NULL,
    [project_id] CHAR (10)        NOT NULL,
    [tab]        VARCHAR (8000)   NULL,
    [lyrics]     VARCHAR (8000)   NULL,
    CONSTRAINT [PK_pm_song] PRIMARY KEY CLUSTERED ([song_id] ASC)
);

