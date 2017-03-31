CREATE TABLE [dbo].[wc_tournament_format] (
    [format_code]                        SMALLINT NOT NULL,
    [total_number_of_teams]              SMALLINT NOT NULL,
    [total_number_of_groups]             SMALLINT NOT NULL,
    [round_1_format_code]                SMALLINT NOT NULL,
    [round_2_format_code]                SMALLINT NULL,
    [round_3_format_code]                SMALLINT NULL,
    [playoff_includes_3rd_place_playoff] BIT      NOT NULL,
    CONSTRAINT [PK_wc_tournament_format] PRIMARY KEY CLUSTERED ([format_code] ASC)
);

