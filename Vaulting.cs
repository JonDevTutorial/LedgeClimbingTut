using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//LICENSE, YOU ARE LEGALLY REQUIRED TO LIKE THE VIDEO IF YOU COPY THIS CODE :) (i promise its legally binding, 50 years prison minimum)
public class Vaulting : MonoBehaviour
{
    // Start is called before the first frame update
    private int vaultLayer;
    public Camera cam;
    private float playerHeight = 2f;
    private float playerRadius = 0.5f;
    void Start()
    {
        vaultLayer = LayerMask.NameToLayer("VaultLayer");
        vaultLayer = ~vaultLayer;
    }

    // Update is called once per frame
    void Update()
    {
        Vault();
    }
    private void Vault()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out var firstHit, 1f, vaultLayer))
            {
                print("vaultable in front");
                if (Physics.Raycast(firstHit.point + (cam.transform.forward * playerRadius) + (Vector3.up * 0.6f * playerHeight), Vector3.down, out var secondHit, playerHeight))
                {
                    print("found place to land");
                    StartCoroutine(LerpVault(secondHit.point, 0.5f));
                }
            }
        }

    }
    IEnumerator LerpVault(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
