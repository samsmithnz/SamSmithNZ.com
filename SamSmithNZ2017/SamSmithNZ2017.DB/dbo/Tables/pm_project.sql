CREATE TABLE [dbo].[pm_project] (
    [project_id]          UNIQUEIDENTIFIER NOT NULL,
    [project_type_code]   SMALLINT         NOT NULL,
    [project_name]        VARCHAR (400)    NOT NULL,
    [project_spec]        VARCHAR (8000)   NOT NULL,
    [project_status_code] SMALLINT         NOT NULL,
    [project_order]       SMALLINT         NOT NULL,
    [last_updated]        DATETIME         NOT NULL,
    CONSTRAINT [PK_pmProject] PRIMARY KEY CLUSTERED ([project_id] ASC)
);

