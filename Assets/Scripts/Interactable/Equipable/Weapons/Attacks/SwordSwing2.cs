using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing2 : SwordSwing
{
    protected override void Swing()
    {
        Vector3 swingPos = new Vector3(-1, -1, 1);
        Vector3 swingPos2 = new Vector3(-.33f, -.33f, 1.33f);
        Vector3 swingPos3 = new Vector3(.33f, .33f, 1.33f);
        Vector3 swingPos4 = new Vector3(1, 1, 1f);
        transform.localPosition = cubeBezier3(swingPos, swingPos2, swingPos3, swingPos4, t);
    }
}
