using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool active;

    public void ChangeLocale(int localeID)//Llamamos a este metodo para cambiar el idioma, el 0 es ingles, el 1 español
    {
        if (active)
        {
            return;
        }

        StartCoroutine(SetLocale(localeID));

    }

    IEnumerator SetLocale(int localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        active = false;
    }
    

}
