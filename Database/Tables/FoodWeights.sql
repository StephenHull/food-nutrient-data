CREATE TABLE [dbo].[FoodWeights]
(
	FoodCode INT,
	Subcode INT DEFAULT 0,
	SeqNum INT,
	PortionCode INT,
	[Version] INT,
	StartDate DATETIME NOT NULL,
	EndDate DATETIME NOT NULL,
	PortionWeight DECIMAL(8,3) NOT NULL,
	ChangeType VARCHAR(1) NULL,
	Created DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
	CONSTRAINT PK_FoodWeights PRIMARY KEY (FoodCode, Subcode, SeqNum, PortionCode, [Version])
)