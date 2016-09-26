using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagManager.MediaItems
{
    public class TagManager : DomainService, ITagManager
    {
        private readonly IRepository<Tag> tagRepository;
        private readonly IRepository<MediaTag> mediaTagRepository;

        private HunspellAdapter hunspellAdapter;

        private List<string> DutchStopwords
        {
            get; set;
        }

        public TagManager(IRepository<Tag> tagRepository, IRepository<MediaTag> mediaTagRepository)
        {
            this.hunspellAdapter = new HunspellAdapter();
            this.tagRepository = tagRepository;
            this.mediaTagRepository = mediaTagRepository;

            // Load the list of dutch stop words
            var stopwordsFilePath = @"D:\\Projects\\TagManagement\\TagManagement.Api\\" + @"Tools\\stop-words_dutch_1_nl.txt";
            this.DutchStopwords = File.ReadAllLines(stopwordsFilePath).ToList();
            this.DutchStopwords.AddRange(new List<string> { "man", "bijt", "hond" });
        }
        public List<PreprocessedTag> PreprocessTags(List<string> tags)
        {
            var result = new List<PreprocessedTag>();
            var distinct = tags.Distinct();
            foreach (var tag in distinct)
            {
                var preprocessedTag = new PreprocessedTag();
                preprocessedTag.TagValue = tag;
                var words = tag.Split(' ');
                preprocessedTag.IsSpelledCorrect = words.All(word => this.hunspellAdapter.GetSpellCheck(word));
                if (!preprocessedTag.IsSpelledCorrect)
                {
                    preprocessedTag.Suggestions = words.SelectMany(word => this.hunspellAdapter.GetSuggestions(word)).ToList();
                }

                result.Add(preprocessedTag);
            }

            return result;
        }

        public List<ProcessedGroup> ProcessTags(List<string> tags)
        {
            var result = new List<ProcessedTag>();

            var counter = 1;

            // Filter the list of tags by the list of stop-words and group the result set by unique values.
            var groupedList = tags.Where(t => !DutchStopwords.Contains(t)).GroupBy(t => t.ToLower(), (key, group) => new { Value = key, Occurences = group.Count() }).ToList();

            // Add a unique identifier to the list.
            var processingList = groupedList.Select(t => new ProcessedTag { Id = counter++, Value = t.Value, Occurences = t.Occurences }).ToList();

            foreach (var tag in processingList)
            {
                this.ProcessTag(tag, processingList);
            }

            var groups = (from r in processingList
                          let identifier = r.DuplicateTagId ?? r.RelatedTagId ?? r.Id
                          group r by identifier into groupedResults
                          select new ProcessedGroup
                          {
                              GroupId = groupedResults.Key,
                              Tags = groupedResults.ToList()
                          }).ToList();

            return groups;
        }

        private List<ProcessedTag> ProcessTag(ProcessedTag tagToCheck, List<ProcessedTag> tagList)
        {
            var returnList = new List<ProcessedTag>();
            var stemmed = hunspellAdapter.GetStem(tagToCheck.Value);
            if (stemmed.Any())
            {
                tagToCheck.Value = stemmed.Last();
            }
            var tagSynonyms = GetTagSynonyms(tagToCheck);
            tagToCheck.MediaTag = FindExistingMediaTag(tagToCheck);

            var result = (from t in tagList
                          where t.Id != tagToCheck.Id
                          where !t.DuplicateTagId.HasValue
                          where !t.RelatedTagId.HasValue
                          let tagWords = GetTagWords(t.Value)
                          let foundDuplicateTagId = !tagToCheck.IsDuplicateReference && t.Value.ToLower() == tagToCheck.Value.ToLower() ? t.Id : (int?)null
                          let foundPartialDuplicateTagId = !tagToCheck.IsRelatedReference && tagWords.Any(a => GetTagWords(tagToCheck.Value).Select(wpr => wpr.ToLower()).Contains(a.ToLower())) ? t.Id : (int?)null
                          let foundSynonymTagId = !tagToCheck.IsRelatedReference && tagWords.Any(w => tagSynonyms.Contains(w.ToLower())) ? t.Id : (int?)null
                          let sortOrderValue = Convert.ToInt32(foundDuplicateTagId.HasValue) + Convert.ToInt32(foundPartialDuplicateTagId.HasValue) + Convert.ToInt32(foundSynonymTagId.HasValue)
                          orderby sortOrderValue descending
                          select new
                          {
                              DuplicateTagId = foundDuplicateTagId,
                              RelatedTagId = foundPartialDuplicateTagId ?? foundSynonymTagId
                          }).FirstOrDefault();

            if (result != null)
            {
                if (result.DuplicateTagId.HasValue)
                {
                    tagList.Find(t => t.Id == tagToCheck.Id).DuplicateTagId = result.DuplicateTagId;
                    tagList.Find(t => t.Id == result.DuplicateTagId).IsDuplicateReference = true;
                }

                if (result.RelatedTagId.HasValue)
                {
                    tagList.Find(t => t.Id == tagToCheck.Id).RelatedTagId = result.RelatedTagId;
                    tagList.Find(t => t.Id == result.RelatedTagId).IsRelatedReference = true;
                }
            }

            return tagList;
        }

        private List<string> GetTagSynonyms(ProcessedTag tag)
        {
            var result = new List<string>();
            var tagWords = GetTagWords(tag.Value);
            var searchWords = new List<string>();
            foreach (var word in tagWords)
            {
                if (!hunspellAdapter.GetSpellCheck(word))
                {
                    var suggestions = hunspellAdapter.GetSuggestions(word);
                    if (suggestions.Any())
                    {
                        tag.Value = suggestions.First();
                    }

                }
                else
                {
                    if (tagWords.Count == 1)
                    {
                        tag.IsSpelledCorrect = true;
                    }
                    searchWords.Add(word);
                }
            }
            searchWords.ForEach(w => result.AddRange(hunspellAdapter.GetSynonyms(w)));
            return result;
        }

        private List<string> GetTagWords(string tag)
        {
            var wordList = new List<string>();
            var splitted = tag.Split(' ');
            Array.ForEach(splitted, wordList.Add);

            return wordList;
        }

        private MediaTag FindExistingMediaTag(ProcessedTag tag)
        {
            return this.mediaTagRepository.GetAll().Where(mt => mt.Value == tag.Value).SingleOrDefault();
        }
    }
}
