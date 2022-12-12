using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer), typeof(Cube))]
public class ControlCubes : MonoBehaviour
{
    enum Enable
    {
        None,
        Right,
        Left,
        Up,
        Down
    }
    enum Gestures
    {
        None,
        Stationary,
        SwipeRight,
        SwipeLeft,
        SwipeUp,
        SwipeDown
    }

    private Gestures _currentGesture = Gestures.None;
    private Enable _currentEnable = Enable.None;

    public static UnityAction<GameObject, Cube, Dot> OnStart;
    public static UnityAction<GameObject, Cube, Dot> OnGame;

    private float _validInputThreshold = 100f;

    private Vector3 _originalPosition;

    private bool _playerIsMoving = false;

    private MeshRenderer _meshRenderer;

    private static GameObject _currentGO;

    private Cube cube;
    private Dot dot;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        cube = GetComponent<Cube>();
        dot = Plane._dots[cube._Id];
        OnStart?.Invoke(gameObject, cube, dot);
    }

    private void OnMouseDown()
    {
        _currentGO = gameObject;

        RaycastHit hit;
        if (!Physics.Linecast(transform.position, transform.position + Vector3.right * _meshRenderer.bounds.size.x, out hit))
            _currentEnable = Enable.Right;
        else if (!Physics.Linecast(transform.position, transform.position + Vector3.left * _meshRenderer.bounds.size.x, out hit))
            _currentEnable = Enable.Left;
        else if (!Physics.Linecast(transform.position, transform.position + Vector3.up * _meshRenderer.bounds.size.y, out hit))
            _currentEnable = Enable.Up;
        else if (!Physics.Linecast(transform.position, transform.position + Vector3.down * _meshRenderer.bounds.size.y, out hit))
            _currentEnable = Enable.Down;
        else
            _currentEnable = Enable.None;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && _currentGO == gameObject)
        {
            HandleTouch(Input.GetTouch(0));
            HandleCharacterMovement(_currentGesture);
        }
    }

    private void HandleTouch(Touch touch)
    {
        if (touch.Equals(null)) return;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                _originalPosition = touch.position;
                break;

            case TouchPhase.Moved:
                Vector3 delta = (Vector3)touch.position - _originalPosition;
                if (delta.magnitude > _validInputThreshold)
                    GetSwipe(touch, delta);
                else
                    _currentGesture = Gestures.Stationary;
                break;

            case TouchPhase.Stationary:
                break;

            case TouchPhase.Ended:
                _playerIsMoving = true;
                _currentGO = null;
                break;

            case TouchPhase.Canceled:
                _currentGO = null;
                _currentGesture = Gestures.None;
                break;
        }
    }

    private void GetSwipe(Touch touch, Vector3 delta)
    {
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
                _currentGesture = Gestures.SwipeRight;
            else
                _currentGesture = Gestures.SwipeLeft;
        }
        else
        {
            if (delta.y > 0)
                _currentGesture = Gestures.SwipeUp;
            else
                _currentGesture = Gestures.SwipeDown;
        }
    }

    private void HandleCharacterMovement(Gestures currentGesture)
    {
        if (!_playerIsMoving) return;

        _playerIsMoving = false;

        switch (currentGesture)
        {

            default:
            case Gestures.None:
            case Gestures.Stationary:
                return;

            case Gestures.SwipeRight:
                if (_currentEnable == Enable.Right)
                    StartCoroutine(Move(Vector3.right * _meshRenderer.bounds.size.x));
                return;
            case Gestures.SwipeLeft:
                if (_currentEnable == Enable.Left)
                    StartCoroutine(Move(Vector3.left * _meshRenderer.bounds.size.x));
                return;
            case Gestures.SwipeUp:
                if (_currentEnable == Enable.Up)
                    StartCoroutine(Move(Vector3.up * _meshRenderer.bounds.size.y));
                return;
            case Gestures.SwipeDown:
                if (_currentEnable == Enable.Down)
                    StartCoroutine(Move(Vector3.down * _meshRenderer.bounds.size.y));
                return;
        }
    }

    private IEnumerator Move(Vector3 offset)
    {
        Vector3 finalPos = transform.position + offset;
        float speed = 10;
        
        while (Vector3.Distance(transform.position, finalPos) > 0.001f)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, finalPos, step);

            yield return null;
        }

        OnGame?.Invoke(gameObject, cube, dot);
    }
}
