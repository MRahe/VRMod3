using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour
{
    private Rigidbody rb;
    bool sliced;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        sliced = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sliceable"))
        {
            Debug.Log("Sword velocity x: " + rb.velocity.x);

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

            float minHeight = .05f;
            float minVelocity = .0000003f;

            if (!sliced && xSliceable &&
                other.gameObject.transform.position.x - transform.position.x + other.gameObject.transform.localScale.x / 2f > minHeight &&
                transform.position.x - other.gameObject.transform.position.x + other.gameObject.transform.localScale.x / 2f > minHeight &&
                Mathf.Sqrt(rb.velocity.y * rb.velocity.y + rb.velocity.z * rb.velocity.z) > minVelocity)
            {
                sliced = true;
                SliceX(other.gameObject, gameObject);
                Destroy(other.gameObject);
            }
            else if (!sliced && ySliceable &&
                other.gameObject.transform.position.y - transform.position.y + other.gameObject.transform.localScale.y / 2f > minHeight && 
                transform.position.y - other.gameObject.transform.position.y + other.gameObject.transform.localScale.y / 2f > minHeight &&
                Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z) > minVelocity)
            {
                sliced = true;
                SliceY(other.gameObject, gameObject);
                Destroy(other.gameObject);
            }
            else if (!sliced && zSliceable &&
                other.gameObject.transform.position.z - transform.position.z + other.gameObject.transform.localScale.z / 2f > minHeight &&
                transform.position.z - other.gameObject.transform.position.z + other.gameObject.transform.localScale.z / 2f > minHeight &&
                Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y) > minVelocity)
            {
                sliced = true;
                SliceZ(other.gameObject, gameObject);
                Destroy(other.gameObject);
            }
        }
    }

    private void SliceX(GameObject sliceable, GameObject slicer)
    {
        Vector3 pos = sliceable.transform.position;
        Vector3 scale = sliceable.transform.localScale;

        Vector3 slice = slicer.transform.position;

        GameObject topHalf = Instantiate(sliceable, new Vector3((pos.x + scale.x / 2f + slice.x), pos.y, pos.z), sliceable.transform.rotation);
        topHalf.transform.localScale = new Vector3(pos.x - slice.x + scale.x / 2f, scale.y, scale.z);
        topHalf.GetComponent<Rigidbody>().isKinematic = false;
        topHalf.GetComponent<Rigidbody>().useGravity = true;
        topHalf.GetComponent<Rigidbody>().AddForce(slicer.GetComponent<Rigidbody>().velocity.x * 10000f * 10000f, 
                                                   slicer.GetComponent<Rigidbody>().velocity.z * 10000f * 10000f + 100f, 
                                                   slicer.GetComponent<Rigidbody>().velocity.z * 10000f * 10000f);
        try
        {
            topHalf.GetComponent<Collider>().isTrigger = false;

        }
        catch
        {
            Debug.Log("Unable to access Collider");
        }
        topHalf.tag = "Sliced";

        GameObject bottomHalf = Instantiate(sliceable, new Vector3((pos.x - scale.x / 2f + slice.x) / 2f, pos.y, pos.z), sliceable.transform.rotation);
        bottomHalf.transform.localScale = new Vector3(slice.x - pos.x + scale.x / 2f, scale.y, scale.z);
        bottomHalf.GetComponent<Rigidbody>().isKinematic = false;
        //bottomHalf.GetComponent<Rigidbody>().useGravity = true;
        //bottomHalf.tag = "Sliced";
    }

    private void SliceY(GameObject sliceable, GameObject slicer)
    {
        Vector3 pos = sliceable.transform.position;
        Vector3 scale = sliceable.transform.localScale;

        Vector3 slice = slicer.transform.position;

        GameObject topHalf = Instantiate(sliceable, new Vector3(pos.x, (pos.y + scale.y / 2f + slice.y) / 2f, pos.z), sliceable.transform.rotation);
        topHalf.transform.localScale = new Vector3(scale.x, pos.y - slice.y + scale.y/2f, scale.z);
        topHalf.GetComponent<Rigidbody>().isKinematic = false;
        topHalf.GetComponent<Rigidbody>().useGravity = true;
        topHalf.GetComponent<Rigidbody>().AddForce(slicer.GetComponent<Rigidbody>().velocity.x * 10000f * 10000f,
                                                   slicer.GetComponent<Rigidbody>().velocity.z * 10000f * 10000f + 100f,
                                                   slicer.GetComponent<Rigidbody>().velocity.z * 10000f * 10000f);
        try
        {
            topHalf.GetComponent<Collider>().isTrigger = false;

        } 
        catch {
            Debug.Log("Unable to access Collider");
        }
        topHalf.tag = "Sliced";

        GameObject bottomHalf = Instantiate(sliceable, new Vector3(pos.x, (pos.y - scale.y / 2f + slice.y) / 2f, pos.z), sliceable.transform.rotation);
        bottomHalf.transform.localScale = new Vector3(scale.x, slice.y- pos.y + scale.y / 2f, scale.z);
        bottomHalf.GetComponent<Rigidbody>().isKinematic = false;
        //bottomHalf.GetComponent<Rigidbody>().useGravity = true;
        //bottomHalf.tag = "Sliced";
    }

    private void SliceZ(GameObject sliceable, GameObject slicer)
    {
        Vector3 pos = sliceable.transform.position;
        Vector3 scale = sliceable.transform.localScale;

        Vector3 slice = slicer.transform.position;

        GameObject topHalf = Instantiate(sliceable, new Vector3(pos.x, pos.y, (pos.z + scale.z / 2f + slice.z) / 2f), sliceable.transform.rotation);
        topHalf.transform.localScale = new Vector3(scale.x, scale.y, pos.z - slice.z + scale.z / 2f);
        topHalf.GetComponent<Rigidbody>().isKinematic = false;
        topHalf.GetComponent<Rigidbody>().useGravity = true;
        topHalf.GetComponent<Rigidbody>().AddForce(slicer.GetComponent<Rigidbody>().velocity.x * 10000f * 10000f,
                                                   slicer.GetComponent<Rigidbody>().velocity.z * 10000f * 10000f + 100f,
                                                   slicer.GetComponent<Rigidbody>().velocity.z * 10000f * 10000f);
        try
        {
            topHalf.GetComponent<Collider>().isTrigger = false;
        }
        catch
        {
            Debug.Log("Unable to access Collider");
        }
        topHalf.tag = "Sliced";

        GameObject bottomHalf = Instantiate(sliceable, new Vector3(pos.x, pos.y, (pos.z - scale.z / 2f + slice.z) / 2f), sliceable.transform.rotation);
        bottomHalf.transform.localScale = new Vector3(scale.x, scale.y, slice.z - pos.z + scale.z / 2f);
        bottomHalf.GetComponent<Rigidbody>().isKinematic = false;
        //bottomHalf.GetComponent<Rigidbody>().useGravity = true;
        //bottomHalf.tag = "Sliced";
    }
}
