using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviourPunCallbacks
{
    public GameObject gameObject;

    public int count;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            PhotonNetwork.Instantiate(gameObject.name, new Vector2(UnityEngine.Random.Range(-18, 18), UnityEngine.Random.Range(-14, 14)), Quaternion.identity);
            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f)); ;
        }
    }
}
