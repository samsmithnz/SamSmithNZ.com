CREATE TABLE [dbo].[wc_penalty_shootout] (
    [penalty_code]  INT NOT NULL,
    [game_code]     INT NOT NULL,
    [player_code]   INT NOT NULL,
    [penalty_order] INT NOT NULL,
    [scored]        BIT      NOT NULL,
    CONSTRAINT [PK_wc_penalty_shootout] PRIMARY KEY CLUSTERED ([penalty_code] ASC)
);

