using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DropIt
{
  class Organisation
  {
    //Attribute
    string[,] FileList;
    ServerHandling server;
    //Konstruktor
    public Organisation()
    {
      FileList = new string[0,3];
    }
    public string[,] GetFileList()
    {
      return FileList;
    }
    public bool Connect(string IPAddress,string UserName, string Password)
    {
      server = new ServerHandling(IPAddress,UserName,Password);
      if(server.IsConnected(ref FileList))
      {
        return true;

      }
      else
      {
        return false;
      }
    }
    public bool UploadFile(string Description, byte[] File)
    {
      string ending = "";
      string name = "";
      bool endinge = false;
      for(int i = 0; i < Description.Length; i++)
      {
        if(Description[i] == '.')
        {
          endinge = true;
        }
        else if(endinge)
        {
          ending += Description[i];
        }
        else
        {
          name += Description[i];
        }
      }
      //now save it in the file List and push the data on the server
      string[,] backup = FileList;
      int LengthOfSpace = (backup.Length/3) + 1;
      FileList = new string[LengthOfSpace,3];
      for(int i = 0; i < (backup.Length/3); i++)
      {
          for (int k = 0; k < 3; k++)
          {
              FileList[i, k] = backup[i, k];
          }
      }
      FileList[LengthOfSpace - 1,0] = name;
      FileList[LengthOfSpace - 1,1] = ending;
      //now check the type
      if(ending == "jpeg" || ending == "gif" || ending == "jpg" || ending == "png")
      {
        FileList[LengthOfSpace - 1,2] = "picture";
      }
      else if(ending == "mov"||ending == "avi" || ending == "wmv" || ending == "m4v")
      {
        FileList[LengthOfSpace - 1,2] = "video";
      }
      else if(ending == "word" || ending == "pages" || ending == "txt" || ending == "html")
      {
        FileList[LengthOfSpace -1 ,2] = "text";
      }
      else if(ending == "pdf")
      {
        FileList[LengthOfSpace - 1,2] = "pdf";
      }
      else
      {
        FileList[LengthOfSpace - 1 ,2] = "ndf";
      }
      //now send the file to the server
      bool check = server.UploadFile(Description,File);
      if(check)
      {
        return true;
      }
      return false;
    }
    public bool RemoveFile(int index)
    {
      //get the FileName and inform the server about the request
      string FileName = FileList[index,0] + "." + FileList[index,1];
      if(server.RemoveFile(FileName))
      {
        //clear the FileList
        int LengthOfNewArray = (FileList.Length/3) - 1;
        string[,] backup = FileList;
        FileList = new string[LengthOfNewArray,3];
        int currentIndex = 0;
        for(int i = 0; i < (backup.Length/3); i++)
        {
          if(i != index)
          {
            for(int k = 0; k < 3 ; k++)
            {
              FileList[currentIndex,k] = backup[i,k];
            }
            currentIndex ++;
          }
        }
        return true;
      }
      return false;
    }
    public byte[] DownLoadFile(ref int state,int index,ref bool check)
    {
      //get the complete FileName
      string FileName = FileList[index,0] + "." + FileList[index,1];
      //now inform the server about the request
      byte[] File = new byte[0];
      check = server.Download(ref state,FileName,ref File);
      return File;
    }

  }
}
