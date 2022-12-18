using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public static Monster Instance;

    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;

    private void Update()
    {
        if (!PlayerController.ControlEnabled) return;
        if (!agent.enabled) return;

        float distToTarget = Vector3.Distance(transform.position, target.position);
        if (distToTarget < 2.5f)
        {
            agent.isStopped = true;
            anim.SetBool("Attack_1", true);
        }
        else
        {
            anim.SetBool("Attack_1", false);

            agent.isStopped = false;
            agent.SetDestination(target.position);

        }

        if (agent.velocity.normalized.magnitude > 0.001f)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);

        }
    }
}
