using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITaskBase : MonoBehaviour
{
    public Image icon;
    public Image loadBar;
    public Button edit;
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
        if (MonsterMaster.GetInstance().GetActiveMonsters().Count == 0)
            edit.gameObject.SetActive(false);
        MonsterMaster.GetInstance().onMonsterActivated += AddEditButton;
    }

    public void AddEditButton(Monster m)
    {
        edit.gameObject.SetActive(true);
    }

    public void UpdateValues(Task task)
    {
        ClearMonsters();
        foreach (Monster m in task.GetMonsters())
        {
            UITaskMonster taskMonster = Instantiate(monsterPrefab.gameObject, monsterList).GetComponent<UITaskMonster>();
            taskMonster.Load(m);
            taskMonster.OnDrag += OnDragMonster;
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
            m.GetComponent<UITaskMonster>().OnDrag -= OnDragMonster;
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
        MonsterMaster.GetInstance().onMonsterActivated -= AddEditButton;
    }

    public void OpenEdit()
    {
        UIMonsterPickerQuickMaster.GetInstance().ShowMonsterPickerQuick(task);
    }

    public Task GetTask() { return task; }

    public void OnDragMonster(Monster m)
    {
        UIMonsterDraggerMaster.GetInstance().CreateMonsterDragger(m, task);
    }
}
