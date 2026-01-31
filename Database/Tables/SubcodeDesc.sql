CREATE TABLE [dbo].[SubcodeDesc]
(
    Subcode INT,
    [Version] INT,
    StartDT DATETIME2 NULL,
    EndDT DATETIME2 NULL,
    SubcodeDescription VARCHAR(80) NOT NULL,
    Created DATETIME
        DEFAULT CURRENT_TIMESTAMP NOT NULL,
    CONSTRAINT PK_SubcodeDesc
        PRIMARY KEY
        (
            Subcode,
            [Version]
        )
)
