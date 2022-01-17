using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }

    public LastSelectedPresetHolder lastSelectedPresetHolder;
    public List<Transform> platformInTheLevel;

    public int totalNumberOfCherries = 25;
    public int currentCherryCount = 1;
    public int previousCherryCount, lastSelectedPlatform;
    public GameObject cherry;

    public TextMeshProUGUI cherryCollectedText;

    bool gameEnded = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        GetPlatformReferences();
        previousCherryCount = -1;
        cherryCollectedText.text = currentCherryCount + " / " + totalNumberOfCherries;
    }

    private void Update()
    {
        if (currentCherryCount > previousCherryCount && currentCherryCount < totalNumberOfCherries)
        {
            StartCoroutine(SpawnCherry());
            previousCherryCount++;
        }
        else if (currentCherryCount >= totalNumberOfCherries && !gameEnded)
        {
            gameEnded = true;
            StartCoroutine(StartGameOverProcedure());
        }
    }

    private IEnumerator StartGameOverProcedure()
    {
        Time.timeScale = 0.01f;
        FindObjectOfType<PlayerController>().isDead = true;
        yield return new WaitForSecondsRealtime(1.5f);
        FindObjectOfType<GameUIManager>().GameOver(false);
    }

    private void GetPlatformReferences()
    {
        platformInTheLevel = new List<Transform>();
        int i = -1;
        foreach (Transform item in lastSelectedPresetHolder.selectedPlatform.transform.GetComponentsInChildren<Transform>())
        {
            i++;
            if (i == 0)
                continue;
            platformInTheLevel.Add(item);
        }
    }

    public IEnumerator SpawnCherry()
    {
        Transform platformSelect = platformInTheLevel[SelectPlatformWhereCherryWillSpawned()];
        Vector3 platformPosition = platformSelect.position;
        platformPosition.y = platformPosition.y + .5f;
        yield return new WaitForSeconds(0.5f);
        Instantiate(cherry, platformPosition, Quaternion.identity);
        
    }

    private int SelectPlatformWhereCherryWillSpawned()
    {
        if (platformInTheLevel.Count <= 1)
            return 0;

        int x = lastSelectedPlatform;
        while (x == lastSelectedPlatform)
        {
            x = Random.Range(0, platformInTheLevel.Count);
        }

        lastSelectedPlatform = x;
        return x;
    }

}
