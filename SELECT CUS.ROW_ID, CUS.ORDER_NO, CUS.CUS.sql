SELECT CUS.ROW_ID, CUS.ORDER_NO, CUS.CUSTOMER_NO, CUS.CUSTOMER_NAME, CUS.PRESENT_CITY, 
                        FINANCIAL_INST, C_CUSTOMER_NO, C_CUSTOMER_NAME, C_PRESENT_CITY, RO_CODE, 
                        RO_NAME, ZONE_NAME, REGNO, CATALOG_NO, CATALOG_DESC, STATUS, 
                        NVL(INS_DP_COLL.NEW_INS_DC_PAYMENT,0)  INS_DC_COLLECTION, 
                        NVL(INS_DP_COLL.DP_DC_PAYMENT,0) DP_DC_COLLECTION, IAL_PENALTY_CUM.INS_DC_COLL_CUM,
                        LA_STATUS, NOVEHICLE, AUTHORIZE_CODE, VEHICLE_POSITION, VEHICLE_POSITION_DATE,
                        ENGINE_NO, CHASSICS_NO, VTS_CHARGE, PAYMENT_DATE LAST_PAYMENT_DATE, CONTRACT,
                        LEASING_DATE , CASE WHEN FSTINSAL_DATE is null OR FSTINSAL_DATE = 0 THEN TO_DATE(LPAD('1012022', 8, '0'), 'mmddyyyy')
                        ELSE TO_DATE(LPAD(FSTINSAL_DATE, 8, '0'), 'mmddyyyy') END FSTINSAL_DATE,
                        COLL.EMI_AMOUNT,  LAST_MONTH_COLL.LAST_MONTH_TOTALDUE, NVL(INS_DP_COLL.INS_DP_DC_PAYMENT, 0) INS_DP_DC_AMOUNT ,
                        NVL(LAST_MONTH_TOTALDUE, 0) - (MR_COLL - MONTHLY_COLL) OPENING_OVERDUE,
                        COLL.TOTALDUE, COLL.INTOTALDUE, COLL.INTRESTAMT, COLL.DP_AMOUNT, COLL.DP_OPEN, 
                        TO_DATE(LPAD(COLL.DELIVERY_DATE, 8, '0'), 'mmddyyyy') DELIVERY_DATE, COLL.SELLPRICE, 
                        COLL.MR_COLL, COLL.MONTHLY_COLL, 
                        TO_DATE(LPAD(COLL.LAST_INSTAL_DATE , 8, '0'), 'mmddyyyy') LAST_INSTAL_DATE, COLL.DP_PENALTY, COLL.DP_PARTIAL_PENALTY, COLL.INSTAL_PENALTY, 
                        COLL.INSTAL_PARTIAL_PENALTY, COLL.DCDC_PENALTY, COLL.FROM_DATE, COLL.TO_DATE, COLL.OPEN_AMOUNT, COLL.CREATE_DATE, 
                        
                        (MR_COLL - MONTHLY_COLL) LAST_MONTH_MR_COLL, 
                        CASE WHEN INTOTALDUE < (MR_COLL - MONTHLY_COLL) 
                        THEN 0 ELSE (INTOTALDUE - (MR_COLL - MONTHLY_COLL)) END OP_RECEIVABLE_BAL,
                        (TOTALDUE - MR_COLL) OVERDUE,
                        CASE WHEN TOTALDUE < MR_COLL THEN 0
                        ELSE (TOTALDUE - MR_COLL) END ACTUAL_OD,
                        CASE WHEN INTOTALDUE < MR_COLL THEN 0
                        ELSE (INTOTALDUE - MR_COLL) END CL_RECEIVABLE
                        
                        FROM 
                        (
                        SELECT ROW_ID, ORDER_NO, CUSTOMER_NO, CUSTOMER_NAME, PRESENT_CITY, 
                        FINANCIAL_INST, C_CUSTOMER_NO, C_CUSTOMER_NAME, C_PRESENT_CITY, RO_CODE, 
                        RO_NAME, ZONE_NAME, REGNO, CATALOG_NO, CATALOG_DESC, STATUS, 
                        NVL(INS_DP_COLL.NEW_INS_DC_PAYMENT,0)  INS_DC_COLLECTION, 
                        NVL(INS_DP_COLL.DP_DC_PAYMENT,0) DP_DC_COLLECTION, IAL_PENALTY_CUM.INS_DC_COLL_CUM,
                        LA_STATUS, NOVEHICLE, AUTHORIZE_CODE, VEHICLE_POSITION, VEHICLE_POSITION_DATE,
                        ENGINE_NO, CHASSICS_NO, VTS_CHARGE, PAYMENT_DATE LAST_PAYMENT_DATE, CONTRACT,
                        LEASING_DATE , CASE WHEN FSTINSAL_DATE is null OR FSTINSAL_DATE = 0 THEN TO_DATE(LPAD('1012022', 8, '0'), 'mmddyyyy')
                        ELSE TO_DATE(LPAD(FSTINSAL_DATE, 8, '0'), 'mmddyyyy') END FSTINSAL_DATE,
                        EMI_AMOUNT,  LAST_MONTH_COLL.LAST_MONTH_TOTALDUE, NVL(INS_DP_COLL.INS_DP_DC_PAYMENT, 0) INS_DP_DC_AMOUNT ,
                        
                        TOTALDUE, INTOTALDUE, INTRESTAMT, DP_AMOUNT, DP_OPEN, 
                        TO_DATE(LPAD(COLL.DELIVERY_DATE, 8, '0'), 'mmddyyyy') DELIVERY_DATE, SELLPRICE, 
                        MR_COLL, MONTHLY_COLL, 
                        TO_DATE(LPAD(COLL.LAST_INSTAL_DATE , 8, '0'), 'mmddyyyy') LAST_INSTAL_DATE, DP_PENALTY, DP_PARTIAL_PENALTY, INSTAL_PENALTY, 
                        INSTAL_PARTIAL_PENALTY, DCDC_PENALTY, FROM_DATE, TO_DATE, OPEN_AMOUNT, CREATE_DATE, 
                                                                                                                                                        
                        FROM IAL_C_CO_COLLECTION_DETAIL COLL INNER JOIN  IAL_C_COWISE_CUSTOMER_INFO CUS
                        ON ORDER_NO = CUS.ORDER_NO AND FROM_DATE = CUS.FROM_DATE
                                                                                    
                        LEFT JOIN
                        IAL_PENALTY_COLLECTION_REP INS_DP_COLL
                        ON INS_DP_COLL.ORDER_NO = COLL.ORDER_NO
                        AND INS_DP_COLL.PAYMENT_DATE = COLL.FROM_DATE
                        
                        LEFT JOIN
                        (SELECT DISTINCT ORDER_NO, SUM(NVL(NEW_INS_DC_PAYMENT,0 )) INS_DC_COLL_CUM
                        FROM IAL_PENALTY_COLLECTION_REP
                        WHERE PAYMENT_DATE <=TO_DATE(P_FROM_DATE,'yyyy-mm-dd')
                        GROUP BY ORDER_NO
                            ) IAL_PENALTY_CUM 
                            ON IAL_PENALTY_CUM.ORDER_NO = COLL.ORDER_NO
                        
                        LEFT JOIN 
                        (SELECT DISTINCT ORDER_NO, TOTALDUE as LAST_MONTH_TOTALDUE
                        FROM IAL_C_CO_COLLECTION_DETAIL WHERE FROM_DATE = ADD_MONTHS(TO_DATE(P_FROM_DATE,'yyyy-mm-dd'), -1)) LAST_MONTH_COLL
                        ON COLL.ORDER_NO = LAST_MONTH_COLL.ORDER_NO
                        where COLL.FROM_DATE=TO_DATE(P_FROM_DATE,'yyyy-mm-dd')