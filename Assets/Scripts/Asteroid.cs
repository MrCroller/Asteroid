using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour
{
   public enum Size
   {
      SMALL = 1,
      MEDIUM = 4,
      BIG = 8
   }
   public Size _size = Size.BIG;

   public delegate void SomeAction();

   /// <summary>
   /// Событие разрушения маленького астероида
   /// </summary>
   public event SomeAction destroySmallAst;
   public GameObject asteroidPrefab;

   public static Asteroid singelton { get; private set; }

   /// <summary>
   /// Минимальная скорость
   /// </summary>
   [Range(0.1f, 26f)] public float minSpeed = 1f;

   /// <summary>
   /// Максимальная скорость
   /// </summary>
   [Range(0.1f, 26f)] public float maxSpeed = 2f;

   /// <summary>
   /// Скорость
   /// </summary>
   [SerializeField] private float speed;

   /// <summary>
   /// Делитель размера астероида
   /// </summary>
   [Tooltip("Делитель размера астероида. Чем больше => тем меньше астероид в размерах")] public float attech = 10f;
   private Rigidbody2D _rb;

   /// <summary>
   /// Спрайты астероидов
   /// </summary>
   public Sprite[] sprites;
   private SpriteRenderer _spriteRender;

   private void Awake()
   {
      singelton = this;

      _rb = GetComponent<Rigidbody2D>();
      _spriteRender = GetComponent<SpriteRenderer>();
   }

   private void Start()
   {
      // Выбор случайного спрайта
      _spriteRender.sprite = sprites[Random.Range(0, sprites.Length)];

      // Выбор цвета в градациях серого
      float color_value = Random.Range(0.1f, 0.9f);
      _spriteRender.color = new Color(color_value, color_value, color_value);

      this.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360);
      // Настрйка видимого размера астероида
      this.transform.localScale = Vector3.one * (int)_size / attech;

      _rb.mass = (int)_size;
   }

   /// <summary>
   /// Траектория падения
   /// </summary>
   /// <param name="direction"></param>
   public void SetTrajectory(Vector2 direction)
   {
      if (minSpeed > maxSpeed) Debug.LogError("Минимальное значение скорости больше максимального");

      speed = Random.Range(minSpeed, maxSpeed);
      _rb.AddForce(direction * speed);
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.tag == "Bullet")
      {
         if (_size != Size.SMALL)
         {
            CreateSplit();
            CreateSplit();
         }
         PoolManager.PutGameobjectToPool(other.gameObject);
         PoolManager.PutGameobjectToPool(this.gameObject);
         Debug.Log("Exploution!");
      }
   }

   /// <summary>
   /// Уничтожение астероида
   /// </summary>
   private void CreateSplit()
   {
      // Нпбольшое смещение при появлении
      Vector2 position = this.transform.position;
      position += Random.insideUnitCircle * 0.5f;

      Vector3 newDirection = this.transform.position;
      Quaternion rndRot = Quaternion.AngleAxis(Random.Range(-45f, 45f), Vector3.forward);
      //FIXME Исправить поворот в сторону трeктории падения

      Asteroid ast = PoolManager.GetGameObjectFromPool(asteroidPrefab, position, rndRot).GetComponent<Asteroid>();
      // TODO Установить адекватное разделение размера астероида согласно его категории
      ast._size = SizeDecrease();
      // ast.speed = this.speed * 2;
      Debug.Log($"Create! ast size = {ast._size.ToString()}");
      ast.SetTrajectory((rndRot * -newDirection) * this.speed);
   }

   private Size SizeDecrease()
   {
      if (_size == Size.BIG)
         return Size.MEDIUM;
      else
         return Size.SMALL;
   }
}
