using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector2 followSpot;
    public float speed;

    private void Update()
    {
        MouseDown();
    }

    void MouseDown()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            followSpot = new Vector2(mousePosition.x, mousePosition.y);
        }
        agent.SetDestination(Vector2.MoveTowards(transform.position, followSpot, Time.deltaTime * speed));
    }
}
