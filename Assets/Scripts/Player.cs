using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player instance;

    public Sprite normalSprite;
    public Sprite immuneSprite;

    public bool isAlive;

    public GameObject leftWall;
    public GameObject rightWall;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public bool canThrow;
    [SerializeField]
    private float jumpSpeed;

    public GameObject magnetBonusObject;

    public GameObject rocketEffect;
    public GameObject coinEffect;
    public GameObject bonusEffect;
    public GameObject debuffEffect;
    public GameObject jetEffect;
    //Buff Statuses
    public bool canBecomeForce;

    public bool canBeSmaller;
    public bool canBeBigger;
    public bool canJetBonus;
    public bool canImmune;
    public bool canLighter;
    public bool canHeavier;
    public bool canMagnet;
    public bool immune;
    public bool moveable;

    public int heightScore;
    public int collectedCoins;

    void Start () {
        if(instance == null)
        {
            instance = this;
        }
        
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        isAlive = true;
        canThrow = true;
        canBeBigger = true;
        canBeSmaller = true;
        canJetBonus = true;
        canBecomeForce = true;
        canMagnet = true;
        moveable = true;
        canImmune = true;
        canLighter = true;
        canHeavier = true;
        heightScore = 0;
        collectedCoins = 0;
    }

    private void OnMouseDown()
    {
        if (canThrow)
        {
            StartPlayer();
            FindObjectOfType<AudioManager>().Play("Throw");
            GameManager.instance.rocketButton.SetActive(false);
            GameManager.instance.tapStartImage.SetActive(false);
            GameManager.instance.turnScreenImage.SetActive(false);
        }
    }

    void Update () {

        HeightScore();

        if (!canThrow && moveable)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(rb.velocity.x, 200));
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddForce(new Vector2(-20, 0));
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce(new Vector2(20, 0));
            }

            //transform.Translate(Input.acceleration.x, 0, 0);

            float dir = Input.acceleration.x;
            transform.Translate(dir * 15 * Time.deltaTime, 0, 0);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Point")
        {
            StartCoroutine(CoinEffect(collision.transform));
            Destroy(collision.gameObject);
            GiveForce();
            collectedCoins++;
            FindObjectOfType<AudioManager>().Play("CollectCoin");
        }
        else if(collision.tag == "Line")
        {
            LevelGenerator.instance.moveLines();
        }
        else if(collision.tag == "LeftWall")
        {
            transform.position = new Vector3(3.25f, transform.position.y, 0);
        }
        else if(collision.tag == "RightWall")
        {
            transform.position = new Vector3(-3.25f, transform.position.y, 0);
        }
        /////////////////////////Buffing List////////////////////////////
        //Making Player Bigger Buff
        else if(collision.tag == "BiggerBonus" && canBeBigger)
        {
            StartCoroutine(BonusEffect(collision.transform));
            StartCoroutine(BiggerBuff());
            GameManager.instance.ShowBonusText("Bigger");
            Destroy(collision.gameObject);
            GiveForce();
            FindObjectOfType<AudioManager>().Play("CollectBuff");
        }
        //Give player a constant speed for a specific time
        else if(collision.tag == "JetBonus" && canJetBonus)
        {
            StartCoroutine(BonusEffect(collision.transform));
            StartCoroutine(JetBuff());
            GameManager.instance.ShowBonusText("Jet");
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().Play("CollectBuff");
        }
        //Gives the Immune to debuffs and makes him immune
        else if(collision.tag == "ImmuneBonus" && canImmune)
        {
            StartCoroutine(BonusEffect(collision.transform));
            StartCoroutine(ImmuneBuff());
            GameManager.instance.ShowBonusText("Immune");
            Destroy(collision.gameObject);
            GiveForce();
            FindObjectOfType<AudioManager>().Play("CollectBuff");
        }
        //Makes Player Stop like a Gum
        else if (collision.tag == "GumBonus")
        {
            StartCoroutine(BonusEffect(collision.transform));
            GameManager.instance.ShowBonusText("Gum");
            GumBonus();
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().Play("CollectBuff");
        }
        //Makes less Gravity
        else if(collision.tag == "GravityBonus" && canLighter)
        {
            StartCoroutine(BonusEffect(collision.transform));
            StartCoroutine(GravityBonus());
            GameManager.instance.ShowBonusText("Lighter");
            GiveForce();
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().Play("CollectBuff");

        }
        //Gives + fev Points
        else if(collision.tag == "CoinBonus")
        {
            StartCoroutine(BonusEffect(collision.transform));
            Destroy(collision.gameObject);
            GameManager.instance.ShowBonusText("Extra Coins");
            GiveForce();
            collectedCoins += PlayerPrefs.GetInt("CoinBonus", 1)*10;
            FindObjectOfType<AudioManager>().Play("CollectBuff");
        }

        else if(collision.tag == "MagnetBonus" && canMagnet)
        {
            StartCoroutine(MagnetBonus());
            StartCoroutine(BonusEffect(collision.transform));
            GameManager.instance.ShowBonusText("Magnet");
            GiveForce();
            FindObjectOfType<AudioManager>().Play("CollectBuff");
            Destroy(collision.gameObject);
        }

        ////////////// Debuffing List ////////////////
        // Gravity Debuff

        else if(collision.tag == "GravityDebuff" && !immune)
        {
            StartCoroutine(DebuffEffect(collision.transform));
            StartCoroutine(GravityDebuff());
            GameManager.instance.ShowBonusText("Heavier");
            GiveForce();
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().Play("CollectDebuff");
        }

        else if(collision.tag == "SmallerDebuff" && canBeSmaller && !immune)
        {
            StartCoroutine(DebuffEffect(collision.transform));
            StartCoroutine(SmallerDebuff());
            GameManager.instance.ShowBonusText("Smaller");
            Destroy(collision.gameObject);
            GiveForce();
            FindObjectOfType<AudioManager>().Play("CollectDebuff");
        }
    }
    
    IEnumerator SmallerDebuff()
    {
        canBeSmaller = false;
        transform.localScale = new Vector3(.5f,.5f,.5f);
        yield return new WaitForSeconds(5);
        transform.localScale = new Vector3(1, 1, 1);
        canBeSmaller = true;
    }

    IEnumerator GravityDebuff()
    {
        rb.gravityScale *= 2;
        yield return new WaitForSeconds(4);
        rb.gravityScale /= 2;
    }

    IEnumerator BiggerBuff()
    {

        canBeBigger = false;
        transform.localScale = new Vector3(2, 2, 2);
        Vector3 lw = leftWall.transform.position;
        lw.x -= 1;
        leftWall.transform.position = lw;
        Vector3 rw = rightWall.transform.position;
        rw.x += 1;
        rightWall.transform.position = rw;
        yield return new WaitForSeconds(PlayerPrefs.GetInt("BiggerBonus" ,1)*3+3);
        transform.localScale = new Vector3(1, 1, 1);
        canBeBigger = true;
        lw = leftWall.transform.position;
        lw.x += 1;
        leftWall.transform.position = lw;
        rw = rightWall.transform.position;
        rw.x -= 1;
        rightWall.transform.position = rw;
    }

    IEnumerator JetBuff()
    {
        canBecomeForce = false;
        canImmune = false;
        moveable = false;
        canJetBonus = false;
        rb.isKinematic = true;
        rb.velocity = new Vector3(0, 12, 0);
        immune = true;
        GameObject effect = Instantiate(jetEffect, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.Euler(90,0,0));
        effect.transform.parent = gameObject.transform;

        yield return new WaitForSeconds(PlayerPrefs.GetInt("JetBonus" ,1)*2+1);
        canImmune = true;
        Destroy(effect);
        moveable = true;
        canJetBonus = true;
        immune = false;

        rb.isKinematic = false;
        canBecomeForce = true;
    }

    IEnumerator ImmuneBuff()
    {
        immune = true;
        canImmune = false;
        sr.sprite = immuneSprite;
        yield return new WaitForSeconds(PlayerPrefs.GetInt("ImmuneBonus", 1)*3+2);
        canImmune = true;
        immune = false;
        sr.sprite = normalSprite;
    }

    IEnumerator GravityBonus()
    {
        rb.gravityScale /= 2;
        canLighter = false;
        yield return new WaitForSeconds(PlayerPrefs.GetInt("GravityBonus", 1)*3+1);
        canLighter = true;
        rb.gravityScale *= 2;
    }

    void GumBonus()
    {
        StopPlayer();
    }

    IEnumerator MagnetBonus()
    {
        magnetBonusObject.SetActive(true);
        canMagnet = false;
        yield return new WaitForSeconds(PlayerPrefs.GetInt("MagnetBonus", 1) * 3);
        canMagnet = true;
        magnetBonusObject.SetActive(false);
    }

    IEnumerator CoinEffect(Transform coin)
    {
        GameObject effect = Instantiate(coinEffect, coin.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(effect);

    }

    IEnumerator BonusEffect(Transform bonus){
        GameObject effect = Instantiate(bonusEffect, bonus.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(effect);
    }

    IEnumerator DebuffEffect(Transform bonus)
    {
        GameObject effect = Instantiate(debuffEffect, bonus.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(effect);
    }

    public void StartRocket()
    {
        FindObjectOfType<AudioManager>().Play("Rocket");
        StartCoroutine(BecomeRocketBonus());
    }

    IEnumerator BecomeRocketBonus()
    {
        canThrow = false;
        canJetBonus = false;
        canBecomeForce = false;
        canImmune = false;
        moveable = false;
        rb.isKinematic = true;
        rb.velocity = new Vector3(0, 15, 0);
        immune = true;
        GameObject effect = Instantiate(rocketEffect, new Vector3(transform.position.x, transform.position.y -.5f, -1), Quaternion.Euler(90, 0, 0));
        effect.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(10);
        Destroy(effect);
        canJetBonus = true;
        canImmune = false;
        moveable = true;
        immune = false;
        rb.isKinematic = false;
        canBecomeForce = true;
    }

    void StopPlayer()
    {
        rb.isKinematic = true;
        canThrow = true;
        rb.velocity = Vector3.zero;
    }

    void StartPlayer()
    {
        rb.isKinematic = false;
        canThrow = false;
        rb.AddForce(new Vector2(rb.velocity.x, 750));
    }

    public void GiveForce()
    {
        if (rb.velocity.y < 8 && canBecomeForce)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(transform.up * jumpSpeed * Time.deltaTime * 100);
        }
    }

    public void HeightScore()
    {
        if (transform.position.y > heightScore)
        {
            heightScore = (int)(transform.position.y);
            GameManager.instance.IncreaseScore(heightScore);
        }

    }
}
