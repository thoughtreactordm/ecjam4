using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Box : MonoBehaviour
{
    bool moving = false;

    public float pushSpeed = 2f;
    public Ease pushEasing;

    public AudioClip[] dragSfx;
    AudioSource audio;

    void Start()
    {
        DOTween.defaultEaseType = pushEasing;
        audio = GetComponent<AudioSource>();
    }

    // Returns whether a block can be moved 1 unit
    public bool CanMove(Vector3 dir)
    {
        // Automatically cancel push if box is moving
        if (moving) return false;

        Ray ray = new Ray(transform.position, dir);
        RaycastHit hit;

        // If ray hits anything, box cannot be pushed
        if (Physics.Raycast(ray, out hit, 1f)) {
            return false;
        }

        return true;
    }

    public void Move(Vector3 dir)
    {
        // Toggle moving true to prevent further pushing while running tween
        moving = true;
        // Move box in the direction over time (as pushSpeed), when finished execute MoveComplete()
        transform.parent.DOMove(transform.parent.position + dir, pushSpeed, false).OnComplete(MoveComplete);

        audio.clip = dragSfx[Random.Range(0, dragSfx.Length - 1)];
        audio.Play();
    }

    // Actions run when the move tween completes
    void MoveComplete()
    {
        moving = false;
    }
}
