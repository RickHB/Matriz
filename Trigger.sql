CREATE TABLE Course(
 ID int IDENTITY(1,1) PRIMARY KEY,
 NAME_COURSE varchar(50) NOT NULL,
 [HOURS] DECIMAL(3,1) CHECK ([HOURS] >= 0 AND [HOURS] <= 49), -- Restringe a valores positivos y máximo 50
 DATE_MAXIMUM DATE NOT NULL,
 TYPE_COURSE varchar(20) NOT NULL, -- P --> Normativo, T --> Interno 
 DATE_CREATED Date NOT NULL,
 CREATED_BY varchar(50) NOT NULL,
 ACTIVE bit DEFAULT 1  
);
drop table course
ALTER TABLE COURSE
ADD CONSTRAINT DF_COURSE_ACTIVE DEFAULT 1 FOR ACTIVE;


INSERT INTO Course (NAME_COURSE, [HOURS], DATE_MAXIMUM, TYPE_COURSE, DATE_CREATED, CREATED_BY, ACTIVE) VALUES
('Mathematics Basics', 10.5,'2024-01-15', 'Normativo','2024-01-15', 'Alice', 1),
('Introduction to Physics', 20.0,'2024-01-15', 'Interno','2024-02-20', 'Bob', 0),
('Advanced Chemistry', 15.3,'2024-01-15', 'Normativo', '2024-03-10', 'Charlie', 1),
('Biology 101', 30.0,'2024-01-15', 'Normativo','2024-04-05', 'David', 1),
('History of Art', 25.5,'2024-01-15', 'Interno', '2024-05-18', 'Eve', 0),
('Philosophy and Ethics', 22.5,'2024-01-15', 'Normativo', '2024-07-03', 'Grace', 1),
('Economics 101', 35.0,'2024-01-15', 'Normativo', '2024-08-15', 'Hank', 1),
('Basic Programming', 40.0,'2024-01-15', 'Normativo', '2024-09-10', 'Ivy', 0),
('Environmental Science', 28.7,'2024-01-15', 'Interno', '2024-10-25', 'Jack', 1);
SELECT * FROM Course
----------------------------------------------------------------------------------------------------------------
CREATE TABLE Employee(
  NOMINA int PRIMARY KEY,
  LEADER_ID int, 
  ACTIVE bit, 
);

INSERT INTO Employee (NOMINA, LEADER_ID, ACTIVE)
VALUES
  (142946, 10120, 1),
  (142947, 10120, 0),
  (142948, 10121, 1),
  (142949, 10121, 1),
  (142950, 10122, 1),
  (142951, 10122, 0),
  (142952, 10123, 1),
  (142953, 10123, 1),
  (142954, 10124, 1),
  (142955, 10124, 0),
  (142956, 10125, 1),
  (142957, 10125, 1),
  (142958, 10126, 1),
  (142959, 10126, 0),
  (142960, 10127, 1),
  (142961, 10127, 1),
  (142962, 10128, 1),
  (142963, 10128, 0),
  (142964, 10129, 1),
  (142965, 10129, 1);

SELECT * FROM Phases_Course
CREATE TABLE Phases_Course(
 ID int IDENTITY(1,1) PRIMARY KEY,
 TYPE_COURSE varchar(20),
 PHASE_COURSE varchar(20),
);
----------------------------------------------------------------------------------------------------------------
drop table Details_Course
CREATE TABLE Details_Course(
 ID int IDENTITY(1,1) PRIMARY KEY,
 ID_COURSE INT FOREIGN KEY REFERENCES Course(ID),
 ID_NOMINA INT FOREIGN KEY REFERENCES Employee(NOMINA),
 ID_Phases_Course INT FOREIGN KEY REFERENCES Phases_Course(ID),
 [STATUS] varchar(20), -- Restricción CHECK para permitir solo 0, 1, 2, o 3
 TIMES_TAKEN INT NULL,
 DATE_TAKEN DATE NULL,
 DATE_VALIDATED DATE NULL, --SI HAY FECHA ES QUE LO PASE ESTATUS 2 (TRIGGER)
);
select * from course
Select PHASE_COURSE from Phases_Course
SELECT * FROM Details_Course
DELETE FROM Details_Course WHERE ID = 1
INSERT INTO Details_Course (ID_COURSE, ID_NOMINA, ID_Phases_Course,[STATUS], TIMES_TAKEN, DATE_TAKEN, DATE_VALIDATED) --PROGRAMAR
VALUES
  (1, 142956,3, 'Completo',1,'2024-01-15','2024-01-15' ), --PENDIENTE

  (1, 142956,3, 'Pendiente',1,NULL,NULL ), --PENDIENTE
  (2, 142956,NULL, 'Pendiente',1,NULL,NULL ) --PENDIENTE
  (1, 142956,1, 'En Progreso',1,'2024-01-15',NULL ) --PENDIENTE

  
-----------------------------------------------------------------------------

 SELECT C.ID,  C.NAME_COURSE, PH.Phase_Course, PH.Type_Course, C.HOURS, C.DATE_CREATED, C.CREATED_BY, C.ACTIVE AS 'COURSE ACTIVE', EM.NOMINA, EM.LEADER_ID, EM.ACTIVE AS 'EMPLOYEE ACTIVE', DE.STATUS, DE.TIMES_TAKEN,  C.TYPE_COURSE, C.DATE_MAXIMUM, DE.DATE_TAKEN, DE.TIMES_TAKEN, DE.DATE_VALIDATED
	FROM Course as C
	INNER JOIN Details_Course DE ON  DE.ID_COURSE = C.ID
    INNER JOIN Employee EM ON  EM.NOMINA = DE.ID_NOMINA
	LEFT JOIN Phases_Course PH ON DE.ID_Phases_Course = PH.ID where c.NAME_COURSE= 'Introduction to Physics'
-----------------------------------------------------------------------------
select D.*, PH.* from Details_Course D
INNER JOIN Phases_Course PH ON PH.ID = D.ID_COURSE 

--VALIDAR CURSO, DEBE MODIFICAR UNA COLUMNA EXISTENTE, SI  TIMES_TAKEN NO ES NULL CREAME UNA NUEVA COLUMNA 

UPDATE Details_Course
SET TIMES_TAKEN = 1
WHERE ID_NOMINA = 142956; 
DROP TABLE Course
delete from Details_Course WHERE ID_COURSE = 7;

DBCC CHECKIDENT ('Details_Course', RESEED, 0);

CREATE TRIGGER trg_UpdateStatusOnDateValidated
ON Details_Course
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE Details_Course
    SET [STATUS] = 2
    FROM inserted i
    WHERE Details_Course.ID = i.ID
      AND i.DATE_VALIDATED IS NOT NULL;
END;


INSERT INTO Phases_Course (Type_Course, Phase_Course) VALUES ('Fatalidad', 'Fase 1');
INSERT INTO Phases_Course (Type_Course, Phase_Course) VALUES ('Fatalidad', 'Fase 2');
INSERT INTO Phases_Course (Type_Course, Phase_Course) VALUES ('Fatalidad', 'Validacion');

-- Tipo de curso: Normal
INSERT INTO Phases_Course (Type_Course, Phase_Course) VALUES ('Normal', 'Fase 1');
INSERT INTO Phases_Course (Type_Course, Phase_Course) VALUES ('Normal', 'Validacion');

-- Tipo de curso: Ex DAY
INSERT INTO Phases_Course (Type_Course, Phase_Course) VALUES ('Ex DAY', 'Validacion');


CREATE TRIGGER trg_UpdateDateValidated
ON Details_Course
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(STATUS)
    BEGIN
        UPDATE dc
        SET dc.DATE_VALIDATED = GETDATE()
        FROM Details_Course dc
        INNER JOIN inserted i ON dc.ID =
		i.ID
        WHERE i.STATUS = 'Completo';
    END
END;
