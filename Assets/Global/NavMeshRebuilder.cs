using UnityEngine;
using UnityEngine.AI;

public class NavMeshRebuilder : MonoBehaviour
{
    private void Start()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
