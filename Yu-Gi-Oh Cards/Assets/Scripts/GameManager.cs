using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject StartPanel;

    public float _myHealth;
    public float _enemyHealth;
    public Image _myHealthBar;
    public Image _enemyHealthBar;
    public Button _endTurnButton;
    public GameObject _yourTurnImage;
    public GameObject _AddMyCard;
    public GameObject _AddEnemyCard;
    public GameObject _victory;
    public GameObject _defeat;

    [Header("Slots ")]
    public GameObject _Slot1Card;
    public GameObject _Slot2Card;
    public GameObject _Slot3Card;
    public GameObject _Slot4Card;
    public GameObject _Slot5Card;
    public GameObject _Slot6Card;

    [Header("Game Tools ")]
    public GameObject[] _slots;
    public GameObject[] _cardsMonster;
    public GameObject[] _cardsEnemyMonster;
    private GameObject _myDeck;
    private GameObject _enemyDeck;
    private GameObject _enemyDeckParent;

    public float _pos;
    public float _Enemypos;

    public ParticleSystem ps;
    public ParticleSystem[] _smokePs;

    void Start()
    {
        instance = this;
        _myDeck= GameObject.Find("CanvasMyCard");
        _enemyDeck= GameObject.Find("CanvasEnemy");
        _enemyDeckParent= GameObject.Find("CanvasEnemyParent");
    }

    void Update()
    {
        #region my health and enemy health
        _myHealthBar.fillAmount = Mathf.Lerp(_myHealthBar.fillAmount, _myHealth, 0.01f);
        _enemyHealthBar.fillAmount = Mathf.Lerp(_enemyHealthBar.fillAmount, _enemyHealth, 0.01f);

        if (_enemyHealthBar.fillAmount <= 0) _victory.SetActive(true);
        if (_myHealthBar.fillAmount <= 0) _defeat.SetActive(true);
        #endregion
    }

    public void StartGame()
    {
        StartPanel.SetActive(false);
        StartCoroutine(MyCards());
        StartCoroutine(EnemyCards());
    }

    #region Card distribution at the beginning of the game
    IEnumerator MyCards()
    {
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.3f);
            GameObject Card = Instantiate(_AddMyCard);
            Destroy(Card.gameObject, 0.4f);
            int _count = Random.Range(0, 16);
            GameObject _monster = Instantiate(_cardsMonster[_count], _myDeck.transform.position, Quaternion.Euler(90, 0, 0));
            _monster.transform.parent = _myDeck.transform;

            _monster.transform.localPosition = new Vector3(_monster.transform.position.x, _monster.transform.position.y, _pos);
            _pos += 0.01f;
        }
    }
    IEnumerator EnemyCards()
    {
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.3f);
            GameObject Card = Instantiate(_AddEnemyCard);
            Destroy(Card.gameObject, 0.4f);
            int _count = Random.Range(0, 16);
            GameObject _monster = Instantiate(_cardsEnemyMonster[_count], _enemyDeck.transform.position, Quaternion.Euler(90, 0, 0));
            _monster.transform.parent = _enemyDeck.transform;

            _monster.transform.localPosition = new Vector3(_monster.transform.position.x, _monster.transform.position.y, _pos);
            _Enemypos += 0.01f;
        }

        yield return new WaitForSeconds(0.5f);

        int _count1 = Random.Range(0, 2);
        int _count2 = Random.Range(2, 4);
        int _count3 = Random.Range(4, 7);
        _smokePs[3].Play();
        _smokePs[4].Play();
        _smokePs[5].Play();
        _Slot4Card = _enemyDeck.transform.GetChild(_count1).gameObject;
        _Slot5Card = _enemyDeck.transform.GetChild(_count2).gameObject;
        _Slot6Card = _enemyDeck.transform.GetChild(_count3).gameObject;
        _enemyDeck.transform.GetChild(_count1).GetChild(0).GetComponent<BoxCollider>().enabled = false;
        _enemyDeck.transform.GetChild(_count2).GetChild(0).GetComponent<BoxCollider>().enabled = false;
        _enemyDeck.transform.GetChild(_count3).GetChild(0).GetComponent<BoxCollider>().enabled = false;
        _enemyDeck.transform.GetChild(_count1).position = new Vector3(_slots[3].transform.position.x, _slots[3].transform.position.y + 1, _slots[3].transform.position.z);
        _enemyDeck.transform.GetChild(_count2).position = new Vector3(_slots[4].transform.position.x, _slots[4].transform.position.y + 1, _slots[4].transform.position.z);
        _enemyDeck.transform.GetChild(_count3).position = new Vector3(_slots[5].transform.position.x, _slots[5].transform.position.y + 1, _slots[5].transform.position.z);
        _enemyDeck.transform.GetChild(_count3).transform.GetChild(1).gameObject.SetActive(true);
        _enemyDeck.transform.GetChild(_count2).transform.GetChild(1).gameObject.SetActive(true);
        _enemyDeck.transform.GetChild(_count1).transform.GetChild(1).gameObject.SetActive(true);
        _enemyDeck.transform.GetChild(_count3).transform.parent = _enemyDeckParent.transform;
        _enemyDeck.transform.GetChild(_count2).transform.parent = _enemyDeckParent.transform;
        _enemyDeck.transform.GetChild(_count1).transform.parent = _enemyDeckParent.transform;
    }
    #endregion

    public void SetEnemyCard()
    {
        if (_Slot4Card.gameObject == null)
        {
            _smokePs[3].Play();
            _Slot4Card = _enemyDeck.transform.GetChild(0).gameObject;
            _Slot4Card.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            _Slot4Card.transform.position = new Vector3(_slots[3].transform.position.x, _slots[3].transform.position.y + 1, _slots[3].transform.position.z);
            _Slot4Card.transform.GetChild(1).gameObject.SetActive(true);
            _Slot4Card.transform.parent = _enemyDeckParent.transform;
        }

        if (_Slot5Card.gameObject == null)
        {
            _smokePs[4].Play();
            _Slot5Card = _enemyDeck.transform.GetChild(0).gameObject;
            _Slot5Card.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            _Slot5Card.transform.position = new Vector3(_slots[4].transform.position.x, _slots[4].transform.position.y + 1, _slots[4].transform.position.z);
            _Slot5Card.transform.GetChild(1).gameObject.SetActive(true);
            _Slot5Card.transform.parent = _enemyDeckParent.transform;
        }

        if (_Slot6Card.gameObject == null)
        {
            _smokePs[5].Play();
            _Slot6Card = _enemyDeck.transform.GetChild(0).gameObject;
            _Slot6Card.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            _Slot6Card.transform.position = new Vector3(_slots[5].transform.position.x, _slots[5].transform.position.y + 1, _slots[5].transform.position.z);
            _Slot6Card.transform.GetChild(1).gameObject.SetActive(true);
            _Slot6Card.transform.parent = _enemyDeckParent.transform;
        }
    }

    #region Add Card
    public void AddCard()
    {
        GameObject Card = Instantiate(_AddMyCard);
        Destroy(Card.gameObject, 0.4f);
        int _count = Random.Range(0, 16);
        GameObject _monster = Instantiate(_cardsMonster[_count], _myDeck.transform.position, Quaternion.Euler(90, 0, 0));
        _monster.transform.parent = _myDeck.transform;

        _monster.transform.localPosition = new Vector3(_monster.transform.position.x, _monster.transform.position.y, _pos);
        _pos = _pos + 0.01f;
    }
    
    public void AddEnemyCard()
    {
        GameObject Card = Instantiate(_AddEnemyCard);
        Destroy(Card.gameObject, 0.4f);
        int _count = Random.Range(0, 16);
        GameObject _monster = Instantiate(_cardsEnemyMonster[_count], _enemyDeck.transform.position, Quaternion.Euler(90, 0, 0));
        _monster.transform.parent = _enemyDeck.transform;

        _monster.transform.localPosition = new Vector3(_monster.transform.position.x, _monster.transform.position.y, _pos);
        _Enemypos += 0.01f;
    }
    #endregion

    // end turn button settings
    public void EndTurnButton()
    {
        print("adsadasdsada");
        if (_Slot1Card.gameObject) _Slot1Card.transform.tag = "CardOff";
        if (_Slot2Card.gameObject) _Slot2Card.transform.tag = "CardOff";
        if (_Slot3Card.gameObject) _Slot3Card.transform.tag = "CardOff";
        SetEnemyCard();
        AddCard();
        AddEnemyCard();
        StartCoroutine(SetEndTurn());
        StartCoroutine(SetEndTurnButton());
    }

    // Enemy Attack
    IEnumerator SetEndTurn()
    {
        yield return new WaitForSeconds(2f);

        int rnd = Random.Range(0,3);
        
        if (rnd == 0)
        {
            if (_Slot1Card.gameObject)
            {
                ParticleSystem heart = Instantiate(ps, new Vector3(_Slot4Card.transform.GetChild(0).position.x + 0.15f, _Slot4Card.transform.GetChild(0).position.y+0.65f, _Slot4Card.transform.GetChild(0).position.z ), Quaternion.identity);
                int randomSlot = Random.Range(0, 3);
                heart.transform.LookAt(GameManager.instance._Slot1Card.transform);
                Destroy(heart.gameObject, 2f);
                Invoke("SetAttack1", 2f);
            }
            else if (_Slot2Card.gameObject)
            {
                ParticleSystem heart = Instantiate(ps, new Vector3(_Slot5Card.transform.GetChild(0).position.x + 0.15f, _Slot5Card.transform.GetChild(0).position.y + 0.65f, _Slot5Card.transform.GetChild(0).position.z ), Quaternion.identity);
                int randomSlot = Random.Range(0, 3);
                heart.transform.LookAt(GameManager.instance._Slot2Card.transform);
                Destroy(heart.gameObject, 2f);
                Invoke("SetAttack2", 2f);
            }
            else if (_Slot3Card.gameObject)
            {
                ParticleSystem heart = Instantiate(ps, new Vector3(_Slot6Card.transform.GetChild(0).position.x + 0.15f, _Slot6Card.transform.GetChild(0).position.y + 0.65f, _Slot6Card.transform.GetChild(0).position.z ), Quaternion.identity);
                int randomSlot = Random.Range(0, 3);
                heart.transform.LookAt(GameManager.instance._Slot3Card.transform);
                Destroy(heart.gameObject, 2f);
                Invoke("SetAttack3", 2f);
            }
        }

        if (rnd == 1)
        {
            if (_Slot2Card.gameObject)
            {
                ParticleSystem heart = Instantiate(ps, new Vector3(_Slot5Card.transform.GetChild(0).position.x +0.15f, _Slot5Card.transform.GetChild(0).position.y + 0.65f, _Slot5Card.transform.GetChild(0).position.z ), Quaternion.identity);
                int randomSlot = Random.Range(0, 3);
                heart.transform.LookAt(GameManager.instance._Slot2Card.transform);
                Destroy(heart.gameObject, 2f);
                Invoke("SetAttack2", 2f);
            }
            else if (_Slot1Card.gameObject)
            {
                ParticleSystem heart = Instantiate(ps, new Vector3(_Slot4Card.transform.GetChild(0).position.x +0.15f, _Slot4Card.transform.GetChild(0).position.y + 0.65f, _Slot4Card.transform.GetChild(0).position.z - 0), Quaternion.identity);
                int randomSlot = Random.Range(0, 3);
                heart.transform.LookAt(GameManager.instance._Slot1Card.transform);
                Destroy(heart.gameObject, 2f);
                Invoke("SetAttack1", 2f);
            }
            else if (_Slot3Card.gameObject)
            {
                ParticleSystem heart = Instantiate(ps, new Vector3(_Slot6Card.transform.GetChild(0).position.x + 0.15f, _Slot6Card.transform.GetChild(0).position.y + 0.65f, _Slot6Card.transform.GetChild(0).position.z ), Quaternion.identity);
                int randomSlot = Random.Range(0, 3);
                heart.transform.LookAt(GameManager.instance._Slot3Card.transform);
                Destroy(heart.gameObject, 2f);
                Invoke("SetAttack3", 2f);
            }      
        }

        if (rnd == 2)
        {
            if (_Slot3Card.gameObject)
            {
                ParticleSystem heart = Instantiate(ps, new Vector3(_Slot6Card.transform.GetChild(0).position.x + 0.15f, _Slot6Card.transform.GetChild(0).position.y + 0.65f, _Slot6Card.transform.GetChild(0).position.z ), Quaternion.identity);
                int randomSlot = Random.Range(0, 3);
                heart.transform.LookAt(GameManager.instance._Slot3Card.transform);
                Destroy(heart.gameObject, 2f);
                Invoke("SetAttack3", 2f);
            }
            else if (_Slot1Card.gameObject)
            {
                ParticleSystem heart = Instantiate(ps, new Vector3(_Slot4Card.transform.GetChild(0).position.x + 0.15f, _Slot4Card.transform.GetChild(0).position.y + 0.65f, _Slot4Card.transform.GetChild(0).position.z), Quaternion.identity);
                int randomSlot = Random.Range(0, 3);
                heart.transform.LookAt(GameManager.instance._Slot1Card.transform);
                Destroy(heart.gameObject, 2f);
                Invoke("SetAttack1", 2f);
            }
            else if (_Slot2Card.gameObject)
            {
                ParticleSystem heart = Instantiate(ps, new Vector3(_Slot5Card.transform.GetChild(0).position.x + 0.15f, _Slot5Card.transform.GetChild(0).position.y + 0.65f, _Slot5Card.transform.GetChild(0).position.z ), Quaternion.identity);
                int randomSlot = Random.Range(0, 3);
                heart.transform.LookAt(GameManager.instance._Slot2Card.transform);
                Destroy(heart.gameObject, 2f);
                Invoke("SetAttack2", 2f);
            }
        }

    }

    IEnumerator SetEndTurnButton()
    {
        _endTurnButton.GetComponent<Image>().color = new Color(_endTurnButton.GetComponent<Image>().color.r, _endTurnButton.GetComponent<Image>().color.g, _endTurnButton.GetComponent<Image>().color.b, 0.5f);
        _endTurnButton.GetComponent<Button>().enabled = false;

        yield return new WaitForSeconds(3f);

        _endTurnButton.GetComponent<Image>().color = new Color(_endTurnButton.GetComponent<Image>().color.r, _endTurnButton.GetComponent<Image>().color.g, _endTurnButton.GetComponent<Image>().color.b, 1f);
        _yourTurnImage.SetActive(true);

        yield return new WaitForSeconds(2f);

        _endTurnButton.GetComponent<Button>().enabled = true;
        _yourTurnImage.SetActive(false);
    }

    private void SetAttack1()
    {
        _Slot1Card.transform.parent.GetComponent<Card>()._health -= _Slot4Card.transform.GetComponent<Card>()._attack;
        _slots[0].GetComponent<BoxCollider>().enabled = true;
        _myHealth -= 0.21f;
    }

    private void SetAttack2()
    {
        _Slot2Card.transform.parent.GetComponent<Card>()._health -= _Slot5Card.transform.GetComponent<Card>()._attack;
        _slots[1].GetComponent<BoxCollider>().enabled = true;
        _myHealth -= 0.21f;
    }

    private void SetAttack3()
    {
        _Slot3Card.transform.parent.GetComponent<Card>()._health -= _Slot6Card.transform.GetComponent<Card>()._attack;
        _slots[2].GetComponent<BoxCollider>().enabled = true;
        _myHealth -= 0.21f;
    }

}
