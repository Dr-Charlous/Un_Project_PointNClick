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
        MeshRotation();
    }

    void MouseDownDestination()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)mousePosition, Vector2.zero);

        if (Input.GetMouseButtonUp(0)/* && hit.collider != null*/)
        {
            followSpot = new Vector2(mousePosition.x, mousePosition.y);
        }

        agent.SetDestination(Vector2.MoveTowards(transform.position, followSpot, Time.deltaTime * speed));
    }

    void MeshRotation()
    {
        if (agent.velocity.x < 0)
        {
            transformChild.localScale = new Vector3(-1, transformChild.localScale.y, transformChild.localScale.z);
        }
        else if (agent.velocity.x > 0)
        {
            transformChild.localScale = new Vector3(1, transformChild.localScale.y, transformChild.localScale.z);
        }
    }
}
