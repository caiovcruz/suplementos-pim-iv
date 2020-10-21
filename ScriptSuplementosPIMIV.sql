DROP TABLE IF EXISTS TB_Login;
DROP TABLE IF EXISTS TB_Produto;
DROP TABLE IF EXISTS TB_Marca;
DROP TABLE IF EXISTS TB_Categoria;
DROP TABLE IF EXISTS TB_Subcategoria;
DROP TABLE IF EXISTS TB_Sabor;
GO

CREATE TABLE TB_Login 
(
	ID_Login INT IDENTITY(1,1),
	DS_Usuario VARCHAR(20) NOT NULL,
	DS_Senha VARCHAR(20) NOT NULL,
	Ativo BIT NOT NULL,
    PRIMARY KEY (ID_Login)
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
    NM_Produto VARCHAR(50) NOT NULL,
    DS_Produto VARCHAR(3000) NOT NULL,
    QTD_Estoque INT NOT NULL,
	PR_Custo DECIMAL(10,2) NOT NULL,
    PR_Venda DECIMAL(10,2) NOT NULL,
    Ativo BIT NOT NULL,
    PRIMARY KEY (ID_Produto),
    FOREIGN KEY (ID_Categoria) REFERENCES TB_Categoria(ID_Categoria),
    FOREIGN KEY (ID_Subcategoria) REFERENCES TB_Subcategoria(ID_Subcategoria)
);

GO
  
  INSERT INTO TB_Login
(
	DS_Usuario,
    DS_Senha,
    Ativo
)
VALUES
(
	'admin',
    'admin',
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
	NM_Produto,
    DS_Produto,
    QTD_Estoque,
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
	'WHEY 100% 900g',
    'Tabela Nutricional: 32g (dose) = 5,8g carboidrato, 20g proteína, 8g glutamina, 4,5 BCAA',
    10,
    50.00,
    70.00,
    1
);

SELECT 
MAR.NM_Marca,
PROD.NM_Produto, 
CAT.NM_Categoria, 
SUB.NM_Subcategoria, 
SAB.NM_Sabor, 
PROD.DS_Produto, 
PROD.QTD_Estoque,
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
WHERE PROD.Ativo = 1 
ORDER BY PROD.ID_Produto DESC;