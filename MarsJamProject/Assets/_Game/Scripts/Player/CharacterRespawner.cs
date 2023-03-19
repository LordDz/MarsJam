using System.Collections;
using UnityEngine;

public class CharacterRespawner : MonoBehaviour
{
    [SerializeField] float respawnTime = 2.0f;
    [SerializeField] FriendSpawner friendSpawner;
    [SerializeField] AudioSource audioRespawnPlayer;
    [SerializeField] ScoreInventory scoreInventory;

    public void RespawnObject(RespawnableObject gameObj, bool isPlayer, FriendCharacter friendCharacter = null)
    {
        if (isPlayer)
        {
            //TODO: Do something cool here
            scoreInventory.Reset();
            StartCoroutine(RespawnPlayer(gameObj, friendSpawner.GetRespawnPos(), null));
        }
        else
        {
            RespawnCharacter(gameObj, friendSpawner.GetRespawnPos(), friendCharacter);
        }
    }

    IEnumerator RespawnPlayer(RespawnableObject gameObj, Vector3 respawnPos, FriendCharacter friendCharacter)
    {
        yield return new WaitForSeconds(respawnTime);
        RespawnCharacter(gameObj, respawnPos, null);
        audioRespawnPlayer.Play();
    }

    private void RespawnCharacter(RespawnableObject gameObj, Vector3 respawnPos, FriendCharacter friendCharacter = null)
    {
        gameObj.gameObject.transform.position = respawnPos;
        gameObj.gameObject.SetActive(true);
        if (friendCharacter != null)
        {
            friendCharacter.StartFollowingPlayer();
        }
    }
}
