using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;

    private void Update()
    {
        Shoot();
        Debug.DrawRay(_mainCamera.position, _mainCamera.forward * 100f, Color.red);
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out RaycastHit hit);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
            }
        }
    }
}
