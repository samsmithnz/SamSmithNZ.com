CREATE TABLE [dbo].[wc_tournament_format_round] (
    [format_round_code]                       INT NOT NULL,
    [is_group_stage]                          BIT      NOT NULL,
    [number_of_teams_in_group]                INT NOT NULL,
    [number_of_groups_in_round]               INT NULL,
    [number_of_teams_from_group_that_advance] INT NULL,
    [total_number_of_teams_that_advance]      INT NULL,
    CONSTRAINT [PK_wc_tournament_format_round] PRIMARY KEY CLUSTERED ([format_round_code] ASC)
);

