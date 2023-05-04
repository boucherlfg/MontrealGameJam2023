using UnityEngine;

public class MakeCameraFollow : MonoBehaviour
{
    public float cameraSpeed = 0.9f;

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
    }
    private void UpdateCamera()
    {
        var camera = Camera.main;
        var camPos = camera.transform.position;
        var z = camPos.z;
        camPos = Vector3.Lerp(camPos, transform.position, cameraSpeed);
        camPos.z = z;
        camera.transform.position = camPos;
    }
}
