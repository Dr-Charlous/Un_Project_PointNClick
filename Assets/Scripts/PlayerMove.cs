using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public UnityEngine.Transform transformChild;
    public Vector2 followSpot;
    public float speed;

    private void Start()
    {
        transformChild.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }

    private void Update()
    {
        MouseDownDestination();
    }

    void MouseDownDestination()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            followSpot = new Vector2(mousePosition.x, mousePosition.y);
        }
        agent.SetDestination(Vector2.MoveTowards(transform.position, followSpot, Time.deltaTime * speed));
    }
}
