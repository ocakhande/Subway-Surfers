using DG.Tweening;
using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    //public Transform Road;
    // public Transform Road_2;
    Animator playerAnimator;
    Rigidbody rb;
    float velocity = 2f;
    private LaneStatus currentLane;
    private bool isJumping = false;
    private bool isRolling = false;
    [SerializeField] GameOver gameOverObject;
    [SerializeField] GameObject coin;
    private bool gameOver = false;
    private Vector2 touchPosition1;
    private Vector2 touchPosition2;
    private Vector2 difference;
    public float transitionDuration = 0.5f; // Geçiþ süresi

    private Vector3 initialPosition; // Baþlangýç pozisyonu
    private Vector3 targetPosition; // Hedef pozisyonu
    private float transitionTimer = 0f; // Geçiþ süresi sayacý
    float moveDuration = 0.1f;
    public bool wallCollision = false;

    //public float acceleration = 100f;

    // Start is called before the first frame update

    public enum LaneStatus
    {
        Left,
        Right,
        Center,
    }

    public enum Direction
    {
        ToLeft,
        ToRight,
    }


    void Start()
    {

        initialPosition = transform.position;
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        playerAnimator.SetFloat("Running", velocity);
        //Vector3 go_forward = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * velocity);
        //transform.position = go_forward;

    }

    void Update()
    {
        velocity += Time.deltaTime;
        //Debug.Log(velocity.ToString());
        Vector3 go_forward = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * velocity / 2);
        transform.position = go_forward;

        if (Input.GetMouseButtonDown(0))
        {
            touchPosition1 = Input.mousePosition;

        }
        if (Input.GetMouseButtonUp(0))
        {
            touchPosition2 = Input.mousePosition;
            difference = touchPosition2 - touchPosition1;

            var isDirectionX = Mathf.Abs(difference.x) > Mathf.Abs(difference.y) ? true : false;

            if (isDirectionX)
            {
                if (difference.x > 0)
                {
                    MoveinLane(Direction.ToRight);
                }
                else
                {
                    MoveinLane(Direction.ToLeft);

                }
            }
            else
            {
                if (difference.y > 0 && !isJumping)
                {
                    playerAnimator.SetBool("Jumping", true);
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    isJumping = true;
                }
                else
                {
                    playerAnimator.SetBool("Rolling", true);
                    isRolling = true;
              
                }
            }
        }






        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);


        //    if (touch.deltaPosition.x < -20)
        //    {
        //        MoveinLane(Direction.ToLeft);
        //    }
        //    else if (touch.deltaPosition.x > 20)
        //    {
        //        MoveinLane(Direction.ToRight);
        //    }
        //    if (touch.deltaPosition.y > 25 && !isJumping)
        //    {
        //        playerAnimator.SetBool("Jumping", true);
        //        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //        isJumping = true;

        //    }
        //    if (touch.deltaPosition.y < -25)
        //    {
        //        playerAnimator.SetBool("Rolling", true);
        //        isRolling = true;
        //    }

        //}

        if (gameOver == true)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<PlayerController>().enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Wall")
        //{
        //    Road.position = new Vector3(Road_2.position.x, Road_2.position.y, Road_2.position.z + 30f);
        //}
        //if (other.gameObject.tag == "Wall2")
        //{
        //    Road_2.position = new Vector3(Road.position.x, Road.position.y, Road.position.z + 30f);
        //}
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            playerAnimator.SetBool("Jumping", false);
            playerAnimator.SetBool("Rolling", false);

            Debug.Log("TEst");
        }
        if (other.gameObject.CompareTag("FivePoint"))
        {
            SocreText.coinCounter += 5;
            Destroy(other.gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("TEst1");

            gameOverObject.ShowGameOver();
            gameOver = true;
        }
        else if (collision.gameObject.CompareTag("RollingObstacle"))
        {
            if (!isRolling)
            {
                gameOverObject.ShowGameOver();
                gameOver = true;

            }
        }
        else if (collision.gameObject.CompareTag("LowObstacle"))
        {
            if (!isJumping)
            {
                gameOverObject.ShowGameOver();
                gameOver = true;
            }
        }
    }



    void ChangeRoad(LaneStatus destinationLane)
    {
        currentLane = destinationLane;

        // TODO: player pozisyonunu setle

        if (currentLane == LaneStatus.Center)
        {
            gameObject.transform.DOMoveX(0f, moveDuration)
                                .SetEase(Ease.Linear);

            //Vector3 center = new Vector3(0f, transform.position.y, transform.position.z + Time.deltaTime * velocity);
            //transform.position = center;
        }
        else if (currentLane == LaneStatus.Right)
        {
            gameObject.transform.DOMoveX(2.7f, moveDuration)
                                .SetEase(Ease.Linear);


            //Vector3 right = new Vector3(2.7f, transform.position.y, transform.position.z + Time.deltaTime * velocity);
            //transform.position = right;
        }
        else if (currentLane == LaneStatus.Left)
        {
            gameObject.transform.DOMoveX(-2.7f, moveDuration)
                                .SetEase(Ease.Linear);
            //Vector3 left = new Vector3(-2.7f, transform.position.y, transform.position.z + Time.deltaTime * velocity);
            //transform.position = left;
        }


    }

    void MoveinLane(Direction direction)
    {


        if (direction == Direction.ToLeft)
        {
            if (currentLane == LaneStatus.Center)
            {
                ChangeRoad(LaneStatus.Left);
            }
            else if (currentLane == LaneStatus.Right)
            {

                ChangeRoad(LaneStatus.Center);
                //Wait();
            }
        }
        if (direction == Direction.ToRight)
        {
            if (currentLane == LaneStatus.Center)
            {
                ChangeRoad(LaneStatus.Right);
            }
            else if (currentLane == LaneStatus.Left)
            {
                ChangeRoad(LaneStatus.Center);
                //Wait();
            }
        }



    }


    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }

}
