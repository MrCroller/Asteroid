using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_Zone : MonoBehaviour
{
   private void OnTriggerExit2D(Collider2D other)
   {
      if (!other.CompareTag("Bullet"))
      {
         Debug.Log($"ExitRegion | Object name: [{other.name}]");
         other.transform.position = new Vector2(-other.transform.position.x, -other.transform.position.y);
         // other.transform.position = new Vector2(GetPosition(other.transform.position.x), GetPosition(other.transform.position.y));
         // TODO Улучшить появление с противолположной стороны
      }
      else
      {
         Debug.Log("Пуля на вылет");
         PoolManager.PutGameobjectToPool(other.gameObject);
      }
   }

   /// <summary>
   /// Определяет границы зоны и возвращает соответствующие значение выходу объекта
   /// </summary>
   /// <param name="posObj">Позиция объекта</param>
   /// <returns></returns>
   private float GetPosition(float posObj)
   {
      if (posObj < -this.transform.localScale.x / 2)
      {
         return this.transform.localScale.x / 2;
      }
      else if (posObj > this.transform.localScale.x / 2)
      {
         return -this.transform.localScale.x / 2;
      }
      else
         return posObj;
   }
}
