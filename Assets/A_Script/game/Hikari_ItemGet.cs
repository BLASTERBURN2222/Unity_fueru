using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hikari_ItemGet : MonoBehaviour
{

    private int lifetime;


    // Start is called before the first frame update
    void Start()
    {
        lifetime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime++;
        if(lifetime > 8){
            Destroy(this.gameObject);
		}
    }
}
