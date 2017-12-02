﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 */

public class Inventory : NetworkBehaviour {
    public List<Item> inventory = new List<Item>();
    [SyncVar]
    public int max = 0; //A maximum item limit, if any.
    //TODO Implement max inventory limits

    private GameObject toSpawn;

    public void Consume(int index, GameObject toDelete) {
        Debug.Log("Consuming...");

        toSpawn = inventory[index].Consume(this.gameObject.GetComponent<Player>());

        if (toSpawn != null) {
            Debug.Log("Spawning...");
            CmdSpawn(toSpawn.name);
        } else {
            Debug.Log("Nope");
        }

        if (inventory[index].GetAmount() <= 0) {
            inventory.RemoveAt(index);
            Destroy(toDelete);
        }
    }

    [Command]
    private void CmdSpawn(string name) {
        Debug.Log("Doit.");
        GameObject go = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/" + name), this.transform.position, Quaternion.identity);
        NetworkServer.Spawn(go);
    }
}