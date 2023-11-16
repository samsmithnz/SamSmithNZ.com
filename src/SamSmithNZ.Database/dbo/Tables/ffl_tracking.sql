CREATE TABLE [dbo].[ffl_tracking] (
    [show_date]               DATETIME NULL,
    [show_key]                INT NULL,
    [ffl_show_key]            INT NULL,
    [show_location_different] BIT      NULL,
    [show_setlist_different]  BIT      NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_ffl_tracking]
    ON [dbo].[ffl_tracking]([show_key] ASC);

