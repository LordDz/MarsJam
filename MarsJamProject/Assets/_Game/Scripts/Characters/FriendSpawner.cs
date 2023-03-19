using System.Collections.Generic;
using UnityEngine;

public class FriendSpawner : MonoBehaviour
{
    [SerializeField] Transform respawnPosition;
    [SerializeField] CharacterRespawner characterRespawner;
    [SerializeField] PlayerController playerController;

    private FriendCharacter[] listFriends;
    private List<RespawnableObject> listFriendsRespawn;

    private void Start()
    {
        listFriends = GetComponentsInChildren<FriendCharacter>();
        listFriendsRespawn = new List<RespawnableObject>();

        foreach (FriendCharacter friend in listFriends)
        {
            friend.Init(playerController);
            listFriendsRespawn.Add(friend.GetComponent<RespawnableObject>());
        }
    }

    public void SpawnFriend()
    {
        Debug.Log("Trying to spawn friend");

        for (int i = 0; i < listFriends.Length; i++)
        {
            if (!listFriends[i].gameObject.activeSelf)
            {
                //characterRespawner.RespawnObject(listFriends[i].GetComponent<RespawnableObject>(), false);
                characterRespawner.RespawnObject(listFriendsRespawn[i], false);
                //listFriends[i].StartFollowingPlayer();

                //listFriends[i].gameObject.SetActive(true);
                Debug.Log("Spawn friend!");
                break;
            }
        }
    }

    public Vector3 GetRespawnPos()
    {
        return respawnPosition.position;
    }
}
