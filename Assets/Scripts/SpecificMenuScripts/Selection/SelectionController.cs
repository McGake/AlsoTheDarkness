using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionController : MonoBehaviour
{

    public bool incrementOnHorizontal;

    public bool invertCounting;

    private string axis;

    private float axisVal;

    [SerializeField]
    private float sensitivity = .1f;
    [SerializeField]
    private float refractoryInterval = .2f;

    private float nextInputTime= 0;

    [SerializeField]
    private SelectionView selectionView;

    private GeneralSelectionModel selectionModel;

    private int indx;

    private List<GameObject> selections;

    public GameObject returnPage;

    private bool paused = false;

    public void RepopulateSelections()
    {
        selections = selectionModel.GetSelections();

        if(indx >= selections.Count)
        {
            indx = selections.Count - 1;
        }

        //selectionView.RepopulateSelections(selections);
    }

    public void Pause()
    {
        paused = true;
    }

    public void Unpause()
    {
        paused = false;
    }

    public void Select(int indx)
    {
        if (selections[indx].GetComponent<ISelectionBehavior>() != null)
        {
            selections[indx].GetComponent<ISelectionBehavior>().DoSelectionBehavior();
        }
        else
        {
            Debug.LogError("no selection behavior attached to button");
        }
        
    }



    public void StartSelection(/*List<GameObject> sT, ISelectionModel iSM*/)
    {
        //selectionModel = iSM;        
        //RepopulateSelections(sT);
        gameObject.SetActive(true);
        TownMovement.inMenu = true;
        OverworldMovement.inMenu = true;
        selectionModel = GetComponent<GeneralSelectionModel>();
        selections = selectionModel.GetSelections();
        indx = 0;
        
        selectionView = GetComponent<SelectionView>();
        selectionView.OpenView(selections);       
        Unpause();
       // selectionView.MoveCursorToIndex(indx);
    }

    public void EndSelection()
    {
        selectionView.CloseView();
    }

    public void Backout()
    {
        selectionView.CloseView();
        if (returnPage != null)
        {
            returnPage.GetComponent<SelectionController>().StartSelection();
        }
        else
        {
            selectionView.LeaveMenus();
            TownMovement.inMenu = false;//This is a dumb hack
            OverworldMovement.inMenu = false;
            gameObject.transform.gameObject.SetActive(false); 
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
        if (paused == false)
        {
            axisVal = Input.GetAxis(axis);
            //Debug.Log(axisVal);
            if (axisVal > sensitivity)
            {
                ChangeSelection(1);
            }
            else if (axisVal < -sensitivity)
            {
                ChangeSelection(-1);
            }

            if (MultiInput.GetAButtonDown())
            {
                Select(indx);
            }
            if (MultiInput.GetBButtonDown())
            {
                Backout();
            }
        }
    }

    public void ChangeSelection(int indxChange)
    {
        if(Time.time > nextInputTime)
        {
            Debug.Log("change");
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
