using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOrb : MonoBehaviour
{
    public PlayerController controller;
    public AudioClip PickUpPointSFX;
    public AudioSource AudioPlayer;

    public bool HasCollided = false;

    void Update()
    {
        if (HasCollided)
            controller.IsGrounded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected");
        AudioPlayer.PlayOneShot(PickUpPointSFX);
        if (collision.gameObject.name == "player_base")
        {
            HasCollided = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player_base")
            HasCollided = false;
    }
}
