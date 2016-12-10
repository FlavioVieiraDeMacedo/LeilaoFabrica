CREATE TABLE [dbo].[Produto]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nome] NVARCHAR(255) NOT NULL, 
    [Descricao] NVARCHAR(MAX) NULL, 
    [Imagem] NVARCHAR(MAX) NOT NULL,
    [Valor_Vendedor] DECIMAL NULL, 
    [Id_Vendedor] INT NULL, 
	[Status_Produto] NVARCHAR(255) NULL,
    CONSTRAINT [FK_Negociacao_Vendedor] FOREIGN KEY (Id_Vendedor) REFERENCES Usuario(Id)
)
