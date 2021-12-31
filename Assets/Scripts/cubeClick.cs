using UnityEngine;

public class CubeClick:MonoBehaviour
{
    public void OnClick_Kangaroo()
    {
        singleStoryGame.HideCharactor();
        //---
        //進入挑戰
        //---
    }

    public void OnClick_Whale()
    {
        singleStoryGame.HideCharactor();
        //---
        //進入挑戰
        //---
    }

    public void OnClick_Owl()
    {
        singleStoryGame.HideCharactor();
        //---
        //進入挑戰
        //---
    }

    public void OnClick_Koala()
    {
        singleStoryGame.HideCharactor();
        //---
        //進入挑戰
        //---
    }

    public void OnClick_Fox()
    {
        singleStoryGame.HideCharactor();
        //---
        //進入挑戰
        //---
    }

    public void OnClick_BigCube()
    {
        singleStoryGame.status = 6;
        singleStoryGame.clickedButtonName = this.transform.parent.gameObject.name;
    }

    public void OnClick_SmallCube()
    {
        singleStoryGame.status = 2;
        singleStoryGame.clickedButtonName = this.name.Substring(4);
    }

    public void OnClick_Footprint()
    {
        singleStoryGame.status = 3;
        singleStoryGame.clickedButtonName = this.name.Substring(9);
    }

    public void OnClick_Skeleton()
    {
        singleStoryGame.status = 4;
    }
}