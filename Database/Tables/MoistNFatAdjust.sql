CREATE TABLE [dbo].[MoistNFatAdjust]
(
    FoodCode INT,
    [Version] INT,
    StartDT DATETIME2 NULL,
    EndDT DATETIME2 NULL,
    MoistureChange DECIMAL(5, 1) NULL,
    FatChange DECIMAL(5, 1) NULL,
    TypeOfFat INT NULL,
    CreatedDT DATETIME2
        DEFAULT GETUTCDATE() NOT NULL,
    CONSTRAINT PK_MoistNFatAdjust
        PRIMARY KEY
        (
            FoodCode,
            [Version]
        )
)
