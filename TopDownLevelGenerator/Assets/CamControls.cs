using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamControls : MonoBehaviour
{
    public float moveSpeed = 0.01f;
    public float zoomSpeed = 0.01f;
    public float runMultiplier = 5.0f;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float mult = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? runMultiplier : 1.0f;
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * moveSpeed * mult;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed * mult;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * moveSpeed * mult;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * moveSpeed * mult;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            cam.orthographicSize += zoomSpeed * mult;
        }
        if (Input.GetKey(KeyCode.E) && cam.orthographicSize > zoomSpeed * mult)
        {
            cam.orthographicSize -= zoomSpeed * mult;
            if (cam.orthographicSize < 0)
            {
                cam.orthographicSize = zoomSpeed * mult;
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
