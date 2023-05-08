using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;

public static class TMPTweenExtension
{
    public static TweenerCore<string, string, StringOptions> DOType(this TMP_Text target, string endValue, float duration)
    {
        if (endValue == null)
        {
            if (Debugger.logPriority > 0) Debugger.LogWarning("You can't pass a NULL string to DOText: an empty string will be used instead to avoid errors");
            endValue = "";
        }

        var t = DOTween.To(() => target.text, x => target.text = x, endValue, endValue.Length / duration);
        t.SetLink(target.gameObject);
        return t;
    }

    public static TweenerCore<string, string, StringOptions> DOType(this TMP_InputField target, string endValue, float duration)
    {
        if (endValue == null)
        {
            if (Debugger.logPriority > 0) Debugger.LogWarning("You can't pass a NULL string to DOText: an empty string will be used instead to avoid errors");
            endValue = "";
        }

        var t = DOTween.To(() => target.text, x => target.text = x, endValue, endValue.Length / duration);
        t.SetLink(target.gameObject);
        return t;
    }
}
