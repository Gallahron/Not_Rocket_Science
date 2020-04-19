using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    BoxCollider2D border;
    public float noStars;
    public GameObject star;
    public GameObject fuel;
    public GameObject air;
    public Vector2 starMaxMinSize;
    public float spawnrad;
    public int fuelQuant;
    public float fuelSpawnRad;

    public int airQuant;
    public float airSpawnRad;

    // Start is called before the first frame update
    void Start()
    {
        Holder.Reset();
        border = GameObject.Find("Boundary").GetComponent<BoxCollider2D>();
    
        for (int i = 0; i < noStars; i++) {
            Vector2 instantPos = new Vector2(Random.Range(border.bounds.min.x, border.bounds.max.x), Random.Range(border.bounds.min.y , border.bounds.max.y));
            bool check = true;
            int count = 0;
            while (check && count < 50) {
                foreach (GameObject j in Holder.objects) {
                    if (Vector2.Distance(instantPos, j.transform.position) < spawnrad)
                    {
                        check = true;
                        instantPos = new Vector2(Random.Range(border.bounds.min.x, border.bounds.max.x), Random.Range(border.bounds.min.y, border.bounds.max.y));
                        break;
                    }
                    else check = false;

                }
                count++;
            }
            GameObject planet = Instantiate(star, (Vector3)instantPos, transform.rotation);
            planet.transform.localScale = Vector3.one * Random.Range(starMaxMinSize.x, starMaxMinSize.y);
            planet.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f));
            planet.GetComponent<Planet>().startveloc = planet.GetComponent<Rigidbody2D>().velocity;
        }
    }
    private void Update()
    {
        while (Holder.fuel.Count < fuelQuant) {
            Vector2 instantPos = new Vector2(Random.Range(border.bounds.min.x, border.bounds.max.x), Random.Range(border.bounds.min.y, border.bounds.max.y));
            bool check = true;
            int count = 0;
            while (check && count < 50)
            {
                foreach (GameObject j in Holder.objects)
                {
                    if (Vector2.Distance(instantPos, j.transform.position) < fuelSpawnRad)
                    {
                        check = true;
                        instantPos = new Vector2(Random.Range(border.bounds.min.x, border.bounds.max.x), Random.Range(border.bounds.min.y, border.bounds.max.y));
                        break;
                    }
                    else check = false;

                }
                count++;
            }
            GameObject planet = Instantiate(fuel, (Vector3)instantPos, transform.rotation);
            planet.transform.localScale = Vector3.one * Random.Range(starMaxMinSize.x, starMaxMinSize.y);
            planet.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f));
            planet.GetComponent<Fuel>().startveloc = planet.GetComponent<Rigidbody2D>().velocity;
            
        }
        while (Holder.air.Count < airQuant)
        {
            Vector2 instantPos = new Vector2(Random.Range(border.bounds.min.x, border.bounds.max.x), Random.Range(border.bounds.min.y, border.bounds.max.y));
            bool check = true;
            int count = 0;
            while (check && count < 50)
            {
                foreach (GameObject j in Holder.objects)
                {
                    if (Vector2.Distance(instantPos, j.transform.position) < airSpawnRad)
                    {
                        check = true;
                        instantPos = new Vector2(Random.Range(border.bounds.min.x, border.bounds.max.x), Random.Range(border.bounds.min.y, border.bounds.max.y));
                        break;
                    }
                    else check = false;

                }
                count++;
            }
            GameObject planet = Instantiate(air, (Vector3)instantPos, transform.rotation);
            planet.transform.localScale = Vector3.one * Random.Range(starMaxMinSize.x, starMaxMinSize.y);
            planet.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f));
            planet.GetComponent<Fuel>().startveloc = planet.GetComponent<Rigidbody2D>().velocity;

        }
    }
}