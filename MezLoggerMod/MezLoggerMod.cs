using UnityEngine;
using UnityEngine.UI;
using MelonLoader;
using System.Collections;
using TMPro;
using Obj = UnityEngine.GameObject;

[assembly: MelonInfo(typeof(MezLogger.MezLogger), "MezLogger", "0.2.0", "Mezque", "github.com/Mezque/MezLoggerMod")]
namespace MezLogger
    {
    public class MezLogger : MelonMod
        {
        public override void OnApplicationStart()
            {
            MelonCoroutines.Start(MakeUI());

            MelonLogger.MsgCallbackHandler += (_, __, a, b) => MelonCoroutines.Start(MezText(b, 1, 2)); //thank you xKiraiChan for helping with this part and for giving this mod idea :)
            MelonLogger.WarningCallbackHandler += (a, b) => MelonCoroutines.Start(MezText(b, 2, 4));
            MelonLogger.ErrorCallbackHandler += (a, b) => MelonCoroutines.Start(MezText(b, 3, 6));
            }

        private static string ClientName = "MezLogger";
        private static string PrimaryColour = "#6A329F";
        private static string SecondaryColour = "#a1dcff";
        private static Vector3 UIPosition = new Vector3(-20, -300, 0);
        private static float TextSpacing = 25f;
        public static IEnumerator MakeUI()
            {
            while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
                yield return new WaitForSeconds(1f);
            GameObject GUI, text;
            (GUI = Obj.Find("/UserInterface").transform.Find("UnscaledUI/HudContent/Hud/AlertTextParent/Capsule").gameObject).SetActive(true);
            text = GUI.transform.Find("Text").gameObject;
            yield return new WaitForEndOfFrame();
            GUI.transform.localPosition = UIPosition;
            Obj.DestroyImmediate(GUI.transform.GetComponent<HorizontalLayoutGroup>());
            Obj.DestroyImmediate(GUI.transform.GetComponent<ContentSizeFitter>());
            Obj.DestroyImmediate(GUI.transform.GetComponent<ContentSizeFitter>());
            Obj.DestroyImmediate(GUI.transform.GetComponent<ImageThreeSlice>());
            GUI.gameObject.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            GUI.gameObject.AddComponent<VerticalLayoutGroup>().spacing = TextSpacing;
            var textMesh = text.GetComponent<TextMeshProUGUI>();
            textMesh.alignment = TextAlignmentOptions.Left;
            textMesh.text = $"<color={PrimaryColour}>[{ClientName}]</color> ";
            yield return new WaitForEndOfFrame();
            text.SetActive(false);
            }
        
        private static IEnumerator MezText(string Text, int TextType, float timeBeforeDeletion)
            {
            GameObject textObj;
            try

                {
                textObj = Object.Instantiate(Obj.Find("UserInterface/UnscaledUI/HudContent/Hud/AlertTextParent/Capsule/Text").gameObject, Obj.Find("UserInterface/UnscaledUI/HudContent/Hud/AlertTextParent/Capsule").transform);
                var textMesh = textObj.GetComponent<TextMeshProUGUI>();
                textMesh.text += TextType switch { 1 => "", 2 => "<color=red>[ERROR]</color> ", 3 => "<color=yellow>[Warning]</color> ", _ => "Something broke whoops" } + $"<color={SecondaryColour}>{Text}</color>";
                textObj.SetActive(true);
                }
            catch { yield break; }
            yield return new WaitForSeconds(timeBeforeDeletion);
            Obj.Destroy(textObj);
            }
        }
    }
