using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoints : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; // Array of spawn point transforms
    [SerializeField] private float gizmoSize = 0.3f; // Size of the gizmo sphere
    [SerializeField] private Color gizmoColor = Color.green; // Color of the gizmos

    private void OnDrawGizmos()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
            return;

        Gizmos.color = gizmoColor;
        
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i] != null)
            {
                Gizmos.DrawSphere(spawnPoints[i].position, gizmoSize);
                UnityEditor.Handles.Label(spawnPoints[i].position + Vector3.up * 0.2f, $"Spawn{i + 1}");
            }
        }
    }
}