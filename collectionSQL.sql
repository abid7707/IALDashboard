ROW_ID, ORDER_NO, FSTINSAL_DATE, 
   EMI_AMOUNT, TOTALDUE, INTOTALDUE, 
   INTRESTAMT, DP_AMOUNT, DP_OPEN, 
   DELIVERY_DATE, SELLPRICE, MR_COLL, 
   MONTHLY_COLL, LAST_INSTAL_DATE, DP_PENALTY, 
   DP_PARTIAL_PENALTY, INSTAL_PENALTY, INSTAL_PARTIAL_PENALTY, 
   DCDC_PENALTY, FROM_DATE, TO_DATE, 
   OVERDUE, OPEN_AMOUNT, CREATE_DATE, 
   LEASING_DATE, ROW_ID, ORDER_NO, CUSTOMER_NO, 
   CUSTOMER_NAME, PRESENT_CITY, FINANCIAL_INST, 
   C_CUSTOMER_NO, C_CUSTOMER_NAME, C_PRESENT_CITY, 
   RO_CODE, RO_NAME, ZONE_NAME, 
   REGNO, CATALOG_NO, CATALOG_DESC, 
   STATUS, LA_STATUS, NOVEHICLE, 
   AUTHORIZE_CODE, FROM_DATE, TO_DATE, 
   OVERDUE, OPEN_AMOUNT, CREATE_DATE, 
   LEASING_DATE




SELECT CUS.ROW_ID, CUS.ORDER_NO, CUS.CUSTOMER_NO, CUS.CUSTOMER_NAME, CUS.PRESENT_CITY, 
CUS.FINANCIAL_INST, CUS.C_CUSTOMER_NO, CUS.C_CUSTOMER_NAME, CUS.C_PRESENT_CITY, CUS.RO_CODE, 
CUS.RO_NAME, CUS.ZONE_NAME, CUS.REGNO, CUS.CATALOG_NO, CUS.CATALOG_DESC, CUS.STATUS, 
CUS.LA_STATUS, CUS.NOVEHICLE, CUS.AUTHORIZE_CODE, CUS.FROM_DATE, CUS.TO_DATE, CUS.OVERDUE, 
CUS.OPEN_AMOUNT, CUS.CREATE_DATE, CUS.LEASING_DATE , COLL.ORDER_NO, COLL.FSTINSAL_DATE, COLL.EMI_AMOUNT, 
COLL.TOTALDUE, COLL.INTOTALDUE, COLL.INTRESTAMT, COLL.DP_AMOUNT, COLL.DP_OPEN, COLL.DELIVERY_DATE, COLL.SELLPRICE, 
COLL.MR_COLL, COLL.MONTHLY_COLL, COLL.LAST_INSTAL_DATE, COLL.DP_PENALTY, COLL.DP_PARTIAL_PENALTY, COLL.INSTAL_PENALTY, 
COLL.INSTAL_PARTIAL_PENALTY, COLL.DCDC_PENALTY, COLL.FROM_DATE, COLL.TO_DATE, COLL.OVERDUE, COLL.OPEN_AMOUNT, COLL.CREATE_DATE, 
COLL.LEASING_DATE,
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
where COLL.FROM_DATE=TO_DATE(P_FROM_DATE,'dd-mm-yyyy');






(AI)=AJ2-AK2
LAST_MONTH_MR_COLL = MR_COLL - MONTHLY_COLL

(AB)=IF(AA2<AI2,0,AA2-AI2)
OP_RECEIVABLE_BAL = INTOTALDUE < LAST_MONTH_MR_COLL ? 0 : INTOTALDUE - LAST_MONTH_MR_COLL

   OP_RECEIVABLE_BAL = CASE WHEN INTOTALDUE < (MR_COLL - MONTHLY_COLL) THEN 0
                       ELSE (INTOTALDUE (MR_COLL - MONTHLY_COLL)) END OP_RECEIVABLE_BAL


(X)=U2-AJ2
Overdue = TOTALDUE - MR_COLL
   Overdue = (TOTALDUE - MR_COLL) OVERDUE

(Y)=IF(U2<AJ2,0,U2-AJ2)
ActualOverdue = TOTALDUE < MR_COLL ? 0, TOTALDUE - MR_COLL
   ACTUAL_OD = CASE WHEN TOTALDUE < MR_COLL THEN 0
               ELSE (TOTALDUE - MR_COLL) END ACTUAL_OD

(AC)=IF(AA2<AJ2,0,AA2-AJ2)
CL.Receivable.balance = INTOTALDUE < MR_COLL ? 0 : INTOTALDUE - MR_COLL

   CL_RECEIVABLE = CASE WHEN INTOTALDUE < MR_COLL THEN 0
                   ELSE (INTOTALDUE - MR_COLL) END CL_RECEIVABLE


-- COMPLEX L1
()=IF(AB2>=Q2,Q2,AB2)
RAW_OP_TAR_INST_AMT =  OP_RECEIVABLE_BAL >= EMI_AMOUNT ? EMI_AMOUNT : OP_RECEIVABLE_BAL

      RAW_OP_TAR_INST_AMT = CASE WHEN OP_RECEIVABLE_BAL >= EMI_AMOUNT THEN EMI_AMOUNT
                        ELSE OP_RECEIVABLE_BAL END OP_TAR_INST_AMT

(S)=IF(Q2>=AC2,AC2,Q2)
Tar.Inst.Amt = CL_RECEIVABLE >= EMI_AMOUNT ? EMI_AMOUNT : CL_RECEIVABLE
      RAW_TAR_INST_AMT = CASE WHEN CL_RECEIVABLE >= EMI_AMOUNT THEN EMI_AMOUNT
                        ELSE CL_RECEIVABLE END RAW_TAR_INST_AMT

-- COMPLEX L2

(R)OpeningTar.Inst.Amt =IF(TO_DATE<FSTINSAL_DATE,0,IF(OVERDUE<EMI_AMOUNT*(-1),0,RAW_OP_TAR_INST_AMT))
   OP_TAR_INST_AMT  = CASE WHEN TO_DATE < FSTINSAL_DATE THEN 0
                      WHEN OVERDUE < (EMI_AMOUNT * (-1)) THEN 0
                      ELSE RAW_OP_TAR_INST_AMT END OP_TAR_INST_AMT

                      if (TO_DATE < FSTINSAL_DATE){
                        print(0);
                      } else if (OVERDUE < (EMI_AMOUNT * (-1))) {
                        print(0);
                      } else {
                        print(RAW_OP_TAR_INST_AMT)
                      }

(S)TAR_INST_AMT  = CASE WHEN TO_DATE < FSTINSAL_DATE THEN 0
                      WHEN OPENING_OVERDUE < (EMI_AMOUNT * (-1)) THEN 0
                      ELSE RAW_TAR_INST_AMT END TAR_INST_AMT


(AL)=IF(AK2>=R2,R2,AK2)
Inst.Col = MONTHLY_COLL >= OpeningTar.Inst.Amt ? OpeningTar.Inst.Amt : MONTHLY_COLL
         INST_COLL = CASE WHEN MONTHLY_COLL >= OP_TAR_INST_AMT THEN OP_TAR_INST_AMT
                     ELSE MONTHLY_COLL END INST_COLL




-- COMPLEX L3

(AM)=AK2-AL2
Bal.Coll = MONTHLY_COLL - Inst.COll


-------------------???????-----------------------------
Last_Month_TotalDue = last month TOTALDUE
 
(Z)=IF(T2<AI2,0,T2-AI2) ??
Opening Overdue = Last_Month_TotalDue - Last_Month_MR_Collection


(AN)=IF(AM2<=Z2,AM2,Z2)
OvdColl = Bal.Coll <= OPENING_OVERDUE ? Bal.Coll : OPENING_OVERDUE

(AO)=AM2-AN2
ExcessColl = Bal.Col - OvdColl

INST

(V)=ROUND(Z2/Q2,2)
No.OVD = Opening Overdue / EMI_AMOUNT

(W)=IF(V2<=6,"06 Month ",IF(V2<=12,"12 month",IF(V2<=24,"24 month",IF(V2<36,"36 month",
IF(V2<=48,"48 month",IF(V2<=60,"60 month",IF(V2<=72,"72 month",IF(V2>72,"72 month above"))))))))

Ageing = CASE WHEN NO.OVD <= 6 THEN '06 Month'
         WHEN NO.OVD <= 12 THEN '12 Month'
         WHEN NO.OVD <= 24 THEN '24 Month'
         WHEN NO.OVD <= 36 THEN '36 Month'
         WHEN NO.OVD <= 48 THEN '48 Month'
         WHEN NO.OVD <= 60 THEN '60 Month'
         WHEN NO.OVD <= 72 THEN '72 Month'
         ELSE '72 Month Above'
         END

         





-----------------------RO SUMMARY---------------------------------------

SELECT RO_CODE, RO_NAME, SUM(TAR_INST_AMT) TAR_INST_AMT , SUM(EMI_AMOUNT) EMI_AMOUNT, 
SUM(MR_COLL) MR_COLL, SUM(OVERDUE) OVERDUE, SUM(MONTHLY_COLL) MONTHLY_COLL, SUM(INST_COLL) INST_COLL
FROM(
        SELECT ROW_ID, ORDER_NO, CUSTOMER_NO, CUSTOMER_NAME, PRESENT_CITY, 
                        FINANCIAL_INST, C_CUSTOMER_NO, C_CUSTOMER_NAME, C_PRESENT_CITY, RO_CODE, 
                        RO_NAME, ZONE_NAME, REGNO, CATALOG_NO, CATALOG_DESC, STATUS, 
                        LA_STATUS, NOVEHICLE, AUTHORIZE_CODE, FROM_DATE, 
                        OPEN_AMOUNT, CREATE_DATE, LEASING_DATE , FSTINSAL_DATE,
                        TOTALDUE, INTOTALDUE, INTRESTAMT, DP_AMOUNT, DP_OPEN, DELIVERY_DATE, SELLPRICE, 
                        MR_COLL, MONTHLY_COLL, TAR_INST_AMT, LAST_INSTAL_DATE, TRUNC(DP_PENALTY,2),  TRUNC(DP_PARTIAL_PENALTY,2) , TRUNC(INSTAL_PENALTY,2), 
                        TRUNC(INSTAL_PARTIAL_PENALTY,2) , TRUNC(DCDC_PENALTY,2) , LAST_MONTH_MR_COLL, OP_RECEIVABLE_BAL, OVERDUE, ACTUAL_OD,
                        CL_RECEIVABLE,  EMI_AMOUNT, OP_TAR_INST_AMT,
                        CASE WHEN MONTHLY_COLL >= OP_TAR_INST_AMT THEN OP_TAR_INST_AMT
                                     ELSE MONTHLY_COLL END INST_COLL
                
                FROM

                (SELECT ROW_ID, ORDER_NO, CUSTOMER_NO, CUSTOMER_NAME, PRESENT_CITY, 
                        FINANCIAL_INST, C_CUSTOMER_NO, C_CUSTOMER_NAME, C_PRESENT_CITY, RO_CODE, 
                        RO_NAME, ZONE_NAME, REGNO, CATALOG_NO, CATALOG_DESC, STATUS, 
                        LA_STATUS, NOVEHICLE, AUTHORIZE_CODE, FROM_DATE, 
                        OPEN_AMOUNT, CREATE_DATE, LEASING_DATE , FSTINSAL_DATE, EMI_AMOUNT, 
                        TOTALDUE, INTOTALDUE, INTRESTAMT, DP_AMOUNT, DP_OPEN, DELIVERY_DATE, SELLPRICE, 
                        MR_COLL, MONTHLY_COLL, LAST_INSTAL_DATE, DP_PENALTY, DP_PARTIAL_PENALTY, INSTAL_PENALTY, 
                        INSTAL_PARTIAL_PENALTY, DCDC_PENALTY, LAST_MONTH_MR_COLL, OP_RECEIVABLE_BAL, OVERDUE, ACTUAL_OD,
                        CL_RECEIVABLE, 
                        CASE WHEN OP_RECEIVABLE_BAL >= EMI_AMOUNT THEN EMI_AMOUNT
                                     ELSE OP_RECEIVABLE_BAL END OP_TAR_INST_AMT,
                        CASE WHEN EMI_AMOUNT >= CL_RECEIVABLE THEN CL_RECEIVABLE
                                     ELSE EMI_AMOUNT END TAR_INST_AMT
                        FROM
                            (
                                SELECT CUS.ROW_ID, CUS.ORDER_NO, CUS.CUSTOMER_NO, CUS.CUSTOMER_NAME, CUS.PRESENT_CITY, 
                                CUS.FINANCIAL_INST, CUS.C_CUSTOMER_NO, CUS.C_CUSTOMER_NAME, CUS.C_PRESENT_CITY, CUS.RO_CODE, 
                                CUS.RO_NAME, CUS.ZONE_NAME, CUS.REGNO, CUS.CATALOG_NO, CUS.CATALOG_DESC, CUS.STATUS, 
                                CUS.LA_STATUS, CUS.NOVEHICLE, CUS.AUTHORIZE_CODE, 
                                CUS.LEASING_DATE , COLL.FSTINSAL_DATE, COLL.EMI_AMOUNT, 
                                COLL.TOTALDUE, COLL.INTOTALDUE, COLL.INTRESTAMT, COLL.DP_AMOUNT, COLL.DP_OPEN, COLL.DELIVERY_DATE, COLL.SELLPRICE, 
                                COLL.MR_COLL, COLL.MONTHLY_COLL, COLL.LAST_INSTAL_DATE, COLL.DP_PENALTY, COLL.DP_PARTIAL_PENALTY, COLL.INSTAL_PENALTY, 
                                COLL.INSTAL_PARTIAL_PENALTY, COLL.DCDC_PENALTY, COLL.FROM_DATE, COLL.TO_DATE, COLL.OPEN_AMOUNT, COLL.CREATE_DATE, 
                                --COLL.LEASING_DATE,
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
                                where COLL.FROM_DATE=TO_DATE('2022-6-1','yyyy-mm-dd')
                            )))
GROUP BY RO_CODE, RO_NAME

------------------RO SHEET--------------------------

SELECT RO_CODE, RO_NAME,ZONE_NAME, ORDER_NO, CUSTOMER_NAME, REGNO, CATALOG_NO, SUM(TAR_INST_AMT) TAR_INST_AMT , SUM(EMI_AMOUNT) EMI_AMOUNT, 
SUM(MR_COLL) MR_COLL, SUM(OVERDUE) OVERDUE, SUM(MONTHLY_COLL) MONTHLY_COLL, SUM(INST_COLL) INST_COLL
FROM(
        SELECT ROW_ID, ORDER_NO, CUSTOMER_NO, CUSTOMER_NAME, PRESENT_CITY, 
                        FINANCIAL_INST, C_CUSTOMER_NO, C_CUSTOMER_NAME, C_PRESENT_CITY, RO_CODE, 
                        RO_NAME, ZONE_NAME, REGNO, CATALOG_NO, CATALOG_DESC, STATUS, 
                        LA_STATUS, NOVEHICLE, AUTHORIZE_CODE, FROM_DATE, 
                        OPEN_AMOUNT, CREATE_DATE, LEASING_DATE , FSTINSAL_DATE,
                        TOTALDUE, INTOTALDUE, INTRESTAMT, DP_AMOUNT, DP_OPEN, DELIVERY_DATE, SELLPRICE, 
                        MR_COLL, MONTHLY_COLL, TAR_INST_AMT, LAST_INSTAL_DATE, TRUNC(DP_PENALTY,2),  TRUNC(DP_PARTIAL_PENALTY,2) , TRUNC(INSTAL_PENALTY,2), 
                        TRUNC(INSTAL_PARTIAL_PENALTY,2) , TRUNC(DCDC_PENALTY,2) , LAST_MONTH_MR_COLL, OP_RECEIVABLE_BAL, OVERDUE, ACTUAL_OD,
                        CL_RECEIVABLE,  EMI_AMOUNT, OP_TAR_INST_AMT,
                        CASE WHEN MONTHLY_COLL >= OP_TAR_INST_AMT THEN OP_TAR_INST_AMT
                                     ELSE MONTHLY_COLL END INST_COLL
                
                FROM

                (SELECT ROW_ID, ORDER_NO, CUSTOMER_NO, CUSTOMER_NAME, PRESENT_CITY, 
                        FINANCIAL_INST, C_CUSTOMER_NO, C_CUSTOMER_NAME, C_PRESENT_CITY, RO_CODE, 
                        RO_NAME, ZONE_NAME, REGNO, CATALOG_NO, CATALOG_DESC, STATUS, 
                        LA_STATUS, NOVEHICLE, AUTHORIZE_CODE, FROM_DATE, 
                        OPEN_AMOUNT, CREATE_DATE, LEASING_DATE , FSTINSAL_DATE, EMI_AMOUNT, 
                        TOTALDUE, INTOTALDUE, INTRESTAMT, DP_AMOUNT, DP_OPEN, DELIVERY_DATE, SELLPRICE, 
                        MR_COLL, MONTHLY_COLL, LAST_INSTAL_DATE, DP_PENALTY, DP_PARTIAL_PENALTY, INSTAL_PENALTY, 
                        INSTAL_PARTIAL_PENALTY, DCDC_PENALTY, LAST_MONTH_MR_COLL, OP_RECEIVABLE_BAL, OVERDUE, ACTUAL_OD,
                        CL_RECEIVABLE, 
                        CASE WHEN OP_RECEIVABLE_BAL >= EMI_AMOUNT THEN EMI_AMOUNT
                                     ELSE OP_RECEIVABLE_BAL END OP_TAR_INST_AMT,
                        CASE WHEN EMI_AMOUNT >= CL_RECEIVABLE THEN CL_RECEIVABLE
                                     ELSE EMI_AMOUNT END TAR_INST_AMT
                        FROM
                            (
                                SELECT CUS.ROW_ID, CUS.ORDER_NO, CUS.CUSTOMER_NO, CUS.CUSTOMER_NAME, CUS.PRESENT_CITY, 
                                CUS.FINANCIAL_INST, CUS.C_CUSTOMER_NO, CUS.C_CUSTOMER_NAME, CUS.C_PRESENT_CITY, CUS.RO_CODE, 
                                CUS.RO_NAME, CUS.ZONE_NAME, CUS.REGNO, CUS.CATALOG_NO, CUS.CATALOG_DESC, CUS.STATUS, 
                                CUS.LA_STATUS, CUS.NOVEHICLE, CUS.AUTHORIZE_CODE, 
                                CUS.LEASING_DATE , COLL.FSTINSAL_DATE, COLL.EMI_AMOUNT, 
                                COLL.TOTALDUE, COLL.INTOTALDUE, COLL.INTRESTAMT, COLL.DP_AMOUNT, COLL.DP_OPEN, COLL.DELIVERY_DATE, COLL.SELLPRICE, 
                                COLL.MR_COLL, COLL.MONTHLY_COLL, COLL.LAST_INSTAL_DATE, COLL.DP_PENALTY, COLL.DP_PARTIAL_PENALTY, COLL.INSTAL_PENALTY, 
                                COLL.INSTAL_PARTIAL_PENALTY, COLL.DCDC_PENALTY, COLL.FROM_DATE, COLL.TO_DATE, COLL.OPEN_AMOUNT, COLL.CREATE_DATE, 
                                --COLL.LEASING_DATE,
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
                                where COLL.FROM_DATE=TO_DATE('2022-6-1','yyyy-mm-dd') AND RO_CODE = '2000495'
                                and UPPER(CUS.ZONE_NAME) like nvl(UPPER(''),'%')
                            )))
GROUP BY ORDER_NO, CUSTOMER_NAME, RO_CODE, RO_NAME, REGNO, CATALOG_NO ,ZONE_NAME
                            
                            

(NVL(INSTAL_PENALTY,0) + NVL(INSTAL_PARTIAL_PENALTY,0)) - NVL(INS_DC_PAYMENT,0) INS_DC









CREATE OR REPLACE PROCEDURE REPORTDB.PROC_COLLECTION_REPORT_5 (
   P_FROM_DATE IN      VARCHAR2,
   pcursor     OUT SYS_REFCURSOR
   )
AS
BEGIN
 
   OPEN PCURSOR FOR 
      SELECT ROW_ID, ORDER_NO, CUSTOMER_NO, CUSTOMER_NAME, PRESENT_CITY, 
                FINANCIAL_INST, C_CUSTOMER_NO, C_CUSTOMER_NAME, C_PRESENT_CITY, RO_CODE, 
                RO_NAME, ZONE_NAME, REGNO, CATALOG_NO, CATALOG_DESC, STATUS, 
                LA_STATUS, NOVEHICLE, AUTHORIZE_CODE, FROM_DATE, 
                OPEN_AMOUNT, CREATE_DATE, LEASING_DATE , FSTINSAL_DATE, EMI_AMOUNT, LAST_MONTH_TOTALDUE,
                TOTALDUE, INTOTALDUE, INTRESTAMT, DP_AMOUNT, DP_OPEN, DELIVERY_DATE, SELLPRICE, 
                MR_COLL, MONTHLY_COLL, LAST_INSTAL_DATE, TRUNC(DP_PENALTY,2),  TRUNC(DP_PARTIAL_PENALTY,2) , TRUNC(INSTAL_PENALTY,2), 
                TRUNC(INSTAL_PARTIAL_PENALTY,2) , TRUNC(DCDC_PENALTY,2) , LAST_MONTH_MR_COLL, OP_RECEIVABLE_BAL, OVERDUE, ACTUAL_OD,
                CL_RECEIVABLE,  EMI_AMOUNT, OP_TAR_INST_AMT,
                CASE WHEN MONTHLY_COLL >= OP_TAR_INST_AMT THEN OP_TAR_INST_AMT
                             ELSE MONTHLY_COLL END INST_COLL
        
        FROM

        (SELECT ROW_ID, ORDER_NO, CUSTOMER_NO, CUSTOMER_NAME, PRESENT_CITY, 
                FINANCIAL_INST, C_CUSTOMER_NO, C_CUSTOMER_NAME, C_PRESENT_CITY, RO_CODE, 
                RO_NAME, ZONE_NAME, REGNO, CATALOG_NO, CATALOG_DESC, STATUS, 
                LA_STATUS, NOVEHICLE, AUTHORIZE_CODE, FROM_DATE, 
                OPEN_AMOUNT, CREATE_DATE, LEASING_DATE , FSTINSAL_DATE, EMI_AMOUNT, LAST_MONTH_TOTALDUE, 
                TOTALDUE, INTOTALDUE, INTRESTAMT, DP_AMOUNT, DP_OPEN, DELIVERY_DATE, SELLPRICE, 
                MR_COLL, MONTHLY_COLL, LAST_INSTAL_DATE, DP_PENALTY, DP_PARTIAL_PENALTY, INSTAL_PENALTY, 
                INSTAL_PARTIAL_PENALTY, DCDC_PENALTY, LAST_MONTH_MR_COLL, OP_RECEIVABLE_BAL, OVERDUE, ACTUAL_OD,
                CL_RECEIVABLE, 
                CASE WHEN OP_RECEIVABLE_BAL >= EMI_AMOUNT THEN EMI_AMOUNT
                             ELSE OP_RECEIVABLE_BAL END OP_TAR_INST_AMT,
                CASE WHEN EMI_AMOUNT >= CL_RECEIVABLE THEN CL_RECEIVABLE
                             ELSE EMI_AMOUNT END TAR_INST_AMT
                FROM
                    (
                        SELECT CUS.ROW_ID, CUS.ORDER_NO, CUS.CUSTOMER_NO, CUS.CUSTOMER_NAME, CUS.PRESENT_CITY, 
                        CUS.FINANCIAL_INST, CUS.C_CUSTOMER_NO, CUS.C_CUSTOMER_NAME, CUS.C_PRESENT_CITY, CUS.RO_CODE, 
                        CUS.RO_NAME, CUS.ZONE_NAME, CUS.REGNO, CUS.CATALOG_NO, CUS.CATALOG_DESC, CUS.STATUS, 
                        CUS.LA_STATUS, CUS.NOVEHICLE, CUS.AUTHORIZE_CODE, 
                        CUS.LEASING_DATE , COLL.FSTINSAL_DATE, COLL.EMI_AMOUNT, 
                        COLL.TOTALDUE, COLL.INTOTALDUE, COLL.INTRESTAMT, COLL.DP_AMOUNT, COLL.DP_OPEN, COLL.DELIVERY_DATE, COLL.SELLPRICE, 
                        COLL.MR_COLL, COLL.MONTHLY_COLL, COLL.LAST_INSTAL_DATE, COLL.DP_PENALTY, COLL.DP_PARTIAL_PENALTY, COLL.INSTAL_PENALTY, 
                        COLL.INSTAL_PARTIAL_PENALTY, COLL.DCDC_PENALTY, COLL.FROM_DATE, COLL.TO_DATE, COLL.OPEN_AMOUNT, COLL.CREATE_DATE, 
                        
                        (SELECT DISTINCT TOTALDUE
                        FROM IAL_C_CO_COLLECTION_DETAIL LAST_MONTH_COLL
                        where LAST_MONTH_COLL.FROM_DATE=ADD_MONTHS(TO_DATE(P_FROM_DATE,'yyyy-mm-dd'),-1) AND LAST_MONTH_COLL.ORDER_NO = CUS.ORDER_NO) LAST_MONTH_TOTALDUE,
                        
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
                        where COLL.FROM_DATE=TO_DATE(P_FROM_DATE,'yyyy-mm-dd')
                    ));
                    

   
EXCEPTION
   WHEN OTHERS
   THEN
       dbms_output.put_line('Error!'); 
END PROC_COLLECTION_REPORT_5;
/
