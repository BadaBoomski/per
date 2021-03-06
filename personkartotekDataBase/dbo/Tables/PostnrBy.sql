﻿--
-- Target: Microsoft SQL Server 
-- Syntax: isql /Uuser /Ppassword /Sserver -i\path\filename.sql
-- Date  : Oct 09 2018 18:51
-- Script Generated by Database Design Studio 2.21.3 
--


--
-- Create Table    : 'PostnrBy'   
-- postnrbyID      :  
-- postnr          :  
-- bynavn          :  
-- land            :  
--
CREATE TABLE PostnrBy (
    postnrbyID     BIGINT IDENTITY(1,1) NOT NULL,
    postnr         NVARCHAR(50) NOT NULL,
    bynavn         NVARCHAR(50) NOT NULL,
    land           NVARCHAR(50) NOT NULL,
CONSTRAINT pk_PostnrBy PRIMARY KEY CLUSTERED (postnrbyID))