CREATE TABLE [dbo].[ffl_show] (
    [show_key]          SMALLINT       NOT NULL,
    [show_date]         DATETIME       NULL,
    [show_location]     VARCHAR (100)  NULL,
    [show_city]         VARCHAR (50)   NULL,
    [show_state]        VARCHAR (50)   NULL,
    [show_country]      VARCHAR (100)  NULL,
    [other_performers]  VARCHAR (5000) NULL,
    [notes]             VARCHAR (5000) NULL,
    [is_postponed_show] BIT            NULL,
    [is_cancelled_show] BIT            NULL,
    [last_updated]      DATETIME       NOT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_ffl_show]
    ON [dbo].[ffl_show]([show_key] ASC);

