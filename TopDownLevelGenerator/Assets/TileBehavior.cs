using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class TileBehavior : MonoBehaviour
{
    private bool _updating = true;
    private List<GameObject> colliding = new List<GameObject>();

    public bool isExit = false;

    // Start is called before the first frame update
    void Start()
    {
        _updating = true;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = LevelDesignParameters.FloorColor;
        tag = "Floor";
        if (LevelDesignParameters.TileCount > LevelDesignParameters.MinDistanceBeforeExit && !LevelDesignParameters.HasExit
            && Random.value < LevelDesignParameters.ExitChance)
        {
            LevelDesignParameters.HasExit = true;
            isExit = true;
            tag = "Exit";
            sprite.color = LevelDesignParameters.ExitColor;
        }

        if (!LevelDesignParameters.LevelReady)
        {
            int branchCount = 0;
            do
            {
                CreateNext();
                branchCount++;
            } while (Random.value < LevelDesignParameters.BranchChance);

            if (branchCount > 1)
            {
                Debug.Log($"A tile branched {branchCount} times");
            }
        }

        // Instantiate Wall Tiles where necessary
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (!(x == 0 && y == 0))
                {
                    Instantiate(LevelDesignParameters.WallTile,
                        new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z),
                        transform.rotation);
                }
            }
        }

        foreach (GameObject o in colliding)
        {
            if (o.tag.Equals("Wall"))
            {
                Destroy(o);
            }
            else if (!o.tag.Equals("Player"))
            {
                Destroy(this.gameObject);
                return;
            }
        }

        LevelDesignParameters.TileCount++;
        
        _updating = false;
    }

    public GameObject CreateNext()
    {
        int x = 0;
        int y = 0;

        while (x == 0 && y == 0)
        {
            x = Random.Range(-1, 2);
            y = Random.Range(-1, 2);
        }

        return Instantiate(LevelDesignParameters.FloorTile,
            new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z), transform.rotation);
    }

    void OnTriggerStay2D(Collider2D Other)
    {
        if (isExit)
        {
            if (Other.gameObject.tag.Equals("Player"))
            {
                Debug.Log("YOU WIN!");
            }
        }
        if (!_updating)
        {
            return;
        }
        colliding.Add(Other.gameObject);
    }
}
