using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
   private enum Orient
   {
      Left_toRight,
      Up_toDown
   }

   [SerializeField] private Orient pos;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.tag != "Bullet")
      {
         other.transform.position = GetPos(other);
      }
      else
      {
         // Debug.Log("Пуля на вылет");
         PoolManager.PutGameobjectToPool(other.gameObject);
      }
   }

   private Vector2 GetPos(Collider2D obj)
   {
      if (this.pos == Orient.Left_toRight)
      {
         return new Vector2(-obj.transform.position.x, obj.transform.position.y);
      }
      else
      {
         return new Vector2(obj.transform.position.x, -obj.transform.position.y);
      }
   }
}
