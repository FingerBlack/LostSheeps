using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[ExecuteInEditMode]
[AddComponentMenu("Navigation/CustomNavMeshObstacle", 38)]
public class CustomNavMeshObstacle : MonoBehaviour
{
    [SerializeField] private float _height = 1f;
    [SerializeField] private float _radius = 0.5f;

    private NavMeshObstacle _navMeshObstacle;

    void Start()
    {
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        SetupObstacle();
    }

        void Update()
    {
        //_navMeshObstacle = GetComponent<NavMeshObstacle>();
        SetupObstacle();
    }
    // 设置障碍物属性
    public void SetupObstacle()
    {
        _navMeshObstacle.height = _height;
        _navMeshObstacle.radius = _radius;
    }

    // 设置障碍物高度
    public void SetHeight(float newHeight)
    {
        _height = Mathf.Clamp(newHeight, 0f, float.MaxValue);
        SetupObstacle();
    }

    // 设置障碍物半径
    public void SetRadius(float newRadius)
    {
        _radius = Mathf.Clamp(newRadius, 0f, float.MaxValue);
        SetupObstacle();
    }
}
