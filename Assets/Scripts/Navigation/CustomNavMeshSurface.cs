using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

[ExecuteInEditMode]
[AddComponentMenu("Navigation/CustomNavMeshSurface", 38)]
public class CustomNavMeshSurface : MonoBehaviour
{
    [SerializeField]private Vector3 m_UpDirection = Vector3.up;
    public NavMeshCollectGeometry m_CollectGeometry = NavMeshCollectGeometry.PhysicsColliders;
    public LayerMask m_LayerMask = ~0;
    public int m_AgentTypeID = 0;
    
    public void BuildNavMesh()
    {
        NavMeshData navMeshData = new NavMeshData();

        List<NavMeshBuildSource> sources = new List<NavMeshBuildSource>();

        // 创建一个Bounds及其半径
        Bounds bounds = new Bounds(transform.position, transform.lossyScale * 100);

        // 使用NavMeshBuilder.CollectSources
        NavMeshBuilder.CollectSources(bounds, m_LayerMask, m_CollectGeometry, 0, new List<UnityEngine.AI.NavMeshBuildMarkup>(), sources);

        NavMeshBuildSettings buildSettings = NavMesh.GetSettingsByID(m_AgentTypeID);
        if (buildSettings.agentTypeID == -1)
        {
            Debug.LogError("The agent type is not set. Please go to Navigation settings and add a new agent type.");
            return;
        }

        if (!NavMeshBuilder.UpdateNavMeshData(navMeshData, buildSettings, sources, bounds))
        {
            Debug.LogError("NavMesh build failed!");
        }

        GetComponent<NavMeshSurface>().navMeshData = navMeshData;
    }
}