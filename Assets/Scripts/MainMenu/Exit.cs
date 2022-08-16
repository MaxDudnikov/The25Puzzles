using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Exit : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() => Click());
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void Click()
    {
        Application.Quit();
    }
}
