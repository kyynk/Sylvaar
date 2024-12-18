using System.Collections.Generic;
using UnityEngine.Serialization;

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
        [FormerlySerializedAs("nextSceneIds")] public List<int> nextSceneIDs;
        public string animation;
        public string voiceClip;
        public string displayType;
    }
}
