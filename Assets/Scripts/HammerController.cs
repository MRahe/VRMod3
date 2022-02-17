using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    private Rigidbody rb;

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


            bool xSliceable = false, ySliceable = false, zSliceable = false;

            try
            {
                xSliceable = other.gameObject.GetComponent<SliceableController>().xSliceable;
                ySliceable = other.gameObject.GetComponent<SliceableController>().ySliceable;
                zSliceable = other.gameObject.GetComponent<SliceableController>().zSliceable;
            }
            catch
            {

            }

            float minVelocity = .0000003f;

            if (xSliceable &&
                Mathf.Sqrt(rb.velocity.y * rb.velocity.y + rb.velocity.z * rb.velocity.z) > minVelocity)
            {
                Smash(other.gameObject, gameObject);
            }
            else if (ySliceable &&
                Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z) > minVelocity)
            {
                Smash(other.gameObject, gameObject);
            }
            else if (zSliceable &&
                Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y) > minVelocity)
            {
                Smash(other.gameObject, gameObject);
            }
        }
    }

    private void Smash(GameObject smashable, GameObject smasher)
    {
        smashable.GetComponent<Rigidbody>().isKinematic = false;
        smashable.GetComponent<Rigidbody>().useGravity = true;

        smashable.GetComponent<Rigidbody>().AddForce(smasher.GetComponent<Rigidbody>().velocity.x * 10000f * 10000f,
                                                   smasher.GetComponent<Rigidbody>().velocity.z * 10000f * 10000f + 100f,
                                                   smasher.GetComponent<Rigidbody>().velocity.z * 10000f * 10000f);

        smashable.GetComponent<Collider>().isTrigger = false;

    }
}
