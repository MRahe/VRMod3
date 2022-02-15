using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableController : MonoBehaviour
{
    public bool xSliceable = false;
    public bool ySliceable = false;
    public bool zSliceable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (transform.position.y < 0f)
        {
            Destroy(gameObject);
        }
    }
}
