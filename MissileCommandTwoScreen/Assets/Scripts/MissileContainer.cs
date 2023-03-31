using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileContainer {
    private float index;
    private GameObject missileParent;
    // Start is called before the first frame update
    public MissileContainer(float i, GameObject m) {
        this.index = i;
        this.missileParent = m;
    }

    public GameObject getMissileParent() {
        return missileParent;
    }

    public float getIndex() {
        return index;
    }
}
