namespace UniAppKids.DNNControllers.Models
{
    using DotNetNuke.ComponentModel.DataAnnotations;

    [TableName("PhraseDictionary")]
    public class PhraseDictionary
    {
    
        public int PhraseDictionaryId { get; set; }

        public string UserName { get; set; }

        public string DictionaryName { get; set; }
    }
}