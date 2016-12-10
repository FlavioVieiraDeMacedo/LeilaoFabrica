CREATE TABLE [dbo].[Negociacao]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Id_Comprador] INT NULL, 
    [Id_Produto] INT NULL, 
    [Valor_Produto] DECIMAL NULL, 
    [Status] NVARCHAR(255) NULL, 
	CONSTRAINT [FK_Negociacao_Cliente] FOREIGN KEY (Id_Comprador) REFERENCES Usuario(Id),
	CONSTRAINT [FK_Negociacao_Produto] FOREIGN KEY (Id_Produto) REFERENCES Produto(Id)
)
