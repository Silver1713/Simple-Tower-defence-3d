using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropHandler : MonoBehaviour, IEndDragHandler, IDragHandler // Allow drag events to be registered.
{
    // Initialization of variables and configurations
    [Header("Configurations")]
    public string towerStorageFolderName;
    public string TowerInstanceFolderName;
    public float rayDistance;
    public string areaTag;
    public string previewFolderName;
    GameObject previewFolder;
    bool result;
    private GameObject previewPlaceObject;
    private GameObject InstantiatedPreview;
    public float IndicatorColorSuccessStrength;
    public float IndicatorColorFailStrength;
    private Vector3 placePoint;
    private GameObject storageFolder;
    private GameObject cointainerPanel;
    private GameManager data;
    public LayerMask layer;
    bool purchaseResult;
    
    
    void Start() //retrieve and search the objects needed.
     {
        data = GameManager.gameManager;
        cointainerPanel = transform.parent.gameObject;
        previewPlaceObject = getInstances();

        GameObject searchRes = GameObject.Find(previewFolderName);
        if (searchRes == null)
        {
            previewFolder = new GameObject(previewFolderName);
        } else
        {
            previewFolder = searchRes;
        }
        GameObject searchRes1 = GameObject.Find(towerStorageFolderName);
        if (searchRes1 == null)
        {
            GameObject storageFolder = new GameObject(towerStorageFolderName);
        }
        else
        {
            GameObject storageFolder = searchRes1;
        }
    }

    GameObject getInstances() // get tower based on the UI name.
    {
        GameObject searchGameRes = data.towerInstance;
       if (searchGameRes != null)
        {
            for (int i = 0; i < searchGameRes.transform.childCount; i++)
            {
                if (searchGameRes.transform.GetChild(i).name == transform.name)
                {
                    return searchGameRes.transform.GetChild(i).gameObject;
                }
            }
        }
        return null;
    }
    
   
    void IEndDragHandler.OnEndDrag(PointerEventData eventData) //Place the actual tower in the indicated position.
        //if the tower can be purchased by the user.
    {

        GameObject tower = Instantiate(previewPlaceObject, placePoint, Quaternion.Euler(-90, Quaternion.identity.y, Quaternion.identity.z)) as GameObject;
        
       if (result == true)
        {
            bool status = Purchasable(tower);
            if (status)
            {

                tower.SetActive(true);
                Destroy(InstantiatedPreview);
            }
        } else
        {
            Destroy(tower);
            Destroy(InstantiatedPreview);
            return;
        }
        return;
    }

    void checkIfUpgraded(GameObject item)
    {
        TowerManager tm = item.GetComponent<TowerManager>();
        purchaseResult =  tm.CheckIfPurchasable(data.getTowersHistories());
    }
    void IDragHandler.OnDrag(PointerEventData eventData) ////Retrieve the game manager to get the tower instance , clone the tower as preview and decide weather user have the
                                                         // correct amount of gold and
                                                         // a the correct drop point by using raycasting
                                                         //return the result.
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hitPoints = Physics.RaycastAll(mouseRay, rayDistance, layer, QueryTriggerInteraction.UseGlobal);
        if (hitPoints != null && hitPoints.Length != 0)
        {
            int placeAreaIndex = getPlaceAreaIndex(hitPoints);
            if (placeAreaIndex != -1)
            {
                RaycastHit point = hitPoints[placeAreaIndex];
                if (InstantiatedPreview == null)
                {
                    InstantiatedPreview = Instantiate(previewPlaceObject, point.point, Quaternion.Euler(-90,Quaternion.identity.y,Quaternion.identity.z), previewFolder.transform) as GameObject;
                    result = getPurchaseInfo(InstantiatedPreview);
                    checkIfUpgraded(InstantiatedPreview);
                    deactiveScriptForPreview(InstantiatedPreview);
                }
                if (result == true  && point.transform.tag == areaTag && purchaseResult)
                {
                    Debug.Log(point.transform.tag);
                    adjustColor(InstantiatedPreview, true);
                    InstantiatedPreview.transform.position = point.point;
                    placePoint = point.point;
                    InstantiatedPreview.SetActive(true);
                } else
                {
                    Debug.Log(point.transform.tag);
                    adjustColor(InstantiatedPreview, false);
                    InstantiatedPreview.transform.position = point.point;
                    placePoint = point.point;
                    InstantiatedPreview.SetActive(true);
                    result = false;
                }

            } else
            {
                
                return;
            }



        }
    }

    int getPlaceAreaIndex(RaycastHit[] hitPoint) //Check if it is a drop point.
    {
        for (int i = 0; i < hitPoint.Length; i++)
        {
            if (hitPoint[i].collider.gameObject.name == "DropZone")
            {
                return i;
            }
        }
        return -1;

    }

    void deactiveScriptForPreview(GameObject previewInstance) //Remove active script to allow preview.
    {
        TowerManager tm = previewInstance.GetComponent<TowerManager>();
        tm.enabled = false;
    }

    void adjustColor(GameObject targetObject, bool Placeable) //Set it to red or green depending on the result.
    {
        MeshRenderer[] meshRenderers = targetObject.GetComponents<MeshRenderer>();

        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
           if (Placeable)
            {
                Material mat = meshRenderer.material;
                mat.color = new Color(mat.color.r, IndicatorColorSuccessStrength, mat.color.b);

            } else
            {
                Material mat = meshRenderer.material;
                mat.color = new Color(IndicatorColorFailStrength, mat.color.g, mat.color.b);
            }
        }
    }

    bool Purchasable(GameObject item) ///Get purchase status
    {
        InGameStore inGameStore = cointainerPanel.GetComponent<InGameStore>();
        if (inGameStore != null)
        {
            bool res = inGameStore.purchaseTurret(item);
            if (res == true)
            {
                return true;
            } else
            {
                return false;
            }
        } else
        {
            return false;
        }
    }

    bool getPurchaseInfo(GameObject item) //get purchase information.
    {
        InGameStore inGameStore = cointainerPanel.GetComponent<InGameStore>();
        if (inGameStore != null)
        {
            int currentUpgrade = data.getTowersHistories();

            return inGameStore.getPurchaseResult(item) && item.GetComponent<TowerManager>().CheckIfPurchasable(currentUpgrade);
        }
        return false;
    } 
}

