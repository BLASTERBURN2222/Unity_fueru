using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mojimoji : MonoBehaviour
{

	public Sprite[] sp_bg;

	private SpriteRenderer sprenderer;
	private int mojinum = 0;

	private void Awake()
	{
		sprenderer = GetComponent<SpriteRenderer>();
	}

	public void setmoji(int nm) {
		mojinum = nm;
	}

	void spset(){ 
		sprenderer.sprite = sp_bg[mojinum];
	}

	// Start is called before the first frame update
	void Start()
    {
		spset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
