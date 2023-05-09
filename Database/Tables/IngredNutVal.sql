CREATE TABLE [dbo].[IngredNutVal]
(
	IngredientCode INT,
	NutrientCode INT,
	[Version] INT,
	StartDate DATETIME NOT NULL,
	EndDate DATETIME NOT NULL,
	IngredientDescription VARCHAR(200) NOT NULL,
	NutrientValue DECIMAL(10,3) NOT NULL,
	NutrientValueSource VARCHAR(80) NOT NULL,
	FdcId INT NULL,
	DerivationCode VARCHAR(4) NULL,
	SrAddModYear INT NULL,
	FoundationYearAcquired INT NULL,
	Created DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
	CONSTRAINT PK_IngredNutVal PRIMARY KEY (IngredientCode, NutrientCode, [Version])
)