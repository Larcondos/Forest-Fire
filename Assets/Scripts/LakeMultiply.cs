using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeMultiply : MonoBehaviour {

    [Tooltip("This is based on 10000, so if you put 100, it's a 1% chance.")]
    public float multiplyRate;

    private int screenSize;

    private GameObject copy;

    private string objectType;

    private GameObject[] otherObjects;

    private bool verified = false;

    public int selfDestructTimer;

    private Vector3 thisPosition;

    public StatisticsMonitor statisticsMonitor;

    // Start is called before the first frame update
    void Start() {
        objectType = this.name;
        copy = this.gameObject;
        thisPosition = this.transform.position;
        screenSize = GameObject.FindGameObjectWithTag("Stage").GetComponent<StageScaler>().stageSize - 1;
        statisticsMonitor = GameObject.FindGameObjectWithTag("Stats").GetComponent<StatisticsMonitor>();
    }

    // Update is called once per frame
    void Update() {
        if (checkMultiply()) {
            spawn();
        }

        if (!verified) {
            checkBounds(thisPosition);
        }

    }

    void spawn() {
        Vector3 spawnLocation = thisPosition;

        // Select a random tile next to this one.
        int tileSelect = Random.Range(0, 4);

        switch (tileSelect) {
            case 0:
                spawnLocation = spawnLocation + Vector3.up;
                break;
            case 1:
                spawnLocation = spawnLocation + Vector3.down;
                break;
            case 2:
                spawnLocation = spawnLocation + Vector3.left;
                break;
            case 3:
                spawnLocation = spawnLocation + Vector3.right;
                break;
        }

        if (spawnLocation.x > screenSize || spawnLocation.x < -screenSize || spawnLocation.y > screenSize || spawnLocation.y < -screenSize) {
            return;
        }

        otherObjects = GameObject.FindGameObjectsWithTag(objectType);
        bool duplicate = false;

        for (int i = 0; i < otherObjects.Length; i++) {
            if (otherObjects[i].transform.position == spawnLocation) {
                duplicate = true;
            }
        }

        if (duplicate) {
            return;
        } else {
            GameObject go = Instantiate(copy, spawnLocation, Quaternion.identity, transform.parent);
            go.name = objectType;
            //print("New Spawn!");
        }

        checkNeighbors(otherObjects);

    }

    // Checks all the neighboring tiles. If they are all occupied, destroy this script!!
    void checkNeighbors(GameObject[] g) {

        bool flag1, flag2, flag3, flag4;
        flag1 = flag2 = flag3 = flag4 = false;

        Vector3[] neighborZones = new[] {   thisPosition + Vector3.up,
                                            thisPosition + Vector3.down,
                                            thisPosition + Vector3.left,
                                            thisPosition + Vector3.right};

        for (int i = 0; i < g.Length; i++) {
            if (g[i].transform.position == neighborZones[0]) {
                flag1 = true;
            }

            if (g[i].transform.position == neighborZones[1]) {
                flag2 = true;
            }

            if (g[i].transform.position == neighborZones[2]) {
                flag3 = true;
            }

            if (g[i].transform.position == neighborZones[3]) {
                flag4 = true;
            }

            if (flag1 && flag2 && flag3 && flag4) {
                //print("Destroying, all neighbors full.");
                Destroy(this);
            }

        }


    }

    void checkBounds(Vector3 v) {
        if (thisPosition.x > screenSize || thisPosition.x < -screenSize || thisPosition.y > screenSize || thisPosition.y < -screenSize) {
            Destroy(this.gameObject);
        } else {
            verified = true;
        }
    }

    bool checkMultiply() {
        float multSuccess = Random.Range(0, 10000);

        selfDestructTimer--;
        if (selfDestructTimer < 0) {
            Destroy(this);
        }

        if (multSuccess < multiplyRate) {
            return true;
        } else {
            return false;
        }


    }

}

