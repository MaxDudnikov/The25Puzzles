using UnityEngine;
using UnityEngine.EventSystems;

public class IgnoreRaycast : MonoBehaviour
{
    public static bool IsOnUI { get; private set; }

    private void Update()
    {
        IsOnUI = EventSystem.current.IsPointerOverGameObject();
    }
}
