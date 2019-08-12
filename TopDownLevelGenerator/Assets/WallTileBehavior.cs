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

    void OnCollisionStay2D(Collider2D other)
    {
        if (!_updating)
        {
            if (other.gameObject.tag.Equals("Wall"))
            {
                Object.Destroy(gameObject);
            }
        }
        else if (!other.gameObject.tag.Equals("Wall"))
        {
            Object.Destroy(gameObject);
        }
    }
}
