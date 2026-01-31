CREATE TABLE [dbo].[FnddsVersion]
(
    ID INT PRIMARY KEY,
    BeginYear INT NOT NULL,
    EndYear INT NOT NULL,
    Major INT NULL,
    Minor INT NULL,
    CreatedDT DATETIME2
        DEFAULT GETUTCDATE() NOT NULL,
)
