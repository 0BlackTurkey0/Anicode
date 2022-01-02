using UnityEngine;

public class CubeClick : MonoBehaviour {
    public void OnClick_Kangaroo()
    {
        SingleStoryGame.HideCharacter();
        //---
        //進入挑戰
        //---
    }

    public void OnClick_Whale()
    {
        SingleStoryGame.HideCharacter();
        //---
        //進入挑戰
        //---
    }

    public void OnClick_Owl()
    {
        SingleStoryGame.HideCharacter();
        //---
        //進入挑戰
        //---
    }

    public void OnClick_Koala()
    {
        SingleStoryGame.HideCharacter();
        //---
        //進入挑戰
        //---
    }

    public void OnClick_Fox()
    {
        SingleStoryGame.HideCharacter();
        //---
        //進入挑戰
        //---
    }

    public void OnClick_BigCube()
    {
        SingleStoryGame.status = 6;
        SingleStoryGame.clickedButtonName = this.transform.parent.gameObject.name;
    }

    public void OnClick_SmallCube()
    {
        SingleStoryGame.status = 2;
        SingleStoryGame.clickedButtonName = this.name.Substring(4);
    }

    public void OnClick_Footprint()
    {
        SingleStoryGame.status = 3;
        SingleStoryGame.clickedButtonName = this.name.Substring(9);
    }

    public void OnClick_Skeleton()
    {
        SingleStoryGame.status = 4;
    }
}