using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
   private static Dictionary<string, LinkedList<GameObject>> poolsDictionary;
   private static Transform deactivatedObjectsParent;

/// <summary>
/// Инициализация
/// </summary>
/// <param name="pooledObjectsContainer">Родительский объект</param>
   public static void Init(Transform pooledObjectsContainer) 
   {
      deactivatedObjectsParent = pooledObjectsContainer; // для деактивированных объектов
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

/// <summary>
/// Возвращение объекта в пул
/// </summary>
/// <param name="target">Объект</param>
   public static void putGameobjectToPool(GameObject target){
      poolsDictionary[target.name].AddFirst(target);
      target.transform.parent = deactivatedObjectsParent;
      target.SetActive(false);
   }
}
