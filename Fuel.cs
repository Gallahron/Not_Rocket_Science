using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Gravity
{
    public bool fuel;

    // Start is called before the first frame update
    void Awake()
    {
        Holder.objects.Add(gameObject);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = startveloc;
        id = Holder.objects.Count;
        if (fuel) Holder.fuel.Add(gameObject);
        else Holder.air.Add(gameObject);
    }
}
