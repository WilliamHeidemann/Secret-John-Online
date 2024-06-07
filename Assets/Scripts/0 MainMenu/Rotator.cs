using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float speed;

    private void Update()
    {
        speed = 50f + (Mathf.Sin(Time.time * 4f) + 1f) * 100;
        transform.Rotate(Vector3.back, Time.deltaTime * speed);
    }
}
