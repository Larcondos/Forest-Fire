using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScaler : MonoBehaviour
{

    public StatisticsMonitor statisticsMonitor;

    // All stage borders will move to the size. Trees can be planted 1 less than stage size.
    public int stageSize;

    // So I can grab the borders and move them.
    public GameObject[] borders;

    // The camera object, also used in move the borders.
    private GameObject cam;
    

    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = false;
        Screen.SetResolution(800, 800, false);
        //Application.targetFrameRate = 5;
        moveBorders();
        statisticsMonitor = GameObject.FindGameObjectWithTag("Stats").GetComponent<StatisticsMonitor>();
        statisticsMonitor.screenSize = stageSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) {
            Application.Quit();

        }
    }

    // Moves all the borders and camera to the necessary location for the stage size.
    void moveBorders() {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cam.GetComponent<Camera>().orthographicSize = stageSize;

        borders[0].transform.position = new Vector3(stageSize, 0, 0);
        borders[1].transform.position = new Vector3(-stageSize, 0, 0);
        borders[2].transform.position = new Vector3(0, stageSize, 0);
        borders[3].transform.position = new Vector3(0, -stageSize, 0);
    }

}
