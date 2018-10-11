--
-- Create Table    : 'Notat'   
-- notatID         :  
-- personID        :  (references Person.personID)
-- Notat           :  
--
CREATE TABLE Notat (
    notatID        BIGINT IDENTITY(1,1) NOT NULL,
    personID       BIGINT NOT NULL,
    Notat          NVARCHAR(MAX) NOT NULL,
CONSTRAINT pk_Notat PRIMARY KEY CLUSTERED (notatID),
CONSTRAINT fk_Notat FOREIGN KEY (personID)
    REFERENCES Person (personID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)