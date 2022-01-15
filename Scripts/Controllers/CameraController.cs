using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform;

    Vector3 cameraPosition;
    public float cameraMoveSpeed;

    public Vector2 center;
    public Vector2 size;
    float height;
    float width;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        cameraPosition = new Vector3(0, 0, -10);

        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
 //       transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + cameraPosition.y, playerTransform.position.z + cameraPosition.z);
        transform.position = Vector3.Lerp(transform.position, new Vector3(playerTransform.position.x, 0) + cameraPosition, Time.deltaTime * cameraMoveSpeed);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
