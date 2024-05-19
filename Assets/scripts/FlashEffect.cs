using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class FlashEffect : MonoBehaviour
{

    [SerializeField] private float flashDuration;
    [SerializeField] private Material flashMaterial;

    private SpriteRenderer spriteRenderer;
    private Material originalMat;
    private Coroutine flashroutine;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMat = spriteRenderer.material;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public virtual void Flash()
    {
        if (flashroutine != null)
        {
            StopCoroutine(flashroutine);

        }
        flashroutine = StartCoroutine(Flashroutine());
    }
    public IEnumerator Flashroutine()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material = originalMat;
        flashroutine = null;
    }
}
