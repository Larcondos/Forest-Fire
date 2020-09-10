using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialGen : MonoBehaviour
{

    public GameObject lakePrefab;

    public GameObject treeSpawner;

    private int screenSize;

    public string otherObjectType;

    private GameObject[] otherObjects;

    private int nextPhaseTimer;

    public StatisticsMonitor statisticsMonitor;

    public int minLakes;
    public int maxLakes;

    // Start is called before the first frame update
    void Start()
    {
        //otherObjectType = this.name;
        screenSize = GameObject.FindGameObjectWithTag("Stage").GetComponent<StageScaler>().stageSize - 1;
        lakeSpawn();
        nextPhaseTimer = lakePrefab.GetComponent<LakeMultiply>().selfDestructTimer;
        statisticsMonitor = GameObject.FindGameObjectWithTag("Stats").GetComponent<StatisticsMonitor>();
    }

    // Update is called once per frame
    void Update()
    {
        nextPhaseTimer--;
        
        // After all lakes are done generating, turn on the trees!!
        if (nextPhaseTimer < 0) {
            treeSpawner.SetActive(true);
        }
    }

    // Spawn anywhere from 3 - 10 lakes. They will all grow, and then stop growing after a short bit.
    void lakeSpawn() {
        int i = 0;
        int lakesToSpawn = Random.Range(minLakes, maxLakes);

        while (i < lakesToSpawn) {
            Vector3 spawnLocation = new Vector3(Random.Range(-screenSize, screenSize), Random.Range(-screenSize, screenSize), 0);

            otherObjects = GameObject.FindGameObjectsWithTag(otherObjectType);
            bool duplicate = false;

            for (int i2 = 0; i2 < otherObjects.Length; i2++) {
                if (otherObjects[i2].transform.position == spawnLocation) {
                    duplicate = true;
                }
            }

            if (duplicate) {
                return;
            } else {
                GameObject go = Instantiate(lakePrefab, spawnLocation, Quaternion.identity, this.gameObject.transform);
                go.name = otherObjectType;
            }

            i++;
        }
    }

}
