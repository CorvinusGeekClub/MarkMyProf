using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MarkMyProfessor.Models
{
    public class SeedData
    {
        private readonly RatingsContext _context;

        public SeedData(RatingsContext context)
        {
            _context = context;
        }

        private readonly HttpClient _browser = new HttpClient();

        public async Task SeedDataAsync()
        {
            string waybackJsonString =
                await _browser.GetStringAsync(
                    "http://web.archive.org/cdx/search?url=http%3A%2F%2Fwww.markmyprofessor.com%3A80%2Ftanar%2Fadatlap%2F&matchType=prefix&collapse=urlkey&output=json&fl=original%2Ctimestamp%2Cendtimestamp%2Cgroupcount%2Cuniqcount&filter=!statuscode%3A%5B45%5D..&_=1494678468765");
            string[,] deserialized = JsonConvert.DeserializeObject<string[,]>(waybackJsonString);
            for(int row = 4380; row < deserialized.GetLength(0); row++){ // TODO: change row back to start from 1
                    // ignore really old records
                    if(Convert.ToInt32(deserialized[row, 2].Substring(0, 4)) < 2014)
                        continue;
                    
                    await ParsePageAsync($"http://web.archive.org/web/{deserialized[row, 2]}/{deserialized[row, 0]}");
                await Task.Delay(1000);
            }
        }

        private async Task ParsePageAsync(string uri)
        {

            System.Diagnostics.Debug.WriteLine(
                $"Reading {uri}");

            try
            {
                bool insertProf = false;
                bool insertSchool = false;

                string lastSegment = uri.Split('/').Last() + "."; // append dot for the rare case the uri does not end with .html
                int profId =  Convert.ToInt32(lastSegment.Substring(0, lastSegment.IndexOf('.')));

                Professor prof = await
                    _context.Professors
                        .Include(p => p.Ratings)
                        .Include(p => p.School)
                        .FirstOrDefaultAsync(p => p.ProfessorId == profId);

                if (prof == null)
                {
                    prof = new Professor {ProfessorId = profId};
                    insertProf = true;
                }
                
                HtmlDocument doc = new HtmlDocument();
                try
                {
                    doc.LoadHtml(await _browser.GetStringAsync(uri));
                }
                catch (Exception x)
                {
                    // we can try this again after a delay, to filter out some 504s for example
                    await Task.Delay(5000);
                    doc.LoadHtml(await _browser.GetStringAsync(uri)); // this might throw again, sh*t happens, that'll get caught by the outter catch
                }
                

                // ignore empty ones
                HtmlNode overall = doc.QuerySelector(".main.professor .big-font7");
                if (overall == null
                    || !Decimal.TryParse(overall.InnerText.Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal totalScore)
                    || totalScore == Decimal.Zero)
                    return;

                var rateHolder = doc.QuerySelector(".rate-holder");
                prof.Name = rateHolder.QuerySelector(".big-font5").InnerText.Trim();
                HtmlNode schoolAnchor = rateHolder.QuerySelectorAll(".big-font2")[0].QuerySelector("a");
                int profSchoolId = Convert.ToInt32(
                    schoolAnchor.Attributes["href"].Value.Trim()
                        .Split('/')
                        .Last()
                        .Replace(".html", ""));

                School school = await _context.Schools.FindAsync(profSchoolId);
                if (school == null)
                {
                    school = new School()
                    {
                        SchoolId = profSchoolId,
                        ShortName = schoolAnchor.InnerText.Trim(),
                        LongName = schoolAnchor.GetAttributeValue("name", schoolAnchor.InnerText).Trim()
                    };
                    insertSchool = true;
                }

                prof.SchoolId =
                    profSchoolId;

                HtmlNode table = rateHolder.QuerySelector("table");
                prof.MigratedRateAchievable = Convert.ToDecimal(table.QuerySelector("tr:nth-child(1) td:nth-child(2)").InnerText.Trim(), CultureInfo.InvariantCulture);
                prof.MigratedRateUseful = Convert.ToDecimal(table.QuerySelector("tr:nth-child(2) td:nth-child(2)").InnerText.Trim(), CultureInfo.InvariantCulture);
                prof.MigratedRateHelpful = Convert.ToDecimal(table.QuerySelector("tr:nth-child(3) td:nth-child(2)").InnerText.Trim(), CultureInfo.InvariantCulture);
                prof.MigratedRatePrepared = Convert.ToDecimal(table.QuerySelector("tr:nth-child(4) td:nth-child(2)").InnerText.Trim(), CultureInfo.InvariantCulture);
                prof.MigratedRateStyle = Convert.ToDecimal(table.QuerySelector("tr:nth-child(5) td:nth-child(2)").InnerText.Trim(), CultureInfo.InvariantCulture);
                prof.MigratedCourses = rateHolder.QuerySelectorAll(".big-font2").Last().InnerText;
                prof.MigratedIsSexy = table.QuerySelectorAll("tr:nth-child(6) td:nth-child(2) img").Count > 0;
                if(prof.Name == null)
                    Debugger.Break();

                if(insertProf)
                    _context.Professors.Add(prof);
                if (insertSchool)
                    _context.Schools.Add(school);

                await _context.SaveChangesAsync();

                // ratings
                // 5 tr-s make up a group: empty, values, comment, metadata, empty
                IList<HtmlNode> rows = doc.QuerySelectorAll(".simpleList dd tbody tr").Where(r => !r.GetAttributeValue("class", "").Contains("responsesTr")).ToList();
                for (int i = 0; i <= rows.Count - 5; i+=5)
                {
                    Rating rating = new Rating()
                    {
                        Date =
                            DateTime.ParseExact(
                                 HtmlEntity.DeEntitize(rows[i + 3].QuerySelectorAll("td")[0].InnerText.Replace("Válaszok", "")).Trim().Substring(0, 16),
                                "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                        Professor = prof,
                        Course = rows[i + 2].QuerySelector("div strong").InnerText.Trim(),
                        Comment = rows[i + 2].SelectSingleNode(".//div/text()[last()]").InnerText.Trim(),
                        AchievableRate = Convert.ToInt32(rows[i + 1].QuerySelectorAll("td")[0].InnerText.Trim()),
                        UsefulRate = Convert.ToInt32(rows[i + 1].QuerySelectorAll("td")[1].InnerText.Trim()),
                        HelpfulRate = Convert.ToInt32(rows[i + 1].QuerySelectorAll("td")[2].InnerText.Trim()),
                        PreparedRate = Convert.ToInt32(rows[i + 1].QuerySelectorAll("td")[3].InnerText.Trim()),
                        StyleRate = Convert.ToInt32(rows[i + 1].QuerySelectorAll("td")[4].InnerText.Trim()),
                        IsSexy = rows[i + 1].QuerySelectorAll("td")[5].QuerySelectorAll("img").Count >= 1
                    };
                    if (_context.Ratings.All(r => r.Comment != rating.Comment && r.Date != rating.Date))
                    {
                        _context.Ratings.Add(rating);
                    }
                }


                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Error!");
            }
        }
    }
}
