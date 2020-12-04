using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baku_boss : MonoBehaviour
{
	private GameObject pf_baku;
	private void Awake()
	{
		pf_baku = (GameObject)Resources.Load("BAKU");
	}

	private int cnt;
	// Start is called before the first frame update
	void Start()
	{
		cnt = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (GG.pause == 1) return;
		cnt++;
		put_baku();

		if (cnt == 60)
		{
			Destroy(this.gameObject);
		}
	}


	void put_baku()
	{
		Vector3 ppp = transform.position;
		ppp.x += UnityEngine.Random.Range(-1.0f, 1.0f);
		ppp.y += UnityEngine.Random.Range(-1.0f, 1.0f);
		Instantiate(pf_baku, ppp, Quaternion.identity);


	}
}
