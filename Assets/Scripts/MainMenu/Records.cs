using System.Collections.Generic;

[System.Serializable]
public class Records
{
    public List<Record> _records = new List<Record>();
}

[System.Serializable]
public class Record
{
    public int _place;
    public int _minutes;
    public int _seconds;
    public int _moves;
}