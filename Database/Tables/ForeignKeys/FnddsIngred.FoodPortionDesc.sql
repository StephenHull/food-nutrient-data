ALTER TABLE [dbo].FnddsIngred
ADD CONSTRAINT FK_FnddsIngred_FoodPortionDesc
	FOREIGN KEY (PortionCode, [Version])
	REFERENCES [dbo].FoodPortionDesc (PortionCode, [Version])