CREATE TABLE [dbo].[ff_album] (
    [album_key]          SMALLINT      NOT NULL,
    [album_name]         VARCHAR (100) NULL,
    [album_release_date] SMALLDATETIME NULL,
    [album_label]        VARCHAR (50)  NULL, --Deprocating, not useful
    [album_image]        VARCHAR (200) NULL,
    CONSTRAINT [PK_ff_album] PRIMARY KEY CLUSTERED ([album_key] ASC)
);

