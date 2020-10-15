DROP TABLE IF EXISTS tb_login;
DROP TABLE IF EXISTS tb_produto;
DROP TABLE IF EXISTS tb_categoria;
DROP TABLE IF EXISTS tb_subcategoria;
DROP TABLE IF EXISTS tb_sabor;

CREATE TABLE tb_login 
(
	ID_Login INT NOT NULL AUTO_INCREMENT,
	DS_Usuario VARCHAR(20) NOT NULL,
	DS_Senha VARCHAR(20) NOT NULL,
	Ativo BIT NOT NULL,
    PRIMARY KEY (ID_Login)
);

CREATE TABLE tb_categoria
(
	ID_Categoria INT NOT NULL AUTO_INCREMENT,
	NM_Categoria VARCHAR(50) NOT NULL,
	DS_Categoria VARCHAR(1500),
    Ativo BIT NOT NULL,
    PRIMARY KEY (ID_Categoria)
);

CREATE TABLE tb_subcategoria
(
	ID_Subcategoria INT NOT NULL AUTO_INCREMENT,
    ID_Categoria INT NOT NULL,
    NM_Subcategoria VARCHAR(50) NOT NULL,
    DS_Subcategoria VARCHAR(1500),
    Ativo BIT NOT NULL,
    PRIMARY KEY (ID_Subcategoria)
);

CREATE TABLE tb_sabor
(
	ID_Sabor INT NOT NULL AUTO_INCREMENT,
    DS_Sabor VARCHAR(50),
    Ativo BIT NOT NULL,
    PRIMARY KEY (ID_Sabor)
);

CREATE TABLE tb_produto 
(
	ID_Produto INT NOT NULL AUTO_INCREMENT,
    ID_Categoria INT NOT NULL,
    ID_Subcategoria INT NOT NULL,
	ID_Sabor INT NOT NULL,
    NM_Produto VARCHAR(50) NOT NULL,
    DS_Produto VARCHAR(3000),
    QTD_Estoque DECIMAL(10,2) NOT NULL,
	PR_Custo DECIMAL(10,2) NOT NULL,
    PR_Venda DECIMAL(10,2) NOT NULL,
    Ativo BIT NOT NULL,
    PRIMARY KEY (ID_Produto),
    FOREIGN KEY (ID_Categoria) REFERENCES tb_categoria(ID_Categoria),
    FOREIGN KEY (ID_Subcategoria) REFERENCES tb_subcategoria(ID_Subcategoria)
  );
  
  INSERT INTO tb_login
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

INSERT INTO tb_categoria
(
	NM_Categoria,
    DS_Categoria,
    Ativo
)
VALUES
(
	'Whey',
    'Categoria utilizada para wheys em geral',
    1
);
  
INSERT INTO tb_subcategoria
(
	ID_Categoria,
	NM_Subcategoria,
    DS_Subcategoria,
    Ativo
)
VALUES
(
	1,
	'Concentrado',
    'Categoria utilizada para wheys concentrados',
    1
); 
  
INSERT INTO tb_sabor
(
    DS_Sabor,
    Ativo
)
VALUES
(
	'Chocolate',
    1
); 
  
INSERT INTO tb_produto
(
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
	'WHEY 100% 900g',
    'Tabela Nutricional: 32g (dose) = 5,8g carboidrato, 20g proteína, 8g glutamina, 4,5 BCAA',
    10.00,
    50.00,
    70.00,
    1
);

SELECT 
PROD.ID_Produto, 
PROD.ID_Categoria, 
CAT.NM_Categoria, 
PROD.ID_Subcategoria, 
SUB.NM_Subcategoria, 
PROD.NM_Produto, 
PROD.ID_Sabor, 
SAB.DS_Sabor, 
PROD.DS_Produto, 
FORMAT(PROD.QTD_Estoque, 2, 'de_DE') AS QTD_Estoque,
FORMAT(PROD.PR_Custo, 2, 'de_DE') AS PR_Custo,
FORMAT(PROD.PR_Venda, 2, 'de_DE') AS PR_Venda
FROM tb_produto AS PROD
INNER JOIN tb_categoria AS CAT
ON PROD.ID_Categoria = CAT.ID_Categoria
INNER JOIN tb_subcategoria AS SUB
ON PROD.ID_Subcategoria = SUB.ID_Subcategoria
INNER JOIN tb_sabor AS SAB
ON PROD.ID_Sabor = SAB.ID_Sabor
WHERE PROD.Ativo = 1 
ORDER BY PROD.ID_Produto DESC;