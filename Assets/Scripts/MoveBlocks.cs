using UnityEngine;
public class MoveBlocks : MonoBehaviour
{
    private void OnMouseDown()
    {
        RaycastHit hit;
        Vector3 _local = transform.position;

        GetComponent<BoxCollider>().enabled = false;
        if (!Physics.Linecast(_local, _local + transform.forward * 2, out hit))
            transform.position += new Vector3(0, 0, 3);
        else if (!Physics.Linecast(_local, _local - transform.forward * 2, out hit))
            transform.position += new Vector3(0, 0, -3);
        else if (!Physics.Linecast(_local, _local + transform.right * 2, out hit))
            transform.position += new Vector3(3, 0, 0);
        else if (!Physics.Linecast(_local, _local - transform.right * 2, out hit))
            transform.position += new Vector3(-3, 0, 0);
        GetComponent<BoxCollider>().enabled = true;
    }
}
