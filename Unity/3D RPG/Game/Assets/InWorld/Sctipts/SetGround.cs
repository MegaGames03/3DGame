using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGround : MonoBehaviour
{
    public Vector3 OffSet;
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        float distance = 100f;
        Vector3 targetLocation = transform.position;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, distance))
            targetLocation = hit.point;

        transform.position = targetLocation + OffSet;
    }

}
