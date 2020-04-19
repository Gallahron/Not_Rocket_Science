using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class well : Gravity
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fuel")
        {
            Holder.fuel.Remove(collision.gameObject);
            Holder.objects.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Air")
        {
            Holder.air.Remove(collision.gameObject);
            Holder.objects.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
