

CREATE DATABASE EstudiantesDb;
GO

USE EstudiantesDb;
GO


CREATE TABLE Profesores (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL
);

GO

CREATE TABLE Materias (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    Creditos INT NOT NULL DEFAULT 3
);

GO

CREATE TABLE MateriaProfesor (
    Id INT PRIMARY KEY IDENTITY,
    MateriaId INT NOT NULL,
    ProfesorId INT NOT NULL,
    FOREIGN KEY (MateriaId) REFERENCES Materias(Id),
    FOREIGN KEY (ProfesorId) REFERENCES Profesores(Id)
);

GO

CREATE INDEX IX_MateriaProfesor_MateriaId ON MateriaProfesor(MateriaId);
GO
CREATE INDEX IX_MateriaProfesor_ProfesorId ON MateriaProfesor(ProfesorId);
GO

CREATE TABLE Estudiantes (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE
);

GO

-- Índice único ya implícito por el UNIQUE del Email
CREATE INDEX IX_Estudiantes_Nombre ON Estudiantes(Nombre);
GO

CREATE TABLE EstudianteMateria (
    Id INT PRIMARY KEY IDENTITY,
    EstudianteId INT NOT NULL,
    MateriaId INT NOT NULL,
    FOREIGN KEY (EstudianteId) REFERENCES Estudiantes(Id),
    FOREIGN KEY (MateriaId) REFERENCES Materias(Id)
);
GO

CREATE INDEX IX_EstudianteMateria_EstudianteId ON EstudianteMateria(EstudianteId);
GO
CREATE INDEX IX_EstudianteMateria_MateriaId ON EstudianteMateria(MateriaId);
GO



-- Profesores
INSERT INTO Profesores (Nombre)
VALUES ('Profesor 1'), ('Profesor 2'), ('Profesor 3'), ('Profesor 4'), ('Profesor 5');

GO

-- Materias
INSERT INTO Materias (Nombre)
VALUES ('Matemáticas'), ('Física'), ('Química'), ('Biología'), ('Historia'),
       ('Literatura'), ('Inglés'), ('Computación'), ('Arte'), ('Educación Física');

GO

-- Relación materias-profesores (2 materias por profe)
INSERT INTO MateriaProfesor (MateriaId, ProfesorId)
VALUES 
  (1, 1), (2, 1),
  (3, 2), (4, 2),
  (5, 3), (6, 3),
  (7, 4), (8, 4),
  (9, 5), (10, 5);

GO



select * from Materias;

select * from Estudiantes;



SELECT m.*
	FROM [MateriaProfesor] AS [m]
		INNER JOIN [Materias] AS [m0] ON [m].[MateriaId] = [m0].[Id]
		INNER JOIN [Profesores] AS [p] ON [m].[ProfesorId] = [p].[Id]
	WHERE [m].[MateriaId] = 1;


    SELECT [t].[Id], [t].[Nombre], [t].[Email], [e0].[MateriaId], [e0].[Id]
      FROM (
          SELECT TOP(1) [e].[Id], [e].[Nombre], [e].[Email]
          FROM [Estudiantes] AS [e]
          WHERE [e].[Id] =  1
      ) AS [t]
      LEFT JOIN [EstudianteMateria] AS [e0] ON [t].[Id] = [e0].[EstudianteId]
      ORDER BY [t].[Id];



Select * from [EstudianteMateria];
delete [EstudianteMateria] where Id in (7,8,9);
