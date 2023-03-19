using Unity.Services.Analytics.Internal;
using UnityEngine;

public class RespawnableObject : MonoBehaviour
{
    [SerializeField] CharacterRespawner playerRespawner;

    [SerializeField] GameObject bloodEffect;

    [SerializeField] GameObject SpriteFront, SpriteSide;

    [SerializeField] bool isPlayer = false;

    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource deathSound2;

    [SerializeField] PlayerController plrController;
    [SerializeField] RespawnableObjectFalling faller;

    private void Start()
    {
        plrController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        UpdateFacingRotation();
    }

    private void UpdateFacingRotation()
    {
        float angleY = Mathf.Abs(transform.rotation.eulerAngles.y);
        if (angleY > 20 && angleY < 160 || angleY > 250f)
        {
            SpriteFront.SetActive(false);
            SpriteSide.SetActive(true);
        }
        else
        {
            SpriteFront.SetActive(true);
            SpriteSide.SetActive(false);
        }
    }

    public void Die()
    {
        Instantiate(bloodEffect, transform.position + Vector3.up * 1.5f, transform.rotation);
        gameObject.SetActive(false);
        deathSound.Play();
        deathSound2.Play();
        faller.Reset();

        if (isPlayer)
        {
            plrController.KillFriends();
            playerRespawner.RespawnObject(this, true);
        }
        else
        {
            plrController.RemoveFriend(this);
            //playerRespawner.RespawnObject(this, false);
        }
    }

    public void FriendDieWithoutRemoveFromPlayerController()
    {
        Instantiate(bloodEffect, transform.position + Vector3.up * 1.5f, transform.rotation);
        gameObject.SetActive(false);
        deathSound.Play();
        deathSound2.Play();
    }
}
