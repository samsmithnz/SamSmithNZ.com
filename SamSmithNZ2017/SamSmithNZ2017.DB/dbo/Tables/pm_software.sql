CREATE TABLE [dbo].[pm_software] (
    [software_id] UNIQUEIDENTIFIER NOT NULL,
    [project_id]  UNIQUEIDENTIFIER NOT NULL,
    [version]     VARCHAR (50)     NOT NULL,
    CONSTRAINT [PK_pm_software] PRIMARY KEY CLUSTERED ([software_id] ASC)
);

