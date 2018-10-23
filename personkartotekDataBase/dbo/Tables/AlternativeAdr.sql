--
-- Create Table    : 'AlternativeAdr'   
-- adresseID       :  (references Adresse.adresseID)
-- personID        :  (references Person.personID)
-- AAtype          :  
--
CREATE TABLE AlternativeAdr (
    adresseID      BIGINT NOT NULL,
    personID       BIGINT NOT NULL,
    AAtype         NVARCHAR(50) NOT NULL,
    CONSTRAINT pk_AlternativeAdr PRIMARY KEY CLUSTERED (adresseID,personID),
CONSTRAINT fk_AlternativeAdr FOREIGN KEY (adresseID)
    REFERENCES Adresse (adresseID)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
CONSTRAINT fk_AlternativeAdr2 FOREIGN KEY (personID)
    REFERENCES Person (personID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)