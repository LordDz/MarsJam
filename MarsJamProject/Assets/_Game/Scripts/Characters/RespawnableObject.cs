using Unity.Services.Analytics.Internal;
using UnityEngine;

public class RespawnableObject : MonoBehaviour
{
    [SerializeField] CharacterRespawner playerRespawner;

    [SerializeField] GameObject bloodEffect;

    [SerializeField] SpriteRenderer SpriteFront, SpriteBack, SpriteSide;

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
        if (angleY > 45 && angleY < 120 || angleY > 220 && angleY <= 320)
        {
            //Side
            SpriteFront.gameObject.SetActive(false);
            SpriteBack.gameObject.SetActive(false);
            SpriteSide.gameObject.SetActive(true);

            SpriteSide.flipX = !(transform.rotation.eulerAngles.y > 220 && transform.rotation.eulerAngles.y <= 320);
        }
        else
        {
            //Front and back
            bool isFacingForward = !(transform.rotation.eulerAngles.y > 0 && transform.rotation.eulerAngles.y < 45 || angleY > 320);
            SpriteFront.gameObject.SetActive(isFacingForward);
            SpriteBack.gameObject.SetActive(!isFacingForward);
            SpriteSide.gameObject.SetActive(false);


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
