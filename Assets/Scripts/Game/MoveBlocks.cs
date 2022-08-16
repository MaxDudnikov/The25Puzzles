using UnityEngine;
public class MoveBlocks : MonoBehaviour
{
    public delegate void d_MoveBlock(GameObject block);
    public static event d_MoveBlock e_MoveBlock;

    private void OnMouseDown()
    {
        if (IgnoreRaycast.IsOnUI)
            return;

        e_MoveBlock.Invoke(gameObject);
    }
}
