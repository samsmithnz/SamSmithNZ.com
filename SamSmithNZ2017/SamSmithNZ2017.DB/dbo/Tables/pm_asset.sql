CREATE TABLE [dbo].[pm_asset] (
    [asset_id]   UNIQUEIDENTIFIER NOT NULL,
    [project_id] UNIQUEIDENTIFIER NOT NULL,
    [asset]      BINARY (8000)    NOT NULL,
    CONSTRAINT [PK_pm_asset] PRIMARY KEY CLUSTERED ([asset_id] ASC)
);

