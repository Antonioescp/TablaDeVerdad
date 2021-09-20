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

    public CellState State { get; private set; }
    private SpriteRenderer cellSprite;

    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
    public SpriteRenderer CellSprite => cellSprite;

    [Header(header: "Expression support")]
    [SerializeField] TextMeshPro expressionText;

    private static UnityEvent<Cell> onHeaderClicked;

    private bool interactuable = true;

    private void Awake()
    {
        cellSprite = GetComponent<SpriteRenderer>();

        onHeaderClicked = new UnityEvent<Cell>();
    }

    private void Start()
    {
        onSpawn.Raise();
        onHeaderClicked.AddListener(OnHeaderClicked);

        State = CellState.Unchanged;

        if(RowNumber == 0)
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
        State = State == CellState.True ? CellState.False : CellState.True;
        switch (State)
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
        if(RowNumber != 0 && interactuable)
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
        if(RowNumber != 0 && interactuable)
            anim.SetBool("Hover", true);
    }

    private void OnMouseExit()
    {
        if(RowNumber != 0 && interactuable)
            anim.SetBool("Hover", false);
    }

    private void OnHeaderClicked(Cell cell)
    {
        if(cell != this)
        {
            anim.SetBool("HeaderHover", false);
        }
    }

    public void Check(bool isCorrect)
    {
        interactuable = !isCorrect;
        
        if(isCorrect)
        {
            anim.SetTrigger("Correct");
        }
        else
        {
            anim.SetTrigger("Wrong");
        }
    }
}
