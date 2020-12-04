using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAP_Hikari : MonoBehaviour
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
		if (GG.pause == 1) return;

		lifetime++;
		if(lifetime > 10){
			Destroy(this.gameObject);
		}
	}

}




