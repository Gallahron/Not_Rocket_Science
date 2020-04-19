using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : Gravity
{
    public float minLaunchVeloc;
    public float maxLaunchVeloc;
    public float maxBoostVeloc;
    public float launchVelocMod;
    public float engineIncrease;
    public GameObject balls;


    public float fuelSpend;
    public float airSpend;

    public float boostFuelSpendMod;

    public GameObject image;
    public GameObject airimage;

    public bool launched = false;
    Vector2 startDrag;

    bool outOfBounds = false;
    Collider2D boundscollider;

    float time;
    public float increaseTime;

    public TextMeshProUGUI score;

    private void Start()
    {
        Holder.lastScene = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (Holder.objects.Find(a => a == gameObject) == null) Holder.objects.Add(gameObject);
        if (Input.GetButton("Engine") && launched && image.transform.localScale.x > 0)
        {

            rb.velocity += rb.velocity.normalized * engineIncrease * Time.deltaTime;
            GetComponent<TrailRenderer>().emitting = true;
            image.transform.localScale -= Vector3.right * fuelSpend * boostFuelSpendMod * Time.deltaTime;
            
            

        }

        else GetComponent<TrailRenderer>().emitting = false;
        if (launched) airimage.transform.localScale -= Vector3.right * airSpend * Time.deltaTime;
        if (launched && airimage.transform.localScale.x < 0) {
            SceneManager.LoadScene("YouAreDead");
        }
        if (!Holder.pause && !Mathf.Approximately(rb.velocity.magnitude, 0))transform.up = rb.velocity.normalized;
        if (outOfBounds) {
            if (transform.position.x < boundscollider.transform.position.x - boundscollider.bounds.extents.x) rb.velocity += Vector2.right * boundscollider.GetComponent<ReverseSpeed>().reversespeed * Time.deltaTime;
            else if (transform.position.x > boundscollider.transform.position.x + boundscollider.bounds.extents.x) rb.velocity -= Vector2.right * boundscollider.GetComponent<ReverseSpeed>().reversespeed * Time.deltaTime;
            if (transform.position.y < boundscollider.transform.position.y - boundscollider.bounds.extents.y) rb.velocity += Vector2.up * boundscollider.GetComponent<ReverseSpeed>().reversespeed * Time.deltaTime;
            else if (transform.position.y > boundscollider.transform.position.y + boundscollider.bounds.extents.y) rb.velocity -= Vector2.up * boundscollider.GetComponent<ReverseSpeed>().reversespeed * Time.deltaTime;
        }
        if (Time.time > time + increaseTime && launched)
        {
            score.text = (int.Parse(score.text) + 1).ToString();
            Holder.score = (int.Parse(score.text));
            while (score.text.Length < 6) {
                score.text = "0" + score.text;
            }
            time = Time.time;
        }
    }

    private void OnMouseDown()
    {
        if (!Holder.animating && Mathf.Approximately(rb.velocity.magnitude, 0))
        {
            startDrag = transform.position;
            balls.SetActive(true);
        }
    }
    private void OnMouseDrag()
    {
        if (!Holder.animating && Mathf.Approximately(rb.velocity.magnitude, 0) && !Mathf.Approximately(startDrag.magnitude, 0))
        {
            for (int i = 0; i < balls.transform.childCount; i++)
            {
                balls.transform.GetChild(i).transform.position = Vector3.Scale(startDrag + ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - startDrag) / balls.transform.childCount * i, new Vector3(1,1,0)) + transform.forward;
            }
            transform.up = -Vector3.Scale((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - startDrag,new Vector3(1,1,0));
        }
    }

    private void OnMouseUp()
    {
        if (!Holder.animating && Mathf.Approximately(rb.velocity.magnitude,0) && !Mathf.Approximately(startDrag.magnitude, 0))
        {
            balls.SetActive(false);
            Vector2 launchveloc = -launchVelocMod * ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - startDrag);
            if (!launched && launchveloc.magnitude > minLaunchVeloc)
            {
                launched = true;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;


                if (launchveloc.magnitude > maxLaunchVeloc) launchveloc = launchveloc.normalized * maxLaunchVeloc;

                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                gameObject.layer = 0;
                rb.velocity = launchveloc;

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fuel")
        {
            Destroy(other.gameObject, 1);
            other.GetComponent<ParticleSystem>().enableEmission = false;
            other.GetComponent<MeshRenderer>().enabled = false;
            other.GetComponent<Gravity>().enabled = false;
            other.transform.GetChild(0).gameObject.SetActive(false);
            Holder.objects.Remove(other.gameObject);
            Holder.fuel.Remove(other.gameObject);
            

            image.transform.localScale = Vector2.Scale(image.transform.localScale, new Vector2(1 / image.transform.localScale.x, 1));

        }
        else if (other.tag == "Air")
        {
            Destroy(other.gameObject, 1);
            other.GetComponent<ParticleSystem>().enableEmission = false;
            other.GetComponent<MeshRenderer>().enabled = false;
            other.GetComponent<Gravity>().enabled = false;
            other.transform.GetChild(0).gameObject.SetActive(false);
            Holder.objects.Remove(other.gameObject);
            Holder.air.Remove(other.gameObject);
            airimage.transform.localScale = Vector2.Scale(image.transform.localScale, new Vector2(1 / image.transform.localScale.x, 1));
        }


        else if (other.tag == "Boundary")
        {
            outOfBounds = false;
        }
        else {
            Holder.objects = new List<GameObject>();
            Holder.fuel = new List<GameObject>();
            Holder.animating = true;
            SceneManager.LoadScene("YouAreDead");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Boundary") {
            outOfBounds = true;
            boundscollider = collision;
           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tutorial") {
            Holder.Reset();
            SceneManager.LoadScene("Tutorial");
        }
        else if (collision.gameObject.tag != "Goal")
        {
            Holder.objects = new List<GameObject>();
            Holder.fuel = new List<GameObject>();
            Holder.animating = true;
            SceneManager.LoadScene("YouAreDead");
        }

    }
}
