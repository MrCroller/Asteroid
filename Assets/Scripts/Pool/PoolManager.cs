using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
   protected static Dictionary<string, LinkedList<GameObject>> poolsDictionary;
   protected static Transform deactivatedObjectsParent;

   private void Awake()
   {
      deactivatedObjectsParent = this.transform; // для деактивированных объектов
      poolsDictionary = new Dictionary<string, LinkedList<GameObject>>();
   }

   /// <summary>
   /// Функция получения объекта из пула
   /// </summary>
   /// <param name="prefab">Префаб</param>
   /// <returns></returns>
   public static GameObject GetGameObjectFromPool(GameObject prefab)
   {
      // проверка был ли объект в пуле
      if (!poolsDictionary.ContainsKey(prefab.name))
      {
         // создается хранилище новых объектов по префабу
         poolsDictionary[prefab.name] = new LinkedList<GameObject>();
      }

      GameObject result;

      // проверка есть ли объект в пуле
      if (poolsDictionary[prefab.name].Count > 0)
      {
         result = poolsDictionary[prefab.name].First.Value;
         poolsDictionary[prefab.name].RemoveFirst();
         // активация и возвращения объекта из пула
         result.SetActive(true);
         return result;
      }

      // создание нового объекта при остустсвии в пуле
      result = GameObject.Instantiate(prefab);
      result.name = prefab.name;

      return result;
   }

   #region Перегрузки метода отправки

   /// <summary>
   /// Функция получения объекта из пула в заданной позиции
   /// </summary>
   /// <param name="prefab">Объект который нужно отправить</param>
   /// <param name="startPosition">Место появления объекта</param>
   /// <returns></returns>
   public static GameObject GetGameObjectFromPool(GameObject prefab, Transform startPosition)
   {
      // проверка был ли объект в пуле
      if (!poolsDictionary.ContainsKey(prefab.name))
      {
         // создается хранилище новых объектов по префабу
         poolsDictionary[prefab.name] = new LinkedList<GameObject>();
      }

      GameObject result;

      // проверка есть ли объект в пуле и активен ли он
      if (poolsDictionary[prefab.name].Count > 0 && poolsDictionary[prefab.name].First.Value.activeSelf != true)
      {
         result = poolsDictionary[prefab.name].First.Value;
         poolsDictionary[prefab.name].RemoveFirst();
         // активация и возвращения объекта из пула
         result.transform.position = startPosition.position;
         result.transform.rotation = startPosition.rotation;
         result.SetActive(true);
         return result;
      }

      // создание нового объекта при остустсвии в пуле
      result = GameObject.Instantiate(prefab, startPosition.position, startPosition.rotation, deactivatedObjectsParent);
      result.name = prefab.name;

      return result;
   }

   /// <summary>
   /// Функция получения объекта из пула в заданной позиции
   /// </summary>
   /// <param name="prefab">Объект который нужно отправить</param>
   /// <param name="position">Позиция появленя объекта</param>
   /// <param name="rotation">Поворот объекта</param>
   /// <returns></returns>
   public static GameObject GetGameObjectFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
   {
      // проверка был ли объект в пуле 
      if (!poolsDictionary.ContainsKey(prefab.name))
      {
         // создается хранилище новых объектов по префабу
         poolsDictionary[prefab.name] = new LinkedList<GameObject>();
      }
      GameObject result;

      // проверка есть ли объект в пуле и активен ли он
      if (poolsDictionary[prefab.name].Count > 0 && poolsDictionary[prefab.name].First.Value.activeSelf != true)
      {
         result = poolsDictionary[prefab.name].First.Value;
         poolsDictionary[prefab.name].RemoveFirst();
         // активация и возвращения объекта из пула
         result.transform.position = position;
         result.transform.rotation = rotation;
         result.SetActive(true);
         return result;
      }

      // создание нового объекта при остустсвии в пуле
      result = GameObject.Instantiate(prefab, position, rotation, deactivatedObjectsParent);
      result.name = prefab.name;

      return result;
   }
   #endregion

   /// <summary>
   /// Возвращение объекта в пул
   /// </summary>
   /// <param name="target">Объект</param>
   public static void PutGameobjectToPool(GameObject target)
   {
      poolsDictionary[target.name].AddFirst(target);
      target.transform.parent = deactivatedObjectsParent;
      target.SetActive(false);
   }
}
