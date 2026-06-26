using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Tornado : MonoBehaviour
{
    enum element_type { Freezer, Oven};

    SphereCollider collider;
    [SerializeField] GameObject TriggerOfDoom;
    [SerializeField] element_type element;
    [SerializeField] GameObject tornado;

    private bool spawned = false;

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Car")
        {
            Vector3 acc_vector = (transform.position - other.transform.position).normalized;
            print(acc_vector * 500.0f);
            Rigidbody body = other.GetComponent<Rigidbody>();
            body.AddForce(acc_vector * 15000.0f);
        }else if(other.tag == "phyObj")
        {
            Vector3 acc_vector = (transform.position - other.transform.position).normalized;
            print(acc_vector * 500.0f);
            Rigidbody body = other.GetComponent<Rigidbody>();
            body.AddForce(acc_vector * 200.0f);
        }
    }

    private void Update()
    {
        
    }
}
