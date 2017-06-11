CREATE TABLE [dbo].[wc_tournament_format] (
    [format_code]                        INT NOT NULL,
    [total_number_of_teams]              INT NOT NULL,
    [total_number_of_groups]             INT NOT NULL,
    [round_1_format_code]                INT NOT NULL,
    [round_2_format_code]                INT NULL,
    [round_3_format_code]                INT NULL,
    [playoff_includes_3rd_place_playoff] BIT      NOT NULL,
    CONSTRAINT [PK_wc_tournament_format] PRIMARY KEY CLUSTERED ([format_code] ASC)
);

