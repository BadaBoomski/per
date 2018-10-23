--
-- Create Table    : 'Adresse'   
-- adresseID       :  
-- vejnavn         :  
-- adressetype     :  
-- postnrbyID      :  (references PostnrBy.postnrbyID)
--
CREATE TABLE Adresse (
    adresseID      BIGINT IDENTITY(1,1) NOT NULL,
    vejnavn        NVARCHAR(50) NOT NULL,
    postnrbyID     BIGINT NOT NULL,
CONSTRAINT pk_Adresse PRIMARY KEY CLUSTERED (adresseID),
CONSTRAINT fk_Adresse FOREIGN KEY (postnrbyID)
    REFERENCES PostnrBy (postnrbyID)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)