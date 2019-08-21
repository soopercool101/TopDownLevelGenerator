using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTileBehavior : MonoBehaviour
{
    private bool _genned = false;

    // Start is called before the first frame update
    void Update()
    {
        if (_genned)
        {
            return;
        }
        _genned = true;
        tag = "Wall";
        gameObject.GetComponent<SpriteRenderer>().color = LevelDesignParameters.WallColor;
    }
}
