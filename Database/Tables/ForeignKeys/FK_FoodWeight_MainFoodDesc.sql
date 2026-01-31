ALTER TABLE [dbo].FoodWeight
ADD CONSTRAINT FK_FoodWeight_MainFoodDesc
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
