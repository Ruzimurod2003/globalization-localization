namespace EaasyGlobalization.ViewModels
{
    public class ResourceEditVM
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string CultureName { get; set; }
        public List<string> CultureNames { get; set; }
    }
}
