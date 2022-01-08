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
        applicationHandler.IsSimple = false;
        applicationHandler.CharaType[0] = CharacterType.Kangaroo;
        applicationHandler.CharaType[1] = CharacterType.Customized;
        SetAniProperty();
        applicationHandler.DiffiType = (DifficultyType)((Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1) / 4);
        applicationHandler.Challenge = Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1;
        SceneManager.LoadScene("Battle");
    }

    public void OnClick_Whale()
    {
        SingleStoryGame.HideCharacter();
        applicationHandler.IsDuel = false;
        applicationHandler.IsSimple = false;
        applicationHandler.CharaType[0] = CharacterType.Whale;
        applicationHandler.CharaType[1] = CharacterType.Customized;
        SetAniProperty();
        applicationHandler.DiffiType = (DifficultyType)((Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1) / 4);
        applicationHandler.Challenge = Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1;
        SceneManager.LoadScene("Battle");
    }

    public void OnClick_Owl()
    {
        SingleStoryGame.HideCharacter();
        applicationHandler.IsDuel = false;
        applicationHandler.IsSimple = false;
        applicationHandler.CharaType[0] = CharacterType.Owl;
        applicationHandler.CharaType[1] = CharacterType.Customized;
        SetAniProperty();
        applicationHandler.DiffiType = (DifficultyType)((Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1) / 4);
        applicationHandler.Challenge = Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1;
        SceneManager.LoadScene("Battle");
    }

    public void OnClick_Koala()
    {
        SingleStoryGame.HideCharacter();
        applicationHandler.IsDuel = false;
        applicationHandler.IsSimple = false;
        applicationHandler.CharaType[0] = CharacterType.Koala;
        applicationHandler.CharaType[1] = CharacterType.Customized;
        SetAniProperty();
        applicationHandler.DiffiType = (DifficultyType)((Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1) / 4);
        applicationHandler.Challenge = Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1;
        SceneManager.LoadScene("Battle");
    }

    public void OnClick_Fox()
    {
        SingleStoryGame.HideCharacter();
        applicationHandler.IsDuel = false;
        applicationHandler.IsSimple = false;
        applicationHandler.CharaType[0] = CharacterType.Fox;
        applicationHandler.CharaType[1] = CharacterType.Customized;
        SetAniProperty();
        applicationHandler.DiffiType = (DifficultyType)((Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1) / 4);
        applicationHandler.Challenge = Convert.ToInt32(SingleStoryGame.clickedButtonName) - 1;
        SceneManager.LoadScene("Battle");
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

    private void SetAniProperty() {

        switch (Convert.ToInt32(SingleStoryGame.clickedButtonName))
        {
            case 0:
                applicationHandler.CharacProperty = new int[6] { 9999, 5, 5, 5, 5, 0 };
                break;
            case 1:
                applicationHandler.CharacProperty = new int[6] { 9999, 5, 5, 5, 5, 0 };
                break;
            case 2:
                applicationHandler.CharacProperty = new int[6] { 9999, 5, 5, 5, 5, 0 };
                break;
            case 3:
                applicationHandler.CharacProperty = new int[6] { 300, 5, 5, 5, 5, 0 };
                break;
            case 4:
                applicationHandler.CharacProperty = new int[6] { 200, 5, 5, 5, 5, 0 };
                break;
            case 5:
                applicationHandler.CharacProperty = new int[6] { 9999, 5, 5, 5, 5, 0 };
                break;
            case 6:
                applicationHandler.CharacProperty = new int[6] { 9999, 5, 5, 5, 5, 0 };
                break;
            case 7:
                applicationHandler.CharacProperty = new int[6] { 400, 5, 5, 5, 5, 0 };
                break;
            case 8:
                applicationHandler.CharacProperty = new int[6] { 250, 5, 5, 5, 5, 0 };
                break;
            case 9:
                applicationHandler.CharacProperty = new int[6] { 9999, 5, 5, 5, 5, 0 };
                break;
            case 10:
                applicationHandler.CharacProperty = new int[6] { 9999, 5, 5, 5, 5, 0 };
                break;
            case 11:
                applicationHandler.CharacProperty = new int[6] { 500, 5, 5, 5, 5, 0 };
                break;
            case 12:
                applicationHandler.CharacProperty = new int[6] { 300, 5, 5, 5, 5, 0 };
                break;
            case 13:
                applicationHandler.CharacProperty = new int[6] { 9999, 5, 5, 5, 5, 0 };
                break;
            case 14:
                applicationHandler.CharacProperty = new int[6] { 9999, 5, 5, 5, 5, 0 };
                break;
            case 15:
                applicationHandler.CharacProperty = new int[6] { 600, 5, 5, 5, 5, 0 };
                break;
            default:
                applicationHandler.CharacProperty = null;
                break;
        }
    }
}