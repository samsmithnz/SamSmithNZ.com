CREATE TABLE [dbo].[tab_track] (
    [track_code]   INT      NOT NULL,
    [album_code]   INT      NULL,
    [track_name]   VARCHAR (100) NULL,
    [track_text]   TEXT          NULL,
    [track_order]  INT      NULL,
    [rating]       INT      NULL,
    [tuning_code]  INT      NULL,
    [last_updated] DATETIME      NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_tab_track]
    ON [dbo].[tab_track]([track_code] ASC);

