using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class Spawner : MonoBehaviour
{
    public GameObject bioms;
    BiomsDatabase biomsDatabase;
    public Biom selectedOrDefault;
    public Button prefab;
    Button lastSelected;
    public GameObject canvas;
    public Animation coinAnim;

    public Sprite lockerIcon;
    public Sprite SelectedIcon;

    public float itemW;
    public float itemH;
    List<Button> butonslist;

    void Start()
    {
        int chosedIndex = PlayerPrefs.GetInt("BiomSelected", -1);
        butonslist = new List<Button>() { };
        biomsDatabase = bioms.GetComponent("BiomsDatabase") as BiomsDatabase;
        int count = 0;
        Button button;
        Button firstSelected = null;
        foreach (Biom b in biomsDatabase.bioms)
        {
            count++;
            button = Instantiate(prefab, Vector3.zero, Quaternion.identity) as Button;
            button.GetComponent<RectTransform>().SetParent(canvas.GetComponent<RectTransform>());
            button.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            button.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            button.GetComponent<RectTransform>().localPosition = new Vector3(Screen.height * (itemW / 100) * count + (Screen.width * (0.05f)) * count, 0, button.GetComponent<RectTransform>().localPosition.z);
            button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.height * (itemW / 100));
            button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height * (itemW / 100));
            button.image.overrideSprite = b.icon;
            AddListener(button, b);
            button.GetComponentsInChildren<Text>()[0].text = b.name;
            UpdateButtonBiom(b, button);
            butonslist.Add(button);
            if (b.index == chosedIndex || (b.isDefault && chosedIndex == -1))
            {
                firstSelected = button;
            }
        }
        lastSelected = firstSelected;
    }
    void Update()
    {
        
    }

    void Call(Biom biom, Button button)
    {
        if (!lastSelected.Equals(button))
        {
            int chosedIndex = PlayerPrefs.GetInt("BiomSelected", -1);
            int CoinsCount = PlayerPrefs.GetInt("CoinsCount", -1);
            if (!biom.isLocked)
            {
                if (biom.isBuyed)
                {
                    if (biom.index != chosedIndex)
                    {
                        PlayerPrefs.SetInt("BiomSelected", biom.index);
                        Debug.Log("Biom selected");
                    }

                }
                else if (CoinsCount != -1)
                {
                    if (CoinsCount >= biom.prise)
                    {
                        biom.isBuyed = true;
                        CoinsCount -= biom.prise;
                        PlayerPrefs.SetInt("CoinsCount", CoinsCount);
                        Debug.Log(string.Format("Biom buyed for {0}", biom.prise));
                    }
                    else
                    {
                        Debug.Log("you dont have enouf money");
                        //TODO massage (you dont have enouf money)
                    }
                }
            }

            else
            {
                Debug.Log("Biom is locked");
                //TODO massage (Biom is locked)
            }
            UpdateButtonBiom(biom, button);
            lastSelected = button;
        }
    }
    void AddListener(Button b, Biom value)
    {
        b.onClick.AddListener(() => Call(value, b));
    }

    void UpdateButtonBiom(Biom b, Button button)
    {
        int chosedIndex = PlayerPrefs.GetInt("BiomSelected", -1);
        if (b.isBuyed)
        {
            button.GetComponentsInChildren<Text>(true)[1].text = "";
            button.GetComponentsInChildren<Text>(true)[1].GetComponentsInChildren<Image>(true)[0].gameObject.SetActive(false);
            if (b.index == chosedIndex || (b.isDefault && chosedIndex == -1))
            {
                button.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true);
                button.GetComponentsInChildren<Image>(true)[1].overrideSprite = SelectedIcon;
                if (lastSelected != null)
                {
                    lastSelected.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);
                }
            }
            else
            {                
                button.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);
            }
        }
        else
        {
            button.GetComponentsInChildren<Text>(true)[1].text = b.prise.ToString();
            button.GetComponentsInChildren<Text>(true)[1].GetComponentsInChildren<Image>(true)[0].gameObject.SetActive(true);
            if (b.isLocked)
            {
                button.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true);
                button.GetComponentsInChildren<Image>(true)[1].overrideSprite = lockerIcon;
            }
            else
            {
                button.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);
            }
        }
    }
}
