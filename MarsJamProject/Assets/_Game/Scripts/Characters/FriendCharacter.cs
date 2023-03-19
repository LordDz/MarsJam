using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.WSA;

public class FriendCharacter : MonoBehaviour
{

    public NavMeshAgent agent;
    public GameObject spawnEffect;
    public GameObject bloodEffect;
    public float followDistance;
    public float findPlrDistance;

    public float walkspeed;
    public float runspeed;

    Transform player;
    PlayerController plrController;
    RespawnableObject followTarget;

    [SerializeField] MoveArea area;


    [SerializeField]
    bool isFollowingTarget;

    Vector3 randomTarget;
    NavMeshPath path;

    [SerializeField] RespawnableObject respawnableObject;
    [SerializeField] RespawnableObjectFalling faller;

    public void Init(PlayerController plr)
    {
        plrController = plr;

        if (plrController != null)
            player = plrController.gameObject.transform;

        path = new NavMeshPath();

        agent.speed = walkspeed;
        agent.stoppingDistance = followDistance;
        agent.enabled = false;

        Instantiate(spawnEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (plrController == null || !faller.hasSpawned) return;

        if (isFollowingTarget)
        {
            agent.CalculatePath(followTarget.transform.position, path);

            if (path.status != NavMeshPathStatus.PathComplete)
            {
                ContinueWalking();
                return;
            }

            agent.destination = followTarget.transform.position;

            if (Vector3.Distance(transform.position, followTarget.transform.position) < followDistance + 0.1f)
            {
                agent.isStopped = true;
                transform.LookAt(followTarget.transform.position);
            }
            else if (Vector3.Distance(transform.position, followTarget.transform.position) > followDistance + 0.1f)
            {
                agent.isStopped = false;
            }
        }
        else if (Vector3.Distance(transform.position, player.position) < findPlrDistance + 0.1f)
        {

            StartFollowingPlayer();
        }
        else
        {
            ContinueWalking();
        }
    }

    public void StartFollowingPlayer()
    {
        agent.speed = runspeed;
        isFollowingTarget = true;
        followTarget = plrController.GetLastFriend();
        plrController.AddFriend(respawnableObject);
    }

    private void ContinueWalking()
    {
        if (Vector3.Distance(transform.position, randomTarget) < agent.stoppingDistance + 0.1f)
        {
            randomTarget = area.RandomPositionAtPos(gameObject.transform.position);
        }
        else
        {
            agent.destination = randomTarget;
        }
    }
}
