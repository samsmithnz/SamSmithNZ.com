CREATE TABLE [dbo].[wc_group_stage] (
    [round_number]                 SMALLINT     NOT NULL,
    [round_code]                   VARCHAR (10) NOT NULL,
    [tournament_code]              SMALLINT     NOT NULL,
    [team_code]                    SMALLINT     NOT NULL,
    [played]                       SMALLINT     NOT NULL,
    [wins]                         SMALLINT     NOT NULL,
    [draws]                        SMALLINT     NOT NULL,
    [losses]                       SMALLINT     NOT NULL,
    [goals_for]                    SMALLINT     NOT NULL,
    [goals_against]                SMALLINT     NOT NULL,
    [goal_difference]              SMALLINT     NOT NULL,
    [points]                       SMALLINT     NOT NULL,
    [has_qualified_for_next_round] BIT          NULL,
    [group_ranking]                SMALLINT     NULL,
    CONSTRAINT [PK_wc_group_stage] PRIMARY KEY CLUSTERED ([round_number] ASC, [round_code] ASC, [tournament_code] ASC, [team_code] ASC)
);

