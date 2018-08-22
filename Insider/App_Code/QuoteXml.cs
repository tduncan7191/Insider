using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for QuoteXml
/// </summary>
public class QuoteXml
{
	public QuoteXml()
	{
        doc = null;
        quoteCount = 0;
	}

    public bool LoadXml(string fileName)
    {
        doc = new XmlDocument();

        try
        {
            doc.Load(fileName);
        }
        catch
        {
            doc = null;
            return false;
        }

       
        XmlNodeList QuoteList = doc.DocumentElement.ChildNodes;
        if (QuoteList == null)
            quoteCount = 0;

        quoteCount = QuoteList.Count;
        this.fileName = fileName;

        return true;
    }

    public void UpdateQuote(Quote quote)
    {
        string xpath = "/Quotes/Quote[@id=\"{0}\"]";
        xpath = String.Format(xpath, quote.Id);

        XmlNode node = doc.SelectSingleNode(xpath);
        string innerXml = "<Text>{0}</Text><Author>{1}</Author>";
        innerXml = String.Format(innerXml, quote.Text, quote.Author);

        node.InnerXml = innerXml;
        doc.Save(fileName);
    }

    public void AddQuote(Quote quote)
    {
        string innerXml = "<Text>{0}</Text><Author>{1}</Author>";
        innerXml = String.Format(innerXml, quote.Text, quote.Author);

        XmlNode node = doc.CreateNode(XmlNodeType.Element, "Quote", "");
        XmlAttribute attr = (XmlAttribute)doc.CreateNode(XmlNodeType.Attribute, "id", "");

        attr.Value = (quoteCount + 1).ToString();
        node.Attributes.Append(attr);        
        node.InnerXml = innerXml;

        doc.DocumentElement.AppendChild(node);
        doc.Save(fileName);
    }

    public void DeleteQuote(int quoteID)
    {
        string xpath = "/Quotes/Quote[@id=\"{0}\"]";
        xpath = String.Format(xpath, quoteID);

        XmlNode node = doc.SelectSingleNode(xpath);
        doc.DocumentElement.RemoveChild(node);

        doc.Save(fileName);
    }

    public int QuoteCount
    {
        get
        {
            return quoteCount;
        }
    }


    public Quote GetDailyQuote()
    {
        XmlNodeList QuoteList = doc.DocumentElement.ChildNodes;
        int days = DateTime.Now.Year * 365 + DateTime.Now.DayOfYear;
        int index = (int)(days  % QuoteList.Count);

        Quote quote = new Quote();
        quote.Text = QuoteList.Item(index).SelectSingleNode("Text").InnerText;
        quote.Author = QuoteList.Item(index).SelectSingleNode("Author").InnerText;

        return quote;
    }

    public Quote GetHourlyQuote()
    {
        XmlNodeList QuoteList = doc.DocumentElement.ChildNodes;
        int hours = DateTime.Now.Year * 365 * 24 + DateTime.Now.DayOfYear * 24 + DateTime.Now.Hour;
        int index = (int)(hours % QuoteList.Count);

        Quote quote = new Quote();
        quote.Text = QuoteList.Item(index).SelectSingleNode("Text").InnerText;
        quote.Author = QuoteList.Item(index).SelectSingleNode("Author").InnerText;

        return quote;
    }

    public Quote GetRandomQuote()
    {
        XmlNodeList QuoteList = doc.DocumentElement.ChildNodes;
        long secs = DateTime.Now.DayOfYear * 24 * 3600 + DateTime.Now.Hour * 3600 + DateTime.Now.Minute*60 + DateTime.Now.Second;
        int index = (int)(secs % QuoteList.Count);


        Quote quote = new Quote();
        quote.Text = QuoteList.Item(index).SelectSingleNode("Text").InnerText;
        quote.Author = QuoteList.Item(index).SelectSingleNode("Author").InnerText;

        return quote;
    }


    private XmlDocument doc;
    private int quoteCount;
    private string fileName;
}
