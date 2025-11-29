using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dragSpeed = 1.0f;
    public float minX = 0f;      // 甘狼 哭率 场 力茄
    public float maxX = 12.8f;       // 甘狼 坷弗率 场 力茄

    private Vector3 dragOrigin;    

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
      
        if (!Input.GetMouseButton(0)) return;
      
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);

        Vector3 move = new Vector3(-pos.x * dragSpeed, 0, 0);

        transform.Translate(move, Space.World);

        Vector3 currentPos = transform.position;
        currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);
        transform.position = currentPos;
    }
}
