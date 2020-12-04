using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwaAnime : MonoBehaviour
{

    public Sprite spr_1;
    public Sprite spr_2;
    private SpriteRenderer sprenderer;

    private int anime_cnt;
    private int pat;

	private void Awake()
	{
        sprenderer = this.gameObject.GetComponent<SpriteRenderer>();
        anime_cnt = 30;
        pat = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anime_cnt--;
        if(anime_cnt <= 0){
            anime_cnt = 8;
            pat++;
            pat &= 1;

            if (pat == 0) SetSprite(spr_1);
            if (pat == 1) SetSprite(spr_2);


        }
    }

    private void SetSprite(Sprite spr)
    {
        sprenderer.sprite = spr;
    }

}
