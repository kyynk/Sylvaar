using System.Collections.Generic;

namespace AVG
{
    [System.Serializable]
    public class DialogData
    {
        public int id;
        public string characterName;
        public string characterExpression;
        public string characterPosition;
        public string eventImage;
        public string soundEffect;
        public string dialogText;
        public List<string> choices;
        public List<int> nextSceneIds;
        public string animation;
        public string voiceClip;
        public string displayType;
    }
}
