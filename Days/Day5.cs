using System.Diagnostics;
using System.Text.RegularExpressions;
using App;
namespace Days;

public class Day5 : ISolve
{
    List<PageNumbers> _pageNumbers = new List<PageNumbers>();
    List<List<int>> _updates = new List<List<int>>();
    public string SolvePartOne(string[] input)
    {

        _pageNumbers.Clear();
        _updates.Clear(); ;
        for (int i = 0; i < input.Length; i++)
        {
            ProcessLine(input, _pageNumbers, i);

        }

        return $"{CountGoodUpdates()}";
    }

    public string SolvePartTwo(string[] input)
    {
        _pageNumbers.Clear();
        _updates.Clear(); ;
        for (int i = 0; i < input.Length; i++)
        {
            ProcessLine(input, _pageNumbers, i);
        }

        return $"{CountGoodUpdates(true)}";
    }

    public int CountGoodUpdates(bool correction = false)
    {

        var count = 0;
        foreach (var update in _updates)
        {
            if (IsGoodOrder(update) && !correction)
            {
                count += TakeMiddel(update);
            }
            else if (!IsGoodOrder(update) && correction)
            {
                count += TakeMiddel(FixUpdate(update));
            }
        }
        return count;
    }

    private List<int> FixUpdate(List<int> update)
    {
        var correcctedLIst = new List<int>();
        var recheck = false;
        for (int i = 0; i < update.Count; i++)
        {
            if (recheck)
            {
                i = 0;
                recheck = false;
            }
            int nr = update[i];
            var pageNumer = _pageNumbers.FirstOrDefault(x => x.PageNumber == nr);
            if (pageNumer == null)
            {
                continue;
            }
            var after = update[(i + 1)..];
            var before = update[..i];
            if (before.Any(pageNumer.PageNumbersAfter.Contains))
            {
                recheck = true;
                var BadpagesInBefore = pageNumer.PageNumbersAfter.Where(before.Contains).ToList();
                var pagesInBeforeGoodORder = before.Where(x => !pageNumer.PageNumbersAfter.Contains(x)).ToList();
                before = pagesInBeforeGoodORder;
                after.InsertRange(0, BadpagesInBefore);
            }
            if (after.Any(pageNumer.PageNumbersBefore.Contains))
            {
                recheck = true;
                var BadpagesInAfter = pageNumer.PageNumbersBefore.Where(after.Contains).ToList();
                var pagesInAfterGoodORder = after.Where(x => !pageNumer.PageNumbersBefore.Contains(x)).ToList();
                after = pagesInAfterGoodORder;
                before.InsertRange(0, BadpagesInAfter);
            }

            if (recheck)
            {
                correcctedLIst = before;
                correcctedLIst.Add(nr);
                correcctedLIst.AddRange(after);
                update = correcctedLIst;
            }
        }
        return update;
    }

    private int TakeMiddel(List<int> update)
    {
        return update[update.Count / 2];
    }

    private bool IsGoodOrder(List<int> update)
    {
        for (int i = 0; i < update.Count; i++)
        {
            int nr = update[i];
            var pageNumer = _pageNumbers.FirstOrDefault(x => x.PageNumber == nr);
            if (pageNumer == null)
            {
                continue;
            }
            var after = update[(i + 1)..];
            var before = update[..i];
            try
            {
                if (before.Any(pageNumer.PageNumbersAfter.Contains))
                {
                    return false;
                }
                if (after.Any(pageNumer.PageNumbersBefore.Contains))
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        return true;
    }

    private void ProcessLine(string[] input, List<PageNumbers> pageNumbers, int i)
    {
        var pagenumberInput = Regex.Match(input[i], @"(\d+)\|(\d+)");
        if (pagenumberInput.Success)
        {
            AddPageRule(pagenumberInput);
        }
        var updateInputFound = Regex.Match(input[i], @"^\d+[,\d+]*$");
        if (updateInputFound.Success)
        {
            AddUpdates(input[i]);
        }
    }

    private void AddUpdates(string v)
    {
        var ls = v.Split(",").Select(x => int.Parse(x.Trim())).ToList();
        _updates.Add(ls);
    }

    private void AddPageRule(Match pagenumberInput)
    {
        _ = int.TryParse(pagenumberInput.Groups[1].Value, out var pageNrPefore);
        _ = int.TryParse(pagenumberInput.Groups[2].Value, out var pageNrNext);

        // Find page before and add next nr to its list and also reverse
        var pageBefore = _pageNumbers.Where(p => p.PageNumber == pageNrPefore).FirstOrDefault();
        if (pageBefore != null)
        {
            pageBefore.PageNumbersAfter.Add(pageNrNext);
        }
        else
        {
            _pageNumbers.Add(new PageNumbers() { PageNumber = pageNrPefore, PageNumbersAfter = [pageNrNext] });
        }
        // ALso add that 
        // find page after and add record to its pages before
        var pageAfter = _pageNumbers.Where(p => p.PageNumber == pageNrNext).FirstOrDefault();
        if (pageAfter != null)
        {
            pageAfter.PageNumbersBefore.Add(pageNrPefore);
        }
        else
        {
            _pageNumbers.Add(new PageNumbers() { PageNumber = pageNrNext, PageNumbersBefore = [pageNrPefore] });
        }
    }
}

public class PageNumbers
{
    public int PageNumber;
    public List<int> PageNumbersAfter = new();
    public List<int> PageNumbersBefore = new();
}