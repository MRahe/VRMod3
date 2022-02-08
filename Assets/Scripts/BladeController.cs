using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour
{
    private Rigidbody rb;

    private float startThickness = -1f;
    private float minThickness = .05f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sliceable"))
        {
            if (startThickness == -1f)
            {
                startThickness = other.gameObject.transform.localScale.y;
            } 

            if (other.gameObject.transform.localScale.y > startThickness * minThickness)
            {
                Slice(other.gameObject);
            }

            Destroy(other.gameObject);
        }
    }

    private void Slice(GameObject gameObject)
    {
        GameObject topHalf = Instantiate(gameObject, gameObject.transform.position + new Vector3(0f, .05f, 0f), gameObject.transform.rotation);
        topHalf.transform.localScale = new Vector3(topHalf.transform.localScale.x, topHalf.transform.localScale.y * .5f, topHalf.transform.localScale.z);
        GameObject bottomHalf = Instantiate(gameObject, gameObject.transform.position + new Vector3(0f, -.05f, 0f), gameObject.transform.rotation);
        bottomHalf.transform.localScale = new Vector3(bottomHalf.transform.localScale.x, bottomHalf.transform.localScale.y * .5f, bottomHalf.transform.localScale.z);
    }
}
