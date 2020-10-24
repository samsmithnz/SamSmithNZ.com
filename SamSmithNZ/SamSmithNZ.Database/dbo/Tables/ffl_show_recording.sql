CREATE TABLE [dbo].[ffl_show_recording] (
    [recording_key]         INT      NOT NULL,
    [show_key]              INT      NOT NULL,
    [number]                INT      NOT NULL,
    [Description]           VARCHAR (500) NOT NULL,
    [IsComplete]            BIT           NULL,
    [IsCirculatedRecording] BIT           NOT NULL,
    [Equipment]             VARCHAR (500) NULL,
    [LowestGeneration]      VARCHAR (500) NULL,
    [LowestAudioGeneration] VARCHAR (500) NULL,
    [LowestVideoGeneration] VARCHAR (500) NULL,
    [LengthSoundQuality]    VARCHAR (500) NULL,
    [Notes]                 VARCHAR (500) NULL,
    [LastUpdated]           DATETIME      NOT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_ffl_show_recording]
    ON [dbo].[ffl_show_recording]([recording_key] ASC);

