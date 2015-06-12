using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DropIt
{
  class Encryption
  {
    //Attributes
    string Key;
    public Encryption(string password)
    {
      key = password;
    }
    public byte[] EncryptFile(byte[] File)
    {
      //encrypt by exoring the file with the password
      //get the byte array of the Password
      byte[] result = new byte[File.Length];
      ASCIIEncoding enc = new ASCIIEncoding();
      byte[] KeyInByte = enc.GetBytes(key);
      int LengthOfKey = KeyInByte.Length;
      int currentUsedByte = 0;
      for(int i = 0; i < File.Length; i++)
      {
        if(currentUsedByte >= LengthOfKey)
        {
          currentUsedByte = 0;
        }
        //now get the 2 xOr Values
        string value1 = getBinaryValueOf(File[i]);
        string value2 = getBinaryValueOf(KeyInByte[currentUsedByte]);
        string result = xOrValues(value1,value2);
        result[i] = GetByteValueOf(result);
        currentUsedByte ++;
      }
      return result;
    }
    private string getBinaryValueOf(int value)
    {
      string returnValue = "";
      while(value != 0)
      {
        double current = value/2;
        string currentInString = Convert.ToString(current);
        if(currentInString.Contains(',')|| currentInString.Contains('.'))
        {
          string NewValue = "";
          for(int i = 0; i < currentInString.Length;i++)
          {
            if(currentInString[i] != "." || currentInString[i] != ",")
            {
              NewValue += currentInString[i];
            }
            else
            {
              break;
            }
          }
          Value = Convert.ToInt32(NewValue);
          returnValue += "1";
        }
        else
        {
          Value = current;
          returnValue += "0";
        }
      }
      //now reverse the result and return it
      string reversedResult = "";
      for(int i = returnValue.Length -1; i >= 0; i--)
      {
        reversedResult += returnValue[i];
      }
      //create 8Bit
      if(reversedResult.Lenght != 8)
      {
        int nullCount = 8 - reversedResult.Lenght;
        string nulls = "";
        for(int i = 0; i < nullCount; i++)
        {
          nulls += "0";
        }
        reversedResult = nulls + reversedResult;
      }
      return reversedResult;
    }
    private byte GetByteValueOf(string value)
    {
      byte a = 0;
      for(int i = Value.Length - 1; i >= 0 ; i--)
      {
        if(Value[i] == '1')
        {
          a+= Math.Pow(2,(Value.Length - 1) - i);
        }
      }
      return a;
    }
    private string xOrValues(string value1 , string value2)
    {
      string result = "";
      for(int i = 0; i < 8 ; i++)
      {
        if(value1[i] == '1' && value2[i] == '1' || value1[i] == '0' && value2[i] == "0")
        {
          result += "0";
        }
        else
        {
          result += "1";
        }
      }
      return result;
    }
  }
}
