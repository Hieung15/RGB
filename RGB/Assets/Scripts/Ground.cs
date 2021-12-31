using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private GameObject[] trees;
    List<GameObject> treeToLIst;
    public int treeLimit;

    public GameObject tree;
    private int treeCount = 0;
    private Rigidbody rb;
    private BoxCollider bc;
    private Renderer meshRenderer;
    private Vector3 randomPos;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        trees = Resources.LoadAll<GameObject>("Prefab");
        GameObject groundEmpty = GameObject.Find("Ground_empty");
        while (treeLimit > treeCount)
        {
            float randomTheta = Random.Range(0, 2 * Mathf.PI);
            float randomPy = Random.Range(0, 2 * Mathf.PI);

            float randomX = 98 * Mathf.Sin(randomTheta) * Mathf.Cos(randomPy);
            float randomY = 98 * Mathf.Sin(randomTheta) * Mathf.Sin(randomPy);
            float randomZ = 98 * Mathf.Cos(randomTheta);


            if ((randomX < 30 && randomX > 10) || (randomX > -30 && randomX < -10))
            {
                randomPos = new Vector3(randomX, randomY, randomZ);
                tree = Instantiate(trees[Random.Range(0, 3)]) as GameObject;



                //bc = tree.AddComponent<BoxCollider>();
                //rb = tree.AddComponent<Rigidbody>();
                //rb.useGravity = false;
                //bc.isTrigger = true;
                //if (tree.name == "tree01(Clone)")
                //{
                //    bc.size = new Vector3(2, 16, 2.5f);
                //    bc.center = new Vector3(0, 7.8f, -1.3f);
                //}
                //else if (tree.name == "tree02(Clone)")
                //{
                //    bc.size = new Vector3(6.4f, 11.5f, 4);
                //    bc.center = new Vector3(0, 5.2f, 1.1f);
                //}
                //else if (tree.name == "tree03(Clone)")
                //{
                //    bc.size = new Vector3(6.5f, 11.3f, 2.67f);
                //    bc.center = new Vector3(0, 6, 0.7f);
                //}

                tree.transform.localPosition = randomPos;
                tree.transform.LookAt(groundEmpty.transform.position);
                tree.transform.rotation = tree.transform.rotation * Quaternion.Euler(-90, 0, 0);
                tree.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
                tree.transform.SetParent(groundEmpty.transform);
                //tree.transform.parent = groundEmpty.transform;

                

                treeCount++;





                if (treeCount == treeLimit)
                    break;
            }



        }
    }


    // Update is called once per frame
    void Update()
    {
        //this.transform.rotation = Quaternion.Euler(20, 0, 0);
        this.transform.Rotate(new Vector3(-5f, 0, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        float randomTheta = Random.Range(0, 2 * Mathf.PI);
        float randomPy = Random.Range(0, 2 * Mathf.PI);

        float randomX = 98 * Mathf.Sin(randomTheta) * Mathf.Cos(randomPy);
        float randomY = 98 * Mathf.Sin(randomTheta) * Mathf.Sin(randomPy);
        float randomZ = 98 * Mathf.Cos(randomTheta);

        randomPos = new Vector3(randomX, randomY, randomZ);
        Debug.Log("collision");
    }
}
