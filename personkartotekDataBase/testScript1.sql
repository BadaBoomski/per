
/* The testScript1.sql is just for testing scripts before adding them to the personkartotekDBUtil.cs 
	Notice: https://www.w3schools.com/sql/ has a lot of help/examples and so on */


SELECT * FROM Person

SELECT personID, fornavn FROM Person

SELECT * FROM PERSON WHERE persontype = 'fætter';

/* Below is shown the CRUD-SQL statements. */

/* Used in AddPersonDB */
INSERT INTO Person(fornavn, mellemnavn, efternavn, persontype, adresseID) OUTPUT INSERTED.personID VALUES ('Rasmus', 'Walter', 'Andersen', 'Ven', 1)

/* Used in the GetPersonByFirstAndLastName */
SELECT  TOP 1 * FROM Person WHERE (Fornavn = @fornavn) AND (Efternavn=@efternavn)

/* Used in UpdatePersonDB */
UPDATE Person SET Fornavn = 'Karl', Mellemnavn = 'Henrik', Efternavn = 'Arnesen', Persontype = 'Fodboldspiller' WHERE personID = 20


/* Used in deletePersonDB - replace the last with (personID=@personID).. */
DELETE FROM Person WHERE personID=32

SELECT * FROM Adresse WHERE CONTAINS (vejnavn, 'Fin')

SELECT * FROM Adresse WHERE vejnavn LIKE '%Fin%'

SELECT * FROM NOTAT ORDER BY Notat