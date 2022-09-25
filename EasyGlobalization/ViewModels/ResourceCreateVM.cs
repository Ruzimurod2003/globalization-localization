namespace EasyGlobalization.ViewModels
{
    public class ResourceCreateVM
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string CultureName { get; set; }
        public List<string> CultureNames { get; set; }
    }
}
