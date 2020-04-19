using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTo : MonoBehaviour
{
    public GameObject target;
    public bool fuel;

    // Update is called once per frame
    void Update()
    {
        if (target == null) {
            target = GameObject.Find("LongDistPoint");
        }
        if (fuel)
        {
            foreach (GameObject i in Holder.fuel)
            {
                if (Vector2.Distance(target.transform.position, Camera.main.GetComponent<CameraController>().track.position) > Vector2.Distance(i.transform.position, Camera.main.GetComponent<CameraController>().track.position)) target = i;
            }
        }
        else {
            foreach (GameObject i in Holder.air)
            {
                if (Vector2.Distance(target.transform.position, Camera.main.GetComponent<CameraController>().track.position) > Vector2.Distance(i.transform.position, Camera.main.GetComponent<CameraController>().track.position)) target = i;
            }
        }

        if (fuel)
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.8f)) + Vector3.forward * 3;
        }
        else {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.6f)) + Vector3.forward * 3;
        }
        Vector2 targetpos = Camera.main.WorldToViewportPoint(target.transform.position);
        if (targetpos.x > 0 && targetpos.x < 1 && targetpos.y > 0 && targetpos.y < 1 && !Holder.animating)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else GetComponent<SpriteRenderer>().enabled = true;
        Vector2 delta = target.transform.position - transform.position;
        transform.eulerAngles = new Vector3(0, 0, Vector2.Angle(new Vector2(0, 1), delta));
        if (delta.x > 0) transform.eulerAngles *= -1;
    }
}
