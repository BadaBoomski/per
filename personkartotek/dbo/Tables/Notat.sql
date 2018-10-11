--
-- Create Table    : 'Notat'   
-- notatID         :  
-- personID        :  (references Person.personID)
-- notat           :  
--
CREATE TABLE Notat (
    notatID        BIGINT IDENTITY(1,1) NOT NULL,
    personID       BIGINT NOT NULL,
    notat          NVARCHAR NOT NULL,
CONSTRAINT pk_Notat PRIMARY KEY CLUSTERED (notatID),
CONSTRAINT fk_Notat FOREIGN KEY (personID)
    REFERENCES Person (personID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)