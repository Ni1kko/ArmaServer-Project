namespace ArmaServerBackend
{
    /// <summary>
    /// New config method
    /// </summary>
    public class ConfigSetting : Helpers
    {
        public string Name { get; set; }
        public object? Value { get; set; }

        /// <summary>
        /// Config Setting
        /// </summary>
        /// <param name="name">Name of item</param>
        /// <param name="value">value of item</param>
        public ConfigSetting(string name, object value = default)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Convent to user friendly string
        /// </summary>
        /// <returns></returns>
        public override string ToString() => NewOption(Name, Value.ToString().ToLower());
    }
}
