namespace Advanced3DVConfig.Model
{
    public class SettingsProfileEntry
    {
        public string KeyName { set; get; }
        public uint Value { get; set; }
        public SettingsProfileEntry()
        {
            
        }

        public SettingsProfileEntry(string keyName, uint value)
        {
            KeyName = keyName;
            Value = value;
        }
    }
}
