--
-- Create Table    : 'Person'   
-- personID        :  
-- fornavn         :  
-- mellemnavn      :  
-- efternavn       :  
-- persontype      :  
-- adresseID       :  (references Adresse.adresseID)
--
CREATE TABLE Person (
    personID       BIGINT IDENTITY(1,1) NOT NULL,
    fornavn        NVARCHAR(50) NOT NULL,
    mellemnavn     NVARCHAR(50) NULL,
    efternavn      NVARCHAR(50) NOT NULL,
    persontype     NVARCHAR(50) NOT NULL,
    adresseID      BIGINT NULL,
CONSTRAINT pk_Person PRIMARY KEY CLUSTERED (personID),
CONSTRAINT fk_Person FOREIGN KEY (adresseID)
    REFERENCES Adresse (adresseID)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)