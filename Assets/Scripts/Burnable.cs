using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{

    [Tooltip("This is based on 10000, so if you put 10, it's a 0.1% chance.")]
    public float burnRate;

    public StatisticsMonitor statisticsMonitor;

    // Is this currently burning?
    public bool isBurning;

    public Material[] mats;

    private Renderer thisMat;

    // If it is burning, how many frames are left until it's done.
    public int burningTime;
    private int startingBurningTime;
    [SerializeField] private int PartnerBurnTimer;

    private GameObject[] otherObjects;

    private string objectType;


    // Start is called before the first frame update
    void Start()
    {
        thisMat = GetComponent<Renderer>();
        objectType = this.name;
        startingBurningTime = burningTime / 2;
        statisticsMonitor = GameObject.FindGameObjectWithTag("Stats").GetComponent<StatisticsMonitor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkBurn()) {
            isBurning = true;
        }

        if (isBurning) {
            burnColor(burningTime);
            burningTime--;
            if (burningTime == (PartnerBurnTimer)) {
                lightNeighbors();
            }
            if (burningTime == 0) {
                Destroy(this.gameObject);
            }
        }
    }

    // This tree is burning, ignite it's neighbors.
    // TODO: This is ignite more than just the neighbors. Why?
    public void lightNeighbors() {
        otherObjects = GameObject.FindGameObjectsWithTag(objectType);

        Vector3[] burnZones = new[] {   this.gameObject.transform.position + Vector3.up,
                                        this.gameObject.transform.position + Vector3.down,
                                        this.gameObject.transform.position + Vector3.left,
                                        this.gameObject.transform.position + Vector3.right};

        for (int i = 0; i < otherObjects.Length; i++) {
            if (otherObjects[i].transform.position == burnZones[0]) {
                otherObjects[i].GetComponent<Burnable>().isBurning = true;
            }
            if (otherObjects[i].transform.position == burnZones[1]) {
                otherObjects[i].GetComponent<Burnable>().isBurning = true;
            }
            if (otherObjects[i].transform.position == burnZones[2]) {
                otherObjects[i].GetComponent<Burnable>().isBurning = true;
            }
            if (otherObjects[i].transform.position == burnZones[3]) {
                otherObjects[i].GetComponent<Burnable>().isBurning = true;
            }

        }
    }

    bool checkBurn() {
        float burnSuccess = Random.Range(0, 10000);

        if (burnSuccess < burnRate) {
            statisticsMonitor.firesStarted++;
            return true;
        } else {
            return false;
        }
    }

    void burnColor(int i) {
        switch(i) {
            case 1:
                thisMat.material = mats[i-1];
                break;
            case 2:
                thisMat.material = mats[i-1];
                break;
            case 3:
                thisMat.material = mats[i-1];
                break;
            case 4:
                thisMat.material = mats[i-1];
                break;
            case 5:
                thisMat.material = mats[i-1];
                break;
            case 6:
                thisMat.material = mats[i-1];
                break;
        }

    }
}
