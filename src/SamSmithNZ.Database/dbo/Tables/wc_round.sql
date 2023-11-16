CREATE TABLE [dbo].[wc_round] (
    [round_code] VARCHAR (10) NOT NULL,
    [round_name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_wc_round] PRIMARY KEY CLUSTERED ([round_code] ASC)
);

