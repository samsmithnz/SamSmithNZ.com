CREATE TABLE [dbo].[tab_album] (
    [album_code]               SMALLINT      NOT NULL,
    [artist_name]              VARCHAR (100) NULL,
    [album_name]               VARCHAR (100) NULL,
    [album_year]               INT           NULL,
    [is_bass_tab]              BIT           NULL,
    [is_new_album]             BIT           NULL,
    [is_misc_collection_album] BIT           NULL,
    [include_in_index]         BIT           NULL,
    [include_on_website]       BIT           NULL,
    [last_updated]             DATETIME      NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_tab_album]
    ON [dbo].[tab_album]([album_code] ASC);

