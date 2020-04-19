using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quickscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = GetComponent<Rigidbody2D>().velocity.normalized;
    }
}
