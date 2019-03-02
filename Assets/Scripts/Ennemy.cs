using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public enum EnnemyType
    {
        Top,
        Bottom,
        RightOrLeft
    }

    public EnnemyType ennemyType;
    public bool isLeftLane = true;
    public float moveSpeed = 10;
    public float coefDeadVelocity = 1;
    public float coefDeadRotationVelocity = 1;
    public Vector3 spawnPosition;
    public float bloodAmount = 1;
    public float hitDamages = 1;
    public float minimalVelocityToDie = 10;
    public float minX;
    public float maxX;

    private bool isDead = false;
    private Vector3 deadVelocity;
    private Vector3 deadRotationVelocity;
    private bool isInHitZone = false;

    private static float SPEED_MODIFIER = -0.1f;
    private static float SPAWN_Y = 10f;//TODO adjust for right time with music

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.transform.tag == "hitZone")
        {
            isInHitZone = true;
            Debug.Log("entered");
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if(other.transform.tag == "hitZone")
        {
            isInHitZone = false;
            OnHitPlayerTroops();
        }
    }

    public bool OnHitByPlayer(Vector3 direction){
        // Death animation (?)
        if (direction.magnitude < minimalVelocityToDie) return false;

        switch (ennemyType)
        {
            case EnnemyType.Top:
                isDead = direction.y > 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x);
                break;
            case EnnemyType.Bottom:
                isDead = direction.y < 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x);
                break;
            case EnnemyType.RightOrLeft:
                if (isLeftLane)
                {
                    isDead = direction.x < 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y);
                }
                else
                {
                    isDead = direction.x > 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y);
                }
                break;
            default:
                return false;
        }
        if (isDead)
        {
            // TODO: DEATH animation here
            Destroy(gameObject, 3);
            float angle = Vector3.Angle(new Vector3(direction.x, 0, 0), new Vector3(0, direction.y, 0));
            if((direction.y > 0 && direction.x > 0 && direction.y > direction.x) ||
                (direction.y > 0 && direction.x < 0 && -direction.x > direction.y) ||
                (direction.y < 0 && direction.x < 0 && -direction.y > -direction.x) ||
                (direction.y < 0 && direction.x > 0 && direction.x > -direction.y)){
                angle = -angle;
            }
            deadRotationVelocity = new Vector3(0, 0, angle);
            deadVelocity = new Vector3(direction.x * coefDeadVelocity, direction.y * coefDeadVelocity, 0);
        }

        return !isDead;
    }

    void OnHitPlayerTroops(){
        // Death animation, decrease player's life points
        // TODO
        // GameManager.instance.Player.RemoveLives(hitDamages);
        isDead = true;
        Destroy(gameObject, 3);
    }

    /// <summary>
    /// Return true if the ennemy is hitable (in the hitzone, not dead yet and the right lane where the hit comes from)
    /// </summary>
    /// <param name="isLeftHand">True if the hit comes from the left lane</param>
    /// <returns></returns>
    public bool IsHitable(bool isLeftHand){
        return isInHitZone && !isDead && (isLeftHand && isLeftLane);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log ("started");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime * SPEED_MODIFIER));
        }
        else
        {
            transform.Translate(deadVelocity * Time.fixedDeltaTime);
            transform.Rotate(deadRotationVelocity * Time.fixedDeltaTime);
        }
    }

    // Instantiate an ennemy (with random X coord between minX and maxX) given a GameObject Prefab of ennemy
    public static Ennemy InstantiateEnnemy(GameObject o)
    {
        Ennemy current = o.GetComponent<Ennemy>();
        GameObject instance = Instantiate(o, new Vector3(Random.Range(current.minX, current.maxX), SPAWN_Y, 0), new Quaternion(0, 0, 0, 0));
        return instance.GetComponent<Ennemy>();
    }
}
