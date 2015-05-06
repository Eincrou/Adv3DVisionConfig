namespace Advanced3DVConfig.Model
{
    public class SettingsProfileEntry
    {
        public string KeyName { set; get; }
        public int Value { get; set; }
        public SettingsProfileEntry()
        {
            
        }

        public SettingsProfileEntry(string keyName, int value)
        {
            KeyName = keyName;
            Value = value;
        }
    }
}
