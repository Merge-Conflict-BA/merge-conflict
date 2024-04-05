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
    private int _pageCount;


    private void Awake()
    {
        Debugger.LogMessage($"Startposition/PanelLocation: {transform.position}");

        _pageCount = transform.childCount;
        _pages = new Page[_pageCount];
        _lastPage = _pageCount - 1;   //-1 because of the difference between page count and array index

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

    public void OnDrag(PointerEventData swipeData)
    {
        Debugger.LogMessage($"Current position: {transform.position}");
        Debugger.LogMessage($"Current panelLocation: {panelLocation}");


        float difference = swipeData.pressPosition.x - swipeData.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
        
        
        if (SwipedToLastPage()) 
        { 
            Debugger.LogMessage("Swiped to last page");
            return; 
        }
    }

    public void OnEndDrag(PointerEventData swipeData)
    {
        Debugger.LogMessage($"panelLocation: {panelLocation}");
        Debugger.LogMessage($"swipeData.pressPosition.x: {swipeData.pressPosition.x}");
        Debugger.LogMessage($"swipeData.position.x: {swipeData.position.x}");
        Debugger.LogMessage($"Screen.width: {Screen.width}");

        float swipedDistanceNormalized = (swipeData.pressPosition.x - swipeData.position.x) / (Screen.width / _pageCount);
        Debugger.LogMessage($"swipedDistanceNormalized: {swipedDistanceNormalized}");
        if (Mathf.Abs(swipedDistanceNormalized) >= normalizedTreshold)
        {
            Debugger.LogMessage($"position end: {transform.position}");
            Debugger.LogMessage($"end panelLocation: {panelLocation}");
            Vector3 newLocation = panelLocation;
            if (swipedDistanceNormalized > 0) { newLocation += new Vector3(-(Screen.width / _pageCount), 0, 0); }  //swiped left
            if (swipedDistanceNormalized < 0) { newLocation += new Vector3((Screen.width / _pageCount), 0, 0); }   //swiped right

            StartCoroutine(SmoothMove(transform.position, newLocation, automatedSwipingVelocity));
            panelLocation = newLocation;
        }
        else { transform.position = panelLocation; }

        int? visiblePage = IdFromVisiblePage(_pages);
        Debugger.LogMessage($"visiblePage: {visiblePage}");

        

    }

    IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 0f, t));
            yield return null;
        }
    }

    private int? IdFromVisiblePage(Page[] pages)
    {
        //get the cameraview planes
        Plane[] cameraPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        foreach (Page page in pages)
        {
            Bounds pageBounds = page.gameObject.GetComponent<Renderer>().bounds;
            bool pageIsVisible = GeometryUtility.TestPlanesAABB(cameraPlanes, pageBounds);

            if (pageIsVisible) 
            {
                return page.Id;
            }
        }

        return null; 
    }

    private bool SwipedToLastPage()
    {
        if (IdFromVisiblePage(_pages) == _lastPage)
        {
            return true;
        }
        return false;
    }

}