CREATE TABLE [dbo].[DerivDesc]
(
    DerivationCode VARCHAR(4),
    [Version] INT,
    DerivationDescription VARCHAR(120) NOT NULL,
    CreatedDT DATETIME2
        DEFAULT GETUTCDATE() NOT NULL,
    CONSTRAINT PK_DerivDesc
        PRIMARY KEY
        (
            DerivationCode,
            [Version]
        )
)
