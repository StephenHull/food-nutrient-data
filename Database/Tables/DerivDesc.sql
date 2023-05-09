CREATE TABLE [dbo].[DerivDesc]
(
	DerivationCode VARCHAR(4),
	[Version] INT,
	DerivationDescription VARCHAR(120) NOT NULL,
	Created DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
	CONSTRAINT PK_DerivDesc PRIMARY KEY (DerivationCode, [Version])
)
