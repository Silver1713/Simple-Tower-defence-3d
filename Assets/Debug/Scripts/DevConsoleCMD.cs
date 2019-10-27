using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevConsoleCMD : MonoBehaviour
{
    DevConsoleCMD console;
   public GameObject manager;
    GameManager data;
    // Start is called before the first frame update
    private void Awake()
    {
        if (console == null)
        {
            console = this;
        }
        if (console != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        data = manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            transform.GetChild(0).transform.gameObject.SetActive(!transform.GetChild(0).transform.gameObject.activeSelf);
            GameObject inp = transform.GetChild(0).transform.gameObject;
            Text t = inp.transform.GetChild(2).GetComponent<Text>();
            if (t.text == "AddValue()")
            {
                data.setShopCurrency(2000f);
            } else if (t.text == "GiveMeGold")
            {
                data.inGameCurrency = 1000f;
            }



        }
            
        
    }
}
