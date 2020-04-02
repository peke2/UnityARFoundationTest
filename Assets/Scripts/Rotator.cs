using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //var rotX = Quaternion.AngleAxis(45, Vector3.right);
        //var rotY = Quaternion.AngleAxis(45, Vector3.up);
        //gameObject.transform.rotation *= rotY * rotX;
    }

    // Update is called once per frame
    void Update()
    {
        //var rot = Quaternion.AngleAxis(2, Vector3.up);
        //var rot = Quaternion.Euler(0,0,1);
        var rot = Quaternion.AngleAxis(2, new Vector3(1, 1, 0));
        gameObject.transform.rotation *= rot;
    }
}
