using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class SwapStats : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropDownL;
    [SerializeField] private TMP_Dropdown dropDownR;
    [SerializeField] private Button confirmButton;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject startPlayer2;
    [SerializeField] private Timer _timer;
    
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
        confirmButton.onClick.RemoveAllListeners();
        dropDownR.gameObject.SetActive(false);
        dropDownL.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        startPlayer2.SetActive(true);
        startButton.onClick.AddListener(startGame);
    }

    public void startGame()
    {
        startButton.onClick.RemoveAllListeners();
        startPlayer2.SetActive(false);
        _timer.StartTimer();
        _playerController.backtogame();
    }
    

    public void confirmClicked()
    {
        selectedValueL = floatList[dropDownL.value];
        selectedValueR = floatList[dropDownR.value];

        float temp = selectedValueL.Value;
        selectedValueL.Value = selectedValueR.Value;
        selectedValueR.Value = temp;
        disableSwapMode();
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Floatref", order = 1)]
    public class FloatRef : ScriptableObject
    {
        public float Value;
        public string Name;
    }
}
