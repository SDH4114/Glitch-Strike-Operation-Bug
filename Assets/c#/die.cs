using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class die : MonoBehaviour
{
    public float deth;

    void Update()
    {
        Destroy(gameObject,deth);
    }
}
