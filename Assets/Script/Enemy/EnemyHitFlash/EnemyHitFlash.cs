using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitFlash : MonoBehaviour
{
   [Header("Refrence")]
   private SpriteRenderer[] _spriterender;
   private Material[] _materials;

   private Color flashcolor = Color.white;
   private float flashtime = 0.2f;
   
   private Coroutine _coroutine;

    private void Awake()
    {  
        _spriterender = GetComponentsInChildren<SpriteRenderer>();
        Init();
    }
    //method dùng để nhận số mảng child of spriterender  và duyệt từng phần material nhận tương đương với màu sprite render
    private void Init() 
    {
        // Những phần có sprirerender ( luôn có material) sẽ được tạo mới và tùy vào mỗi phần 
        // nó sẽ  nhận mỗi lengh khác nhau tùy vào các child  
        _materials = new Material[_spriterender.Length];

        //  Giao  phan sprire rendering material cho _materials   ?
        for (int i = 0; i < _spriterender.Length; i++) 
        {
           _materials[i] = _spriterender[i].material;
        }
    }
    public void CallDamagesFlash() 
    {
        _coroutine = StartCoroutine(Damegeflash()); 
    }

    private IEnumerator Damegeflash() 
    {
        // Set the color
        SetFlashColor();
        // Thuat Toan de flash tu 1 xuong 0 trong 0,2s
        float currentFlashTime = 0f;
        float elapsedTime = 0f;
        while(elapsedTime < flashtime) 
        {
          // Lap Time troi qua   ???
          elapsedTime += Time.deltaTime;

            // Tạo nội suy giảm dần từ độ sáng 1f xuống 0f trong thoi gian elaptime/flashtime(0,2)
            currentFlashTime = Mathf.Lerp(1f, 0f, elapsedTime/flashtime);
            SetflashAmount(currentFlashTime);
          yield return null;
        }
    } 
   private void SetFlashColor() 
    {
        // Tạo Color flash cho materials liên kết từ shader graps
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetColor("_Color_Flash", flashcolor);
        }
    }
   private void SetflashAmount(float amount) 
    {
        // Tạo  Amount flash cho materials liên kết từ shader graps
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetFloat("_FlashAmount", amount);
        }
    }
}
