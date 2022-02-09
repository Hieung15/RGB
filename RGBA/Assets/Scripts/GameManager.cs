using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;



public class GameManager : MonoBehaviour
{
    

    private GameObject selectedObejct;
    private GameObject[] children;
    private GameObject tree;
    public GameObject aTreePanel;
    
    private GameObject currentATree;
    private GameObject makeANewTree;
    private GameObject obj;
    private Color currentColor = Color.red;
    private RaycastHit hit;


    private Material[] materialsOfParent;
    private Material[] originalMaterials;
    public GameObject menuPanel;
    public GameObject[] trees;
    public GameObject colorPicker;
    public GameObject treeObj;
    public Button[] buttons;
    public Button button;
    public GameObject exitButton;
    public Camera uiCamera;
    public Image resultColorPicker;
    public TextMeshProUGUI leavesText;
    public TextMeshProUGUI colorText;

    GameObject tree1, tree2, tree3;
    bool apply;

    private void Awake()
    {
        
    }
    void Start()
    {
        originalMaterials = Resources.LoadAll<Material>("Materials");
        apply = false;
        SetOrtho();
        leavesText.text = "nothing is selected";

    }


    private void Update()
    {

        if (aTreePanel.activeSelf == true)
        {
            
            treeObj.transform.Rotate(new Vector3(0, 0.5f, 0));

            if (SelectedChild() != null)
            {
                obj = SelectedChild();
                Debug.Log(obj + " is selected");
                leavesText.text = obj.name + " is selected";
                colorPicker.SetActive(true);

            }

            if (currentColor != GetCurrentColorFromColorPicker())
            { 
                // Debug.Log("");
                // Debug.Log("you choose the color: " + currentColor);
                if (obj != null)
                {
                    this.currentColor = GetCurrentColorFromColorPicker();
                    SetNewColor(currentColor, obj);
                    float r = currentColor.r;
                    float g = currentColor.g;
                    float b = currentColor.b;

                    colorText.text = "R: " + r.ToString("0.000") + ", G: " + g.ToString("0.000") + ", B: " + b.ToString("0.000");
                }


            }


        }

    }


    public void GetTreeButtons()
    {
        menuPanel.SetActive(true);
        button.interactable = false;

        float halbWidth = menuPanel.GetComponent<RectTransform>().sizeDelta.x/ 2;
        float width = Screen.width / (trees.Length + 2);

        
        
        if(trees.Length % 2 == 1) // odd number
        {
            int a = (trees.Length / 2);
            tree1 = Instantiate(trees[a]);
            NewButtonPosition(a, new Vector3(0, -20, 0), tree1);


            for (int i = 0; i< (trees.Length / 2); i++)
            {
                float newXValue = halbWidth + width * (i+1) + 100;
                tree2 = Instantiate(trees[a + i + 1]);
                tree3 = Instantiate(trees[a - (i + 1)]);

                NewButtonPosition(a + i + 1, new Vector3(newXValue, -20, 0), tree2);
                NewButtonPosition(a - (i + 1), new Vector3(-newXValue, -20, 0), tree3);


            }
        }

        exitButton.SetActive(true);
    }


    public void NewButtonPosition(int index, Vector3 newPosition, GameObject obj)
    {
        buttons[index].transform.localPosition = newPosition;
        obj.transform.SetParent(buttons[index].transform);
        //obj.transform.parent = buttons[index].transform;
        float objHeight = buttons[index].GetComponent<RectTransform>().rect.height/2;

        obj.transform.localPosition = new Vector3(0, -objHeight, -100f);
        SetLayer(obj.transform, 5);

    }


    public void SetLayer(Transform trans, int layer)
    {
        trans.gameObject.layer = layer;
        foreach (Transform child in trans)
        {
            SetLayer(child, layer);
        }
    }


    public void MakeATreeButton()
    {
        menuPanel.SetActive(false);
        aTreePanel.SetActive(true);
        //colorPicker.SetActive(true);


        if (treeObj.transform.childCount < 2)
        {
            currentATree = MakeATree();
            currentATree.transform.SetParent(treeObj.transform);
            //currentATree.transform.parent = treeObj.transform;
            float height = treeObj.GetComponent<RectTransform>().rect.height/2;
            currentATree.transform.localPosition = new Vector3(0, 0, 0f);
            currentATree.transform.localScale = treeObj.transform.localScale * 30;
            //currentATree.transform.localScale = new Vector3(20, 20, 20);
        }


    }

    public GameObject MakeATree()
    {

        GameObject currentTreeBtn = GetSelectedObject();

        GameObject childOfTree = currentTreeBtn.transform.GetChild(0).gameObject;

        makeANewTree = Instantiate(childOfTree);

        return makeANewTree;

    }



    public GameObject SelectedChild()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = uiCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //if(hit.collider.gameObject == child)

                return hit.collider.gameObject;
            }
        }

        return null;
    }



    public void Apply()
    {
        apply = true;
    }


    private void SetNewColor(Color color, GameObject obj)
    {
        GameObject parent = obj.transform.parent.gameObject;
        materialsOfParent = parent.GetComponent<Renderer>().materials;

        if (obj.tag == "Leaf")
        {
            obj.GetComponent<Renderer>().material.color = color;

            for (int j = 0; j < materialsOfParent.Length; j++)
            {
                if (obj.GetComponent<Renderer>().material.name == materialsOfParent[j].name)
                {
                    materialsOfParent[j].color = color;
                    
                }

            }

            for(int i = 0; i< parent.transform.childCount; i++)
            {
                if(parent.transform.GetChild(i).GetComponent<Renderer>().material.name == obj.GetComponent<Renderer>().material.name)
                {
                    parent.transform.GetChild(i).GetComponent<Renderer>().material.color = color;
                }
            }
            //Debug.Log(obj.name + "    " + obj.GetComponent<Renderer>().material.name);
        }



    }

    public void SetApplyButton()
    {
        if (materialsOfParent != null)
        {
            for (int i = 0; i < originalMaterials.Length; i++)
            {
                for (int j = 0; j < materialsOfParent.Length; j++)
                {
                    if (originalMaterials[i].name + " (Instance)" == materialsOfParent[j].name)
                    {

                        originalMaterials[i].color = materialsOfParent[j].color;
                    }

                }

                //if(trees[i].name)
            }

        }
        aTreePanel.SetActive(false);
        colorPicker.SetActive(false);
        exitButton.SetActive(false);
        button.interactable = true;
        Destroy(makeANewTree);
        Destroy(tree1);
        Destroy(tree2);
        Destroy(tree3);
        apply = false;
        leavesText.text = "nothing is selected";

    }

    public void ExitButton()
    {
        if (aTreePanel.activeSelf)
        {
            //obj.SetActive(false);
            colorPicker.SetActive(false);
            menuPanel.SetActive(true);
            aTreePanel.SetActive(false);

            Destroy(makeANewTree);
            leavesText.text = "nothing is selected";
        }
        else if (menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);
            button.interactable = true;
            exitButton.SetActive(false);
            Destroy(tree1);
            Destroy(tree2);
            Destroy(tree3);

        }
        

    }


    public void SetOrtho()
    {
        float width = aTreePanel.GetComponent<RectTransform>().sizeDelta.x;
        float hight = aTreePanel.GetComponent<RectTransform>().sizeDelta.y;
        float orthoSize = width * Screen.height / Screen.width * 0.5f;

        uiCamera.orthographicSize = orthoSize;
    }

    public GameObject GetSelectedObject()
    {
        selectedObejct = EventSystem.current.currentSelectedGameObject;
       
        return selectedObejct;
    }

    public GameObject[] GetChildObject(GameObject parentObject)
    {

        int count = parentObject.transform.childCount;

        for (int i = 0; i < count; i++)
        {
            children[i] = parentObject.transform.GetChild(i).gameObject;
        }

        return children;
    }
    public Color GetCurrentColorFromColorPicker()
    {
        Color currentColor = resultColorPicker.color;
        return currentColor;
    }
    public Color GetColor(GameObject obj)
    {
        Color color = obj.GetComponent<Renderer>().material.color;
        return color;
    }
}
