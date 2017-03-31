CREATE TABLE [dbo].[FBPlayer] (
    [year_code]       SMALLINT     NULL,
    [player_code]     SMALLINT     NULL,
    [player_name]     VARCHAR (50) NULL,
    [player_password] VARCHAR (50) NULL,
    [employee_code]   CHAR (5)     NULL,
    [is_celebrity]    BIT          CONSTRAINT [DF_FBPlayer_is_celebrity] DEFAULT ((0)) NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_FBPlayer]
    ON [dbo].[FBPlayer]([year_code] ASC);

