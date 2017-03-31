CREATE TABLE [dbo].[FBTeam] (
    [team_code]         SMALLINT     NULL,
    [team_name]         VARCHAR (50) NULL,
    [american_league]   BIT          CONSTRAINT [DF_FBTeam_american_league] DEFAULT ((0)) NOT NULL,
    [image_name]        VARCHAR (50) NULL,
    [grey_prefix]       VARCHAR (50) NULL,
    [grey_right_image]  VARCHAR (50) NULL,
    [grey_left_image]   VARCHAR (50) NULL,
    [white_prefix]      VARCHAR (50) NULL,
    [white_right_image] VARCHAR (50) NULL,
    [white_left_image]  VARCHAR (50) NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_FBTeam]
    ON [dbo].[FBTeam]([team_code] ASC);

