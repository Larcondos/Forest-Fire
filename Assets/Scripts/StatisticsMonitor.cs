using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatisticsMonitor : MonoBehaviour
{
    public int screenSize;
    private int totalScreenSize;

    // Various stats that will be updated.
    public int forestsStarted;
    public int treesGrown;
    public int firesStarted;
    public float percentForest;
    public float timeRan;
    private int convertedTimeRan;

    public Text forestText, treesGrownText, firesStartedText, percentForestText, timeRanText;

    public GameObject[] treeSize;
    private int amountOfTrees;

    // Start is called before the first frame update
    void Start()
    {
        screenSize = GameObject.FindGameObjectWithTag("Stage").GetComponent<StageScaler>().stageSize - 1;
        totalScreenSize = screenSize * screenSize;
    }

    // Update is called once per frame
    void Update()
    {
        timeRan += Time.deltaTime;
        convertedTimeRan = (int)timeRan;
        treeSize = GameObject.FindGameObjectsWithTag("Tree");

        amountOfTrees = treeSize.Length;

        percentForest = ((amountOfTrees / (float)screenSize) / 100f);
        if (amountOfTrees != 0)
            print(totalScreenSize / amountOfTrees);

        updateText();
    }

    void updateText() {
        forestText.text = "Forests Started: " + forestsStarted.ToString();
        treesGrownText.text = "Trees Grown: " + treesGrown.ToString();
        firesStartedText.text = "Fires Started: " + firesStarted.ToString();
        percentForestText.text = "Percent of Land as Trees: " + percentForest.ToString("P");
        timeRanText.text = "Time Simulation Running: " + convertedTimeRan.ToString() + "s";
    }
}
