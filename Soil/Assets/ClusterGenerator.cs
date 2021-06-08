using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterGenerator : MonoBehaviour
{
    public LayerMask ableToPutStructuresOn;
    public int amount;
    public GameObject itemToCluster;
    public Vector3 area;

    private void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 startPos = RandomPointAboveTerrain();

            RaycastHit hit;
            if (Physics.Raycast(startPos, Vector3.down, out hit, Mathf.Infinity, ableToPutStructuresOn))
            {
                GameObject newItem = Instantiate(itemToCluster, position: hit.point, Quaternion.identity);

                Vector3 newUp = hit.normal;
                Vector3 oldForward = newItem.transform.forward;

                Vector3 newRight = Vector3.Cross(newUp, oldForward);
                Vector3 newForward = Vector3.Cross(newRight, newUp);

                newItem.transform.rotation = Quaternion.LookRotation(newForward, newUp);
            }
        }
        Destroy(gameObject);
    }

    private Vector3 RandomPointAboveTerrain()
    {
        return new Vector3(
            Random.Range(transform.position.x - area.x / 2, transform.position.x + area.x / 2),
            transform.position.y + area.y * 2,
            Random.Range(transform.position.z - area.z / 2, transform.position.z + area.z / 2)
        );
    }
}
