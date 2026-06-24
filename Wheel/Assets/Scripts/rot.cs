using Unity.VisualScripting;
using UnityEngine;

public class rot : MonoBehaviour
{
    [Header("Controls")]
    public float speed = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0.0f, speed, 0.0f));
    }
}
