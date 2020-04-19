using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Gravity
{
    public Sprite[] planets;
    public Sprite[] clouds;
    public Color[] colours;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = planets[Random.Range(0,planets.Length)];
        GetComponent<SpriteRenderer>().color = colours[Random.Range(0, colours.Length)];
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet") {

            if (Mathf.Approximately(collision.gameObject.GetComponent<Planet>().radius, radius))
            {
                if (collision.gameObject.GetComponent<Planet>().id > id)
                {
                    Holder.objects.Remove(gameObject);
                    Destroy(gameObject);
                }
                else {
                    float oldmass = mass;
                    mass += collision.gameObject.GetComponent<Planet>().mass;
                    transform.localScale += (Vector3)Vector2.one * Mathf.Sqrt(radius * radius + collision.gameObject.GetComponent<Planet>().radius * collision.gameObject.GetComponent<Planet>().radius);
                    radius += Mathf.Sqrt(radius * radius + collision.gameObject.GetComponent<Planet>().radius * collision.gameObject.GetComponent<Planet>().radius);

                }
            }
            else
            {
                if (radius < collision.gameObject.GetComponent<Planet>().radius)
                {
                    Holder.objects.Remove(gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    float oldmass = mass;
                    mass += collision.gameObject.GetComponent<Planet>().mass;

                    transform.localScale += (Vector3)Vector2.one * Mathf.Sqrt(mass / oldmass);
                    radius += Mathf.Sqrt(mass / oldmass);
                }
            }
        }
        if (collision.gameObject.tag == "Fuel" || collision.gameObject.tag == "Air" || collision.gameObject.tag == "Point")
        {
            Holder.objects.Remove(collision.gameObject);
            if (collision.gameObject.tag == "Fuel") Holder.fuel.Remove(collision.gameObject);
            if (collision.gameObject.tag == "Air") Holder.air.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
    private void Update()
    {
        transform.up = rb.velocity.normalized;
    }
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
