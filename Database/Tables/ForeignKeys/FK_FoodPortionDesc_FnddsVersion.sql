ALTER TABLE [dbo].FoodPortionDesc
ADD CONSTRAINT FK_FoodPortionDesc_FnddsVersion
    FOREIGN KEY ([Version])
    REFERENCES [dbo].[FnddsVersion] (ID) ON DELETE CASCADE