using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialCameraController : MonoBehaviour
{
    public Rigidbody2D player;

    public float[] pauseHeights;
    int index = 0;
    public Vector2 storVeloc;
    bool zoomout;
    public float pauseTime;
    float time = 0;

    private void Awake()
    {
        Holder.Reset();
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        print(Holder.pause);
        if (Holder.pause)
        {
            
            if (zoomout && Camera.main.orthographicSize < 6)
            {
                Camera.main.orthographicSize += Time.deltaTime * 3;
            }
            else if (!zoomout && Camera.main.orthographicSize > 3.7)
            {
                Camera.main.orthographicSize -= Time.deltaTime * 3;
            }
            else if (zoomout && time == 0)
            {
                time = Time.time;
            }
            else if (!zoomout)
            {
                Holder.pause = false;
                player.constraints = RigidbodyConstraints2D.FreezeRotation;
                player.velocity = storVeloc;
                time = 0;
                
            }
            if (Camera.main.orthographicSize >= 6 && time + pauseTime < Time.time && time != 0)
            {
                if (index == pauseHeights.Length - 1) SceneManager.LoadScene("Endless");
                zoomout = false;
            }

        }
        if (player.transform.position.y > pauseHeights[index]) {
            Holder.pause = true;
            storVeloc = player.velocity;
            player.constraints = RigidbodyConstraints2D.FreezeAll;
            zoomout = true;
            index++;
        }
    }
}
