using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bloodSplat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Aiming();
    }

    private void Aiming()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(0.5f, 0.5f, 0);
            Ray rayOrigin = Camera.main.ViewportPointToRay(center);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 0))
            {
                Debug.Log("Hit:" + hitInfo.collider.name);
                Health health = hitInfo.collider.GetComponent<Health>();

                if (health != null)
                {
                    var rot = Quaternion.LookRotation(hitInfo.normal);
                    Instantiate(_bloodSplat, hitInfo.point, rot);
                    health.Damage(5);
                }
            }
        }
    }
}
