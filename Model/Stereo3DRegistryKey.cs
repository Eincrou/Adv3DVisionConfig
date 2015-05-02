using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Advanced3DVConfig.Model
{
    public class Stereo3DRegistryKey
    {
        public string KeyName { get; private set; }
        public int KeyValue { get; set; }
        public int KeyDefaultValue { get; set; }
        public Stereo3DRegistryKey(string keyName, int keyValue)
        {
            KeyName = keyName;
            KeyValue = keyValue;
            KeyDefaultValue = Stereo3DRegistryKeyDefaults.GetDefaultKeyValue(keyName);
            //   KeyDefaultValue = Stereo3DRegistryKeyDefaults.GetDefaultKeyValue(keyType);
        }
    }
}
