using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Quote
/// </summary>
public class Quote
{
	public Quote()
	{
		
	}

    private int id;
    private string text;
    private string author;

    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    public string Text
    {
        get
        {
            return text;
        }
        set
        {
            text = value;
        }
    }

    public string Author
    {
        get
        {
            return author;
        }
        set
        {
            author = value;
        }
    }
}
