using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    public float normalizedTreshold = 0.2f;
    public float automatedSwipingVelocity = 0.5f;
    private Page[] _pages;    //store all swipable pages
    private int _lastPage;
    public Camera mainCamera;
    private float _pageWidth;


    private void Awake()
    {
        Debugger.LogMessage($"Startposition/PanelLocation: {transform.position}");

        int pageCount = transform.childCount;
        _pages = new Page[pageCount];
        _lastPage = pageCount - 1;   //-1 because of the difference between page count and array index
        _pageWidth = Screen.width / pageCount;

        //link the page GameObjects to the array of pages
        for (int i = 0; i < transform.childCount; i++)
        {
            _pages[i] = new Page(i, transform.GetChild(i).gameObject);
        }

        panelLocation = transform.position;
    }

    private void Update()
    {



    }

    public void OnDrag(PointerEventData swipingData)
    {
        float difference = swipingData.pressPosition.x - swipingData.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);

        // if (SwipedToLastPage())
        // {
        //     Debugger.LogMessage("Swiped to last page");
        // }
    }

    public void OnEndDrag(PointerEventData swipingData)
    {

        float swipedDistanceNormalized = (swipingData.pressPosition.x - swipingData.position.x) / _pageWidth;
        Debugger.LogMessage($"last x pos: {swipingData.position.x}");
        if (Mathf.Abs(swipedDistanceNormalized) >= normalizedTreshold)
        {
            Vector3 newLocation = panelLocation;
            if (swipedDistanceNormalized > 0) { newLocation += new Vector3(-_pageWidth, 0, 0); }  //swiped left
            if (swipedDistanceNormalized < 0) { newLocation += new Vector3(_pageWidth, 0, 0); }   //swiped right

            StartCoroutine(SmoothMove(transform.position, newLocation, automatedSwipingVelocity));
            panelLocation = newLocation;
        }
        else { StartCoroutine(SmoothMove(transform.position, panelLocation, automatedSwipingVelocity)); }

        //int? visiblePage = IdFromVisiblePage(_pages);
    }

    IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float seconds)
    {
        Debugger.LogMessage("SmoothMove active...");
        Debugger.LogMessage($"startPos: {startPos}");
        Debugger.LogMessage($"endPos: {endPos}");
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    // private int? IdFromVisiblePage(Page[] pages)
    // {
    //     //get the cameraview planes
    //     Plane[] cameraPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

    //     foreach (Page page in pages)
    //     {
    //         Bounds pageBounds = page.gameObject.GetComponent<Renderer>().bounds;
    //         bool pageIsVisible = GeometryUtility.TestPlanesAABB(cameraPlanes, pageBounds);

    //         if (pageIsVisible)
    //         {
    //             return page.Id;
    //         }
    //     }

    //     return null;
    // }

    // private bool SwipedToLastPage()
    // {
    //     if (IdFromVisiblePage(_pages) == _lastPage)
    //     {
    //         return true;
    //     }
    //     return false;
    // }

}