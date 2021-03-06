﻿CREATE TABLE [dbo].[PLAYER] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [VORNAME]       VARCHAR (MAX) NULL,
    [NACHNAME]      VARCHAR (MAX) NULL,
    [TELEFONNUMMER] VARCHAR (10)  NULL,
    [ID_ADDRESS]    INT           NULL,
	[ID_TEAMS]    INT           NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([ID_ADDRESS]) REFERENCES [dbo].[ADDRESS] ([ID]),
	FOREIGN KEY ([ID_ADDRESS]) REFERENCES [dbo].[TEAMS] ([ID])
);

