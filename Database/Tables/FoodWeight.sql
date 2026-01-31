CREATE TABLE [dbo].[FoodWeight]
(
    FoodCode INT,
    Subcode INT
        DEFAULT 0,
    SeqNum INT,
    PortionCode INT,
    [Version] INT,
    StartDT DATETIME2 NULL,
    EndDT DATETIME2 NULL,
    PortionWeight DECIMAL(8, 3) NOT NULL,
    ChangeType VARCHAR(1) NULL,
    CreatedDT DATETIME2
        DEFAULT GETUTCDATE() NOT NULL,
    CONSTRAINT PK_FoodWeight
        PRIMARY KEY
        (
            FoodCode,
            SubCode,
            SeqNum,
            PortionCode,
            [Version]
        )
)
