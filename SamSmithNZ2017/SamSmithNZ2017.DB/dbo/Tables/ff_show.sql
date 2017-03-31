CREATE TABLE [dbo].[ff_show] (
    [show_key]      SMALLINT      NOT NULL,
    [show_date]     DATETIME      NULL,
    [show_location] VARCHAR (100) NULL,
    [show_city]     VARCHAR (50)  NULL,
    CONSTRAINT [PK_ff_show] PRIMARY KEY CLUSTERED ([show_key] ASC)
);

