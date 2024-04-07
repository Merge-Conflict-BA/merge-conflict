using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    public float normalizedTreshold = 0.2f;
    public float automatedSwipingVelocity = 0.5f;
    private Page[] _pages;    //store all swipable pages
    private int _lastPage;
    public Camera mainCamera;
    private float _pageReferenceWidth;

    private int _pageIndex = 0;
    private int _pageCount;


    private void Awake()
    {
        Debugger.LogMessage($"Startposition/PanelLocation: {GetCanvasPosition()}");
        _pageReferenceWidth = transform.GetComponentInParent<Canvas>().rootCanvas.GetComponent<CanvasScaler>().referenceResolution.x;

        _pageCount = transform.childCount;
        transform.localScale = new Vector3(_pageCount, transform.localScale.y, transform.localScale.z);

        // to start at first page
        float pageCountToFirst = (_pageCount - 1f) / 2f;
        SetCanvasPosition(GetCanvasPosition() + new Vector2(pageCountToFirst * _pageReferenceWidth, 0));
        panelLocation = GetCanvasPosition();

        InitializePages();
    }

    // TODO remove pages, not necessary for calculation
    private void InitializePages()
    {
        int pageCount = transform.childCount;
        _pages = new Page[pageCount];
        _lastPage = pageCount - 1;   //-1 because of the difference between page count and array index

        //link the page GameObjects to the array of pages
        for (int i = 0; i < transform.childCount; i++)
        {
            _pages[i] = new Page(i, transform.GetChild(i).gameObject);
        }
    }

    public void OnDrag(PointerEventData swipingData)
    {
        float difference = swipingData.pressPosition.x - swipingData.position.x; //diff = position grapping the page --> current mouse position
        SetCanvasPosition(panelLocation - new Vector3(difference, 0, 0)); //move the panel according to the mouse movement
        Debugger.LogMessage($"Difference: {difference}");
        Debugger.LogMessage($"Position: {GetCanvasPosition().x}");
        // if (SwipedToLastPage())
        // {
        //     Debugger.LogMessage("Swiped to last page");
        // }
    }

    public void OnEndDrag(PointerEventData swipingData)
    {
        Debugger.LogMessage("########### End of dragging ###########");
        Debugger.LogMessage($"pageWidth = {_pageReferenceWidth}");
        Debugger.LogMessage($"pressPosition.x = {swipingData.pressPosition.x}");
        Debugger.LogMessage($"position.x = {swipingData.position.x}");
        Debugger.LogMessage($"panelLocation bevor calc = {panelLocation.x}");

        float swipedDistanceNormalized = (swipingData.pressPosition.x - swipingData.position.x) / _pageReferenceWidth;
        Debugger.LogMessage($"last x pos: {swipingData.position.x}");

        bool swipeToTheLeft = swipedDistanceNormalized < 0;

        if (Mathf.Abs(swipedDistanceNormalized) < normalizedTreshold
            || canSwipeFurther(swipeToTheLeft) == false)
        {
            MoveToCurrentPage();
            return;
        }

        MoveToNewPage(swipeToTheLeft);        

        Debugger.LogMessage($"panelLocation after calc = {panelLocation.x}");
    }

    private bool canSwipeFurther(bool toLeft)
    {
        if (toLeft && _pageIndex == 0) { return false; }
        if (toLeft == false && _pageIndex >= _pageCount - 1) { return false; }

        return true;
    }

    private void MoveToCurrentPage()
    {
        StartCoroutine(SmoothMove(GetCanvasPosition(), panelLocation, automatedSwipingVelocity));
    }

    private void MoveToNewPage(bool toLeft)
    {
        Vector3 newLocation = panelLocation;

        if (toLeft) //swiped left
        {
            newLocation += new Vector3(_pageReferenceWidth, 0, 0);
            _pageIndex--;
        }
        else //swiped right
        {
            newLocation += new Vector3(-_pageReferenceWidth, 0, 0);
            _pageIndex++;
        } 

        StartCoroutine(SmoothMove(GetCanvasPosition(), newLocation, automatedSwipingVelocity));
        panelLocation = newLocation;        
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
            SetCanvasPosition(Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t)));
            yield return null;
        }
    }

    private Vector2 GetCanvasPosition()
    {
        return transform.GetComponent<RectTransform>().anchoredPosition;
    }

    private void SetCanvasPosition(Vector2 position)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = position;
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