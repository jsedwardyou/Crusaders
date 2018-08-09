using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreen : MonoBehaviour {

    GameObject[] playerCameras;
    GameObject[] canvases;
    public GameObject[] players;

    Camera[] cams;

	// Use this for initialization
	void Start () {
        cams = Camera.allCameras;
        canvases = GameObject.FindGameObjectsWithTag("Canvas");
        players = GameObject.FindGameObjectsWithTag("player");
        screenSplit(cams.Length);
    }

    private void Update()
    {
        for (int i = 0; i < canvases.Length; i++) {
            GameObject canvas = canvases[i]; ;
            canvas.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            if (players[i].GetComponent<PlayerController>().GetPlayerDirection())
            {
                canvas.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, -180, 0);
            }
            else {
                canvas.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void screenSplit(int i) {
        switch (i) {
            case 1:
                cams[0].rect = new Rect(0, 0, 1, 1);
                break;
            case 2:
                cams[0].rect = new Rect(0, 0, 0.5f, 1);
                cams[1].rect = new Rect(0.5f, 0, 0.5f, 1);
                for (int ci = 0; ci < canvases.Length; ci++) {
                    canvases[ci].GetComponent<RectTransform>().localScale = new Vector3(0.5f, 1, 1);
                }
                break;
            case 3:
                cams[0].rect = new Rect(0, 0.5f, 1, 0.5f);
                cams[1].rect = new Rect(0, 0, 0.5f, 0.5f);
                cams[2].rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                break;
        }
    }
}
