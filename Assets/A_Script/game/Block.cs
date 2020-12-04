using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : ENE_COMMON
{


	private void Awake()
	{
		init_zako(this.gameObject, 0);
		set_hp_score_exp(10, 100, 0);
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
