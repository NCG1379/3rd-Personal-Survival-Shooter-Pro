using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private float lookRange = 20.0f;
    // Start is called before the first frame update
    private int _damageAmount = 20;

    [SerializeField]
    private GameObject _bloodSplater;
    GameObject _bloodClone;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

            if (Physics.Raycast(rayOrigin, out hit, lookRange, 7<<0))  //Physics.Raycast(rayOrigin, out hit, lookRange, 1[Layer Affected])
            {
                if (hit.collider != null)
                {
                    Debug.Log("Name of Hit Object: " + hit.collider.name);
                    Debug.DrawRay(rayOrigin.origin, rayOrigin.direction * lookRange, Color.blue);

                    Health health = hit.collider.GetComponent<Health>();

                    if (health != null)
                    {
                        _bloodClone = Instantiate(_bloodSplater, hit.point, Quaternion.LookRotation(hit.normal));
                        health.Damage(_damageAmount);                        
                    }


                }
            }

        }
            if(_bloodClone != null)
                Destroy(_bloodClone.gameObject, 0.25f);
    }
}
