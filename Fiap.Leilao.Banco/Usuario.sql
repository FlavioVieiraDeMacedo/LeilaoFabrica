CREATE TABLE [dbo].[Usuario]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nome] NVARCHAR(255) NOT NULL, 
    [Email] NVARCHAR(255) NOT NULL, 
    [Senha] NVARCHAR(30) NOT NULL, 
    [CPF] NVARCHAR(30) NOT NULL, 
    [CEP] NVARCHAR(30) NOT NULL, 
    [Numero] INT NOT NULL, 
    [Complemento] NVARCHAR(255) NULL, 
    [Dt_Nascimento] DATE NOT NULL, 
    [Telefone] NVARCHAR(50) NOT NULL
)
