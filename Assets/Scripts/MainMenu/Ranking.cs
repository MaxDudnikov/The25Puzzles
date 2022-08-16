using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Ranking : MonoBehaviour
{
    public delegate void d_OpenStats();
    public static event d_OpenStats e_OpenStats;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private TextMeshProUGUI _textStats;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() => ShowStats());
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private void ShowStats()
    {
        string path = Application.persistentDataPath + "/records.rec";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Records data = formatter.Deserialize(stream) as Records;
            stream.Close();

            _textStats.text = "";
            foreach (var item in data._records)
            {
                _textStats.text += item._place + "." + "\t" +
                    item._minutes + ":" + item._seconds + "\t" +
                    item._moves + "\n";
            }
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            _textStats.text = "You don't have any records(((";
        }

        _animator.SetTrigger("OpenIt");
    }
}
