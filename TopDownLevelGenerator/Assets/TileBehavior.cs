using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class TileBehavior : MonoBehaviour
{
    private List<GameObject> colliding = new List<GameObject>();

    public bool isExit = false;

    private bool _genned = false;

    private bool _updating = true;

    private bool _markedForDeletion;
    
    private DateTime id = DateTime.Now;

    void Start()
    {
        if (!LevelDesignParameters.HasExit
            && LevelDesignParameters.TileCount > LevelDesignParameters.MinTilesBeforeExit
            && (Math.Abs(transform.position.x) > LevelDesignParameters.MinDistanceBeforeExit
                || Math.Abs(transform.position.y) > LevelDesignParameters.MinDistanceBeforeExit)
            && Random.value < LevelDesignParameters.ExitChance)
        {
            LevelDesignParameters.HasExit = true;
            isExit = true;
            tag = "Exit";
            name = "EXIT";
            Debug.Log($"Exit Created at: ({transform.position.x},{transform.position.y})");
        }
    }

    // Start is called before the first frame update
    void Update()
    {
        if (_genned)
        {
            if (!isExit && _markedForDeletion && !_updating)
            {
                GameObject.Destroy(gameObject);
                LevelDesignParameters.TileCount--;
            }
            return;
        }

        _updating = true;
        _genned = true;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        tag = "Floor";

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

        sprite.color = isExit ? LevelDesignParameters.ExitColor : LevelDesignParameters.FloorColor;
        _updating = false;
        LevelDesignParameters.TileCount++;

        if (!LevelDesignParameters.LevelReady)
        {
            int branchCount = -1;
            do
            {
                CreateNext();
                branchCount++;
            } while (LevelDesignParameters.BranchCount < LevelDesignParameters.MaxBranches && Random.value < LevelDesignParameters.BranchChance);

            if (branchCount > 0)
            {
                LevelDesignParameters.BranchCount += branchCount;
                //Debug.Log($"A tile branched {branchCount} times");
            }
        }
    }

    public GameObject CreateNext()
    {
        if (LevelDesignParameters.LevelReady)
        {
            return null;
        }
        int x = 0;
        int y = 0;

        while (Math.Abs(x) == Math.Abs(y))
        {
            x = Random.Range(-1, 2);
            y = Random.Range(-1, 2);
        }

        return Instantiate(LevelDesignParameters.FloorTile,
            new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z), transform.rotation);
    }


    private bool checkedColl = false;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Equals("WallCheck", StringComparison.OrdinalIgnoreCase))
        {
            GameObject.Destroy(other.transform.parent.gameObject);
        } else if (other.tag.Equals("Wall", StringComparison.OrdinalIgnoreCase))
        {
            GameObject.Destroy(other.gameObject);
        }
        if (_updating)
        {
            return;
        }
        if (isExit)
        {
            if (other.gameObject.tag.Equals("Player", StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("YOU WIN!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
            }
            if (other.tag.Equals("Floor"))
            {
                other.gameObject.GetComponent<TileBehavior>()._markedForDeletion = true;
            }
        }
        else if (other.tag.Equals("Exit", StringComparison.OrdinalIgnoreCase))
        {
            _markedForDeletion = true;
        }
        else if (other.tag.Equals("Floor") && id > (other.gameObject.GetComponent<TileBehavior>()?.id ?? DateTime.MinValue))
        {
            other.gameObject.GetComponent<TileBehavior>()._markedForDeletion = true;
        }
    }
}
