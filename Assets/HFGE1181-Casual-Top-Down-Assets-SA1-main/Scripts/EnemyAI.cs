using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private float wanderDistance;
    [SerializeField] private float wanderSpeed;
    [SerializeField] private float idleTime;

    [Header("Map Bounds")]
    [SerializeField] private Vector2 minBounds = new Vector2(1.5f, 1.5f);
    [SerializeField] private Vector2 maxBounds = new Vector2(22.5f, 14.5f);
    [SerializeField] private LayerMask wallLayers;

    private void Start()
    {
        StartCoroutine(IdleAtLocation());
    }

    public Vector3 SelectNewLocation()
    {
        float radius = 0.3f * transform.localScale.x;

        for (int i = 0; i < 10; i++)
        {
            Vector3 candidate = new Vector3(
                Random.Range(transform.position.x - wanderDistance, transform.position.x + wanderDistance),
                Random.Range(transform.position.y - wanderDistance, transform.position.y + wanderDistance), 0);

            candidate.x = Mathf.Clamp(candidate.x, minBounds.x, maxBounds.x);
            candidate.y = Mathf.Clamp(candidate.y, minBounds.y, maxBounds.y);

            Vector2 direction = candidate - transform.position;
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, direction.normalized, direction.magnitude, wallLayers);

            if (hit.collider == null)
            {
                return candidate;
            }
        }

        return transform.position;
    }

    public IEnumerator IdleAtLocation()
    {
        yield return new WaitForSecondsRealtime(idleTime);
        StartCoroutine(MoveTowardsLocation());
    }
    
    public IEnumerator MoveTowardsLocation()
    {
        
        Vector3 newLocation = SelectNewLocation();
        while (Vector2.Distance(transform.position, newLocation) > 0.015f)
        {
            
            yield return new WaitWhile(() => GameStateManager.Instance.GetGameState() == GameState.Paused); 
            
            transform.position = Vector3.MoveTowards(transform.position, newLocation, 
                wanderSpeed * Time.deltaTime);
            yield return null;
        }
        
        yield return StartCoroutine(IdleAtLocation());    
    }


    
}
