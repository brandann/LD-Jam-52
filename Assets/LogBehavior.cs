using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogBehavior : MonoBehaviour
{
    private TextMeshProUGUI LogText;
    private Queue<string> log_list;


    // Start is called before the first frame update
    void Start()
    {
        LogText = this.GetComponent<TextMeshProUGUI>();
        log_list = new Queue<string>();
        StartCoroutine(PopQueueTimer());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }


    public void AddLog(string s)
    {
        log_list.Enqueue(s);
        getStringList();
    }

    private void getStringList()
    {
        string li = ""; ;
        if (log_list.Count > 0)
        {
            foreach (string s in log_list)
            {
                li += s + '\n';
            }
        }

        LogText.text = li;
    }

    IEnumerator PopQueueTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            if(log_list.Count > 0)
                log_list.Dequeue();
            getStringList();
        }
    }
}
