using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmMsg : MonoBehaviour
{
    public Text textMsg;

    UnityAction funcConfirm;
    UnityAction funcCancel;

    public void Set(string msg, UnityAction funcConfirm = null, UnityAction funcCancel = null)
    {
        textMsg.text = msg;
        this.funcConfirm = funcConfirm;
        this.funcCancel = funcCancel;
    }

    public void OnConfirm()
    {
        funcConfirm?.Invoke();
        Destroy(this);
    }

    public void OnCancel()
    {
        funcCancel?.Invoke();
        Destroy(this);
    }
}
