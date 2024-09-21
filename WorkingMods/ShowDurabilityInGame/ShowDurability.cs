using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ShowDurability
{
    public class ShowDurabilityInGame : MelonMod
    {
        public bool IsSceneActive = false;
        public ManagersScript _MS;
        public Image _BGtool;
        GameObject _BGtoolActiveSelf;
        GameObject _BGshieldActiveSelf;
        public Image _BGshield;
        public Text _TextSLOT1;
        public Text _TextSLOT2;
        public Image _IMGSLOT1;
        public Image _IMGSLOT2;

        public float _BGtoolCooldown;
        public float _BGshieldCooldown;
        public RectTransform _RectSLOT1;
        public RectTransform _RectSLOT2;


        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (SceneManager.GetActiveScene().IsValid() == true && sceneName == "scene_01")//Is scene awake?
            {
                IsSceneActive = true;
                //------Create-OBJ------//
                GameObject Canvas = new GameObject("Durability");
                Canvas.transform.parent = GameObject.Find("StaticGroup/MainCanvas").transform;
                Canvas.transform.SetSiblingIndex(1);
                RectTransform RectCanvas = Canvas.AddComponent<RectTransform>();
                RectCanvas.anchorMax = Vector2.zero;
                RectCanvas.anchorMin = Vector2.zero;
                RectCanvas.anchoredPosition = new Vector2(1, 105);
                RectCanvas.localScale = new Vector3(0.6f, 0.6f, 0);
                RectCanvas.sizeDelta = new Vector2(95, 95);
                //------Create-IMG------//
                CreateImageBox.IMGCreate(true, 75, 50, 50, 89, true, "StaticGroup/MainCanvas/Durability/", "bg_shield", false);
                CreateImageBox.IMGCreate(true, 75, 50, 50, 55, true, "StaticGroup/MainCanvas/Durability/", "bg_sword_or_tool", false);
                CreateImageBox.IMGCreate(false, 35, 35, 51, 25, true, "StaticGroup/MainCanvas/Durability/bg_sword_or_tool", "sword_or_tool", true);
                CreateImageBox.IMGCreate(false, 35, 35, 51, 25, true, "StaticGroup/MainCanvas/Durability/bg_shield", "shield", true);
                //----Sets-VARIABLES----//
                _BGtool = GameObject.Find("StaticGroup/MainCanvas/Durability/bg_sword_or_tool").GetComponent<Image>();
                _BGtoolActiveSelf = GameObject.Find("StaticGroup/MainCanvas/Durability/bg_sword_or_tool");
                _BGshield = GameObject.Find("StaticGroup/MainCanvas/Durability/bg_shield").GetComponent<Image>();
                _BGshieldActiveSelf = GameObject.Find("StaticGroup/MainCanvas/Durability/bg_shield");
                _TextSLOT1 = GameObject.Find("StaticGroup/MainCanvas/Durability/bg_sword_or_tool/sword_or_tool/text_durability").GetComponent<Text>();
                _TextSLOT2 = GameObject.Find("StaticGroup/MainCanvas/Durability/bg_shield/shield/text_durability").GetComponent<Text>();
                _IMGSLOT1 = GameObject.Find("StaticGroup/MainCanvas/Durability/bg_sword_or_tool/sword_or_tool").GetComponent<Image>();
                _IMGSLOT2 = GameObject.Find("StaticGroup/MainCanvas/Durability/bg_shield/shield").GetComponent<Image>();
                _RectSLOT1 = GameObject.Find("StaticGroup/MainCanvas/Durability/bg_sword_or_tool").GetComponent<RectTransform>();
                _RectSLOT2 = GameObject.Find("StaticGroup/MainCanvas/Durability/bg_shield").GetComponent<RectTransform>();
                _MS = GameObject.Find("Managers").GetComponent<ManagersScript>();
                //FINALIZER//
                RectCanvas.localScale = new Vector3(0.8f, 0.8f, 0);
            }
            else
            {
                IsSceneActive = false;
            }
        }
        public override void OnUpdate()
        {
            if (IsSceneActive == true)
            {
                if (_MS.gameMN.pMove.common.equip[1].itemLife >= 1 && _MS.gameMN.pMove.common.equip[1].itemLife != Convert.ToInt32(_TextSLOT1.text))
                {
                    float FullDurability = _MS.itemMN.FindItem(_MS.gameMN.pMove.common.equip[1].itemKey).itemLife;
                    float EquipDurability = _MS.gameMN.pMove.common.equip[1].itemLife;
                    float mathColor = Mathf.Round(EquipDurability / FullDurability * 510);
                    _TextSLOT1.text = EquipDurability.ToString();
                    if (Mathf.Round(mathColor) >= 255)
                    {
                        _TextSLOT1.color = new Color32(Convert.ToByte(Mathf.Clamp(Mathf.Abs(mathColor - 510), 0, 255)), 255, 0, 255);
                    }
                    else if (Mathf.Round(mathColor) >= 0)
                    {
                        _TextSLOT1.color = new Color32(255, Convert.ToByte(Mathf.Clamp(Mathf.Abs(mathColor), 0, 255)), 0, 255);
                    }
                }
                else if (_MS.gameMN.pMove.common.equip[1].itemLife == 0 && _BGtoolActiveSelf.activeSelf == true)
                {
                    _BGtoolActiveSelf.SetActive(false);
                }
                else if (_MS.gameMN.pMove.common.equip[1].itemLife >= 1 && _BGtoolActiveSelf.activeSelf == false)
                {
                    _BGtoolActiveSelf.SetActive(true);
                }
                if (_MS.gameMN.pMove.common.equip[1].itemImage != _IMGSLOT1.sprite && _IMGSLOT1 == true)
                {
                    _IMGSLOT1.overrideSprite = _MS.gameMN.pMove.common.equip[1].itemImage;
                }
                if (_MS.gameMN.pMove.common.equip[6].itemLife >= 1 && _MS.gameMN.pMove.common.equip[6].itemLife != Convert.ToInt32(_TextSLOT2.text))
                {
                    float FullDurability = _MS.itemMN.FindItem(_MS.gameMN.pMove.common.equip[6].itemKey).itemLife;
                    float EquipDurability = _MS.gameMN.pMove.common.equip[6].itemLife;
                    float mathColor = Mathf.Round(EquipDurability / FullDurability * 510);
                    _TextSLOT2.text = EquipDurability.ToString();
                    if (Mathf.Round(mathColor) >= 255)
                    {
                        _TextSLOT2.color = new Color32(Convert.ToByte(Mathf.Clamp(Mathf.Abs(mathColor - 510), 0, 255)), 255, 0, 255);
                    }
                    else if (Mathf.Round(mathColor) >= 0)
                    {
                        _TextSLOT2.color = new Color32(255, Convert.ToByte(Mathf.Clamp(Mathf.Abs(mathColor), 0, 255)), 0, 255);
                    }
                }
                else if (_MS.gameMN.pMove.common.equip[6].itemLife == 0 && _BGshieldActiveSelf.activeSelf == true)
                {
                    _BGshieldActiveSelf.SetActive(false);
                }
                else if (_MS.gameMN.pMove.common.equip[6].itemLife >= 1 && _BGshieldActiveSelf.activeSelf == false)
                {
                    _BGshieldActiveSelf.SetActive(true);
                }
                if (_MS.gameMN.pMove.common.equip[6].itemImage != _IMGSLOT2.sprite && _IMGSLOT2 == true)
                {
                    _IMGSLOT2.overrideSprite = _MS.gameMN.pMove.common.equip[6].itemImage;
                }
            }
        }
    }
}
