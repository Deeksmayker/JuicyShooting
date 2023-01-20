using System;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class HitPopup : MonoBehaviour
{
    [SerializeField] private float lifetime;
    
    private MeshRenderer _renderer;
    //private MeshRenderer _secondSideRenderer;
    private MaterialPropertyBlock _propertyBlock;

    public static ObjectPool<HitPopup> PopupPool;

    public static void SetupPool(HitPopup prefab)
    {
        PopupPool = new ObjectPool<HitPopup>(
            () => Instantiate(prefab),
            (popup) => popup.gameObject.SetActive(true),
            (popup) => popup.gameObject.SetActive(false),
            (popup) => Destroy(popup),
            true, 20, 100);
    }
    
    private void Awake()
    {
        _propertyBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<MeshRenderer>();
        //_secondSideRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnEnable()
    {
        transform.LookAt(Camera.main.transform, -Vector3.up);
        transform.Rotate(new Vector3(0, 180, 0));
        Invoke(nameof(Disable), lifetime);
    }

    public void SetupPopup(Color color)
    {
        _propertyBlock.SetColor("_Color", color);
        _renderer.SetPropertyBlock(_propertyBlock);
        //_secondSideRenderer.SetPropertyBlock(_propertyBlock);
    }

    private void Disable()
    {
        PopupPool.Release(this);
    }
}
