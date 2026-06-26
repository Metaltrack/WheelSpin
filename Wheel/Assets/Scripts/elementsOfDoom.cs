using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
        GameObject instance = Instantiate(tornado, pos, tornado.transform.rotation);
        Destroy(instance, 5f);
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
                Vector3 pos = new Vector3(
                        (transform.position.x + other_pos.x) / 2,
                        (transform.position.y + other_pos.y) / 2,
                        (transform.position.z + other_pos.z) / 2
                    );

                Doom(pos);
            }
        }
    }

    private void Update()
    {
        
    }
}
