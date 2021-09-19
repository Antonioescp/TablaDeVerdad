using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

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

    [Header(header: "Expression support")]
    [SerializeField] TextMeshPro expressionText;

    private static UnityEvent<Cell> onHeaderClicked;

    private void Awake()
    {
        cellSprite = GetComponent<SpriteRenderer>();

        onHeaderClicked = new UnityEvent<Cell>();
    }

    private void Start()
    {
        onSpawn.Raise();
        onHeaderClicked.AddListener(OnHeaderClicked);

        if(IsHeader)
        {
            int variablesCount = ConfigurationUtils.Tables[GameManager.LevelSelected].variables.Count;
            if(ColumnNumber < variablesCount)
            {
                expressionText.text = ConfigurationUtils.Tables[GameManager.LevelSelected].variables[ColumnNumber].Trim();
            }
            else
            {
                expressionText.text = ConfigurationUtils.Tables[GameManager.LevelSelected].expressions[ColumnNumber - variablesCount].Trim();
            }
        }
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
        {
            ChangeState();
            onClick.Raise();
        }
        else
        {
            onHeaderClicked.Invoke(this);
            anim.SetBool("HeaderHover", !anim.GetBool("HeaderHover"));
        }
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

    private void OnHeaderClicked(Cell cell)
    {
        if(cell != this)
        {
            anim.SetBool("HeaderHover", false);
        }
    }
}
