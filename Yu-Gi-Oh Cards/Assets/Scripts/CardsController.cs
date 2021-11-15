using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsController : MonoBehaviour
{
    public static CardsController instance;

    [HideInInspector]
    public bool _isSlotOne;
    public bool _isSlotTwo;
    public bool _isSlotThree;
    public bool _isNotChange;
    public bool _attackSelectionSlotOne;
    public bool _attackSelectionSlotTwo;
    public bool _attackSelectionSlotthree;
    Vector3 _firstLocation;
    Vector3 _firstLocationSelection;

    public GameObject _cards;
    public float _pos=0;
    public GameObject _cardsSlot;

    private bool _tableControl;
    public ParticleSystem ps;
  
    void Start()
    {
        instance = this;
        _isNotChange = true;
        _tableControl = false;
        _attackSelectionSlotOne = false;
        _attackSelectionSlotTwo = false;
        _attackSelectionSlotthree = false;
        _cards = GameObject.Find("CanvasMyCard");
        _cardsSlot = GameObject.Find("NewCanvas");
    }

    void Update()
    {
        #region Where we choose where our cards will be placed
        if (Input.GetMouseButtonUp(0) && _isNotChange)
        {
            if (SelectionControl.instance.target.GetComponent<CardsController>()._isSlotOne)
            {
                GameManager.instance._Slot1Card = SelectionControl.instance.target;
                SelectionControl.instance.target.transform.position = new Vector3(GameManager.instance._slots[0].transform.position.x, GameManager.instance._slots[0].transform.position.y + 0.1f, GameManager.instance._slots[0].transform.position.z);
               // SelectionControl.instance.target.tag = "CardOff";
                SelectionControl.instance.target.tag = "Player";
                GameManager.instance._slots[0].GetComponent<BoxCollider>().enabled = false;
                SelectionControl.instance.target.GetComponent<CardsController>()._isNotChange = false;
                SelectionControl.instance.target.transform.localScale = new Vector3(3.4f, 3.4f, 3.4f);
                SelectionControl.instance.target.transform.parent.transform.parent = _cardsSlot.transform;
                SelectionControl.instance.target.transform.parent.GetChild(1).gameObject.SetActive(true);
                SelectionControl.instance.target.transform.parent.GetChild(1).transform.position = SelectionControl.instance.target.transform.position;
                GameManager.instance._smokePs[0].Play();
            }

            if (SelectionControl.instance.target.GetComponent<CardsController>()._isSlotTwo)
            {
                GameManager.instance._Slot2Card = SelectionControl.instance.target;
                SelectionControl.instance.target.transform.position = new Vector3(GameManager.instance._slots[1].transform.position.x, GameManager.instance._slots[1].transform.position.y + 0.1f, GameManager.instance._slots[1].transform.position.z);
                SelectionControl.instance.target.tag = "Player";
                GameManager.instance._slots[1].GetComponent<BoxCollider>().enabled = false;
                SelectionControl.instance.target.GetComponent<CardsController>()._isNotChange = false;
                SelectionControl.instance.target.transform.localScale = new Vector3(3.4f, 3.4f, 3.4f);
                SelectionControl.instance.target.transform.parent.transform.parent = _cardsSlot.transform;
                SelectionControl.instance.target.transform.parent.GetChild(1).gameObject.SetActive(true);
                SelectionControl.instance.target.transform.parent.GetChild(1).transform.position = SelectionControl.instance.target.transform.position;
                GameManager.instance._smokePs[1].Play();
            }

            if (SelectionControl.instance.target.GetComponent<CardsController>()._isSlotThree)
            {
                GameManager.instance._Slot3Card = SelectionControl.instance.target;
                SelectionControl.instance.target.transform.position = new Vector3(GameManager.instance._slots[2].transform.position.x, GameManager.instance._slots[2].transform.position.y + 0.1f, GameManager.instance._slots[2].transform.position.z);
                SelectionControl.instance.target.tag = "Player";
                GameManager.instance._slots[2].GetComponent<BoxCollider>().enabled = false;
                SelectionControl.instance.target.GetComponent<CardsController>()._isNotChange = false;
                SelectionControl.instance.target.transform.localScale = new Vector3(3.4f, 3.4f, 3.4f);
                SelectionControl.instance.target.transform.parent.transform.parent = _cardsSlot.transform;
                SelectionControl.instance.target.transform.parent.GetChild(1).gameObject.SetActive(true);
                SelectionControl.instance.target.transform.parent.GetChild(1).transform.position = SelectionControl.instance.target.transform.position;
                GameManager.instance._smokePs[2].Play();
            }
            Invoke("SetTarget", 0.1f);
        }
        if (Input.GetMouseButtonDown(0))
        {
            _firstLocation = transform.localPosition;
            _firstLocationSelection = transform.localPosition;
        }
        if (Input.GetMouseButtonUp(0) && _isSlotOne==false && _isSlotTwo==false && _isSlotThree==false)
        {
            transform.localPosition=_firstLocation;
        }

        if (Input.GetMouseButtonUp(0))
        {
            transform.localPosition = _firstLocationSelection;
        }
        #endregion

        #region choosing the area where our cards attack
        if (Input.GetMouseButtonUp(0) && _attackSelectionSlotOne)
        {
            ParticleSystem heart = Instantiate(ps, new Vector3(SelectionControl.instance.targetSelection.transform.position.x + 0.15f, SelectionControl.instance.targetSelection.transform.position.y+0.65f, SelectionControl.instance.targetSelection.transform.position.z+0.30f) , Quaternion.identity);
            heart.transform.LookAt(GameManager.instance._slots[3].transform);
            SelectionControl.instance.targetSelection.transform.tag = "Player";
            GameManager.instance._enemyHealth -= 0.21f;
            Destroy(heart.gameObject, 2f);
            Invoke("SetAttack4", 2f);
        }

        if (Input.GetMouseButtonUp(0) && _attackSelectionSlotTwo)
        {
            ParticleSystem heart = Instantiate(ps, new Vector3(SelectionControl.instance.targetSelection.transform.position.x + 0.15f, SelectionControl.instance.targetSelection.transform.position.y+0.65f, SelectionControl.instance.targetSelection.transform.position.z + 0.30f), Quaternion.identity);
            heart.transform.LookAt(GameManager.instance._slots[4].transform);
            SelectionControl.instance.targetSelection.transform.tag = "Player";
            GameManager.instance._enemyHealth -= 0.21f;
            Destroy(heart.gameObject, 2f);
            Invoke("SetAttack5", 2f);
        }

        if (Input.GetMouseButtonUp(0) && _attackSelectionSlotthree)
        {
            ParticleSystem heart = Instantiate(ps, new Vector3(SelectionControl.instance.targetSelection.transform.position.x+0.15f, SelectionControl.instance.targetSelection.transform.position.y+0.65f, SelectionControl.instance.targetSelection.transform.position.z + 0.30f), Quaternion.identity);
            heart.transform.LookAt(GameManager.instance._slots[5].transform);
            SelectionControl.instance.targetSelection.transform.tag = "Player";
            GameManager.instance._enemyHealth -= 0.21f;
            Destroy(heart.gameObject, 2f);
            Invoke("SetAttack6", 2f);
        }
        #endregion

        #region card selection area
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) )
        {
            if (Physics.Raycast(_ray, out _hit))
            {
                if (_hit.transform.tag == "CardOn")
                {
                    print("Yukarý");
                    for (int i = 0; i < _cards.transform.childCount; i++)
                    {
                        _cards.gameObject.transform.GetComponent<GridLayoutGroup>().spacing = new Vector2(-1.1f, 0f);
                        _cards.transform.GetChild(i).GetChild(0).localScale= new Vector3(5.5f, 5.5f, 5.5f);
                        _cards.transform.GetChild(i).GetChild(0).tag = "Card";
                    }
                    _tableControl = true;
                    _pos = 0;
                }
                if (_hit.transform.tag == "Table" && _tableControl)
                {
                    print("Yukarý");
                    for (int i = 0; i < _cards.transform.childCount; i++)
                    {
                        _cards.gameObject.transform.GetComponent<GridLayoutGroup>().spacing = new Vector2(-1.8f, 0f);
                        _cards.transform.GetChild(i).GetChild(0).localScale= new Vector3(3.4f, 3.4f, 3.4f);
                        _cards.transform.GetChild(i).GetChild(0).tag = "CardOn";
                        _cards.transform.GetChild(i).localPosition = new Vector3(_cards.transform.GetChild(i).transform.position.x, _cards.transform.GetChild(i).transform.position.y, _pos);
                        _pos = _pos + 0.01f;
                    }
                    _tableControl = false;
                    _pos = 0;
                }
            }
        }
        #endregion
    }

    private void OnTriggerStay(Collider other)
    {
        // Where we choose where our cards will be placed
        if (!_isSlotOne && !_isSlotTwo && !_isSlotThree)
        {
            if (other.gameObject.CompareTag("SlotOne"))
            {
                _isSlotOne = true;
            }
            else if (other.gameObject.CompareTag("SlotTwo"))
            {
                _isSlotTwo = true;
            }
            else if (other.gameObject.CompareTag("SlotThree"))
            {
                _isSlotThree = true;
            }
        }

        // Choose Attack cards
        if (!_attackSelectionSlotOne && !_attackSelectionSlotTwo && !_attackSelectionSlotthree)
        {
            if (other.gameObject.CompareTag("EnemyCardsSlot1"))
            {
                if (this.gameObject.transform.parent.transform.childCount == 2)
                {
                    _attackSelectionSlotOne = true;

                }
            }

            if (other.gameObject.CompareTag("EnemyCardsSlot2"))
            {
                if (this.gameObject.transform.parent.transform.childCount == 2)
                {
                    _attackSelectionSlotTwo = true;

                }
            }

            if (other.gameObject.CompareTag("EnemyCardsSlot3"))
            {
                if (this.gameObject.transform.parent.transform.childCount == 2)
                {
                    _attackSelectionSlotthree = true;

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SlotOne"))
        {
            _isSlotOne = false;
        }

        if (other.gameObject.CompareTag("SlotTwo"))
        {
            _isSlotTwo = false;
        }

        if (other.gameObject.CompareTag("SlotThree"))
        {
            _isSlotThree = false;
        }

        if (other.gameObject.CompareTag("EnemyCardsSlot1"))
        {
            if (this.gameObject.transform.parent.transform.childCount == 2)
            {
                _attackSelectionSlotOne = false;

            }
        }

        if (other.gameObject.CompareTag("EnemyCardsSlot2"))
        {
            if (this.gameObject.transform.parent.transform.childCount == 2)
            {
                _attackSelectionSlotTwo = false;

            }
        }

        if (other.gameObject.CompareTag("EnemyCardsSlot3"))
        {
            if (this.gameObject.transform.parent.transform.childCount == 2)
            {
                _attackSelectionSlotthree = false;

            }
        }
    }

    public void SetTarget()
    {
        SelectionControl.instance.target = null;
    }

    #region Functions that select places to attack
    private void SetAttack4()
    {
        GameManager.instance._Slot4Card.transform.GetComponent<Card>()._health -= SelectionControl.instance.targetSelection.transform.parent.GetComponent<Card>()._attack;
        SelectionControl.instance.targetSelection = null;
    }

    private void SetAttack5()
    {
        GameManager.instance._Slot5Card.transform.GetComponent<Card>()._health -= SelectionControl.instance.targetSelection.transform.parent.GetComponent<Card>()._attack;
        SelectionControl.instance.targetSelection = null;
    }

    private void SetAttack6()
    {
        GameManager.instance._Slot6Card.transform.GetComponent<Card>()._health -= SelectionControl.instance.targetSelection.transform.parent.GetComponent<Card>()._attack;
        SelectionControl.instance.targetSelection = null;
    }
    #endregion
}
