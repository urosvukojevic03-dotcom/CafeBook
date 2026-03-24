create database CafeBook;

go

USE CafeBook;

go

-- Tabela: Stolovi
CREATE TABLE Stolovi (
    Id INT PRIMARY KEY IDENTITY,
    BrojStola INT NOT NULL,
    Kapacitet INT NOT NULL,
    Lokacija NVARCHAR(50) NOT NULL  -- npr. "Terasa", "Unutra"
);

-- Tabela: Rezervacije
CREATE TABLE Rezervacije (
    Id INT PRIMARY KEY IDENTITY,
    ImeGosta NVARCHAR(100) NOT NULL,
    BrojTelefona NVARCHAR(20) NOT NULL,
    DatumVreme DATETIME NOT NULL,
    BrojOsoba INT NOT NULL,
    StolId INT NOT NULL,
    FOREIGN KEY (StolId) REFERENCES Stolovi(Id)
);

-- Tabela: Korisnici (admin)
CREATE TABLE Korisnici (
    Id INT PRIMARY KEY IDENTITY,
    KorisnickoIme NVARCHAR(50) NOT NULL,
    Lozinka NVARCHAR(255) NOT NULL,
    JeAdmin BIT NOT NULL DEFAULT 0
);

INSERT INTO Stolovi (BrojStola, Kapacitet, Lokacija) VALUES
(1, 2, 'Unutra'),
(2, 4, 'Unutra'),
(3, 4, 'Terasa'),
(4, 6, 'Terasa'),
(5, 2, 'Bar');