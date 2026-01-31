ALTER TABLE [dbo].FnddsIngred
ADD CONSTRAINT FK_FnddsIngred_MainFoodDesc
    FOREIGN KEY
    (
        FoodCode,
        [Version]
    )
    REFERENCES [dbo].MainFoodDesc
    (
        FoodCode,
        [Version]
    ) ON DELETE CASCADE