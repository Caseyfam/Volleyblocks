using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSetInput : MonoBehaviour {

    public string axisVertical;
    public string axisHorizontal;
    public string counterClockwise;
    public string clockwise;

	void Awake()
    {
        GetComponent<ActiveSet>().SetInputs(axisVertical, axisHorizontal, counterClockwise, clockwise);
    }
}
