using Unity.VisualScripting;
using UnityEngine;

public class rot : MonoBehaviour
{
    [Header("Controls")]
    public float speed = 2.0f;
    public Vector3 axis;
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
        transform.Rotate(axis * speed);
    }
}
