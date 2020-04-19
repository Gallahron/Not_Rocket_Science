using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravStrength;
    public Vector2 startveloc;
    public float mass;
    public float radius = 1;
    public Rigidbody2D rb;
    Collider2D boundsCollider;
    bool outofbounds;
    public int id;


    // Start is called before the first frame update
    void Awake()
    {
        Holder.objects.Add(gameObject);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = startveloc;
        id = Holder.objects.Count;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mass = gravStrength * radius * radius;
        rb.mass = mass;
        foreach (GameObject i in Holder.objects)
        {
            if (i != gameObject && Vector2.Distance(transform.position, i.transform.position) < (i.transform.GetChild(0).localScale.x * i.transform.localScale.x) / 2)
            {
                float dist = Vector2.Distance(i.transform.position, transform.position);
                float forcemag = i.GetComponent<Gravity>().mass * mass / Mathf.Pow(dist, 2);
                Vector2 delta = new Vector2(i.transform.position.x - transform.position.x, i.transform.position.y - transform.position.y).normalized;

                Vector2 acc = delta * forcemag * Time.fixedDeltaTime / mass;

                rb.velocity += acc;
            }
        }
        if (outofbounds)
        {
            if (transform.position.x < boundsCollider.transform.position.x - boundsCollider.bounds.extents.x) rb.velocity += Vector2.right * boundsCollider.GetComponent<ReverseSpeed>().reversespeed * Time.deltaTime;
            else if (transform.position.x > boundsCollider.transform.position.x + boundsCollider.bounds.extents.x) rb.velocity -= Vector2.right * boundsCollider.GetComponent<ReverseSpeed>().reversespeed * Time.deltaTime;
            if (transform.position.y < boundsCollider.transform.position.y - boundsCollider.bounds.extents.y) rb.velocity += Vector2.up * boundsCollider.GetComponent<ReverseSpeed>().reversespeed * Time.deltaTime;
            else if (transform.position.y > boundsCollider.transform.position.y + boundsCollider.bounds.extents.y) rb.velocity -= Vector2.up * boundsCollider.GetComponent<ReverseSpeed>().reversespeed * Time.deltaTime;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Boundary")
        {
            outofbounds = true;
            boundsCollider = collision;

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary")
        {
            outofbounds = false;
        }
    }
}
