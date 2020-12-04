using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TitleMoji : MonoBehaviour
{


    [SerializeField]
    int mode;

    Text text;
    private int cnt;

	private void Awake()
	{
        text = this.GetComponent<Text>();
        cnt = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        Sequence seq = DOTween.Sequence();

        float py = 10.5f;
        float tim = 1.0f;
        switch(mode){
            case 0: py = 10.5f;tim = 1.0f; break;
            case 1: py = -11.5f;tim = 1.1f; break;
            case 2: py = 12.5f; tim = 1.2f; break;
            case 3: py = -13.5f; tim = 1.3f; break;
            case 4: py = 14.5f; tim = 1.4f; break;
            case 5: py = -15.5f; tim = 1.5f; break;

        }


        seq.Append(
            transform.DOLocalMove(new Vector3(0.0f, py), tim).SetRelative()
        );
        seq.Append(
        transform.DOLocalMove(new Vector3(0.0f, -py), tim).SetRelative()
        );

        seq.SetLoops(-1);
        seq.Play();


    }

    // Update is called once per frame
    void Update()
    {
        cnt--;
        if (cnt <= 0)
        {
            cnt = (60 + mode);
            mojicolorchange();
        }

    }


    void mojicolorchange(){
        float randr = UnityEngine.Random.Range(0.0f, 1.0f);
        float randg = UnityEngine.Random.Range(0.0f, 1.0f);
        float randb = UnityEngine.Random.Range(0.0f, 1.0f);
        Color col = new Color(randr, randg, randb, 1.0f);
        text.color = col;
    }


}
