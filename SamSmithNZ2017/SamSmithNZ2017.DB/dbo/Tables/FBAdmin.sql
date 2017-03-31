CREATE TABLE [dbo].[FBAdmin] (
    [employee_code] CHAR (5) NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_FBAdmin]
    ON [dbo].[FBAdmin]([employee_code] ASC);

