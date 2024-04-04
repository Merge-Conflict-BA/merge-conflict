using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    public float percentTreshold = 0.2f;
    public float easing = 0.5f;
    private Page[] _pages;    //store all swipable pages
    private int _lastPage;
    public Camera mainCamera;


    private void Awake()
    {
        int pageCount = transform.childCount;
        _pages = new Page[pageCount];
        _lastPage = pageCount - 1;   //-1 because of the difference between page count and array index

        for (int i = 0; i < transform.childCount; i++)
        {
            _pages[i] = new Page(i, transform.GetChild(i).gameObject);
        }

        panelLocation = transform.position;
    }

    private void Update()
    {
        


    }

    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentTreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0) { newLocation += new Vector3(-Screen.width, 0, 0); }
            if (percentage < 0) { newLocation += new Vector3(Screen.width, 0, 0); }

            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            transform.position = panelLocation;
        }

        int? visiblePage = GetIdFromVisiblePage(_pages);
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

    private int? GetIdFromVisiblePage(Page[] pages)
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
}