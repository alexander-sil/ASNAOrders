select
info_str_11 as item_desc,
pro_name as category,
max(Str_Value4_04) as price,
sum(ws_Prt.Prt_CurrQnt - isnull(ws_prt.Prt_Reserve, 0)) as qtty,
cast(cmp_unicode as varchar) as item_id,
cmp_name as item_name,
cntr_name as country,
rnp_name as composition,
str_barcode as barcode
			
        from ws_Prt
        where Prt_Qnt > 0 and Prt_CurrQnt - isnull(Prt_Reserve, 0) > 0 and Prt_DateClose is null
            and Cmp_UniCode not in (select Lnk_CmpUniCode2 from Lnk) and Cmp_UniCode > 1
            and Skd_UniCode in (select cll_numprint from cll where cll_typecode = 0)
        group by Cmp_Name, cmp_unicode, pro_name, info_str_11, rnp_name, cntr_name, str_barcode


