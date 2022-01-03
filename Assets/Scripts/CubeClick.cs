using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeClick : MonoBehaviour {

    private ApplicationHandler applicationHandler;
    void Awake()
    {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
    }

    public void OnClick_Kangaroo()
    {
        SingleStoryGame.HideCharacter();
        applicationHandler.IsDuel = false;
        applicationHandler.CharaType[0] = CharacterType.Kangaroo;
        //applicationHandler.CharaType[1] = CharacterType.Customized;
        applicationHandler.DiffiType = (DifficultyType)((Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1) / 4);
        applicationHandler.Challenge = Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1;
        SceneManager.LoadScene(8);
    }

    public void OnClick_Whale()
    {
        SingleStoryGame.HideCharacter();
        applicationHandler.IsDuel = false;
        applicationHandler.CharaType[0] = CharacterType.Whale;
        //applicationHandler.CharaType[1] = CharacterType.Customized;
        applicationHandler.DiffiType = (DifficultyType)((Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1) / 4);
        applicationHandler.Challenge = Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1;
        SceneManager.LoadScene(8);
    }

    public void OnClick_Owl()
    {
        SingleStoryGame.HideCharacter();
        applicationHandler.IsDuel = false;
        applicationHandler.CharaType[0] = CharacterType.Owl;
        //applicationHandler.CharaType[1] = CharacterType.Customized;
        applicationHandler.DiffiType = (DifficultyType)((Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1) / 4);
        applicationHandler.Challenge = Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1;
        SceneManager.LoadScene(8);
    }

    public void OnClick_Koala()
    {
        SingleStoryGame.HideCharacter();
        applicationHandler.IsDuel = false;
        applicationHandler.CharaType[0] = CharacterType.Koala;
        //applicationHandler.CharaType[1] = CharacterType.Customized;
        applicationHandler.DiffiType = (DifficultyType)((Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1) / 4);
        applicationHandler.Challenge = Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1;
        SceneManager.LoadScene(8);
    }

    public void OnClick_Fox()
    {
        SingleStoryGame.HideCharacter();
        applicationHandler.IsDuel = false;
        applicationHandler.CharaType[0] = CharacterType.Fox;
        //applicationHandler.CharaType[1] = CharacterType.Customized;
        applicationHandler.DiffiType = (DifficultyType)((Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1) / 4);
        applicationHandler.Challenge = Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1;
        SceneManager.LoadScene(8);
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