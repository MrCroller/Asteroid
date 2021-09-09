using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   /// <summary>
   /// Скорость пули
   /// </summary>
   public float speed;
   private Rigidbody2D _rb;
   private bool flag = true;

   private void Awake()
   {
      _rb = GetComponent<Rigidbody2D>();
   }

   public void StartForse(Vector2 dir)
   {
      _rb.AddForce(dir * this.speed, ForceMode2D.Impulse);
   }
}
