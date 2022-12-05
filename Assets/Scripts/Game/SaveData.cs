using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tmp_time;

    [SerializeField]
    private TextMeshProUGUI _tmp_count;

    private int _count, _min, _sec;

    private void OnEnable()
    {
        Game.OnWin += WriteResult;
    }

    private void OnDisable()
    {
        Game.OnWin -= WriteResult;
    }

    private void WriteResult()
    {
        _count = Convert.ToInt32(_tmp_count.text);

        _min = Convert.ToInt32(_tmp_time.text.Split(' ')[0]);
        _sec = Convert.ToInt32(_tmp_time.text.Split(' ')[2]);

        int pos_in_table = 0;

        bool was_written = false;

        string path = Application.persistentDataPath + "/records.rec";

        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            FileStream streamRead = new FileStream(path, FileMode.Open);

            Records data = formatter.Deserialize(streamRead) as Records;

            streamRead.Close();

            FileStream streamWrite = new FileStream(path, FileMode.Open);

            Records newRecords = new Records();

            foreach (var item in data._records)
            {
                if (pos_in_table == 10) break;

                pos_in_table++;

                if (!was_written && (item._minutes > _min || (item._minutes == _min && item._seconds > _sec)))
                {
                    Record newRecord1 = new Record();

                    newRecord1._place = pos_in_table;
                    newRecord1._minutes = _min;
                    newRecord1._seconds = _sec;
                    newRecord1._moves = _count;
                    newRecords._records.Add(newRecord1);

                    if (pos_in_table == 10) break;

                    pos_in_table++;

                    Record newRecord2 = new Record();

                    newRecord2._place = pos_in_table;
                    newRecord2._minutes = item._minutes;
                    newRecord2._seconds = item._seconds;
                    newRecord2._moves = item._moves;
                    newRecords._records.Add(newRecord2);

                    was_written = true;
                }
                else
                {
                    Record newRecord = new Record();

                    newRecord._place = pos_in_table;
                    newRecord._minutes = item._minutes;
                    newRecord._seconds = item._seconds;
                    newRecord._moves = item._moves;
                    newRecords._records.Add(newRecord);
                }


                if (!was_written && pos_in_table < 10 && pos_in_table == data._records.Count)
                {
                    pos_in_table++;

                    Record newRecord = new Record();
                    
                    newRecord._place = pos_in_table;
                    newRecord._minutes = _min;
                    newRecord._seconds = _sec;
                    newRecord._moves = _count;
                    newRecords._records.Add(newRecord);
                }
            }

            formatter.Serialize(streamWrite, newRecords);
            streamWrite.Close();

            return;
        }

        FileStream stream = new FileStream(path, FileMode.Create);

        Records firstRecords = new Records();
        Record record = new Record();

        record._place = 1;
        record._minutes = _min;
        record._seconds = _sec;
        record._moves = _count;
        firstRecords._records.Add(record);

        formatter.Serialize(stream, firstRecords);
        stream.Close();
    }
}
