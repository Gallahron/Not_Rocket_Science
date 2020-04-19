using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody2D track;
    [Range(0.0f, 1f)]
    public float trackspeed;
    public float animatespeed;

    Vector2 lastFrame;

    Rigidbody2D rb;
    public Transform start;
    // Start is called before the first frame update
    void Start()
    {
        Holder.animating = true;
        rb = GetComponent<Rigidbody2D>();
        lastFrame = track.transform.position;
        transform.position = Vector3.Scale(start.transform.position, new Vector3(1,1,0)) + Vector3.back * 10;
    }

    private void Update()
    {
        if (Holder.animating)
        {
            if (Vector2.Distance(transform.position, track.transform.position) > 6)
            {
                rb.velocity = (track.transform.position - start.position).normalized * animatespeed;
            }
            else if (Vector2.Distance(transform.position, track.transform.position) < 6 && Vector2.Distance(transform.position, track.transform.position) > 0.5)
            {
                //rb.velocity -= (Vector2)(track.transform.position - start.position).normalized * Time.deltaTime * animatespeed/100;
                if (GetComponent<Camera>().orthographicSize > 3.8) Camera.main.orthographicSize -= 1.5f * Time.deltaTime;
            }
            if (GetComponent<Camera>().orthographicSize > 3.8) Camera.main.orthographicSize -= 4f * Time.deltaTime;
            else
            {
                rb.velocity = Vector2.zero;
                Holder.animating = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (!Holder.animating)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, ((Vector2)track.transform.position - lastFrame) / Time.fixedDeltaTime, trackspeed);
            if (track.velocity.magnitude - rb.velocity.magnitude < 0.15f) rb.velocity = track.velocity;
            lastFrame = track.transform.position;
        }
    }
}