ALTER TABLE [dbo].DerivDesc
ADD CONSTRAINT FK_DerivDesc_FnddsVersion
	FOREIGN KEY ([Version])
	REFERENCES [dbo].[FnddsVersion] (Id) ON DELETE CASCADE