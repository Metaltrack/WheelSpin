using Unity.VisualScripting;
using UnityEngine;

public class elementsOfDoom : MonoBehaviour
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

    private void Doom(Vector3 pos)
    {
        Instantiate(tornado, pos, tornado.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Oven")
        {
            Vector3 other_pos = other.transform.position;
            float dist = Mathf.Sqrt(((other_pos.x - transform.position.x) * (other_pos.x - transform.position.x)) +
                ((other_pos.y - transform.position.y) * (other_pos.y - transform.position.y)) +
                ((other_pos.z - transform.position.z) * (other_pos.z - transform.position.z))
            );

            if (!spawned)
            {
                spawned = true;
                Doom(new Vector3(transform.position.x - dist, transform.position.y - dist, transform.position.z - dist));
            }
        }
    }

    private void Update()
    {
        
    }
}
