using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlacementOnMesh_Character : MonoBehaviour
{
    public static event Action<NeedsController, PetController> CharacterPlaced;
   
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject placementObject;
    [SerializeField] private MinigameUIController _minigameUIController;

    private List<GameObject> placedObjects = new();

    private bool isPlaced = false;

    //public static event Action characterPlaced;
   
    void Update()
    {
        if (isPlaced) return;


#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("UI Hit was recognized");
                return;
            }
            TouchToRay(Input.mousePosition);
        }
#endif
#if UNITY_IOS || UNITY_ANDROID
        
        if (Input.touchCount > 0 && Input.touchCount < 2 &&
            Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = touch.position;

            List<RaycastResult> results = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0) {
                // We hit a UI element
                Debug.Log("We hit an UI Element");
                return;
            }
            
            Debug.Log("Touch detected, fingerId: " + touch.fingerId);  // Debugging line


            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                Debug.Log("Is Pointer Over GOJ, No placement ");
                return;
            }
            TouchToRay(touch.position);
        }
#endif
    }


    void TouchToRay(Vector3 touch) 
    {
        Ray ray = mainCam.ScreenPointToRay(touch);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var cat = Instantiate(placementObject, hit.point, Quaternion.FromToRotation(transform.up, hit.normal));
            placedObjects.Add(cat);
            var needsController = cat.GetComponent<NeedsController>();
            //needsController.MinigameUIController = _minigameUIController;
            var petController = cat.GetComponent<PetController>();
            isPlaced = true;
            CharacterPlaced?.Invoke(needsController, petController);
        }
    }
}


