using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBG : MonoBehaviour
{
    private Material backgroundMaterial;
    
    private Vector2 movement;
    public Vector2 speed;
    
    private void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        movement += speed * Time.deltaTime;
        backgroundMaterial.mainTextureOffset = movement;
    }
}
