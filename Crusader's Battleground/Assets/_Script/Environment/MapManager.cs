using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public GameObject grid;

    private int _numOfArea;
    private int _currentIndexOfArea;
    private Transform currentStage;

    [SerializeField]
    private float periodInterval = 5;
    float timer;

    public GameObject gas;
    Transform gasPos;

    private bool startPhase = true;

	// Use this for initialization
	void Start () {
        grid = GameObject.Find("Grid");
        _numOfArea = grid.transform.childCount;
        

        timer = Time.fixedTime + periodInterval;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer < Time.fixedTime)
        {
            if (startPhase && _numOfArea > 1)
            {
                StartCoroutine(PhaseStart());
            }
        }
	}

    IEnumerator PhaseStart() {
        startPhase = false;
        _currentIndexOfArea = Random.Range(0, _numOfArea);
        currentStage = grid.transform.GetChild(_currentIndexOfArea);
        gasPos = currentStage.GetChild(2);
        GameObject gasSpawn = Instantiate(gas, gasPos.position, Quaternion.identity);
        gasSpawn.transform.SetParent(gasPos);
        gasSpawn.transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(periodInterval);
        timer = Time.fixedTime; startPhase = true;
        yield return null;
    }
}
