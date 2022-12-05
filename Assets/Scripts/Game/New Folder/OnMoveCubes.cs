using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Cube))]
public class OnMoveCubes : MonoBehaviour
{
    private float mzCoord;
    private Vector3 mOffset;
    private Rigidbody rigidbody;
    private Cube cube;
    private Dot dot;

    public static UnityAction<GameObject, Cube, Dot> OnStart;
    public static UnityAction<GameObject, Cube, Dot> OnGame;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        cube = GetComponent<Cube>();
        dot = Plane._dots[cube._Id];
        OnStart?.Invoke(gameObject, cube, dot);
    }
    private void OnMouseDown()
    {
        mzCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mOffset = transform.position - GetMouseWorldPosition();
    }
    private void OnMouseUp()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.Sleep();
        OnGame?.Invoke(gameObject, cube, dot);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mzCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        rigidbody.velocity = (GetMouseWorldPosition() + mOffset - transform.position) * 10;
    }
}
