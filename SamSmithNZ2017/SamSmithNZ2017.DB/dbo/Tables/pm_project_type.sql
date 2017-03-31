CREATE TABLE [dbo].[pm_project_type] (
    [project_type_code] SMALLINT      NOT NULL,
    [project_type_name] VARCHAR (100) NOT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_pm_project_type]
    ON [dbo].[pm_project_type]([project_type_code] ASC);

