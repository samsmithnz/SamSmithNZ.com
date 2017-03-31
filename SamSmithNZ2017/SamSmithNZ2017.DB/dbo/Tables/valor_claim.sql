CREATE TABLE [dbo].[valor_claim] (
    [claim_code]   INT           NOT NULL,
    [world_num]    INT           NOT NULL,
    [x]            INT           NOT NULL,
    [y]            INT           NOT NULL,
    [owner]        VARCHAR (100) NOT NULL,
    [added_by]     VARCHAR (100) NOT NULL,
    [last_updated] DATETIME      NOT NULL,
    [guild_code]   INT           NULL,
    PRIMARY KEY CLUSTERED ([claim_code] ASC)
);

