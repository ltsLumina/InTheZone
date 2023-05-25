using System;
using UnityEngine;

public class Aim_Down_Sights : MonoBehaviour
{
    [SerializeField] Transform activeWeapon;

    [SerializeField] Transform defaultPosition;
    [SerializeField] Transform adsPosition;
    [SerializeField] Vector3 weaponPosition; // set to 0 0 0 in inspector

    [SerializeField] float aimSpeed = 0.25f;  // time to enter ADS
    [SerializeField] float _defaultFOV = 80f; // FOV in degrees
    [SerializeField] float zoomRatio = 0.5f;  // 1/zoom times

    [SerializeField] CameraController fpsCam; // player camera

    // Cached References
    Animator gunAnimator;

    // Cached Hashes
    readonly static int IsAds = Animator.StringToHash("isADS");

    void Start()
    {
        gunAnimator = FindObjectOfType<Gun>().GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // ADS camera and gun movement
        if(Input.GetButton("Fire2"))
        {
            weaponPosition = Vector3.Lerp(weaponPosition, adsPosition.localPosition, aimSpeed * Time.deltaTime);
            activeWeapon.localPosition = weaponPosition;
            SetFieldOfView(Mathf.Lerp(fpsCam.FOV, zoomRatio * _defaultFOV, aimSpeed * Time.deltaTime));

            // slow down idle animation
            gunAnimator.SetBool(IsAds, true);
        }
        else
        {
            weaponPosition = Vector3.Lerp(weaponPosition, defaultPosition.localPosition, aimSpeed * Time.deltaTime);
            activeWeapon.localPosition = weaponPosition;
            SetFieldOfView(Mathf.Lerp(fpsCam.FOV, _defaultFOV, aimSpeed * Time.deltaTime));
            gunAnimator.SetBool(IsAds, false);
        }
    }

    void SetFieldOfView(float fov)
    {
        fpsCam.FOV = fov;
    }
}