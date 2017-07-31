﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

/*
 * AUTHOR: Trenton Pottruff
 */

public class MapSelectionDisplay : MonoBehaviour {
    public MenuManager menuManager;

    [SerializeField]
    private Transform officialMapHolder;
    [SerializeField]
    private Transform customMapHolder;

    public GameObject mapSelectButtonPrefab;

    private void Start() {
        PopulateList();
    }

    private void PopulateList() {
        Map[] maps = new Map[Game.MAPS.Count];
        Game.MAPS.Values.CopyTo(maps, 0);
        string[] mapPaths = new string[] { };

        if (Directory.Exists(Application.persistentDataPath + "/maps"))
            mapPaths = Directory.GetFiles(Application.persistentDataPath + "/maps");

        string[] names = new string[mapPaths.Length];

        /*for (int i = 0; i < maps.Length; i++) {
            names[i] = maps[i].name;
        }*/
        for (int i = 0; i < mapPaths.Length; i++) {
            names[i] = mapPaths[i].Replace(Application.persistentDataPath, "").Replace(".map", "").Replace("/maps\\", "");
        }
    
        //Populate the official maps
        Utilities.ClearChildren(officialMapHolder);
        for (int i = 0; i < maps.Length; i++) {
            GameObject go = Instantiate(mapSelectButtonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            go.name = mapSelectButtonPrefab.name;
            go.transform.SetParent(officialMapHolder);
            MapSelectButton msb = go.GetComponent<MapSelectButton>();
            
            msb.SetInfo(maps[i].name, "This is a map!");
        }

        //Populate the custom maps
        Utilities.ClearChildren(customMapHolder);
        for (int i = 0; i < names.Length; i++) {
            GameObject go = Instantiate(mapSelectButtonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            go.name = mapSelectButtonPrefab.name;
            go.transform.SetParent(customMapHolder);
            MapSelectButton msb = go.GetComponent<MapSelectButton>();

            msb.SetInfo(names[i], "This is a map!");
        }
    }

    public void PlayGame() {
        SceneManager.LoadScene("Game");
    }

    public void BackToMain() {
        menuManager.ChangeMenu(0);
    }
}