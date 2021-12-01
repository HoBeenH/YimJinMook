using Script.Util;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.UI;
using static Script.Util.Facade;

namespace Script.Manager
{
    public class UiManager : MonoSingleton<UiManager>
    {
        public Image healthUI;

        public void ChangeHealthValue(float maxHealth, float currentHealth) =>
            healthUI.fillAmount = maxHealth / currentHealth;
    }
}