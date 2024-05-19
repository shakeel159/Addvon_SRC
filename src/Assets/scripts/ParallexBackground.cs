using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexBackground : MonoBehaviour
{
    //https://www.youtube.com/watch?v=wBol2xzxCOU

    [SerializeField] private Vector2 parralaxEffectMutliplier;

    private Transform cameraTransform;
    private Vector3 lastCameraPos;
    private float textureUnitSizeX;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPos = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPos;
        transform.position += new Vector3(deltaMovement.x * parralaxEffectMutliplier.x, deltaMovement.y * parralaxEffectMutliplier.y);
        lastCameraPos = cameraTransform.position;

        if(Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetposX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetposX, transform.position.y);
        
        }
    }
}
