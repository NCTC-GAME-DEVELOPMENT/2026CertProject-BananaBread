using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Info
{
    public void ResetLevel()
    {
        GameObject[] crates = GameObject.FindGameObjectsWithTag("crate");

        foreach (GameObject c in crates)
        {
            Crate crate = c.GetComponent<Crate>();
            crate.ResetPosition();
        }
    }
}