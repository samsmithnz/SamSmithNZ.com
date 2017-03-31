CREATE TABLE [dbo].[pm_project_status] (
    [project_status_code] SMALLINT     NOT NULL,
    [project_status_name] VARCHAR (50) NULL,
    CONSTRAINT [PK_pm_status] PRIMARY KEY CLUSTERED ([project_status_code] ASC)
);

