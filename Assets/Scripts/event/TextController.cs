using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public string[] scenarios;
    [SerializeField]
    Text uiText;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;

    private string currentText = string.Empty;
    private float timeUntilDisplay = 0;
    private float timeElapsed = 1;
    private int currentLine = 0;
    private int lastUpdateCharacter = -1;
    private bool hitflag;              //接触フラグ

    public GameObject textUIset;       //2次元UIのCanvasのテキストイベント用UIを指定
    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }


    void Update()
    {
        // 文字の表示が完了してるならクリック時に次の行を表示する
        if (IsCompleteDisplayText)
        {
            if (currentLine < scenarios.Length && Input.GetKeyDown(KeyCode.Return))
            {
                SetNextLine();
            }
            else if (currentLine == scenarios.Length && Input.GetKeyDown(KeyCode.Return))
            {
                textUIset.SetActive(false);
                Destroy(this.gameObject);
            }
        }
        else
        {
            // 完了してないなら文字をすべて表示する
            if (Input.GetKeyDown(KeyCode.Return))
            {
                timeUntilDisplay = 0;
            }
        }

        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
        if (displayCharacterCount != lastUpdateCharacter)
        {
            uiText.text = currentText.Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }
    }


    void SetNextLine()
    {
        currentText = scenarios[currentLine];
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        currentLine++;
        lastUpdateCharacter = -1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hitflag)
        {
            hitflag = true;
            SetNextLine();
            textUIset.SetActive(true);
        }
    }
}

