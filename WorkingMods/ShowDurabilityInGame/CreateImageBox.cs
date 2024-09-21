using UnityEngine;
using UnityEngine.UI;

namespace ShowDurability
{
    internal class CreateImageBox
    {
        public static void IMGCreate(bool SliceAndSetDefault,float size_x,float size_y, float anchor_x, float anchor_y, bool SetAsAParent,string ParentName,string name, bool CreateTextBox)
        {
            //--------Create-the-img--------
            GameObject ImageBox = new GameObject(name);
            //------Set-layer-and-scale------
            ImageBox.layer = LayerMask.NameToLayer("UI");
            ImageBox.transform.localScale = new Vector3(1, 1, 0);
            //------Adds-and-find-comps------
            ImageBox.AddComponent<Image>();
            Image _compIMGImage = ImageBox.GetComponent<Image>();
            ImageBox.AddComponent<RectTransform>();
            RectTransform _compIMGRect = ImageBox.GetComponent<RectTransform>();
            //---------Set-as-Parent---------
            var Display = GameObject.Find("StaticGroup/MainCanvas");
            if (SetAsAParent == false) 
            {
                ImageBox.transform.parent = Display.transform;
            }
            //-----------Settings-----------
            _compIMGRect.anchorMax = Vector2.zero;
            _compIMGRect.anchorMin = Vector2.zero;
            if (SliceAndSetDefault == true)
            {
                _compIMGImage.overrideSprite = GameObject.Find("StaticGroup/MainCanvas/InventoryPanel").GetComponent<Image>().sprite;
                _compIMGImage.type = Image.Type.Sliced;
                _compIMGImage.pixelsPerUnitMultiplier = 4;
            }
            if (SetAsAParent == true)
            {
                ImageBox.transform.parent = GameObject.Find(ParentName).transform;
            }
            if (CreateTextBox == true)//ADDS TextBox
            {
                GameObject DurabilityText = new GameObject("text_durability");
                DurabilityText.transform.parent = ImageBox.transform;
                DurabilityText.AddComponent<Text>();
                DurabilityText.AddComponent<Outline>();
                DurabilityText.AddComponent<RectTransform>();
                DurabilityText.layer = LayerMask.NameToLayer("UI");
                RectTransform _compTEXTRect = DurabilityText.GetComponent<RectTransform>();
                _compTEXTRect.anchorMin = Vector2.zero;
                _compTEXTRect.anchorMax = Vector2.zero;
                _compTEXTRect.anchoredPosition = new Vector2(17.5f,8);
                _compTEXTRect.sizeDelta = new Vector2(50,50);
                Text _compTEXTtext = DurabilityText.GetComponent<Text>();
                _compTEXTtext.color = Color.green;
                _compTEXTtext.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                _compTEXTtext.text = "0";
                _compTEXTtext.alignment = TextAnchor.MiddleCenter;
                _compTEXTtext.fontSize = 16;
            }
            _compIMGRect.sizeDelta = new Vector2(size_x,size_y);
            _compIMGRect.anchoredPosition = new Vector2(anchor_x,anchor_y);
        }
    }
}
