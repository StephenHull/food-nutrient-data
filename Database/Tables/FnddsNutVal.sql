CREATE TABLE [dbo].[FnddsNutVal]
(
    FoodCode INT,
    NutrientCode INT,
    [Version] INT,
    StartDT DATETIME2 NULL,
    EndDT DATETIME2 NULL,
    NutrientValue DECIMAL(10, 3) NOT NULL,
    CreatedDT DATETIME2
        DEFAULT GETUTCDATE() NOT NULL,
    CONSTRAINT PK_FnddsNutVal
        PRIMARY KEY
        (
            FoodCode,
            NutrientCode,
            [Version]
        )
)
