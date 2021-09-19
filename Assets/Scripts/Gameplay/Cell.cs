using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private Sprite trueSprite;
    [SerializeField] private Sprite falseSprite;

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

    private void OnMouseOver()
    {
        if(!IsHeader)
        {
            anim.SetBool("Hover", true);
        }
    }

    private void OnMouseExit()
    {
        if(!IsHeader)
        {
            anim.SetBool("Hover", false);
        }
    }

    private void OnHoverStart()
    {
        cellSprite.sortingOrder = 1;
    }

    private void OnHoverEnd()
    {
        cellSprite.sortingOrder = 0;
    }

    private void OnStateChange()
    {
        switch (state)
        {
            case CellState.True:
                cellSprite.sprite = trueSprite;
                break;
            case CellState.False:
                cellSprite.sprite = falseSprite;
                break;
        }

        AudioManager.PlayOneShot(AudioClipName.SFXPop);
    }

    private void OnSpawn()
    {
        AudioManager.PlayOneShot(AudioClipName.SFXPop);
    }
}
