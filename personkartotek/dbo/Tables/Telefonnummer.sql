--
-- Create Table    : 'Telefonnummer'   
-- telefonID       :  
-- telefonummer    :  
-- personID        :  (references Person.personID)
-- teleselskab     :  
-- telefontype     :  
--
CREATE TABLE Telefonnummer (
    telefonID      BIGINT IDENTITY(1,1) NOT NULL,
    telefonummer   BIGINT NOT NULL,
    personID       BIGINT NOT NULL,
    teleselskab    NVARCHAR(20) NOT NULL,
    telefontype    NVARCHAR(20) NOT NULL,
CONSTRAINT pk_Telefonnummer PRIMARY KEY CLUSTERED (telefonID),
CONSTRAINT fk_Telefonnummer FOREIGN KEY (personID)
    REFERENCES Person (personID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)