using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Globalization;
using System.Xml;


public partial class QuoteWidget : System.Web.UI.UserControl
{
    public string Width
    {
        get
        {
            return width;
        }
        set
        {
            width = value;
            tblQuote.Width = width;
        }
    }

    public string BackColor
    {
        get
        {
            return backColor;
        }
        set
        {
            backColor = value;
            if (backColor == "")
                backColor = "LightYellow";

            CssStyleCollection cssStyle = tblQuote.Style;
            // assumes that this style is already there
            cssStyle[HtmlTextWriterStyle.BackgroundColor] = backColor;
        }
    }

    public string QuoteColor
    {
        get
        {
            return quoteColor;
        }
        set
        {
            quoteColor = value;
            Color color = GetColor(quoteColor);
            if (color == Color.Empty)
                color = Color.Blue;
            lblQuote.ForeColor = color;
        }
    }

    public string AuthorColor
    {
        get
        {
            return authorColor;
        }
        set
        {
            authorColor = value;
            Color color = GetColor(authorColor);
            if (color == Color.Empty)
                color = Color.Green;
            lblAuthor.ForeColor = color;
        }
    }

    public string QuoteFontName
    {
        get
        {
            return quoteFontName;
        }
        set
        {
            quoteFontName = value;
            if (quoteFontName == "")
                quoteFontName = "Times New Roman";

            lblQuote.Font.Name = quoteFontName;
        }
    }

    public string AuthorFontName
    {
        get
        {
            return authorFontName;
        }
        set
        {
            authorFontName = value;
            if (authorFontName == "")
                authorFontName = "Times New Roman";

            lblAuthor.Font.Name = authorFontName;
        }
    }

    public string QuoteFontSize
    {
        get
        {
            return quoteFontSize;
        }
        set
        {
            quoteFontSize = value;
            if (quoteFontSize == "")
                quoteFontSize = "Medium";


            FontUnit fontUnit = FontUnit.Medium;
            try
            {
                fontUnit = FontUnit.Parse(quoteFontSize);
            }
            catch { }
            
            lblQuote.Font.Size = fontUnit;

        }
    }

    public string AuthorFontSize
    {
        get
        {
            return authorFontSize;
        }
        set
        {
            authorFontSize = value;
            if (authorFontSize == "")
                authorFontSize = "Medium";


            FontUnit fontUnit = FontUnit.Medium;
            try
            {
                fontUnit = FontUnit.Parse(authorFontSize);
            }
            catch { }

            lblAuthor.Font.Size = fontUnit;

        }
    }

    public string QuoteFontStyle
    {
        get
        {
            return quoteFontStyle;
        }
        set
        {
            quoteFontStyle = value;
            if (quoteFontStyle == "")
                quoteFontStyle = "Italic";

            
            switch (quoteFontStyle.ToLower())
            {
                case "bold":
                    lblQuote.Font.Bold = true;
                    lblQuote.Font.Italic = false;
                    break;
                case "italic":
                    lblQuote.Font.Bold = false;
                    lblQuote.Font.Italic = true;
                    break;
                case "bolditalic":
                    lblQuote.Font.Bold = true;
                    lblQuote.Font.Italic = true;
                    break;
                default:
                    lblQuote.Font.Bold = false;
                    lblQuote.Font.Italic = false;
                    break;
            }
        }

    }

    public string AuthorFontStyle
    {
        get
        {
            return authorFontStyle;
        }
        set
        {
            authorFontStyle = value;
            if (authorFontStyle == "")
                authorFontStyle = "Bold";


            switch (authorFontStyle.ToLower())
            {
                case "bold":
                    lblAuthor.Font.Bold = true;
                    lblAuthor.Font.Italic = false;
                    break;
                case "italic":
                    lblAuthor.Font.Bold = false;
                    lblAuthor.Font.Italic = true;
                    break;
                case "bolditalic":
                    lblAuthor.Font.Bold = true;
                    lblAuthor.Font.Italic = true;
                    break;
                default:
                    lblAuthor.Font.Bold = false;
                    lblAuthor.Font.Italic = false;
                    break;
            }
        }

    }

    public string Selection
    {
        get
        {
            return selection.ToString();
        }
        set
        {
            switch (value.ToLower())
            {
                case "random":
                    selection = SelectionType.Random;
                    break;
                case "hourly":
                    selection = SelectionType.Hourly;
                    break;
                case "daily":
                    selection = SelectionType.Daily;
                    break;
                default:
                    selection = SelectionType.Hourly;
                    break;
            }
            
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Quote quote = SelectQuote();
        lblQuote.Text = quote.Text;
        if(quote.Author != "")
            lblAuthor.Text = "- " + quote.Author;

    }

    private Quote SelectQuote()
    {
        Quote quote = GetDefaultQuote();
        string fileName = Server.MapPath("App_Data") + "\\Quotes.xml";
        
        QuoteXml xml = new QuoteXml();
        if (xml.LoadXml(fileName) && xml.QuoteCount>0)
        {
            switch (selection)
            {
                case SelectionType.Random:
                    quote = xml.GetRandomQuote();
                    break;
                case SelectionType.Hourly:
                    quote = xml.GetHourlyQuote();
                    break;
                case SelectionType.Daily:
                    quote = xml.GetDailyQuote();
                    break;
            }
        }

        return quote;
    }

    private Quote GetDefaultQuote()
    {
        Quote quote = new Quote();

        quote.Text = "Please add quotes with the admin page";
        quote.Author = "Vijay";

        return quote;
    }

    

    private Color GetColor(string color)
    {
        Color r_Color = Color.Empty;

        if(color.StartsWith("#"))
        {
            if (color.Length == 7)
            {
                string red = color.Substring(1, 2);
                string green = color.Substring(3, 2);
                string blue = color.Substring(5, 2);

                int iRed, iGreen, iBlue;

                try
                {
                    iRed = int.Parse(red, NumberStyles.HexNumber);
                    iGreen = int.Parse(green, NumberStyles.HexNumber);
                    iBlue = int.Parse(blue, NumberStyles.HexNumber);
                }
                catch
                {
                    return r_Color;
                }

                
                r_Color = Color.FromArgb(iRed, iGreen, iBlue);
            }
            
        }
        else
        {
            r_Color = Color.FromName(color);
        }

        return r_Color;
    }

    private string width;
    private string backColor;
    private string quoteColor;
    private string authorColor;
    private string quoteFontName;
    private string authorFontName;
    private string quoteFontSize;
    private string authorFontSize;
    private string quoteFontStyle;
    private string authorFontStyle;
    private SelectionType selection;
}


enum SelectionType
{
    Random, Hourly, Daily
}