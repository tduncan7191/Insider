using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for clsJW_Encrypt
/// </summary>
public class clsJW_Encrypt
{
	//These Constants are used to create part of the unique key in the string.
	//The values must remain the same for decryption to work properly.
	//If you encrypt with iXor1 = 100 and decrypt with iXor1 = 101 you will not get the same string.
	//The values can be changed to alter the key for different applications.
	private const int iXor1 = 129; //Must be a valid integer between 1 and 255
	private const int iXor2 = 56; //Must be a valid integer between 1 and 199

	public static string fnEncrypt(string sString)
	{
		//-------------------------------------------------------
		//Written By: Jonathan M. Willis
		//Revision Date: 10/3/2008
		//Purpose: Use simple encryption combined with a unique
		//integer to create a strongly encrypted string from 
		//a passed-in string.
		//-------------------------------------------------------
		try
		{
			int l = 0; int l2 = 0; int iX = 0;
			string s = ""; string s1 = ""; string s2 = ""; string s3 = ""; string s4 = "";
	
			//Get the current seconds and minutes, then add them together to create mostly unique Xor integer.
			iX = System.DateTime.Now.Second + System.DateTime.Now.Minute;

			//If the integer value is less than 100 make it 3 characters by adding 100 to the value
			if (iX < 100) iX = iX + 100;
			
			//Encrypt the Xor integer value, to be added to encrypted string later
			s = iX.ToString();
			for (l = 0; l < s.Length; l++)
			{
				//Get next character in source string
				s1 = s.Substring(l, 1);
				s1 = Convert.ToInt32(Convert.ToChar(s1)).ToString();
				s1 = (Convert.ToInt32(s1) ^ (iXor1)).ToString();
				s1 = ((char)Convert.ToInt32(s1)).ToString();
				
				//s = Mid(iX, l, 1)
				//s = Asc(s)
				//s = s Xor iXor1
				//s = Chr(s)
				
				//Add converted value to new string
				s2 = s2 + s1;
			}
			
			s1 = s2;
			
			//Encrypt the string character by character
			s = sString;
			s2 = null;
			for (l = 0; l < s.Length; l++)
			{
				//Get the ASCII value of the character, Xor it, then convert the value to Hexadecimal
				//s2 = Conversion.Hex(Asc(Mid(s, l, 1)) Xor iX)

				//Get next character in source string
				s2 = s.Substring(l, 1);
				s2 = Convert.ToInt32(Convert.ToChar(s2)).ToString();
				s2 = (Convert.ToInt32(s2) ^ iX).ToString();
				s2 = int.Parse(s2).ToString("x");
				
				//s2 = Mid(s, l, 1)
				//s2 = Asc(s2)
				//s2 = s2 Xor iX
				//s2 = Hex(s2)

				//If the Hex value is less than 2 characters make it 2 by adding 0 in front
				if (s2.Length == 1) s2 = "0" + s2;
				
				//Get the ASCII value of each character Hexadecimal value, Xor it, and then return the character value to the string
				for (l2 = 0; l2 < s2.Length; l2++)
				{
					s3 = s2.Substring(l2, 1);
					s3 = Convert.ToInt32(Convert.ToChar(s3)).ToString();
					s3 = (Convert.ToInt32(s3) ^ (iX + iXor2)).ToString();
					s4 = s4 + ((char)Convert.ToInt32(s3)).ToString();
					
					//s3 = s3 & Chr(Asc(Mid(s2, l2, 1)) Xor iX + iXor2);
					//s = Mid(s2, l2, 1)
					//s = Asc(s)
					//s = s Xor (iX + iXor2)
					//s = Chr(s)
					//Mid(s1, l, 1) = s
				}
			}

			//Add the encrypted value of the unique Xor integer to the end of the encrypted string
			//return s3 + s1;
			return s4 + s1;
		}
		catch (Exception ex)
		{
			return "";
		}
	}

	public static string fnDecrypt(string sString)
	{
		//-------------------------------------------------------
		//Written By: Jonathan M. Willis
		//Revision Date: 10/3/2008
		//Purpose: Decrypt a string that was encrypted using the
		//fnEncStrHexOr function.
		//-------------------------------------------------------
		try
		{
			int l = 0; int iX = 0;
			string s = ""; string s1 = ""; string s2 = ""; string s3 = "";

			//First get the encrypted value of the mostly unique Xor integer and decrypt it
			s = sString.Substring(sString.Length-3,3);
			for (l = 0; l < s.Length; l++)
			{
				//Mid(s, l, 1) = Chr(Asc(Mid(s, l, 1)) Xor iXor1)
				s1 = s.Substring(l, 1);
				s1 = Convert.ToInt32(Convert.ToChar(s1)).ToString();
				s1 = (Convert.ToInt32(s1) ^ iXor1).ToString();
				s2 = s2 + ((char)Convert.ToInt32(s1)).ToString();
			}
			iX = Convert.ToInt32(s2);
			
			//Get the actual string data to decrypt
			s = sString.Substring(0, sString.Length - 3);
			
			s1 = "";
			s2 = "";
			s3 = "";
			
			//Decrypt the string character by character
			for (l = 0; l < s.Length - 1; l += 2)
			{
				//Get the first encrypted character of the Hex value and Xor it to get the actual value
				//s2 = Chr(Asc(Mid(s, l, 1)) Xor iX + iXor2)
				s1 = s.Substring(l, 1);
				s1 = Convert.ToInt32(Convert.ToChar(s1)).ToString();
				s1 = (Convert.ToInt32(s1) ^ (iX + iXor2)).ToString();
				s2 = ((char)Convert.ToInt32(s1)).ToString();
				
				//Get the second encrypted character of the Hex value and Xor it to get the actual value
				//If l < Len(s) Then s2 = s2 & Chr(Asc(Mid(s, l + 1, 1)) Xor iX + iXor2)
				if (l < s.Length)
				{
					s1 = s.Substring(l + 1, 1);
					s1 = Convert.ToInt32(Convert.ToChar(s1)).ToString();
					s1 = (Convert.ToInt32(s1) ^ (iX + iXor2)).ToString();
					s2 = s2 + ((char)Convert.ToInt32(s1)).ToString();
				}

				//Convert the Hex value back to decimal, Xor it, and then return the actual character
				//s3 = s3 & Chr(Asc(Chr(Convert.ToByte(s2, 16))) Xor iX)
				s1 = Convert.ToInt32(Convert.ToByte(s2, 16)).ToString();
				s1 = (Convert.ToInt32(s1) ^ (iX)).ToString();
				s3 = s3 + ((char)Convert.ToInt32(s1)).ToString();
			}

			return s3;
		}
		catch (Exception ex)
		{
		    return "";
		}
	}
}