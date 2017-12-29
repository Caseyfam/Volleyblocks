using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassedOverworld : MonoBehaviour
{
    public static PassedOverworld instance;

    public List<string> storedNames = new List<string>();
    public List<Vector3> storedPositions = new List<Vector3>();
    public List<Quaternion> storedRotations = new List<Quaternion>();

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void StoreObject(GameObject thisObj)
    {
        storedNames.Add(thisObj.name);
        storedPositions.Add(thisObj.transform.position);
        storedRotations.Add(thisObj.transform.rotation);
    }

    public void Clear()
    {
        storedNames.Clear();
        storedPositions.Clear();
        storedRotations.Clear();
    }

    public void ReloadTransforms()
    {
       Debug.Log("Reload called");

        int counter = 0;

        foreach (string name in storedNames)
        {
            GameObject.Find(name).transform.position = storedPositions[counter];
            GameObject.Find(name).transform.rotation = storedRotations[counter];


            counter++;
        }
       for (int i = 0; i < storedNames.Count; i++)
        {
        }
        storedNames.Clear();
        storedPositions.Clear();
        storedRotations.Clear();
    }

}
