using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class Music : MonoBehaviour
{
    [SerializeField]
    private AudioSource _music;

    [SerializeField]
    private Sprite _on;

    [SerializeField]
    private Sprite _off;

    private Image _icon;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() => OnVolumeOnOff());
        _icon = GetComponent<Image>();
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private void OnVolumeOnOff()
    {
        _music.mute = !_music.mute;

        if (_music.mute)
            _icon.sprite = _off;
        else
            _icon.sprite = _on;
    }
}
