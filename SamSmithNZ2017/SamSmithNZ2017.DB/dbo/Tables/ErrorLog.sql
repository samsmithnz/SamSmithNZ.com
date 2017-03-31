CREATE TABLE [dbo].[ErrorLog] (
    [last_updated]  DATETIME       NOT NULL,
    [source]        VARCHAR (100)  NOT NULL,
    [error_message] VARCHAR (1000) NOT NULL,
    [error_text]    VARCHAR (8000) NOT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_ErrorLog]
    ON [dbo].[ErrorLog]([last_updated] ASC);

