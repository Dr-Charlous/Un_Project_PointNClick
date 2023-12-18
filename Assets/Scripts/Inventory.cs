using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InventoryS", menuName = "ScriptableObjects/ScriptableInventory", order = 1)]
public class InventoryScriptable : ScriptableObject
{
    public CollectableUIScriptableObject[] ObjectsInInventoryS;
}

public class Inventory : MonoBehaviour
{
    public Ui Ui;
    public PlayerMove PlayerMoveCode;
    public RythmGpe rythm;
    [Header("")]
    public GameObject Interogation;
    public GameObject Exclamation;
    [Header("")]
    public InventoryScriptable InventoryS;
    public CollectableUIScriptableObject[] ObjectsInInventory;
    public GameObject ObjectDragUi;
    [Header("")]
    public float ValueDistanceObject = 3.5f;
    public float ValueDistanceDoor = 7.5f;

    [Header("ScreenShake N Anims : ")]
    public float Intensity = 1f;
    public float Duration = 4f;
    [Header("")]
    public Animator Animator;

    private GameObject _objectCollect;
    private CollectableUIScriptableObject _objectUse;
    private ObjectActionWithCollectable _doorUse;
    private int _objectIndex = -1;
    private int _objectUseIndex = -1;

    private void Start()
    {
        Interogation.SetActive(false);
        Exclamation.SetActive(false);
        ObjectDragUi.SetActive(false);

        Animator.gameObject.SetActive(true);
        ObjectsInInventory = InventoryS.ObjectsInInventoryS;
        Ui.AffObjectUi(ObjectsInInventory, Ui.Image, true);
    }

    private void Update()
    {
        if (rythm.IsActive == false)
        {
            MouseDown();
            GetObjectPlayer(ValueDistanceObject);
            UseObjectPlayer(ValueDistanceDoor, _objectUse, _doorUse, _objectUseIndex);

            if (_objectIndex != -1)
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var pos = new Vector3(mousePos.x, mousePos.y, 0);

                ObjectDragUi.transform.position = pos;

                if (Input.GetMouseButtonUp(0))
                {
                    Drop(_objectIndex, ObjectsInInventory[_objectIndex]);
                }
            }
        }
    }

    void MouseDown()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.GetComponent<Collectable>() != null)
            {
                _objectCollect = hit.collider.gameObject;

                StartCoroutine(AffExpressions(Exclamation, 1));
            }
            else if (hit.collider != null && hit.collider.GetComponent<ObjectActionWithCollectable>() != null)
            {
                StartCoroutine(AffExpressions(Interogation, 1));
            }
        }
    }

    public void Drag(int index)
    {
        if (rythm.IsActive == false)
        {
            if (ObjectsInInventory[index] != null)
            {
                if (!ObjectDragUi.activeInHierarchy)
                {
                    ObjectDragUi.SetActive(true);
                    ObjectDragUi.GetComponent<Image>().sprite = ObjectsInInventory[index].sprite;
                    ObjectDragUi.GetComponent<Image>().color = ObjectsInInventory[index].color;
                }

                _objectIndex = index;
            }
        }
    }

    public void Drop(int index, CollectableUIScriptableObject scriptableObject)
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.GetComponent<ObjectActionWithCollectable>() != null)
        {
            if (ObjectsInInventory[index] != null)
            {
                _doorUse = hit.collider.GetComponent<ObjectActionWithCollectable>();
                _objectUse = ObjectsInInventory[index];
                _objectUseIndex = index;

                if (hit.collider.GetComponent<ObjectActionWithCollectable>().Check(scriptableObject))
                {
                    StartCoroutine(AffExpressions(Exclamation, 1));
                }
                else
                {
                    StartCoroutine(AffExpressions(Interogation, 1));
                }
            }
        }

        _objectIndex = -1;
        ObjectDragUi.SetActive(false);
    }

    public void GetInInventory(GameObject obj)
    {
        for (int i = 0; i < ObjectsInInventory.Length; i++)
        {
            if (ObjectsInInventory[i] == null)
            {
                ObjectsInInventory[i] = obj.GetComponent<Collectable>().ScrpitableObjectRefObjects;
                obj.SetActive(false);

                Debug.Log($"{obj.name} go in Inventory !");
                Ui.AffObjectUi(ObjectsInInventory, Ui.Image, false);
                _objectCollect = null;
                break;
            }
        }

        if (_objectCollect != null)
        {
            Debug.Log("Not enough place in Inventory !");
        }
    }

    public void GetObjectPlayer(float value)
    {
        if (_objectCollect == null) return;
        
        if (Vector3.Distance(PlayerMoveCode.gameObject.transform.position, _objectCollect.transform.position) < value && _objectCollect != null)
        {
            GetInInventory(_objectCollect);
        }
    }

    public void UseObjectPlayer(float value, CollectableUIScriptableObject scriptable, ObjectActionWithCollectable doorUse, int index)
    {
        if (_objectUse != null && _doorUse != null && _objectUseIndex != -1)
        {
            if (Vector3.Distance(PlayerMoveCode.gameObject.transform.position, doorUse.transform.position) < value)
            {
                if (doorUse.Check(scriptable))
                {
                    doorUse.Action(scriptable, index);
                    StartCoroutine(ChangeScene(2, doorUse, index));

                    scriptable.IsUsed = true;
                    _doorUse = null;
                    _objectUse = null;
                    _objectUseIndex = -1;

                    StartCoroutine(Ui.ScreenShake(Intensity, Duration));
                    //Animator.SetTrigger("Closing");
                }
            }
        }
    }

    public IEnumerator AffExpressions(GameObject obj, int time)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }

    public IEnumerator ChangeScene(float time, ObjectActionWithCollectable doorUse, int index)
    {
        InventoryS.ObjectsInInventoryS = ObjectsInInventory;
        yield return new WaitForSeconds(time);
        doorUse.Action(ObjectsInInventory[index], index);
    }
}
