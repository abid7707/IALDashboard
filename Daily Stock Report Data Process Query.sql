/**
Pull data from IFS and save into REPORTDB as per report fromat
Create Date: 29 June, 2022
**/



DELETE FROM REPORTDB.IAL_DAILY_STK_RPT_TEMP_DATA;

COMMIT;

INSERT INTO REPORTDB.IAL_DAILY_STK_RPT_TEMP_DATA (BUS_BODY_QTY,
                                                  CONTRACT,
                                                  DEALER_PD_QTY,
                                                  FAIR_DEMO_QTY,
                                                  LC_QTY,
                                                  LOCATION_DES,
                                                  OUTSIDE_BWS_QTY,
                                                  PARTDES,
                                                  PART_NO,
                                                  PRODUCT_FAMILY,
                                                  STOCK_LOCATION,
                                                  TOTAL_OT_QTY,
                                                  TRANSPORTT_QTY,
                                                  WDR,
                                                  BOGRA_XX_BOOKED,
                                                  BOGRA_XX_DO_ISSUED,
                                                  BOGRA_XX_RFD,
                                                  CHATTOGRAM_XX_BOOKED,
                                                  CHATTOGRAM_XX_DO_ISSUED,
                                                  CHATTOGRAM_XX_RFD,
                                                  CUMILLA_XX_BOOKED,
                                                  CUMILLA_XX_DO_ISSUED,
                                                  CUMILLA_XX_RFD,
                                                  DHAMRAI_CBU_XX_DO_ISSUED,
                                                  DHAMRAI_CKD_XX_BUS_BODY,
                                                  DHAMRAI_CKD_XX_DO_ISSUED,
                                                  JASHORE_XX_BOOKED,
                                                  JASHORE_XX_DO_ISSUED,
                                                  JASHORE_XX_RFD,
                                                  JOYDEBPUR_XX_BOOKED,
                                                  JOYDEBPUR_XX_DO_ISSUED,
                                                  JOYDEBPUR_XX_RFD)
     SELECT BUS_BODY_QTY,
            CONTRACT,
            DEALER_PD_QTY,
            FAIR_DEMO_QTY,
            LC_QTY,
            LOCATION_DES,
            OUTSIDE_BWS_QTY,
            PARTDES,
            PART_NO,
            PRODUCT_FAMILY,
            STOCK_LOCATION,
            TOTAL_OT_QTY,
            TRANSPORTT_QTY,
            WDR,
            BOGRA_XX_BOOKED,
            BOGRA_XX_DO_ISSUED,
            BOGRA_XX_RFD,
            CHATTOGRAM_XX_BOOKED,
            CHATTOGRAM_XX_DO_ISSUED,
            CHATTOGRAM_XX_RFD,
            CUMILLA_XX_BOOKED,
            CUMILLA_XX_DO_ISSUED,
            CUMILLA_XX_RFD,
            DHAMRAI_CBU_XX_DO_ISSUED,
            DHAMRAI_CKD_XX_BUS_BODY,
            DHAMRAI_CKD_XX_DO_ISSUED,
            JASHORE_XX_BOOKED,
            JASHORE_XX_DO_ISSUED,
            JASHORE_XX_RFD,
            JOYDEBPUR_XX_BOOKED,
            JOYDEBPUR_XX_DO_ISSUED,
            JOYDEBPUR_XX_RFD
       FROM (SELECT NVL (BUS_BODY_QTY, 0) BUS_BODY_QTY,
                    CONTRACT,
                    NVL (DEALER_PD_QTY, 0) DEALER_PD_QTY,
                    NVL (FAIR_DEMO_QTY, 0) FAIR_DEMO_QTY,
                    NVL (LC_QTY, 0) LC_QTY,
                    LOCATION_DES,
                    NVL (OUTSIDE_BWS_QTY, 0) OUTSIDE_BWS_QTY,
                    PARTDES,
                    PART_NO,
                    PRODUCT_FAMILY,
                    SITE_STOCK_LOCATION,
                    NVL (STK_QTY, 0) STK_QTY,
                    STOCK_LOCATION,
                    NVL (TOTAL_OT_QTY, 0) TOTAL_OT_QTY,
                    NVL (TRANSPORTT_QTY, 0) TRANSPORTT_QTY,
                    WDR
               FROM REPORTDB.IAL_STOCK_REPORT_VIEW) PIVOT (SUM (
                                                              NVL (STK_QTY, 0))
                                                    FOR SITE_STOCK_LOCATION
                                                    IN  ('BOGRA_XX_BOOKED' BOGRA_XX_BOOKED,
                                                        'BOGRA_XX_DO_ISSUED' BOGRA_XX_DO_ISSUED,
                                                        'BOGRA_XX_RFD' BOGRA_XX_RFD,
                                                        'CHATTOGRAM_XX_BOOKED' CHATTOGRAM_XX_BOOKED,
                                                        'CHATTOGRAM_XX_DO_ISSUED' CHATTOGRAM_XX_DO_ISSUED,
                                                        'CHATTOGRAM_XX_RFD' CHATTOGRAM_XX_RFD,
                                                        'CUMILLA_XX_BOOKED' CUMILLA_XX_BOOKED,
                                                        'CUMILLA_XX_DO_ISSUED' CUMILLA_XX_DO_ISSUED,
                                                        'CUMILLA_XX_RFD' CUMILLA_XX_RFD,
                                                        'DHAMRAI_CBU_XX_DO_ISSUED' DHAMRAI_CBU_XX_DO_ISSUED,
                                                        'DHAMRAI_CKD_XX_BUS_BODY' DHAMRAI_CKD_XX_BUS_BODY,
                                                        'DHAMRAI_CKD_XX_DO_ISSUED' DHAMRAI_CKD_XX_DO_ISSUED,
                                                        'JASHORE_XX_BOOKED' JASHORE_XX_BOOKED,
                                                        'JASHORE_XX_DO_ISSUED' JASHORE_XX_DO_ISSUED,
                                                        'JASHORE_XX_RFD' JASHORE_XX_RFD,
                                                        'JOYDEBPUR_XX_BOOKED' JOYDEBPUR_XX_BOOKED,
                                                        'JOYDEBPUR_XX_DO_ISSUED' JOYDEBPUR_XX_DO_ISSUED,
                                                        'JOYDEBPUR_XX_RFD' JOYDEBPUR_XX_RFD))
   ORDER BY PART_NO;



DELETE FROM REPORTDB.IAL_DAILY_STK_RPT_PROS_DATA;

COMMIT;


INSERT INTO REPORTDB.IAL_DAILY_STK_RPT_PROS_DATA
   SELECT * FROM REPORTDB.IAL_DAILY_STK_RPT_TEMP_DATA;

COMMIT;