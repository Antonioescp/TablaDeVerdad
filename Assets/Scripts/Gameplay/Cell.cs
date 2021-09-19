using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Sprite trueSprite;
    [SerializeField] private Sprite falseSprite;
    
    [Header(header: "Events")]
    [SerializeField] private GameEvent onSpawn;
    [SerializeField] private GameEvent onClick;

    private CellState state = CellState.Unchanged;
    private SpriteRenderer cellSprite;

    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
    public bool IsHeader { get; set; }
    public SpriteRenderer CellSprite => cellSprite;

    private void Awake()
    {
        cellSprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        onSpawn.Raise();
    }

    private void ChangeState()
    {
        state = state == CellState.True ? CellState.False : CellState.True;
        switch (state)
        {
            case CellState.True:
                cellSprite.sprite = trueSprite;
                break;
            case CellState.False:
                cellSprite.sprite = falseSprite;
                break;
        }
    }

    private void OnMouseDown()
    {
        if(!IsHeader)
            ChangeState();

        onClick.Raise();
    }

    private void OnMouseEnter()
    {
        if(!IsHeader)
            anim.SetBool("Hover", true);
    }

    private void OnMouseExit()
    {
        if(!IsHeader)
            anim.SetBool("Hover", false);
    }
}
