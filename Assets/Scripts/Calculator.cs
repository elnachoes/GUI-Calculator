using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Operation
{
    add,
    subtract,
    multiply,
    divide,
}

public class Calculator : MonoBehaviour
{

    //serialize all the button fields
    [SerializeField] Button _zeroButton;
    [SerializeField] Button _oneButton;
    [SerializeField] Button _twoButton;
    [SerializeField] Button _threeButton;
    [SerializeField] Button _fourButton;
    [SerializeField] Button _fiveButton;
    [SerializeField] Button _sixButton;
    [SerializeField] Button _sevenButton;
    [SerializeField] Button _eightButton;
    [SerializeField] Button _nineButton;

    [SerializeField] Button _plusButton;
    [SerializeField] Button _minusButton;
    [SerializeField] Button _multiplyButton;
    [SerializeField] Button _divideButton;

    [SerializeField] Button _equalButton;
    [SerializeField] Button _clearButton;

    [SerializeField] Button _decimalButton;
    [SerializeField] Button _signButton;

    [SerializeField] TMP_Text _displayText;

    // set up the variables
    string _firstNum = "0";
    bool _firstNumSet = false;
    string _secondNum = "0";
    Operation? _operation = null;

    private void Start()
    {
        // set up the character keys to add strings to the current number that is being typed in
        if (_zeroButton != null) _zeroButton.onClick.AddListener(() => AddCharToNumberString('0'));
        if (_oneButton != null) _oneButton.onClick.AddListener(() => AddCharToNumberString('1'));
        if (_twoButton != null) _twoButton.onClick.AddListener(() => AddCharToNumberString('2'));
        if (_threeButton != null) _threeButton.onClick.AddListener(() => AddCharToNumberString('3'));
        if (_fourButton != null) _fourButton.onClick.AddListener(() => AddCharToNumberString('4'));
        if (_fiveButton != null) _fiveButton.onClick.AddListener(() =>AddCharToNumberString('5'));
        if (_sixButton != null) _sixButton.onClick.AddListener(() => AddCharToNumberString('6'));
        if (_sevenButton != null) _sevenButton.onClick.AddListener(() => AddCharToNumberString('7'));
        if (_eightButton != null) _eightButton.onClick.AddListener(() => AddCharToNumberString('8'));
        if (_nineButton != null) _nineButton.onClick.AddListener(() => AddCharToNumberString('9'));
        if (_decimalButton != null) _decimalButton.onClick.AddListener(() => AddCharToNumberString('.'));

        // set up the toggle sign key
        if (_signButton != null) _signButton.onClick.AddListener(() => ToggleSign());

        // set up the 4 operator buttons to input an operation
        if (_plusButton != null) _plusButton.onClick.AddListener(() => SetOperation(Operation.add));
        if (_minusButton != null) _minusButton.onClick.AddListener(() => SetOperation(Operation.subtract));
        if (_multiplyButton != null) _multiplyButton.onClick.AddListener(() => SetOperation(Operation.multiply));
        if (_divideButton != null) _divideButton.onClick.AddListener(() => SetOperation(Operation.divide));

        // set up the equal button to compute the calculation and the clear button to reset
        if (_equalButton != null) _equalButton.onClick.AddListener(() => ComputeCalculation());
        if (_clearButton != null) _clearButton.onClick.AddListener(() => Clear());
        
        // set up the display for the numbers
        if (_displayText != null) _displayText.text = $"";

        // clear the display and set it to the first num which will be eddited first
        Clear();
        SetDisplay(_firstNum);
    }

    /// <summary>
    /// this sets the display of the text
    /// </summary>
    /// <param name="text"> the text you wish to set the display too </param>
    private void SetDisplay(string text) { _displayText.text = text; }

    /// <summary>
    /// this adds a new character to the number that is currently being typed out
    /// if the first number hasn't been set it will add characters to the first number
    /// if the opperation has been set then the second number is being set so add characters to the second number
    /// </summary>
    /// <param name="new_char"> this is the char that will get added to the number that is being typed out </param>
    private void AddCharToNumberString(char new_char)
    {
        if (!_firstNumSet)
        {
            // if the user types in a decimal twice then disregaurd their input because there is already a decimal there
            if (_firstNum.Contains('.') && new_char == '.') return;
            // both numbers are set to 0 by default so that there isn't an error when calculating
            _firstNum = $"{_firstNum.TrimStart('0')}{new_char}";
            SetDisplay(_firstNum);
        }
        else if (_operation != null)
        {
            // if the user types in a decimal twice then disregaurd their input because there is already a decimal there
            if (_secondNum.Contains('.') && new_char == '.') return;
            // both numbers are set to 0 by default so that there isn't an error when calculating
            _secondNum = $"{_secondNum.TrimStart('0')}{new_char}";
            SetDisplay(_secondNum);
        }
    }

    /// <summary>
    /// this will toggle the sign of the number currently being input
    /// </summary>
    private void ToggleSign()
    {
        if (!_firstNumSet)
        {
            if (_firstNum[0] == '-') _firstNum = _firstNum.Replace('-', ' ');
            else if (_firstNum[0] == ' ') _firstNum = _firstNum.Replace(' ', '-');
            else _firstNum = $"-{_firstNum}";
            SetDisplay(_firstNum);
        }
        else if (_operation != null)
        {
            if (_secondNum[0] == '-') _secondNum = _secondNum.Replace(' ', '-');
            else if (_secondNum[0] != ' ') _secondNum = _secondNum.Replace('-', ' ');
            else _secondNum = $"-{_secondNum}";
            SetDisplay(_secondNum);
        }
    }

    /// <summary>
    /// this sets the operation that the calculator will execute
    /// </summary>
    /// <param name="operation"></param>
    private void SetOperation(Operation operation)
    {
        _operation = operation;
        switch (_operation)
        {
            case Operation.add:
                SetDisplay("+");
                _firstNumSet = true;
                break;
            case Operation.subtract:
                SetDisplay("-");
                _firstNumSet = true;
                break;
            case Operation.multiply:
                SetDisplay("*");
                _firstNumSet = true;
                break;
            case Operation.divide:
                SetDisplay("/");
                _firstNumSet = true;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// this will actually execute a calculation
    /// </summary>
    private void ComputeCalculation()
    {
        var firstNum = System.Double.Parse(_firstNum);
        var secondNum = System.Double.Parse(_secondNum);
        double _result = 0;

        switch (_operation)
        {
            case Operation.add:
                _result = firstNum + secondNum;
                SetDisplay($"{firstNum + secondNum}");
                break;
            case Operation.subtract:
                _result = firstNum - secondNum;
                SetDisplay($"{firstNum - secondNum}");
                break;
            case Operation.multiply:
                _result = firstNum * secondNum;
                SetDisplay($"{firstNum * secondNum}");
                break;            
            case Operation.divide:
                try { _result = firstNum / secondNum; }
                catch (System.DivideByZeroException) 
                { 
                    SetDisplay($"error : div by zero");
                    return;
                }
                break;
            default:
                break;
        }

        SetDisplay($"{_result}");
        _firstNumSet = false;
        _firstNum = $"{_result}";
        _secondNum = "0";
        _operation = null;
    }

    // this will reset the calculator
    private void Clear()
    {
        _firstNum = "0";
        _firstNumSet = false;
        _secondNum = "0";
        _operation = null;
        SetDisplay(_firstNum);
    }
}
