using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTileBehavior : MonoBehaviour
{
    private bool _updating = true;

    // Start is called before the first frame update
    void Start()
    {
        _updating = true;
        tag = "Wall";
        gameObject.GetComponent<SpriteRenderer>().color = LevelDesignParameters.WallColor;
        _updating = false;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.tag.Equals("Wall"))
        {
            Object.Destroy(gameObject);
        }
        else if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
        {
            Object.Destroy(gameObject);
        }
    }
}
