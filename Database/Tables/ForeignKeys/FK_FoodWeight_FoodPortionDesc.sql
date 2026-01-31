ALTER TABLE [dbo].FoodWeight
ADD CONSTRAINT FK_FoodWeight_FoodPortionDesc
    FOREIGN KEY
    (
        PortionCode,
        [Version]
    )
    REFERENCES [dbo].FoodPortionDesc
    (
        PortionCode,
        [Version]
    )
