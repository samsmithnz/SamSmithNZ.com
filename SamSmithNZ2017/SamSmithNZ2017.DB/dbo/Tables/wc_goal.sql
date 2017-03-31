CREATE TABLE [dbo].[wc_goal] (
    [goal_code]   SMALLINT NOT NULL,
    [game_code]   SMALLINT NOT NULL,
    [player_code] SMALLINT NOT NULL,
    [goal_time]   SMALLINT NOT NULL,
    [injury_time] SMALLINT NULL,
    [is_penalty]  BIT      NOT NULL,
    [is_own_goal] BIT      NOT NULL,
    CONSTRAINT [PK_wc_goal] PRIMARY KEY CLUSTERED ([goal_code] ASC)
);

