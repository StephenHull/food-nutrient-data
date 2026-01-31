CREATE TABLE [dbo].[NutDesc]
(
    NutrientCode INT,
    [Version] INT,
    NutrientDescription VARCHAR(45) NOT NULL,
    Tagname VARCHAR(15) NULL,
    Unit VARCHAR(10) NOT NULL,
    Decimals INT NOT NULL,
    CreatedDT DATETIME2
        DEFAULT GETUTCDATE() NOT NULL,
    CONSTRAINT PK_NutDesc
        PRIMARY KEY
        (
            NutrientCode,
            [Version]
        )

)