// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdatePhraseDictionaryInput.cs" company="uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_APPKids.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Uni_AppKids.Core.EntityModels;

    public class UpdatePhraseDictionaryInput
    {
        [Range(1, int.MaxValue)]
        public int DictionaryId { get; set; }

        [Range(1, 20)]
        public string DictionaryName { get; set; }

        public List<Phrase> Phrases { get; set; }

        public void AddValidationErrors(List<ValidationResult> results)
        {
            if (string.IsNullOrEmpty(this.DictionaryName))
            {
                results.Add(
                    new ValidationResult(
                        "Dictionary name can not be empty!!!",
                        new[] { "AssignedPersonId", "State" }));
            }

            if (this.DictionaryId < 0)
            {
                results.Add(
                    new ValidationResult(
                        "Dictionary Id can not be smaller than 0!!!",
                        new[] { "DictionaryID", "State" }));
            }
        }

        public override string ToString()
        {
            return string.Format(
                "[UpdateDictionary > Dictionary Id = {0}, Dictionary name = {1}]", 
                this.DictionaryId, 
                this.DictionaryName);
        }
    }
}
