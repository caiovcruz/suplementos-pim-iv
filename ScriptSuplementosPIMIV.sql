CREATE TABLE TB_UF
(
	ID_UF INT PRIMARY KEY IDENTITY(1,1),
	NM_UF VARCHAR(75) NOT NULL,
	DS_UF VARCHAR(5) NOT NULL 
);

CREATE TABLE TB_Cidade
(
	ID_Cidade INT PRIMARY KEY,
	ID_UF INT NOT NULL,
	NM_Cidade VARCHAR(120) NOT NULL,
	FOREIGN KEY(ID_UF) REFERENCES TB_UF(ID_UF)
);

CREATE TABLE TB_Funcionario
(
	ID_Funcionario INT PRIMARY KEY IDENTITY(1,1),
	NM_Funcionario VARCHAR(50) NOT NULL,
	DS_Sexo VARCHAR(10) NOT NULL,
	DT_Nascimento DATE NOT NULL,
	NR_CPF VARCHAR(11) NOT NULL,
	NR_Telefone VARCHAR(11) NOT NULL,
	DS_Email VARCHAR(35) NOT NULL,
	NR_CEP VARCHAR(8) NOT NULL,
	DS_Logradouro VARCHAR(50) NOT NULL,
	NR_Casa VARCHAR(5) NOT NULL,
	NM_Bairro VARCHAR(50) NOT NULL,
	DS_Complemento VARCHAR(50),
	ID_UF INT NOT NULL,
	ID_Cidade INT NOT NULL,
	DS_Cargo VARCHAR(30) NOT NULL,
	VL_Salario DECIMAL(7,2) NOT NULL,
	DT_Admissao DATE NOT NULL,
	DT_Demissao DATE,
	Ativo BIT NOT NULL,
	FOREIGN KEY (ID_UF) REFERENCES TB_UF(ID_UF),
	FOREIGN KEY (ID_Cidade) REFERENCES TB_Cidade(ID_Cidade)
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