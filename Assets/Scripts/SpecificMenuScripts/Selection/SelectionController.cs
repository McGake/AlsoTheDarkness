using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionController : MonoBehaviour, ISelectionController
{

    public bool incrementOnHorizontal;

    public bool invertCounting;

    private string axis;

    private float axisVal;

    [SerializeField]
    private float sensitivity = .1f;
    [SerializeField]
    private float refractoryInterval = .1f;

    private float nextInputTime= 0;

    [SerializeField]
    private ISelectionView selectionView;

    private ISelectionModel selectionModel;

    private int indx;

    private List<GameObject> selections;
    private List<string> selectionsText;

    public GameObject returnPage;



    public void RepopulateSelections(List<GameObject> sT)
    {
        selections = sT;
        selectionsText.Clear();

        selectionView.RepopulateSelections(selections);
    }

    public void Select(int indx)
    {
        selections[indx].GetComponent<ISelectionBehavior>().DoSelectionBehavior();
    }

    public void OnEnable()
    {
        StartSelection();
    }

    public void StartSelection(/*List<GameObject> sT, ISelectionModel iSM*/)
    {   
        //selectionModel = iSM;        
        //RepopulateSelections(sT);
        selectionModel = GetComponent<ISelectionModel>();
        selections = selectionModel.GetSelections();
        indx = 0;
        selectionView = GetComponent<ISelectionView>();

        Debug.Log("Positions");
        foreach(GameObject s in selections)
        {
            Debug.Log(s.transform.position);
        }
        selectionView.OpenView(selections);
    }

    public void EndSelection()
    {
        selectionView.CloseView();
        if (returnPage != null)
        {
            returnPage.SetActive(true);
        }
    }


    void Awake()
    {
        if (incrementOnHorizontal)
        {
            axis = "Horizontal";
        }
        else
        {
            axis = "Vertical";
        }
    }
    // Update is called once per frame
    void Update()
    {
        axisVal = Input.GetAxis(axis);
        //Debug.Log(axisVal);
        if(axisVal > sensitivity)
        {           
            ChangeSelection(1);            
        }
        else if(axisVal< -sensitivity)
        {
            ChangeSelection(-1);
        }

        if(Input.GetButtonDown("A"))
        {
            Select(indx);
        }
        if(Input.GetButtonDown("B"))
        {
            EndSelection();
        }
    }

    public void ChangeSelection(int indxChange)
    {
        if(Time.time > nextInputTime)
        {
            if(invertCounting)
            {
                indxChange *= -1;
            }
            indx += indxChange;

            if (indx >= selections.Count)
            {
                indx = 0;
            }
            else if (indx < 0)
            {
                indx = selections.Count - 1;
            }
            selectionView.MoveCursorToIndex(indx);
            nextInputTime = Time.time + refractoryInterval;
        }
    }


}
