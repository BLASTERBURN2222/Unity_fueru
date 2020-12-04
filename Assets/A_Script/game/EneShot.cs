using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneShot : MonoBehaviour
{

	private float vxf = 0.0f;
	private float vyf = 0.0f;
	private int lifetime;

	private void Awake()
	{
		lifetime = 300;
	}



	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (GG.pause == 1) return;
		Vector3 pos = transform.position;
		pos.x += vxf;
		pos.y += vyf;

		transform.position = pos;

		lifetime--;
		if (lifetime <= 0)
		{
			Destroy(this.gameObject);
		}

	}

	public void set_kakudo(float direction, float speed)
	{
		if ((direction == 999)|| (direction == 998)|| (direction == 997))
		{
			//自機狙いになる
			Vector3 pos = transform.position;
			float dir = getzikinerai((int)direction,pos.x, pos.y);
			SetVelocity(dir, speed);
		}
		else
		{
			vxf = Util.CosEx(direction) * speed;
			vyf = Util.SinEx(direction) * speed;
		}
	}


	public void SetVelocity(float direction, float speed)
	{
		vxf = Util.CosEx(direction) * speed;
		vyf = Util.SinEx(direction) * speed;
	}


	private float getzikinerai(int mode,float x, float y)
	{

		Vector3 mypos = transform.position;
		float distance;
		Vector3 zikipos;
		float min_distance = 0;
		float max_distance = 0;
		GameObject neraiobj = null;
		
		GameObject[] takaras = GameObject.FindGameObjectsWithTag("TAG_ZIKI");
		if (takaras.Length != 0)
		{

			if (mode == 997)
			{
				//ランダムにする
				int rn = UnityEngine.Random.Range(0, takaras.Length);

				int i = 0;
				foreach (GameObject takara in takaras)
				{
					if (i == rn)
					{
						neraiobj = takara;
						break;
					}
					i++;
				}

			}
			else
			{


				foreach (GameObject takara in takaras)
				{
					zikipos = takara.transform.position;

					distance = (mypos - zikipos).magnitude;

					if (mode == 999)
					{//一番近い自機
						if (min_distance == 0)
						{
							min_distance = distance;
							neraiobj = takara;
						}
						else
						{
							if (min_distance > distance)
							{
								min_distance = distance;
								neraiobj = takara;
							}
						}
					}


					if (mode == 998)
					{//一番遠い自機
						if (max_distance == 0)
						{
							max_distance = distance;
							neraiobj = takara;
						}
						else
						{
							if (max_distance < distance)
							{
								max_distance = distance;
								neraiobj = takara;
							}
						}
					}

				}
			}
		}



		float ziki_x = 0.0f;
		float ziki_y = 0.0f;

		if(neraiobj != null){
			Vector3 ppp = neraiobj.transform.position;
			ziki_x = ppp.x;
			ziki_y = ppp.y;
		}

		float dx = ziki_x - x;
		float dy = ziki_y - y;
		return Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
	}

}

