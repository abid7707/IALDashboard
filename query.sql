--DROP table IAL_STOCK_LOCATION_TEMP1; 

 create table    IAL_STOCK_LOCATION_TEMP1 as
 SELECT 
   PARTDES,PART_NO,PRODUCT_FAMILY,  
sum(BUS_BODY_QTY) BUS_BODY_QTY,                
sum(DEALER_PD_QTY) DEALER_PD_QTY,
sum(FAIR_DEMO_QTY) FAIR_DEMO_QTY,
sum(LC_QTY) LC_QTY,                 
sum(OUTSIDE_BWS_QTY) OUTSIDE_BWS_QTY,
sum(TOTAL_OT_QTY) TOTAL_OT_QTY,
sum(TRANSPORTT_QTY)TRANSPORTT_QTY,

  
sum(Bogra#DO_ISSUED)Bogra#DO_ISSUED,
sum(Bogra#RFD)Bogra#RFD,
sum(Chattogram#BOOKED)Chattogram#BOOKED,
sum(Chattogram#DO_ISSUED)Chattogram#DO_ISSUED,
sum(Chattogram#RFD)Chattogram#RFD,
sum(Cumilla#BOOKED)Cumilla#BOOKED,
sum(Cumilla#DO_ISSUED)Cumilla#DO_ISSUED,
sum(Cumilla#RFD)Cumilla#RFD,
sum(Dhamrai_CBU#DO_ISSUED)Dhamrai_CBU#DO_ISSUED,
sum(Dhamrai_CKD#BUS_BODY)Dhamrai_CKD#BUS_BODY,
sum(Dhamrai_CKD#DO_ISSUED)Dhamrai_CKD#DO_ISSUED,
sum(Jashore#BOOKED)Jashore#BOOKED,
sum(Jashore#DO_ISSUED)Jashore#DO_ISSUED,
sum(Jashore#RFD)Jashore#RFD,
sum(Joydebpur#BOOKED)Joydebpur#BOOKED,
sum(Joydebpur#DO_ISSUED)Joydebpur#DO_ISSUED,
sum(Joydebpur#RFD)Joydebpur#RFD

    FROM (SELECT BUS_BODY_QTY,
                 CONTRACT,
                 DEALER_PD_QTY,
                 FAIR_DEMO_QTY,
                 LC_QTY,
                 LOCATION_DES,
                 OUTSIDE_BWS_QTY,
                 PARTDES,
                 PART_NO,
                 PRODUCT_FAMILY,
                 SITE_STOCK_LOCATION,
                 STK_QTY,
                 STOCK_LOCATION,
                 TOTAL_OT_QTY,
                 TRANSPORTT_QTY
             
            FROM REPORTDB.IAL_STOCK_REPORT_V) PIVOT (SUM (STK_QTY)
                                              FOR SITE_STOCK_LOCATION
                                              IN  ('Bogra#BOOKED' Bogra#BOOKED,
'Bogra#DO_ISSUED' Bogra#DO_ISSUED,
'Bogra#RFD' Bogra#RFD,
'Chattogram#BOOKED' Chattogram#BOOKED,
'Chattogram#DO_ISSUED' Chattogram#DO_ISSUED,
'Chattogram#RFD' Chattogram#RFD,
'Cumilla#BOOKED' Cumilla#BOOKED,
'Cumilla#DO_ISSUED' Cumilla#DO_ISSUED,
'Cumilla#RFD' Cumilla#RFD,
'Dhamrai_CBU#DO_ISSUED' Dhamrai_CBU#DO_ISSUED,
'Dhamrai_CKD#BUS_BODY' Dhamrai_CKD#BUS_BODY,
'Dhamrai_CKD#DO_ISSUED' Dhamrai_CKD#DO_ISSUED,
'Jashore#BOOKED' Jashore#BOOKED,
'Jashore#DO_ISSUED' Jashore#DO_ISSUED,
'Jashore#RFD' Jashore#RFD,
'Joydebpur#BOOKED' Joydebpur#BOOKED,
'Joydebpur#DO_ISSUED' Joydebpur#DO_ISSUED,
'Joydebpur#RFD' Joydebpur#RFD))
GROUP BY PART_NO,PRODUCT_FAMILY, PARTDES
ORDER BY PART_NO
