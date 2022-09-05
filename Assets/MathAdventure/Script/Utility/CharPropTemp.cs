using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathAdventure.Char;

public class CharPropTemp : MonoBehaviour
{
    public CharData data;
    public Transform target;
    public float animationDuration;
    Animator animator;

    [Header("HP Component")]
    [SerializeField]
    GameObject hPPoint;
    public HPScript hPScript;
    // Start is called before the first frame update
    void Start()
    {
        InitiateHP();
        animator = GetComponent<Animator>();
    }
    void InitiateHP(){
        // Instantiate HP UI Prefabs
        GameObject go = Instantiate(Resources.Load("UI/HPBar"),GameObject.FindGameObjectWithTag("HPContainer").transform) as GameObject;
        hPScript = go.GetComponent<HPScript>();
        hPScript.maxValue = data.properties.HP;

        // set following object
        go.GetComponent<UIFollowObject>().target = hPPoint;
    }
    public void GameOver(){
        StartCoroutine(Die());
    }
    IEnumerator Die(){
        animator.SetTrigger("Die");

        // delay for animation
        yield return new WaitForSeconds(1);

        Destroy(hPScript.gameObject);

        GameController.instance.Die(gameObject);
    }
    public void Attack(){
        print(name + "Attack" + target.name);
        StartCoroutine(MoveAttack());
    }
    IEnumerator MoveAttack(){
        float t = 0;
        float duration = 1;
        Vector3 startPos = transform.position;
        Vector3 endPos = target.position;

        // Change on top
        int normalLayer = GetComponent<SpriteRenderer>().sortingOrder;
        GetComponent<SpriteRenderer>().sortingOrder = 6;

        while (t < 1)
        {
            yield return null;
            t += Time.deltaTime / duration;
            transform.parent.position = Vector3.Lerp(startPos,endPos,t);
        }
        transform.parent.position = endPos;

        animator.SetTrigger("Attack");
        target.GetComponent<CharPropTemp>().hPScript.DecreaseVal(data.properties.attackDemage);
        yield return new WaitForSeconds(1);

        // Get back to position
        t = 0;
        while (t < 1)
        {
            yield return null;
            t += Time.deltaTime / duration;
            transform.parent.position = Vector3.Lerp(endPos,startPos,t);
        }
        transform.parent.position = startPos;
        GetComponent<SpriteRenderer>().sortingOrder = normalLayer;
    }
}
