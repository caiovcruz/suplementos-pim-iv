DROP TABLE IF EXISTS TB_Login;
DROP TABLE IF EXISTS TB_NivelAcesso;
DROP TABLE IF EXISTS TB_Funcionario;
DROP TABLE IF EXISTS TB_MovimentacaoEstoque;
DROP TABLE IF EXISTS TB_Estoque;
DROP TABLE IF EXISTS TB_Produto;
DROP TABLE IF EXISTS TB_Marca;
DROP TABLE IF EXISTS TB_Categoria;
DROP TABLE IF EXISTS TB_Subcategoria;
DROP TABLE IF EXISTS TB_Sabor;
GO

CREATE TABLE TB_Funcionario
(
	ID_Funcionario INT PRIMARY KEY IDENTITY(1,1),
	NM_Funcionario VARCHAR(50) NOT NULL,
	DS_Sexo VARCHAR(1),
	DT_Nascimento DATE,
	NR_CPF NUMERIC(11) NOT NULL,
	NR_Telefone NUMERIC(11) NOT NULL,
	DS_Email VARCHAR(35),
	NR_CEP VARCHAR(10),
	DS_Logradouro VARCHAR(50) NOT NULL,
	NR_Casa VARCHAR(5) NOT NULL,
	NM_Bairro VARCHAR(50) NOT NULL,
	DS_Complemento VARCHAR(50),
	NM_Cidade VARCHAR(30),
	DS_UF VARCHAR(2),
	DS_Cargo VARCHAR(30) NOT NULL,
	VL_Salario DECIMAL(7,2) NOT NULL,
	DT_Admissao DATE NOT NULL,
	DT_Demissao DATE,
	Ativo BIT NOT NULL
);

CREATE TABLE TB_NivelAcesso
(
	ID_NivelAcesso INT PRIMARY KEY IDENTITY(1,1),
	DS_NivelAcesso VARCHAR(10) NOT NULL
);

CREATE TABLE TB_Login
(
	ID_Login INT PRIMARY KEY IDENTITY(1,1),
	ID_NivelAcesso INT NOT NULL,
	ID_Funcionario INT NOT NULL,
	DS_Usuario VARCHAR(20) NOT NULL,
	DS_Senha VARCHAR(20) NOT NULL,
	Ativo BIT NOT NULL,
	FOREIGN KEY (ID_NivelAcesso) REFERENCES TB_NivelAcesso(ID_NivelAcesso),
	FOREIGN KEY (ID_Funcionario) REFERENCES TB_Funcionario(ID_Funcionario)
);

CREATE TABLE TB_Categoria
(
	ID_Categoria INT IDENTITY(1,1),
	NM_Categoria VARCHAR(50) NOT NULL,
	DS_Categoria VARCHAR(1500) NOT NULL,
    Ativo BIT NOT NULL,
    PRIMARY KEY (ID_Categoria)
);

CREATE TABLE TB_Subcategoria
(
	ID_Subcategoria INT IDENTITY(1,1),
    ID_Categoria INT NOT NULL,
    NM_Subcategoria VARCHAR(50) NOT NULL,
    DS_Subcategoria VARCHAR(1500) NOT NULL,
    Ativo BIT NOT NULL,
    PRIMARY KEY (ID_Subcategoria)
);

CREATE TABLE TB_Sabor
(
	ID_Sabor INT IDENTITY(1,1),
    NM_Sabor VARCHAR(50) NOT NULL,
    Ativo BIT NOT NULL,
    PRIMARY KEY (ID_Sabor)
);

CREATE TABLE TB_Marca
(
	ID_Marca INT IDENTITY(1,1),
    NM_Marca VARCHAR(50) NOT NULL,
    Ativo BIT NOT NULL,
    PRIMARY KEY (ID_Marca)
);

CREATE TABLE TB_Produto 
(
	ID_Produto INT IDENTITY(1,1),
    ID_Marca INT NOT NULL,
    ID_Categoria INT NOT NULL,
    ID_Subcategoria INT NOT NULL,
	ID_Sabor INT NOT NULL,
	NR_EAN VARCHAR(18) NOT NULL,
    NM_Produto VARCHAR(50) NOT NULL,
    DS_Produto VARCHAR(3000) NOT NULL,
	PR_Custo DECIMAL(10,2) NOT NULL,
    PR_Venda DECIMAL(10,2) NOT NULL,
    Ativo BIT NOT NULL,
    PRIMARY KEY (ID_Produto),
    FOREIGN KEY (ID_Categoria) REFERENCES TB_Categoria(ID_Categoria),
    FOREIGN KEY (ID_Subcategoria) REFERENCES TB_Subcategoria(ID_Subcategoria)
);

CREATE TABLE TB_Estoque
(
	ID_Produto INT NOT NULL,
    QTD_Estoque INT NOT NULL,
    Ativo BIT NOT NULL,
    PRIMARY KEY (ID_Produto),
	FOREIGN KEY (ID_Produto) REFERENCES TB_Produto(ID_Produto)
);

CREATE TABLE TB_MovimentacaoEstoque
(
	ID_MovimentacaoEstoque INT IDENTITY(1,1),
	ID_Produto INT NOT NULL,
	QTD_MovimentacaoEstoque INT NOT NULL,
	DS_MovimentacaoEstoque VARCHAR(20) NOT NULL,
	DT_MovimentacaoEstoque DATETIME,
	QTD_Estoque INT NOT NULL,
	PRIMARY KEY(ID_MovimentacaoEstoque),
	FOREIGN KEY(ID_Produto) REFERENCES TB_Produto(ID_Produto)
);

GO

INSERT INTO TB_Funcionario
(
	NM_Funcionario,
	DS_Sexo,
	DT_Nascimento,
	NR_CPF,
	NR_Telefone,
	DS_Email,
	NR_CEP,
	DS_Logradouro,
	NR_Casa,
	NM_Bairro,
	DS_Complemento,
	NM_Cidade,
	DS_UF,
	DS_Cargo,
	VL_Salario,
	DT_Admissao,
	Ativo
)

VALUES
(
	'Caio',
	'M',
	'2001-01-08',
	43867140812,
	15974079495,
	'caio.vcruz@outlook.com',
	18076290,
	'Rubião de Almeida',
	1426,
	'Jardim São Conrado',
	'',
	'Sorocaba',
	'SP',
	'Gerente',
	11000.00,
	'2010-01-08',
	1
);
  
INSERT INTO TB_NivelAcesso
(
	DS_NivelAcesso
)
VALUES
(
	'Gerente'
);

INSERT INTO TB_Login
(
	ID_NivelAcesso,
	ID_Funcionario,
	DS_Usuario,
    DS_Senha,
    Ativo
)
VALUES
(
	1,
    1,
    'caiovcruz',
	'cruz123',
	1
);

INSERT INTO TB_Categoria
(
	NM_Categoria,
    DS_Categoria,
    Ativo
)
VALUES
(
	'Proteina',
    'Categoria utilizada para wheys em geral',
    1
);
  
INSERT INTO TB_Subcategoria
(
	ID_Categoria,
	NM_Subcategoria,
    DS_Subcategoria,
    Ativo
)
VALUES
(
	1,
	'Concentrada',
    'Categoria utilizada para wheys concentrados',
    1
); 
  
INSERT INTO TB_Sabor
(
    NM_Sabor,
    Ativo
)
VALUES
(
	'Chocolate',
    1
); 
  
INSERT INTO TB_Marca
(
	NM_Marca,
    Ativo
)
VALUES
(
	'Muscle Definition',
    1
);
  
INSERT INTO TB_Produto
(
    ID_Marca,
	ID_Categoria,
    ID_Subcategoria,
	ID_Sabor,
	NR_EAN,
	NM_Produto,
    DS_Produto,
	PR_Custo,
    PR_Venda,
    Ativo
)
VALUES
(
	1,
	1,
    1,
    1,
	'7895461257854',
	'WHEY 100% 900g',
    'Tabela Nutricional: 32g (dose) = 5,8g carboidrato, 20g proteína, 8g glutamina, 4,5 BCAA',
    50.00,
    70.00,
    1
);

INSERT INTO TB_Estoque
(
	ID_Produto,
	QTD_Estoque,
    Ativo
)
VALUES
(
	1,
	10,
    1
);

INSERT INTO TB_MovimentacaoEstoque
(
	ID_Produto,
	QTD_MovimentacaoEstoque,
	DS_MovimentacaoEstoque,
	DT_MovimentacaoEstoque,
	QTD_Estoque
)
VALUES
(
	1,
	2,
	'Entrada',
	'2020-10-01',
	5
);

SELECT 
PROD.ID_Produto,
PROD.NR_EAN,
PROD.NM_Produto, 
MAR.NM_Marca,
CAT.NM_Categoria, 
SUB.NM_Subcategoria, 
SAB.NM_Sabor, 
PROD.DS_Produto, 
EST.QTD_Estoque,
FORMAT(PROD.PR_Custo, 'N2') AS PR_Custo,
FORMAT(PROD.PR_Venda, 'N2') AS PR_Venda
FROM TB_Produto AS PROD
INNER JOIN TB_Marca AS MAR
ON PROD.ID_Marca = MAR.ID_Marca
INNER JOIN TB_Categoria AS CAT
ON PROD.ID_Categoria = CAT.ID_Categoria
INNER JOIN TB_Subcategoria AS SUB
ON PROD.ID_Subcategoria = SUB.ID_Subcategoria
INNER JOIN TB_Sabor AS SAB
ON PROD.ID_Sabor = SAB.ID_Sabor
INNER JOIN TB_Estoque AS EST
ON PROD.ID_Produto = EST.ID_Produto
WHERE PROD.Ativo = 1 
ORDER BY PROD.ID_Produto DESC;