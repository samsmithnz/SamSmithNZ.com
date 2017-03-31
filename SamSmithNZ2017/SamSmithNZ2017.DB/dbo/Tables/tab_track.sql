CREATE TABLE [dbo].[tab_track] (
    [track_code]   SMALLINT      NOT NULL,
    [album_code]   SMALLINT      NULL,
    [track_name]   VARCHAR (100) NULL,
    [track_text]   TEXT          NULL,
    [track_order]  SMALLINT      NULL,
    [rating]       SMALLINT      NULL,
    [tuning_code]  SMALLINT      NULL,
    [last_updated] DATETIME      NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_tab_track]
    ON [dbo].[tab_track]([track_code] ASC);

