CREATE TABLE [dbo].[FBStats] (
    [employee_code] CHAR (5) NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_FBStats]
    ON [dbo].[FBStats]([employee_code] ASC);

