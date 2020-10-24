CREATE TABLE [dbo].[problem] (
    [problem_number] INT           NOT NULL,
    [description]    VARCHAR (MAX) NULL,
    [notes]          VARCHAR (MAX) NULL,
    [is_completed]   BIT           NULL,
    [last_updated]   DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([problem_number] ASC)
);

