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

            float minHeight = .05f;
            if (!sliced &&
                other.gameObject.transform.position.y - transform.position.y + other.gameObject.transform.localScale.y / 2f > minHeight &&
                transform.position.y - other.gameObject.transform.position.y + other.gameObject.transform.localScale.y / 2f > minHeight &&
                (rb.velocity.x > .0000003f || rb.velocity.x < -.0000003f))
            {
                Slice(other.gameObject, gameObject);
                Destroy(other.gameObject);
                sliced = true;
            }
        }
    }

    private void Slice(GameObject sliceable, GameObject slicer)
    {
        Vector3 pos = sliceable.transform.position;
        Vector3 scale = sliceable.transform.localScale;

        Vector3 slice = slicer.transform.position;

        GameObject topHalf = Instantiate(sliceable, new Vector3(pos.x, (pos.y + scale.y / 2f + slice.y) / 2f, pos.z), sliceable.transform.rotation);
        topHalf.transform.localScale = new Vector3(scale.x, pos.y - slice.y + scale.y/2f, scale.z);
        topHalf.GetComponent<Rigidbody>().isKinematic = false;
        topHalf.GetComponent<Rigidbody>().useGravity = true;
        topHalf.GetComponent<Rigidbody>().AddForce(100f, 100f, 0f);
        topHalf.GetComponent<BoxCollider>().isTrigger = false;
        topHalf.tag = "Sliced";

        GameObject bottomHalf = Instantiate(sliceable, new Vector3(pos.x, (pos.y - scale.y / 2f + slice.y) / 2f, pos.z), sliceable.transform.rotation);
        bottomHalf.transform.localScale = new Vector3(scale.x, slice.y- pos.y + scale.y / 2f, scale.z);
        bottomHalf.GetComponent<Rigidbody>().isKinematic = false;
        //bottomHalf.GetComponent<Rigidbody>().useGravity = true;
        //bottomHalf.tag = "Sliced";
    }
}
