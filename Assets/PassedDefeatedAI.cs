using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassedDefeatedAI : MonoBehaviour
{
    Dictionary<string, bool> defeated = new Dictionary<string, bool>();

    public void StoreDefeated (string name)
    {
        defeated.Add(name, true);
    }

    public bool GetDefeatedVal(string name)
    {
        if (defeated.ContainsKey(name))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
	
}
