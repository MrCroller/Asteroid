using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
   /// <summary>
   /// Ускорение
   /// </summary>
   public float speed;
   /// <summary>
   /// Скорость поворота
   /// </summary>
   [Range(1, 10)] public float speed_rotate = 3.6f;
   private Rigidbody2D _rb;
   /// <summary>
   /// Вектор направления движения
   /// </summary>
   private Vector2 _moveVector;
   /// <summary>
   /// Нос корабля
   /// </summary>
   private Transform _nose;

   /// <summary>
   /// Префаб пули
   /// </summary>
   public GameObject bullet;
   /// <summary>
   /// Родительский объект для пуль
   /// </summary>
   public Transform bulletFather;

   private void Awake()
   {
      _rb = GetComponent<Rigidbody2D>();
      _nose = GameObject.Find("Nose").GetComponent<Transform>();

      PoolManager.Init(bulletFather);
   }

   void Update()
   {
      Controller();
   }

   /// <summary>
   /// Метод управления кораблем
   /// </summary>
   private void Controller()
   {
      _moveVector = _nose.position - transform.position;

      if (Input.GetKeyDown(KeyCode.W))
         _rb.AddForce(_moveVector * speed, ForceMode2D.Impulse);
      // Ускорение в направлении вектора

      if (Input.GetKey(KeyCode.A))
         _rb.rotation += speed_rotate / 10;
      if (Input.GetKey(KeyCode.D))
         _rb.rotation -= speed_rotate / 10;
      // Повороты

      if (Input.GetKeyDown(KeyCode.Space))
      {
         Fire();
      }

   }

   private void Fire()
   {
      // var bul = PoolManager.GetGameObjectFromPool(bullet).transform;
      var bul = GameObject.Instantiate(bullet).transform;
      bul = _nose;
      bul.position *= 8f;
   }
}
