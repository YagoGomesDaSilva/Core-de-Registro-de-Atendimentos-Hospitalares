IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'HospitalDB')
BEGIN
    CREATE DATABASE HospitalDB;
END
GO

USE HospitalDB;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Paciente')
BEGIN
    CREATE TABLE Paciente (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nome VARCHAR(150) NOT NULL,
        Cpf VARCHAR(11) NOT NULL UNIQUE,
        DataNascimento DATE NOT NULL,
        DataRegisto DATETIME DEFAULT GETDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Atendimento')
BEGIN
    CREATE TABLE Atendimento (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        PacienteId INT NOT NULL,
        DataEntrada DATETIME NOT NULL DEFAULT GETDATE(),
        StatusAtendimento VARCHAR(20) NOT NULL DEFAULT 'Ativo',
        PressaoArterial VARCHAR(15) NULL,
        Temperatura DECIMAL(4,2) NULL,
        FrequenciaCardiaca INT NULL,
        CONSTRAINT FK_Atendimento_Paciente FOREIGN KEY (PacienteId) REFERENCES Paciente(Id)
    );
END
GO

INSERT INTO Paciente (Nome, Cpf, DataNascimento) 
VALUES ('João Silva', '12345678901', '1985-04-12');

INSERT INTO Paciente (Nome, Cpf, DataNascimento) 
VALUES ('Maria Santos', '10987654321', '1990-08-25');
GO

INSERT INTO Atendimento (PacienteId, DataEntrada, StatusAtendimento, PressaoArterial, Temperatura, FrequenciaCardiaca)
VALUES (1, GETDATE(), 'Finalizado', '120/80', 36.5, 75);

INSERT INTO Atendimento (PacienteId, DataEntrada, StatusAtendimento, PressaoArterial, Temperatura, FrequenciaCardiaca)
VALUES (2, GETDATE(), 'Ativo', '130/85', 37.2, 82);
GO
