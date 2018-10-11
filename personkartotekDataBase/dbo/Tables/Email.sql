--
-- Create Table    : 'Email'   
-- emailID         :  
-- email           :  
-- personID        :  (references Person.personID)
--
CREATE TABLE Email (
    emailID        BIGINT IDENTITY(1,1) NOT NULL,
    email          NVARCHAR(100) NOT NULL,
    personID       BIGINT NOT NULL,
CONSTRAINT pk_Email PRIMARY KEY CLUSTERED (emailID),
CONSTRAINT fk_Email FOREIGN KEY (personID)
    REFERENCES Person (personID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)