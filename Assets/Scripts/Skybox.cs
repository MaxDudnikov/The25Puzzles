using System;
using System.Collections;
using UnityEngine;

public class Skybox : MonoBehaviour
{
    [SerializeField] private Material[] _skybox = new Material[] { };

    internal DateTime _local = new DateTime();

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        StartCoroutine(Get_Time());
    }

    private IEnumerator Get_Time()
    {
        _local = DateTime.Now;

        if (DateTime.Parse("00:00") < _local && _local < DateTime.Parse("06:00"))
            RenderSettings.skybox = _skybox[0];
        if (DateTime.Parse("06:00") < _local && _local < DateTime.Parse("09:00"))
            RenderSettings.skybox = _skybox[1];
        if (DateTime.Parse("09:00") < _local && _local < DateTime.Parse("12:00"))
            RenderSettings.skybox = _skybox[2];
        if (DateTime.Parse("12:00") < _local && _local < DateTime.Parse("17:00"))
            RenderSettings.skybox = _skybox[3];
        if (DateTime.Parse("17:00") < _local && _local < DateTime.Parse("21:00"))
            RenderSettings.skybox = _skybox[4];
        if (DateTime.Parse("21:00") < _local && _local < DateTime.Parse("23:59"))
            RenderSettings.skybox = _skybox[5];

        yield return new WaitForSeconds(600);
    }
}
public static class StringExtension
{
    public static string ToTitleCase(this string s)
    {
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s);
    }
}
