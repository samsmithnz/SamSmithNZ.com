CREATE TABLE [dbo].[wc_group_stage] (
    [round_number]                 INT     NOT NULL,
    [round_code]                   VARCHAR (10) NOT NULL,
    [tournament_code]              INT     NOT NULL,
    [team_code]                    INT     NOT NULL,
    [played]                       INT     NOT NULL,
    [wins]                         INT     NOT NULL,
    [draws]                        INT     NOT NULL,
    [losses]                       INT     NOT NULL,
    [goals_for]                    INT     NOT NULL,
    [goals_against]                INT     NOT NULL,
    [goal_difference]              INT     NOT NULL,
    [points]                       INT     NOT NULL,
    [has_qualified_for_next_round] BIT          NULL,
    [group_ranking]                INT     NULL,
    CONSTRAINT [PK_wc_group_stage] PRIMARY KEY CLUSTERED ([round_number] ASC, [round_code] ASC, [tournament_code] ASC, [team_code] ASC)
);

