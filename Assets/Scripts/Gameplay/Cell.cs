using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private CellState state = CellState.Unchanged;
    private SpriteRenderer cellSprite;

    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
    public bool IsHeader { get; set; }

    private void Start()
    {
        cellSprite = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (state == CellState.Unchanged || state == CellState.False)
            state = CellState.True;
        else
            state = CellState.False;

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Click"))
            anim.SetTrigger("OnClick");
    }

    private void OnMouseOver()
    {
        if (!IsHeader)
        {
            anim.SetBool("OnHover", true);
            cellSprite.sortingOrder = 1;
        }
        
    }

    private void OnMouseExit()
    {
        if (!IsHeader)
        {
            anim.SetBool("OnHover", false);
            cellSprite.sortingOrder = 0;
        }
    }
}
