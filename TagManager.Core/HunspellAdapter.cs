namespace TagManager
{
    using NHunspell;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    public class HunspellAdapter
   {
      private readonly string AffFilePath = @"D:\\Projects\\TagManager\\TagManager.Core\\" + ConfigurationManager.AppSettings["AffFilePath"];

      private readonly string DictionaryFilePath = @"D:\\Projects\\TagManager\\TagManager.Core\\" + ConfigurationManager.AppSettings["DictionaryFilePath"];

      private readonly string DatFilePath = @"D:\\Projects\\TagManager\\TagManager.Core\\" + ConfigurationManager.AppSettings["DatFilePath"];

      private Hunspell hunSpell;
      private MyThes thesaurus;

      public HunspellAdapter()
      {
         this.hunSpell = new Hunspell(AffFilePath, DictionaryFilePath);
         this.thesaurus = new MyThes(DatFilePath);
      }

      public List<string> GetStem(string word)
      {
         return hunSpell.Stem(word);
      }

      public List<string> GetSuggestions(string word)
      {
         return hunSpell.Suggest(word);
      }

      public List<string> GetAnalysis(string word)
      {
         return hunSpell.Analyze(word);
      }

      public List<string> GetSynonyms(string word)
      {
         var result = new List<string>();
         var stemmedWordResult = this.GetStem(word);
         if (stemmedWordResult.Any())
         {
            foreach (var stemmedWord in stemmedWordResult)
            {
               if (!string.IsNullOrEmpty(stemmedWord))
               {
                  var thesaurusResult = this.GetLookup(stemmedWord);

                  if (thesaurusResult != null && thesaurusResult.Meanings != null && thesaurusResult.Meanings.Any())
                  {
                     thesaurusResult.Meanings.ForEach(m => m.Synonyms
                        .ToList()
                        .ForEach(s => result.Add(s.ToLower()))
                     );
                  }
                  result.Add(stemmedWord);
               }
            }
         }

         return result;
      }

      public bool GetSpellCheck(string word)
      {
         return hunSpell.Spell(word);
      }

      public ThesResult GetLookup(string word)
      {
         return thesaurus.Lookup(word, hunSpell);
      }
   }
}
