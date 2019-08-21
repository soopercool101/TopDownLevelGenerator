using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class WallCheck : MonoBehaviour
{
    private DateTime id = DateTime.Now;

    // Update is called once per frame
    void Update()
    {
        if (LevelDesignParameters.LevelReady)
        {
            Object.Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("WallCheck") && id > (other.gameObject.GetComponent<WallCheck>()?.id ?? DateTime.MinValue))
        {
            GameObject.Destroy(other.transform.parent.gameObject);
        }
    }
}
