using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public CharacterController controller;
    //public Animator anim;
    public Transform targetIndicator;
    public Transform target;
    public ParticleSystem movementEffect;

    private RespawnableObjectFalling faller;

    public float speed;
    public float gravity;

    Vector3 moveDirection;

    public List<RespawnableObject> listFriends;


    [HideInInspector]
    public bool safe;

    GameManager manager;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        faller = GetComponent<RespawnableObjectFalling>();
    }

    void Update()
    {
        if (!manager.gameStarted)
            return;

        if (!faller.hasSpawned)
            return;

        Vector2 direction = joystick.direction;
        bool isMoving = false;
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(direction.x, 0, direction.y);

            isMoving = moveDirection != Vector3.zero;

            Quaternion targetRotation = isMoving ? Quaternion.LookRotation(moveDirection) : transform.rotation;
            transform.rotation = targetRotation;
            moveDirection *= speed;
        }

        moveDirection.y -= (gravity * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
        //respawnableObject.UpdateFacingRotation(moveDirection);


        if (isMoving)
        {
            movementEffect.Play();
        }
        else
        {
            movementEffect.Stop();
        }

        //if (!safe)
        //{
        //    Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        //    Quaternion targetIndicatorRotation = Quaternion.LookRotation((targetPosition - transform.position).normalized);
        //    targetIndicator.rotation = Quaternion.Slerp(targetIndicator.rotation, targetIndicatorRotation, Time.deltaTime * 50);
        //}
    }

    public void AddFriend(RespawnableObject friend)
    {
        listFriends.Add(friend);
    }

    public void RemoveFriend(RespawnableObject friend)
    {
        listFriends.Remove(friend);

        foreach (RespawnableObject character in listFriends)
        {
            if (character.TryGetComponent<FriendCharacter>(out var friendChar))
            {
                friendChar.StartFollowingPlayer();
            }
        }
    }

    public void KillFriends()
    {
        foreach (RespawnableObject friend in listFriends)
        {
            friend.FriendDieWithoutRemoveFromPlayerController();
        }
        listFriends.Clear();
    }

    public RespawnableObject GetLastFriend()
    {
        if (listFriends.Count < 1)
        {
            RespawnableObject plr = GetComponent<RespawnableObject>();
            return plr;
        }
        return listFriends[^1];
    }

    public void Fire()
    {
        //GameObject newBullet = bulletStorage.Count > 0 ? recycleBullet() : Instantiate(bullet);

        //newBullet.transform.rotation = transform.rotation;
        //newBullet.transform.position = gunFront.position;

        //Bullet bulletController = newBullet.GetComponent<Bullet>();
        //bulletController.player = this;
        //shootingEffect.Play();
    }

    public void SwitchSafeState(bool safe)
    {
        this.safe = safe;

        targetIndicator.gameObject.SetActive(!safe);
    }
}
