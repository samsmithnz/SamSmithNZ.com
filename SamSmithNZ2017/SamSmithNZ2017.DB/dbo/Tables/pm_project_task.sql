CREATE TABLE [dbo].[pm_project_task] (
    [task_id]      UNIQUEIDENTIFIER NOT NULL,
    [project_id]   UNIQUEIDENTIFIER NOT NULL,
    [task_name]    VARCHAR (400)    NOT NULL,
    [task_order]   SMALLINT         NOT NULL,
    [incremented]  SMALLINT         NOT NULL,
    [is_completed] BIT              NOT NULL,
    [last_updated] DATETIME         NOT NULL,
    CONSTRAINT [PK_pmTask] PRIMARY KEY CLUSTERED ([task_id] ASC)
);

