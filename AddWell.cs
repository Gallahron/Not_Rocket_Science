using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddWell : MonoBehaviour
{
    public GameObject planet;
    Vector2 startpos;
    float startTime;
    
    void Update()
    {
        /*if (Input.GetButtonDown("Place"))       
        {
            startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startTime = Time.time;
        }
        if (Input.GetButtonUp("Place")) {
            Vector2 endpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 delta = endpos - startpos;
            Instantiate(planet, endpos, transform.rotation).GetComponent<Gravity>().startveloc = delta/ (Time.time - startTime);
        }
        */

        if (Input.GetButtonDown("Place") && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().launched && !Holder.pause)
        {
            bool fail = false;
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (GameObject i in Holder.objects) {
                if (Vector2.Distance(pos, i.transform.position) < 1f) fail = true;
            }
            if (!fail)
            {
                GameObject a = Instantiate(planet, pos, transform.rotation);
                if (SceneManager.GetActiveScene().name == "Tutorial") a.tag = "Tutorial";
            }
        }
    }
    
}
