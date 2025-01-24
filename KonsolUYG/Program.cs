using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Taxonomy ID: ");
            string txid = Console.ReadLine();
            var items = Fetcher(txid);
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
        /*txid1921407[Organism:exp]*/

        static List<string> Fetcher(string taxonomyId)
        {
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArgument("--headless");
            using (var driver = new FirefoxDriver(firefoxOptions))
            {
                try
                {
                    driver.Navigate().GoToUrl("https://www.ncbi.nlm.nih.gov/nuccore");

                    // Locate the input field with id 'term' and enter the search query
                    var searchInput = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                        .Until(drv => drv.FindElement(By.Id("term")));
                    searchInput.SendKeys(taxonomyId);

                    // Locate the search button with id 'search' and click it
                    var searchButton = driver.FindElement(By.Id("search"));
                    searchButton.Click();

                    var kacadetlistelenecek = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                        .Until(drv => drv.FindElement(By.XPath("//*[text()='20 per page']")));
                    kacadetlistelenecek.Click();

                    var menuItem = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                        .Until(drv => drv.FindElement(By.Id("ps200")));
                   menuItem.Click();

                    // Wait for the results to load
                    System.Threading.Thread.Sleep(8000); // Adjust time as necessary or use WebDriverWait for better handling
                    var sayfasayisi = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                        .Until(drv => drv.FindElement(By.ClassName("result_count")).Text.Substring(19));
                    var itemList = new List<string>();
                    for (int i = 0; i < 1; i++)
                    {
                    var results = driver.FindElements(By.ClassName("title"));
                    int sayac = 1;
                    foreach (var result in results)
                    {
                        Console.WriteLine($"{sayac} {result.Text}");
                        itemList.Add($"{sayac} {result.Text}");
                        sayac++;
                    }
                    }
                
                    return itemList;
                }
                finally
                {
                    driver.Quit();
                }
            }
        }
    }
}
