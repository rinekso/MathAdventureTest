using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathAdventure.Char;
using MathAdventure.Level;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Space]
    [Header("Hero Information")]
    [SerializeField]
    Transform heroLocation;
    [SerializeField]
    CharData heroData;

    [Space]
    [Header("Enemy Information")]
    [SerializeField]
    Transform enemyLocation;

    [Space]
    [Header("Level Information")]
    [SerializeField]
    public LevelData levelData;
    public int currentLevel = 0;
    [SerializeField]
    List<GameObject> charPlaying;
    int intervalPlay = 0;
    int intervalIncrease = 2;
    Coroutine intervalCorotine;
    
    [Space]
    [Header("UI Component")]
    [SerializeField]
    TextMeshProUGUI CounterText;
    Coroutine coroutineTemp;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start() {
        PrepGame();
        StartCoroutine(InitCounter());
    }
    void PrepGame(){
        // Reset Char Playing Data;
        charPlaying = new List<GameObject>();

        // Spawn Hero
        GameObject goHero = Instantiate(heroData.prefabs, heroLocation.GetChild(1));
        goHero.GetComponent<CharPropTemp>().data = heroData;

        charPlaying.Add(goHero);

        // Spawn Enemy
        for (int i = 0; i < levelData.levels[currentLevel].enemySlot.Count; i++)
        {
            GameObject go = Instantiate(levelData.levels[currentLevel].enemySlot[i].prefabs,enemyLocation.GetChild(i));
            go.GetComponent<CharPropTemp>().data = levelData.levels[currentLevel].enemySlot[i];

            charPlaying.Add(go);
        }

    }
    IEnumerator InitCounter(){
        for (int i = 3; i > 0; i--)
        {
            CounterText.text = i+"";
            yield return new WaitForSeconds(1);
        }
        CounterText.text = "GO !!!";
        yield return new WaitForSeconds(1);
        CounterText.gameObject.SetActive(false);
        
        // Start Game
        intervalCorotine = StartCoroutine(AutoPlay());
    }

    void StartGame(){
    }
    IEnumerator AutoPlay(){
        yield return new WaitForSeconds((float)intervalIncrease/10);
        intervalPlay += intervalIncrease;

        foreach (GameObject item in charPlaying.ToArray())
        {
            if(item){
                CharPropTemp charProp = item.GetComponent<CharPropTemp>();
                CharData data = charProp.data;
                if(intervalPlay % (int) (data.properties.attackInterval*10) == 0){
                    print(intervalPlay+"/"+data.name+"/"+data.properties.attackInterval);
                    // finding opponent
                    GameObject opponent = charPlaying.Find(x => x.GetComponent<CharPropTemp>().data.side != data.side);
                    if(opponent){
                        charProp.target = opponent.transform;
                        charProp.Attack();
                        yield return new WaitForSeconds(charProp.animationDuration);
                    }else
                        Win(item);
                }
            }
        }
        yield return AutoPlay();
    }
    void Win(GameObject target){
        StopCoroutine(intervalCorotine);
        CounterText.gameObject.SetActive(true);
        switch(target.GetComponent<CharPropTemp>().data.side){
            case Side.HERO :
                CounterText.text = "Hero Win!!!";
            break;
            case Side.ENEMY :
                CounterText.text = "Hero Lost";
                CounterText.GetComponent<Animator>().enabled = false;
            break;
        }
        
    }
    public void Die(GameObject target){
        charPlaying.Remove(
            charPlaying.Find(x => x == target)
        );
        Destroy(target);
    }
}
