--create database TRANSQL;

--use TRANSQL;

--create table Departamento (
--IdDepartamento INT primary key IDENTITY NOT NULL,
--NombreDepartamento varchar(100) NOT NULL

--)

--CREATE TABLE Colaborador (
--IdColaborador INT PRIMARY KEY IDENTITY NOT NULL,
--PrimerNombre varchar(50) NOT NULL,
--SegundoNombre varchar(50),
--PrimerApellido varchar(50) NOT NULL,
--SegundoApellido varchar(50),
--ApellidoDeCasada varchar(50),
--Correo varchar(75) NOT NULL,
--IdDepartamento INT NOT NULL,
--FOREIGN KEY (IdDepartamento) REFERENCES Departamento(IdDepartamento),
--)

--create table TipoVehiculo (
--IdTipoVehiculo INT primary key IDENTITY NOT NULL,
--NombreTipoVehiculo varchar(100) NOT NULL
--)

--create table EstadoVehiculo (
--IdEstadoVehiculo INT primary key IDENTITY NOT NULL,
--NombreEstadoVehiculo varchar(100) NOT NULL
--)

--create table Vehiculo (
-- Placa varchar(10) primary key NOT NULL,
--Modelo INT NOT NULL,
--OdometroInicial INT,
--OdometroFinal INT,
--IdTipoVehiculo INT,
--IdEstadoVehiculo INT,
--FOREIGN KEY (IdTipoVehiculo) REFERENCES TipoVehiculo(IdTipoVehiculo),
--FOREIGN KEY (IdEstadoVehiculo) REFERENCES EstadoVehiculo(IdEstadoVehiculo),
--)

--create table Asignacion(
--IdAsignacion INT IDENTITY Primary key,
--IdColaborador INT NULL,
--IdEstadoSolicitud INT NULL,
--CONSTRAINT FK_Asignacion_EstadoSolicitud FOREIGN KEY (IdEstadoSolicitud) REFERENCES EstadoSolicitud(IdEstadoSolicitud),
--CONSTRAINT FK_Asignacion_Colaborador FOREIGN KEY (IdColaborador) REFERENCES Colaborador(IdColaborador),
--)

--create table EstadoSolicitud (
--IdEstadoSolicitud INT primary key IDENTITY NOT NULL,
--NombreEstadoSolicitud varchar(50) NOT NULL
--)

--create table SolicitudReservacion (
--IdSolicitud INT primary key IDENTITY NOT NULL,
--Motivo varchar(300) NOT NULL,
--Fecha DATETIME ,
--IdColaborador INT,
--IdEstadoSolicitud INT,
--FOREIGN KEY (IdColaborador) REFERENCES Colaborador(IdColaborador),
--FOREIGN KEY (IdEstadoSolicitud) REFERENCES EstadoSolicitud(IdEstadoSolicitud),
--)

--create table Reservacion (
--IdReservacion INT primary key IDENTITY NOT NULL,
--FechaReservacion DATETIME,
--FechaInicio DATETIME,
--FechaFin DATETIME,
--Observaciones varchar (300),
--IdSolicitud INT,
--Placa varchar (10),
--FOREIGN KEY (IdSolicitud) REFERENCES SolicitudReservacion(IdSolicitud),
--FOREIGN KEY (Placa) REFERENCES Vehiculo(Placa),
--)

--AQUÍ VOY------------------------------------------------------------------------------------

----create table Odometro (
----IdOdometro INT primary key IDENTITY NOT NULL,
----ValorOdometro INT NOT NULL,
----)

--create table Accesorio (
--IdAccesorio INT primary key IDENTITY NOT NULL,
--NombreAccesorio varchar(75),
--EstadoAccesorio varchar(75),
--)

--create table TipoInspeccion (
--IdTipoInspeccion INT primary key IDENTITY NOT NULL,
--NombreTipoInsepccion varchar(75),
--)

--create table InspeccionVehiculo (
--IdInspeccion INT primary key IDENTITY NOT NULL,
--FechaInspeccion DATETIME,
--Observaciones varchar(250),
--IdReservacion INT,
--IdColaborador INT,
--IdAccesorio INT,
--IdTipoInspeccion INT,
--FOREIGN KEY (IdReservacion) REFERENCES Reservacion(IdReservacion),
--FOREIGN KEY (IdColaborador) REFERENCES Colaborador(IdColaborador),
--FOREIGN KEY (IdAccesorio) REFERENCES Accesorio(IdAccesorio),
--FOREIGN KEY (IdTipoInspeccion) REFERENCES TipoInspeccion(IdTipoInspeccion),
--)

--CREATE TABLE InspeccionAccesorio (
--    IdInspeccion INT,
--    IdAccesorio INT,
--    EstadoAccesorio varchar(75),
--    PRIMARY KEY (IdInspeccion, IdAccesorio),
--    FOREIGN KEY (IdInspeccion) REFERENCES InspeccionVehiculo(IdInspeccion),
--    FOREIGN KEY (IdAccesorio) REFERENCES Accesorio(IdAccesorio)
--)

--ALTER TABLE InspeccionVehiculo ADD OdometroInicial INT, OdometroFinal INT;

--ALTER TABLE SolicitudReservacion ADD JustificacionRechazo VARCHAR(300);

--CREATE TRIGGER Check_Vehiculo_Disponible
--ON Reservacion
--FOR INSERT
--AS
--BEGIN
--    DECLARE @placa VARCHAR(10);
--    SELECT @placa = Placa FROM inserted;
--    IF EXISTS (SELECT 1 FROM Vehiculo WHERE Placa = @placa AND IdEstadoVehiculo != (SELECT IdEstadoVehiculo FROM EstadoVehiculo WHERE NombreEstadoVehiculo = 'Disponible'))
--    BEGIN
--        RAISERROR('El vehículo no está disponible', 16, 1);
--        ROLLBACK TRANSACTION;
--    END
--END;

--Para consultas podría usar:
--SELECT 
--    SR.IdSolicitud, 
--    V.Placa, 
--    R.FechaReservacion, 
--    R.FechaInicio, 
--    R.FechaFin, 
--    C.PrimerNombre + ' ' + C.PrimerApellido AS ResponsableRecepcion, 
--    IV.OdometroInicial, 
--    IV.OdometroFinal, 
--    IV.Observaciones AS ObservacionesInspeccion
--FROM 
--    SolicitudReservacion SR
--JOIN 
--    Reservacion R ON SR.IdSolicitud = R.IdSolicitud
--JOIN 
--    Vehiculo V ON R.Placa = V.Placa
--JOIN 
--    InspeccionVehiculo IV ON IV.IdReservacion = R.IdReservacion
--JOIN 
--    Colaborador C ON C.IdColaborador = SR.IdColaborador
--WHERE 
--    R.FechaInicio BETWEEN '2024-01-01' AND '2024-12-31'
--ORDER BY 
--    R.FechaInicio;

--SELECT 
--    V.Placa, 
--    COUNT(R.IdReservacion) AS TotalViajes, 
--    SUM(IV.OdometroFinal - IV.OdometroInicial) AS RecorridoTotal
--FROM 
--    Vehiculo V
--JOIN 
--    Reservacion R ON V.Placa = R.Placa
--JOIN 
--    InspeccionVehiculo IV ON R.IdReservacion = IV.IdReservacion
--WHERE 
--    V.Placa = 'ABC123'
--GROUP BY 
--    V.Placa;

--CREATE INDEX IDX_Vehiculo_Placa ON Vehiculo (Placa);

--CREATE INDEX IDX_Colaborador_IdDepartamento ON Colaborador (IdDepartamento);

--CREATE INDEX IDX_SolicitudReservacion_IdColaborador ON SolicitudReservacion (IdColaborador);

--CREATE INDEX IDX_SolicitudReservacion_IdEstadoSolicitud ON SolicitudReservacion (IdEstadoSolicitud);

--CREATE INDEX IDX_Reservacion_FechaReservacion ON Reservacion (FechaReservacion);

--CREATE INDEX IDX_InspeccionVehiculo_IdReservacion ON InspeccionVehiculo (IdReservacion);

--CREATE INDEX IDX_Asignacion_IdColaborador ON Asignacion (IdColaborador);

--CREATE INDEX IDX_InspeccionVehiculo_IdColaborador ON InspeccionVehiculo (IdColaborador);

--CREATE INDEX IDX_SolicitudReservacion_Colaborador_Estado ON SolicitudReservacion (IdColaborador, IdEstadoSolicitud);

--SELECT 
--    i.name AS IndexName,
--    c.name AS ColumnName,
--    t.name AS TableName
--FROM 
--    sys.indexes AS i
--JOIN 
--    sys.index_columns AS ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
--JOIN 
--    sys.columns AS c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
--JOIN 
--    sys.tables AS t ON i.object_id = t.object_id
--WHERE 
--    t.name = 'NombreDeLaTabla';

--INSERT INTO Departamento (NombreDepartamento)
--VALUES
--('Recursos Humanos'),
--('Tecnología'),
--('Logística'),
--('Transporte'),
--('Administración'),
--('IT'),
--('Finanzas');

--INSERT INTO Colaborador (PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, ApellidoDeCasada, Correo, IdDepartamento)
--VALUES
--('Juan', 'Carlos', 'Perez', 'Ramirez', NULL, 'juan.perez@example.com', 1),
--('Ana', 'Maria', 'Lopez', 'Hernandez', NULL, 'ana.lopez@example.com', 2),
--('Juan', 'Carlos', 'Pérez', 'López', NULL, 'juan.perez@example.com', 3),
--('María', 'Fernanda', 'Gómez', 'Martínez', 'Lopez', 'maria.gomez@example.com', 4),
--('Carlos', NULL, 'Hernández', 'Sánchez', NULL, 'carlos.hernandez@example.com', 5),
--('Ana', 'Lucía', 'Torres', 'Morales', 'Pérez', 'ana.torres@example.com', 6),
--('Luis', NULL, 'Martínez', 'Jiménez', NULL, 'luis.martinez@example.com', 7),
--('Sara', 'Isabel', 'Castillo', 'Ramírez', NULL, 'sara.castillo@example.com', 8),
--('Lucia', NULL, 'Rodriguez', 'Diaz', 'de Gomez', 'lucia.gomez@example.com', 3);

--INSERT INTO TipoVehiculo (NombreTipoVehiculo) VALUES
--('Pickup'),
--('Automóvil'),
--('Bus');

--INSERT INTO EstadoVehiculo (NombreEstadoVehiculo) VALUES
--('Disponible'),
--('Solicitado'),
--('En Ruta'),
--('Inactivo');

--INSERT INTO Vehiculo (Placa, Modelo, OdometroInicial, OdometroFinal, IdTipoVehiculo, IdEstadoVehiculo) VALUES
--('ABC123', 2020, 5000, NULL, 1, 1),
--('XYZ456', 2019, 30000, NULL, 2, 1),
--('LMN789', 2021, 15000, NULL, 3, 1);

--INSERT INTO EstadoSolicitud (NombreEstadoSolicitud) VALUES
--('Aprobada'),
--('Rechazada'),
--('Pendiente');

--INSERT INTO SolicitudReservacion (Motivo, Fecha, IdColaborador, IdEstadoSolicitud) VALUES
--('Capacitación en ventas', GETDATE(), 2, 3),
--('Compra de insumos', GETDATE(), 3, 3),
--('Viaje a la planta', GETDATE(), 4, 3);

--INSERT INTO Reservacion (FechaReservacion, FechaInicio, FechaFin, Observaciones, IdSolicitud, Placa) VALUES
--(GETDATE(), DATEADD(DAY, 1, GETDATE()), DATEADD(DAY, 3, GETDATE()), 'Reservación para capacitación', 3, 'ABC123'),
--(GETDATE(), DATEADD(DAY, 2, GETDATE()), DATEADD(DAY, 4, GETDATE()), 'Reservación para compra de insumos', 2, 'XYZ456');

--INSERT INTO Accesorio (NombreAccesorio, EstadoAccesorio) VALUES
--('Retrovisor', 'Buen estado'),
--('Radio', 'Mal estado'),
--('Aire acondicionado', 'Medio estado');

--INSERT INTO TipoInspeccion (NombreTipoInsepccion) VALUES
--('Inspección inicial'),
--('Inspección final');

--INSERT INTO InspeccionVehiculo (FechaInspeccion, Observaciones, IdReservacion, IdColaborador, IdAccesorio, IdTipoInspeccion, OdometroInicial, OdometroFinal) VALUES
--(GETDATE(), 'Inspección antes de la entrega', 2, 2, 1, 1, 5000, NULL),
--(GETDATE(), 'Inspección después de la entrega', 2, 2, 1, 2, NULL, 5500);

--INSERT INTO InspeccionAccesorio (IdInspeccion, IdAccesorio, EstadoAccesorio) VALUES
--(3, 1, 'Buen estado'),
--(3, 2, 'Mal estado'),
--(4, 1, 'Buen estado');

--CREATE TRIGGER Check_Vehiculo_Disponible
--ON Reservacion
--FOR INSERT
--AS
--BEGIN
--    DECLARE @placa VARCHAR(10);
--    SELECT @placa = Placa FROM inserted;
--    IF EXISTS (SELECT 1 FROM Vehiculo WHERE Placa = @placa AND IdEstadoVehiculo != (SELECT IdEstadoVehiculo FROM EstadoVehiculo WHERE NombreEstadoVehiculo = 'Disponible'))
--    BEGIN
--        RAISERROR('El vehículo no está disponible', 16, 1);
--        ROLLBACK TRANSACTION;
--    END
--END;
