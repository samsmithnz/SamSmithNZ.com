CREATE TABLE [dbo].[wc_goal] (
    [goal_code]   INT NOT NULL,
    [game_code]   INT NOT NULL,
    [player_code] INT NOT NULL,
    [goal_time]   INT NOT NULL,
    [injury_time] INT NULL,
    [is_penalty]  BIT      NOT NULL,
    [is_own_goal] BIT      NOT NULL,
    [is_golden_goal] BIT NULL, 
    CONSTRAINT [PK_wc_goal] PRIMARY KEY CLUSTERED ([goal_code] ASC)
);

