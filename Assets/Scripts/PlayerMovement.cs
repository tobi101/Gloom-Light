using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public SpriteMask spriteMaskObject;

    public float speed = 10f;

    PhotonView view;

    private Rigidbody2D rigidbody2D;

    public int size;

    public TMP_Text foodCounterText;

    public float interval = 1000f;
    private float timer = 0f;

    public float minX, minY, maxX, maxY;

    public GameObject player;

    void Start()
    {
        view = GetComponent<PhotonView>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        if (view.Owner.IsLocal)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Camera.main.GetComponent<CameraFollow>().player = gameObject.transform;
        }

        size = 1;
        foodCounterText.text = size.ToString();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * speed;
        float moveVertical = Input.GetAxis("Vertical") * speed;

        if (view.IsMine)
        {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            rigidbody2D.MovePosition(rigidbody2D.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            size++;
            foodCounterText.text = size.ToString();

            spriteMaskObject.transform.localScale += new Vector3(1, 1, 0);

            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gamer")
        {
            PlayerMovement otherPlayer = collision.gameObject.GetComponent<PlayerMovement>();

            if (size > otherPlayer.size && otherPlayer.size > 0)
            {
                if (timer >= interval)
                {
                    otherPlayer.size--;
                    otherPlayer.foodCounterText.text = otherPlayer.size.ToString();
                    collision.gameObject.GetComponentInChildren<SpriteMask>().transform.localScale -= new Vector3(1, 1, 0);
                }
                else
                {
                    timer += Time.fixedDeltaTime;
                }
            }
            else if (size < otherPlayer.size && size > 0)
            {
                if (timer >= interval)
                {
                    size--;
                    foodCounterText.text = size.ToString();
                    gameObject.GetComponentInChildren<SpriteMask>().transform.localScale -= new Vector3(1, 1, 0);
                }
                else
                {
                    timer += Time.fixedDeltaTime;
                }
            }

            if (size == 0)
            {
                Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                gameObject.transform.localPosition = randomPosition;
                gameObject.GetComponentInChildren<SpriteMask>().transform.localScale = new Vector3(3, 3, 1);
                size = 1;
                foodCounterText.text = size.ToString();

            }
            else if (otherPlayer.size == 0)
            {
                Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                otherPlayer.gameObject.transform.localPosition = randomPosition;
                otherPlayer.gameObject.GetComponentInChildren<SpriteMask>().transform.localScale = new Vector3(3, 3, 1);
                otherPlayer.size = 1;
                otherPlayer.foodCounterText.text = otherPlayer.size.ToString();
            }
        }
    }
}
