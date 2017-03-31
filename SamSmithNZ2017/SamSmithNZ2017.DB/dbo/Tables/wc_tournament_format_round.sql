CREATE TABLE [dbo].[wc_tournament_format_round] (
    [format_round_code]                       SMALLINT NOT NULL,
    [is_group_stage]                          BIT      NOT NULL,
    [number_of_teams_in_group]                SMALLINT NOT NULL,
    [number_of_groups_in_round]               SMALLINT NULL,
    [number_of_teams_from_group_that_advance] SMALLINT NULL,
    [total_number_of_teams_that_advance]      SMALLINT NULL,
    CONSTRAINT [PK_wc_tournament_format_round] PRIMARY KEY CLUSTERED ([format_round_code] ASC)
);

