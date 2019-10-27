using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //Get the towerInstance and the image
    public GameObject imageInstance;
    public GameObject towerInstance;
    
    public string IconName;
    private  int  numOfIcons;
    // Start is called before the first frame update
    void Start() // Clone the image and instatiate apply the sprite and in accordance to the current tower applies the related UI to it.
    {
        //Get how many Icons that will be Instatiated
        numOfIcons = towerInstance.transform.childCount;
        for (int i = 0; i < numOfIcons; i++)
        {
            //Using a for loop clone the amount of the buy and sell UIs which will be instatiated and assign it accordingly.
            GameObject currentInstance = Instantiate(imageInstance, transform) as GameObject;
            currentInstance.name = towerInstance.transform.GetChild(i).name;
            Image currentImage = currentInstance.GetComponent<Image>();
            
            if (currentImage)
            {
                Debug.Log("/UI/" + towerInstance.transform.GetChild(i).transform.name + " - UI");
                currentImage.sprite = Resources.Load<Sprite>("UI/" + towerInstance.transform.GetChild(i).transform.name + " - UI");
            }
            currentInstance.SetActive(true);
        }
    }

    
}
