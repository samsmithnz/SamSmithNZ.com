CREATE TABLE [dbo].[wc_penalty_shootout] (
    [penalty_code]  SMALLINT NOT NULL,
    [game_code]     SMALLINT NOT NULL,
    [player_code]   SMALLINT NOT NULL,
    [penalty_order] SMALLINT NOT NULL,
    [scored]        BIT      NOT NULL,
    CONSTRAINT [PK_wc_penalty_shootout] PRIMARY KEY CLUSTERED ([penalty_code] ASC)
);

