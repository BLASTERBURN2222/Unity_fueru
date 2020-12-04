using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Kakushuku : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		transform.DOScale(new Vector3(0.5f, 0.5f,0.0f), 0.5f).SetLoops(-1);

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
