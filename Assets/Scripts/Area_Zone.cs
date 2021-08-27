using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_Zone : MonoBehaviour
{
   private void OnTriggerExit2D(Collider2D other)
   {
      if (!other.CompareTag("Bullet"))
      {
         Debug.Log("ExitRegion");
         other.transform.position = new Vector2(-other.transform.position.x, -other.transform.position.y);
      }
   }
}
