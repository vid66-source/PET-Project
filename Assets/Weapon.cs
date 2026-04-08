using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private float _maxDistance = 15;
    [SerializeField] private GameObject _bloodVFX;
    [SerializeField] private float _fireDelay = 0.5f;
    [SerializeField] private float _bloodVFXDelay = 2f;
    private bool _canFire = true;

    private List<GameObject> _bloodVFXPool;

    public List<GameObject> BloodVFXPool
    {
        get
        {
            if (_bloodVFXPool == null)
            {
                _bloodVFXPool = BloodPoolCreation();
            }

            return _bloodVFXPool;
        }
    }

    private void Update()
    {
        Shoot();
        Debug.DrawRay(_mainCamera.position, _mainCamera.forward * _maxDistance, Color.red);
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && _canFire)
        {
            _canFire = false;
            StartCoroutine(WaitForFire());
            Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out RaycastHit hit,
                _maxDistance);
            if (hit.collider != null)
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.TakeDamage(10);
                    Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    int indexReadyToUse = BloodVFXPool.FindIndex(b => !b.activeInHierarchy);

                    if (indexReadyToUse == -1)
                    {
                        GameObject newPrefab = Instantiate(_bloodVFX, hit.point, spawnRotation);
                        StartCoroutine(DeactivateAfterDelay(newPrefab, _bloodVFXDelay));
                        _bloodVFXPool.Add(newPrefab);
                    }
                    else
                    {
                        BloodVFXPool[indexReadyToUse].transform.position = hit.point;
                        BloodVFXPool[indexReadyToUse].transform.rotation = spawnRotation;
                        BloodVFXPool[indexReadyToUse].SetActive(true);
                        StartCoroutine(DeactivateAfterDelay(BloodVFXPool[indexReadyToUse], _bloodVFXDelay));
                    }
                }
            }
        }
    }

    private IEnumerator WaitForFire()
    {
        yield return new WaitForSeconds(_fireDelay);
        _canFire = true;
    }

    private IEnumerator DeactivateAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }

    private List<GameObject> BloodPoolCreation()
    {
        List<GameObject> bloodPool = new List<GameObject>(10);
        for (int i = 0; i < 10; i++)
        {
            bloodPool.Add(Instantiate(_bloodVFX));
        }

        foreach (var bl in bloodPool)
        {
            bl.SetActive(false);
        }
        return bloodPool;
    }

}