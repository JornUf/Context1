using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.UI;

public class SwapStats : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropDownL;
    [SerializeField] private TMP_Dropdown dropDownR;
    [SerializeField] private Button confirmButton;
    [SerializeField] private PlayerController _playerController;
    
    private FloatRef selectedValueL;
    private FloatRef selectedValueR;
    
    internal List<FloatRef> floatList = new List<FloatRef>();
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void enableSwapMode()
    {
        dropDownR.gameObject.SetActive(true);
        dropDownL.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(true);
        
        dropDownL.onValueChanged.AddListener(valueChangedL);
        dropDownR.onValueChanged.AddListener(valueChangedR);
        confirmButton.onClick.AddListener(confirmClicked);
        
        List<TMP_Dropdown.OptionData> optionslist = new List<TMP_Dropdown.OptionData>();

        foreach (FloatRef option in floatList)
        {
            TMP_Dropdown.OptionData optdata = new TMP_Dropdown.OptionData(option.Name + ": " + option.Value);
            optionslist.Add(optdata);
        }

        dropDownL.options = optionslist;
        dropDownR.options = optionslist;
    }

    public void disableSwapMode()
    {
        dropDownL.options.Clear();
        dropDownR.options.Clear();
        selectedValueL = null;
        selectedValueR = null;
        dropDownL.onValueChanged.RemoveAllListeners();
        dropDownR.onValueChanged.RemoveAllListeners();
        confirmButton.onClick.RemoveAllListeners();
        dropDownR.gameObject.SetActive(false);
        dropDownL.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        _playerController.backtogame();
    }
    

    public void valueChangedL(int index)
    {
        print("L");
        selectedValueL = floatList[index];
    }

    public void valueChangedR(int index)
    {
        print("R");
        selectedValueR = floatList[index];
    }

    public void confirmClicked()
    {
        print("a");
        if (selectedValueL != null && selectedValueR != null)
        {
            float temp = selectedValueL.Value;
            selectedValueL.Value = selectedValueR.Value;
            selectedValueR.Value = temp;
            disableSwapMode();
        }
        else
        {
            //do nothing maybe give a warning that nothing has been selected
        }
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Floatref", order = 1)]
    public class FloatRef : ScriptableObject
    {
        public float Value;
        public string Name;
    }
}
