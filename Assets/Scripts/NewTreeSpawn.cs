using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewTreeSpawn : MonoBehaviour
{

    public StatisticsMonitor statisticsMonitor;

    private int screenSize;

    [Tooltip("This is based on 1000, so if you put 10, it's a 1% chance.")]
    public float multiplyRate;

    public GameObject copy;

    public string objectType;

    private GameObject[] otherObjects;

    // Start is called before the first frame update
    void Start() {
        screenSize = GameObject.FindGameObjectWithTag("Stage").GetComponent<StageScaler>().stageSize - 1;
        spawn();
        statisticsMonitor = GameObject.FindGameObjectWithTag("Stats").GetComponent<StatisticsMonitor>();
    }

    // Update is called once per frame
    void Update() {
        if (checkMultiply()) {
            spawn();
        }

    }

    void spawn() {
        Vector3 spawnLocation = new Vector3(Random.Range(-screenSize, screenSize), Random.Range(-screenSize, screenSize), 0);

        otherObjects = FindGameObjectsWithTags(new string[] { "Lake", "Tree" });
        bool duplicate = false;

        for (int i = 0; i < otherObjects.Length; i++) {
            if (otherObjects[i].transform.position == spawnLocation) {
                duplicate = true;
            }
        }

        if (duplicate) {
            return;
        } else {
            GameObject go = Instantiate(copy, spawnLocation, Quaternion.identity, this.gameObject.transform);
            go.name = objectType;
            statisticsMonitor.forestsStarted++;
            statisticsMonitor.treesGrown++;
        }

    }

    bool checkMultiply() {
        float multSuccess = Random.Range(0, 1000);

        if (multSuccess < multiplyRate) {
            return true;
        } else {
            return false;
        }


    }

    GameObject[] FindGameObjectsWithTags(params string[] tags) {
        var all = new List<GameObject>();

        foreach (string tag in tags) {
            var temp = GameObject.FindGameObjectsWithTag(tag).ToList();
            all = all.Concat(temp).ToList();
        }

        return all.ToArray();
    }
}
