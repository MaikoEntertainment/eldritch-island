using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITaskBase : MonoBehaviour
{
    public Image icon;
    public Image loadBar;
    public TextMeshProUGUI progress;
    public TextMeshProUGUI iterations;
    public Transform monsterList;
    public RectTransform clickPlusArea;

    public UITaskMonster monsterPrefab;
    public ClickPlusText plusTextPrefab;

    private Task task;
    public void Load(Task task)
    {
        this.task = task;
        icon.sprite = task.GetTask().GetIcon();
        task.onUpdate += UpdateValues;
        UpdateValues(task);
    }

    public void UpdateValues(Task task)
    {
        ClearMonsters();
        foreach (Monster m in task.GetMonsters())
        {
            Instantiate(monsterPrefab.gameObject, monsterList).GetComponent<UITaskMonster>().Load(m);
        }
        double progressMade = task.GetProgressMade();
        double progressGoal = (long)task.GetProgressGoal();
        progress.text = Utils.ToFormat(progressMade) + "/" + Utils.ToFormat(progressGoal);
        iterations.text = task.GetIsInfinite() ? "" : (task.GetIterationsLeft()).ToString();
        loadBar.fillAmount = (float)(progressMade / progressGoal);
    }

    public void OnClick()
    {
        double progress = task.OnProgressClick();
        ClickPlusText plus = Instantiate(plusTextPrefab.gameObject, clickPlusArea).GetComponent<ClickPlusText>();
        plus.Load("+" + progress);
        Vector2 place = new Vector2(clickPlusArea.sizeDelta.x * Random.value, clickPlusArea.sizeDelta.y * Random.value);
        plus.transform.localPosition = place;
    }

    public void ClearMonsters()
    {
        foreach (Transform m in monsterList)
        {
            Destroy(m.gameObject);
        }
    }

    public void CancelTask()
    {
        task.CancelTask();
    }

    private void OnDisable()
    {
        task.onUpdate -= UpdateValues;
    }

}
