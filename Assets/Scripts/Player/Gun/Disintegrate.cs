using System;
using Essentials;
using UnityEngine;

public class Disintegrate : MonoBehaviour
{
    [Tooltip("The time the object is on the ground before dissipating."),
    SerializeField] float groundTime;

    // Cached References
    Collider col;

    void OnEnable()
    {
        col = GetComponent<Collider>();
        Dissipate();
    }

    void Dissipate()
    {
        StartCoroutine(Sequencing.WaitForSeconds(OnDestruction, groundTime));

        void OnDestruction()
        {
            StartCoroutine(Sequencing.SequenceActions(() =>
            {
                col.enabled = false;
            }, 1f, () => Destroy(gameObject)));
        }

    }
}