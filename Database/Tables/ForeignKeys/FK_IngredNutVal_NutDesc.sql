ALTER TABLE [dbo].IngredNutVal
ADD CONSTRAINT FK_IngredNutVal_NutDesc
    FOREIGN KEY
    (
        NutrientCode,
        [Version]
    )
    REFERENCES [dbo].NutDesc
    (
        NutrientCode,
        [Version]
    ) ON DELETE CASCADE