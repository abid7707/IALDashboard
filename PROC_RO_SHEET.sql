CREATE OR REPLACE PROCEDURE REPORTDB.PROC_RO_SHEET (
   P_FROM_DATE IN      VARCHAR2,
   P_RO_CODE IN VARCHAR2,
   P_ZONE_NAME IN VARCHAR2,
   pcursor     OUT SYS_REFCURSOR
   )
AS
BEGIN
 
   OPEN PCURSOR FOR 
        SELECT RO_CODE, RO_NAME,ZONE_NAME, ORDER_NO, FSTINSAL_DATE, CUSTOMER_NAME, NVL(SUM(EMI_AMOUNT),0) EMI_AMOUNT,
        NVL(SUM(INS_DC_PAYMENT),0) INS_DC_PAYMENT, NVL(SUM(DP_DC_PAYMENT),0) DP_DC_PAYMENT, REGNO, CATALOG_NO,CATALOG_DESC, 
        NVL(SUM(TAR_INST_AMT),0) TAR_INST_AMT , NVL(SUM(EMI_AMOUNT),0) EMI_AMOUNT, 
        NVL(SUM(NO_OF_OVERDUE),0) NO_OF_OVERDUE, NVL(SUM(MR_COLL),0) MR_COLL, NVL(SUM(OVERDUE),0) OVERDUE, 
        NVL(SUM(MONTHLY_COLL),0) MONTHLY_COLL, NVL(SUM(INST_COLL),0) INST_COLL,
        CASE WHEN SUM(MONTHLY_COLL) = 0 THEN 0
        ELSE 1 END ATTEN_V
        FROM(SELECT ROW_ID, ORDER_NO, CUSTOMER_NO, CUSTOMER_NAME, PRESENT_CITY,
                 INS_DC_PAYMENT, DP_DC_PAYMENT,
                FINANCIAL_INST, C_CUSTOMER_NO, C_CUSTOMER_NAME, C_PRESENT_CITY, RO_CODE, NO_OF_OVERDUE,
                RO_NAME, ZONE_NAME, REGNO, CATALOG_NO, CATALOG_DESC, STATUS, TAR_INST_AMT,
                LA_STATUS, NOVEHICLE, AUTHORIZE_CODE, FROM_DATE, 
                OPEN_AMOUNT, CREATE_DATE, LEASING_DATE , FSTINSAL_DATE, EMI_AMOUNT, LAST_MONTH_TOTALDUE, OPENING_OVERDUE,
                TOTALDUE, INTOTALDUE, INTRESTAMT, DP_AMOUNT, DP_OPEN, DELIVERY_DATE, SELLPRICE, 
                MR_COLL, MONTHLY_COLL, LAST_INSTAL_DATE, DP_PENALTY,  DP_PARTIAL_PENALTY , INSTAL_PENALTY, 
                INSTAL_PARTIAL_PENALTY, DCDC_PENALTY , LAST_MONTH_MR_COLL, OP_RECEIVABLE_BAL, OVERDUE, ACTUAL_OD,
                CL_RECEIVABLE, OP_TAR_INST_AMT, INST_COLL, AGEING, BAL_COLL, OD_COLECTION,
                (BAL_COLL - OD_COLECTION) EXCESS_COLLECTION
                
                FROM (

                SELECT ROW_ID, ORDER_NO, CUSTOMER_NO, CUSTOMER_NAME, PRESENT_CITY, 
                                FINANCIAL_INST, C_CUSTOMER_NO, C_CUSTOMER_NAME, C_PRESENT_CITY, RO_CODE, 
                                INS_DC_PAYMENT, DP_DC_PAYMENT,
                                RO_NAME, ZONE_NAME, REGNO, CATALOG_NO, CATALOG_DESC, STATUS, NO_OF_OVERDUE,
                                LA_STATUS, NOVEHICLE, AUTHORIZE_CODE, FROM_DATE, 
                                OPEN_AMOUNT, CREATE_DATE, LEASING_DATE , FSTINSAL_DATE, EMI_AMOUNT, LAST_MONTH_TOTALDUE, OPENING_OVERDUE,
                                TOTALDUE, INTOTALDUE, INTRESTAMT, DP_AMOUNT, DP_OPEN, DELIVERY_DATE, SELLPRICE, TAR_INST_AMT,
                                MR_COLL, MONTHLY_COLL, LAST_INSTAL_DATE, DP_PENALTY,  DP_PARTIAL_PENALTY , INSTAL_PENALTY, 
                                INSTAL_PARTIAL_PENALTY, DCDC_PENALTY , LAST_MONTH_MR_COLL, OP_RECEIVABLE_BAL, OVERDUE, ACTUAL_OD,
                                CL_RECEIVABLE, OP_TAR_INST_AMT, INST_COLL, AGEING, (MONTHLY_COLL - INST_COLL) BAL_COLL,
                                CASE WHEN (MONTHLY_COLL - INST_COLL) <= OPENING_OVERDUE THEN (MONTHLY_COLL - INST_COLL)
                                ELSE OPENING_OVERDUE END OD_COLECTION
                                
                                FROM
                                
                                (SELECT ROW_ID, ORDER_NO, CUSTOMER_NO, CUSTOMER_NAME, PRESENT_CITY, 
                                        FINANCIAL_INST, C_CUSTOMER_NO, C_CUSTOMER_NAME, C_PRESENT_CITY, RO_CODE,
                                        INS_DC_PAYMENT, DP_DC_PAYMENT,
                                        RO_NAME, ZONE_NAME, REGNO, CATALOG_NO, CATALOG_DESC, STATUS, NO_OF_OVERDUE,
                                        LA_STATUS, NOVEHICLE, AUTHORIZE_CODE, FROM_DATE, 
                                        OPEN_AMOUNT, CREATE_DATE, LEASING_DATE , FSTINSAL_DATE, EMI_AMOUNT, LAST_MONTH_TOTALDUE, OPENING_OVERDUE,
                                        TOTALDUE, INTOTALDUE, INTRESTAMT, DP_AMOUNT, DP_OPEN, DELIVERY_DATE, SELLPRICE, 
                                        MR_COLL, MONTHLY_COLL, LAST_INSTAL_DATE, TRUNC(DP_PENALTY,2) DP_PENALTY,  TRUNC(DP_PARTIAL_PENALTY,2) DP_PARTIAL_PENALTY, 
                                        TRUNC(INSTAL_PENALTY,2) INSTAL_PENALTY,  TRUNC(INSTAL_PARTIAL_PENALTY,2) INSTAL_PARTIAL_PENALTY, 
                                        TRUNC(DCDC_PENALTY,2) DCDC_PENALTY, LAST_MONTH_MR_COLL, OP_RECEIVABLE_BAL, OVERDUE, ACTUAL_OD,
                                        CL_RECEIVABLE, OP_TAR_INST_AMT, TAR_INST_AMT,
                                        CASE WHEN MONTHLY_COLL >= OP_TAR_INST_AMT THEN OP_TAR_INST_AMT
                                                     ELSE MONTHLY_COLL END INST_COLL,
                                         CASE WHEN NO_OF_OVERDUE <= 6 THEN '06 Month'
                                         WHEN NO_OF_OVERDUE <= 12 THEN '12 Month'
                                         WHEN NO_OF_OVERDUE <= 24 THEN '24 Month'
                                         WHEN NO_OF_OVERDUE <= 36 THEN '36 Month'
                                         WHEN NO_OF_OVERDUE <= 48 THEN '48 Month'
                                         WHEN NO_OF_OVERDUE <= 60 THEN '60 Month'
                                         WHEN NO_OF_OVERDUE <= 72 THEN '72 Month'
                                         ELSE '72 Month Above'
                                         END AGEING
                                
                                FROM

                                (SELECT ROW_ID, ORDER_NO, CUSTOMER_NO, CUSTOMER_NAME, PRESENT_CITY, 
                                        FINANCIAL_INST, C_CUSTOMER_NO, C_CUSTOMER_NAME, C_PRESENT_CITY, RO_CODE, 
                                        INS_DC_PAYMENT, DP_DC_PAYMENT,
                                        RO_NAME, ZONE_NAME, REGNO, CATALOG_NO, CATALOG_DESC, STATUS, 
                                        LA_STATUS, NOVEHICLE, AUTHORIZE_CODE, FROM_DATE, 
                                        OPEN_AMOUNT, CREATE_DATE, LEASING_DATE , FSTINSAL_DATE, EMI_AMOUNT, LAST_MONTH_TOTALDUE, OPENING_OVERDUE, 
                                        TOTALDUE, INTOTALDUE, INTRESTAMT, DP_AMOUNT, DP_OPEN, DELIVERY_DATE, SELLPRICE, 
                                        MR_COLL, MONTHLY_COLL, LAST_INSTAL_DATE, DP_PENALTY, DP_PARTIAL_PENALTY, INSTAL_PENALTY, 
                                        INSTAL_PARTIAL_PENALTY, DCDC_PENALTY, LAST_MONTH_MR_COLL, OP_RECEIVABLE_BAL, OVERDUE, ACTUAL_OD,
                                        (NVL(OPENING_OVERDUE, 0) / EMI_AMOUNT) NO_OF_OVERDUE,
                                        CL_RECEIVABLE, 
                                        CASE WHEN OP_RECEIVABLE_BAL >= EMI_AMOUNT THEN EMI_AMOUNT
                                                     ELSE OP_RECEIVABLE_BAL END OP_TAR_INST_AMT,
                                        CASE WHEN EMI_AMOUNT >= CL_RECEIVABLE THEN CL_RECEIVABLE
                                                     ELSE EMI_AMOUNT END TAR_INST_AMT
                                        FROM
                                            (
                                                SELECT CUS.ROW_ID, CUS.ORDER_NO, CUS.CUSTOMER_NO, CUS.CUSTOMER_NAME, CUS.PRESENT_CITY, 
                                                IAL_DC.INS_DC_PAYMENT, IAL_DC.DP_DC_PAYMENT,
                                                CUS.FINANCIAL_INST, CUS.C_CUSTOMER_NO, CUS.C_CUSTOMER_NAME, CUS.C_PRESENT_CITY, CUS.RO_CODE, 
                                                CUS.RO_NAME, CUS.ZONE_NAME, CUS.REGNO, CUS.CATALOG_NO, CUS.CATALOG_DESC, CUS.STATUS, 
                                                CUS.LA_STATUS, CUS.NOVEHICLE, CUS.AUTHORIZE_CODE, 
                                                CUS.LEASING_DATE , TO_DATE(LPAD(COLL.FSTINSAL_DATE, 8, '0'), 'mmddyyyy') FSTINSAL_DATE, COLL.EMI_AMOUNT,  LAST_MONTH_COLL.LAST_MONTH_TOTALDUE,
                                                CASE WHEN LAST_MONTH_COLL.LAST_MONTH_TOTALDUE < (MR_COLL - MONTHLY_COLL) THEN 0
                                                    ELSE  LAST_MONTH_TOTALDUE - (MR_COLL - MONTHLY_COLL) END OPENING_OVERDUE,
                                                COLL.TOTALDUE, COLL.INTOTALDUE, COLL.INTRESTAMT, COLL.DP_AMOUNT, COLL.DP_OPEN, COLL.DELIVERY_DATE, COLL.SELLPRICE, 
                                                COLL.MR_COLL, COLL.MONTHLY_COLL, COLL.LAST_INSTAL_DATE, COLL.DP_PENALTY, COLL.DP_PARTIAL_PENALTY, COLL.INSTAL_PENALTY, 
                                                COLL.INSTAL_PARTIAL_PENALTY, COLL.DCDC_PENALTY, COLL.FROM_DATE, COLL.TO_DATE, COLL.OPEN_AMOUNT, COLL.CREATE_DATE, 
                                                
                                                (MR_COLL - MONTHLY_COLL) LAST_MONTH_MR_COLL, 
                                                CASE WHEN INTOTALDUE < (MR_COLL - MONTHLY_COLL) 
                                                THEN 0 ELSE (INTOTALDUE - (MR_COLL - MONTHLY_COLL)) END OP_RECEIVABLE_BAL,
                                                (TOTALDUE - MR_COLL) OVERDUE,
                                                CASE WHEN TOTALDUE < MR_COLL THEN 0
                                                ELSE (TOTALDUE - MR_COLL) END ACTUAL_OD,
                                                CASE WHEN INTOTALDUE < MR_COLL THEN 0
                                                ELSE (INTOTALDUE - MR_COLL) END CL_RECEIVABLE
                                                FROM IAL_C_CO_COLLECTION_DETAIL COLL LEFT JOIN  IAL_C_COWISE_CUSTOMER_INFO CUS
                                                ON COLL.ORDER_NO = CUS.ORDER_NO AND COLL.FROM_DATE = CUS.FROM_DATE
                                                LEFT JOIN
                                                (SELECT DISTINCT ORDER_NO, TOTALDUE as LAST_MONTH_TOTALDUE
                                                FROM IAL_C_CO_COLLECTION_DETAIL WHERE FROM_DATE = ADD_MONTHS(TO_DATE(P_FROM_DATE ,'yyyy-mm-dd'), -1)) LAST_MONTH_COLL
                                                ON COLL.ORDER_NO = LAST_MONTH_COLL.ORDER_NO
                                                LEFT JOIN
                                                IAL_DC_PAYMENT_MONTH_WISE IAL_DC
                                                ON IAL_DC.ORDER_NO = COLL.ORDER_NO
                                                AND IAL_DC.FROM_DATE = COLL.FROM_DATE
                                               where COLL.FROM_DATE=TO_DATE(P_FROM_DATE ,'yyyy-mm-dd') AND RO_CODE like nvl(P_RO_CODE, '%')
                                                and UPPER(CUS.ZONE_NAME) like nvl(UPPER(P_ZONE_NAME),'%')
                        
                    )))))
    GROUP BY ORDER_NO, CUSTOMER_NAME, RO_CODE, RO_NAME, REGNO, CATALOG_NO , CATALOG_DESC, ZONE_NAME, FSTINSAL_DATE
    ORDER BY ZONE_NAME, RO_NAME, FSTINSAL_DATE;
   
EXCEPTION
   WHEN OTHERS
   THEN
       dbms_output.put_line('Error!'); 
END PROC_RO_SHEET;
/
