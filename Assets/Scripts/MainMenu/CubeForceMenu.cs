using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeForceMenu : MonoBehaviour
{
    [SerializeField]
    private float _upforce = 1f;
    
    [SerializeField]
    private float _sideforce = .1f;

    private void Start()
    {
        float xForce = Random.Range(-_sideforce, _sideforce);
        float yForce = Random.Range(_upforce / 2, _sideforce);
        float zForce = Random.Range(-_sideforce, _sideforce);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        GetComponent<Rigidbody>().velocity = force;
    }
}
