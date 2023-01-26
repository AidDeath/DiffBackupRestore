#Логика работы:
Будем просить выбрать файл бэкапа в папке, искать все файлы TRN и анализировать цепочку.
Далее предлагаем время, на которое можем восстановить бэкапы.

267 1	269 1*
267 1	272 1/
272 1	273 1/
274 1	276 1*
273 1	279 1/
279 1	280 1/
280 1	281 1/
282 1	284 1*
281 1	287 1/
287 1	288 1/  -- нет изменений
288 1	288 1/ -- нет изменений

CheckpointLsn - Указывает на FirstLsn последнего полного бэкапа?
	-- ТРН, где значение Чекпоинта меньше, чем у полного бэкапа - отсеивать. Они создавались до полного бэкапа.


#Скрипт наших умников:

	# первая TRN
    //Первая ТРН имеет FirstLSN меньше bak.LastLSN? f LastLSN больше bak.LastLSN 
                if (($item_trn.Type -eq "TRN") -and ($item_trn.LastLSN -gt $LastLSN) -and ($item_trn.FirstLSN -lt $LastLSN)) {
                    $TRN_Name = $item_trn.File
                    $LaSstLSN  = $item_trn.LastLN
                    if ($LogChain) {
                        WriteLog "Первая TRN    $($TRN_Name)" $FullLogFile
                    }
                    $null = $ChainTRN.Add($item_trn)
                    continue
                }

                # следующие TRN
                if (($item_trn.Type -eq "TRN") -and ($item_trn.FirstLSN -eq $LastLSN)) {
                    $TRN_Name = $item_trn.File
                    $LastLSN  = $item_trn.LastLSN
                    if ($LogChain) {
                        WriteLog "Следующая TRN $($TRN_Name)" $FullLogFile
                    }
                    $null = $ChainTRN.Add($item_trn)
                    continue
                }

                # не связанные TRN
                if (($item_trn.Type -eq "TRN") -and ($item_trn.FirstLSN -gt $LastLSN)) {
                    $TRN_Name = $item_trn.File
                    WriteLog "! Не связанная TRN $($TRN_Name)" $FullLogFile
                    $null = $SendMail.Add("Не связанная TRN $($TRN_Name)")
                    $null = $LostFiles.Add($item_trn)
                }