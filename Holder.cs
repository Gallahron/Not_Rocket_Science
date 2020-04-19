using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Holder
{
    public static List<GameObject> objects = new List<GameObject>();
    public static List<GameObject> fuel = new List<GameObject>();
    public static List<GameObject> air = new List<GameObject>();
    public static List<GameObject> wells = new List<GameObject>();
    public static int highscore;
    public static int score;
    const float G = 1;
    public static bool animating = true;
    public static string lastScene;
    public static bool pause;

    public static void Reset() {
        objects = new List<GameObject>();
        fuel = new List<GameObject>();
        air = new List<GameObject>();
        animating = true;
        score = 0;
        pause = false;
    }
}
