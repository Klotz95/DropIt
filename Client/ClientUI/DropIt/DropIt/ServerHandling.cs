using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace DropIt
{
  class ServerHandling
  {
    Socket Server;
    string[,] Filelist;
    Encryption enc;
    bool connected;
    public ServerHandling(string IPAddressOfServer, string UserName, string Password)
    {
      //establish connection
      IPAddress ServerIP = IPAddress.Parse(IPAddressOfServer);
      IPEndPoint ie = new IPEndPoint(ServerIP,2040);
      enc = new Encryption(Password);
      Server = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
      try
      {
        Server.Connect(ie);
        connected = true;
      }
      catch
      {
        connected = false;
      }
      if(connected)
      {
        ASCIIEncoding en = new ASCIIEncoding();
        //inform the server about the user and check the Password
        string WelcomeMessage = UserName + ";" + Password;
        byte[] WelcomeByte = en.GetBytes(WelcomeMessage);
        Server.Send(WelcomeByte);
        //now wait for the result of the server
        byte[] resultOfPasswordCheck = new Byte[1];
        Server.Receive(resultOfPasswordCheck);
        if(resultOfPasswordCheck[0] == 1)
        {
          //Password was correct
          //get the Filelist
          byte[] Buffer = new byte[1000];
          int Length = Server.Receive(Buffer);
          byte[] FileListBuffer = Buffer;
          if(Length > 1000)
          {
            //there is some RestMessage
            Buffer = new byte[Length - 1000];
            Server.Receive(Buffer);
            byte[] Backup = FileListBuffer;
            FileListBuffer = new byte[Length];
            for(int i = 0; i < Backup.Length; i++)
            {
              FileListBuffer[i] = Backup[i];
            }
            for(int i = 0; i < Buffer.Length; i++)
            {
              FileListBuffer[i + Backup.Length] = Buffer[i];
            }
          }
          //now seperate the message and translate the informations
          string FileListInString = en.GetString(FileListBuffer);
          string[] FileListSeperated = seperateMessage(FileListInString);
          string[] currentBracket = new string[0];
          for(int i = 0; i < FileListSeperated.Length ; i++)
          {
            if(currentBracket.Length == 3)
            {
              string[,] backup = Filelist;
              int NewArraySize = (Filelist.Length/3) + 1;
              Filelist = new string[NewArraySize,3];
              for (int k = 0; k < NewArraySize - 1; k++)
              {
                  for (int j = 0; j < 3; j++)
                  {
                      Filelist[k, j] = backup[k, j];
                  }
              }
              for(int k = 0; k < currentBracket.Length; k++)
              {
                Filelist[NewArraySize -1,k] = currentBracket[k];
              }
            }
            else
            {
              string[] backup = currentBracket;
              currentBracket = new string[backup.Length + 1];
              for(int k = 0; k < backup.Length; k++)
              {
                currentBracket[k] = backup[k];
              }
              currentBracket[backup.Length + 1] = FileListSeperated[i];
            }
          }
          //filelist has now been created
        }
        else
        {
          //Password was incorrect
          connected = false;
          Server.Close();
        }
      }
    }
    public bool IsConnected(ref string[,] Filelist)
    {
      Filelist = this.Filelist;
      return connected;
    }
    public bool RemoveFile(string FileName)
    {
      //inform the server about that request
      string Message = "{RM}" + FileName +"\n";
      ASCIIEncoding en = new ASCIIEncoding();
      byte[] sendableMessage = en.GetBytes(Message);
      Server.Send(sendableMessage);
      //now receive the check bool of the server
      byte[] checkbool = new byte[1];
      Server.Receive(checkbool);
      if(checkbool[0] == 1)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    public bool UploadFile(string Description,byte[] File)
    {
      //create the request for the server
      string Message = "{UP}" + Description + "\n";
      ASCIIEncoding en = new ASCIIEncoding();
      byte[] sendableMessage = en.GetBytes(Message);
      Server.Send(sendableMessage);
      Thread.Sleep(100);
      //now send the File
      File = enc.EncryptFile(File);
      Server.Send(File);
      return true;
    }
    public bool Download(ref int state, string FileName, ref byte[] returnValue)
    {
      //create the request for the server
      string Message = "{DW}" + FileName + "\n";
      ASCIIEncoding en = new ASCIIEncoding();
      byte[] sendableMessage = en.GetBytes(Message);
      Server.Send(sendableMessage);
      //now wait for the answer of the server
      byte[] ServerAnswerInByte = new byte[100];
      Server.Receive(ServerAnswerInByte);
      string ServerAnswer = en.GetString(ServerAnswerInByte);
      //seperate the Message of the server to get the size of the file and the avaiability
      string[] ServerAnswerSeperated = seperateMessage(ServerAnswer);
      if(ServerAnswerSeperated[0] == "1")
      {
        //File is avaiable
        int LenghtOfFile = Convert.ToInt32(ServerAnswerSeperated[1]);
        byte[] File = new byte[LenghtOfFile];
        int currentReceivedSize = 0;
        while(currentReceivedSize < LenghtOfFile)
        {
          byte[] buffer = new byte[1000];
          if(LenghtOfFile - currentReceivedSize < 1000)
          {
            buffer = new byte[LenghtOfFile - currentReceivedSize];
          }
          //now receive the next kb
          Server.Receive(buffer);
          for(int i = 0; i < buffer.Length; i++)
          {
            File[currentReceivedSize] = buffer[i];
            currentReceivedSize ++;
          }
          //calculate the progress
          state = (currentReceivedSize/LenghtOfFile) * 100;
        }
        //file is now on this device
        returnValue = enc.EncryptFile(File);
        return true;

      }
      else
      {
        return false;
      }
    }

    private string[] seperateMessage(string Message)
    {
      string[] returnValue = new string[0];
      string current = "";
      for(int i = 0; i < Message.Length; i++)
      {
        if(Message[i] == '\n')
        {
          //Save it to the array
          string[] backup = returnValue;
          returnValue = new string[backup.Length + 1];
          for(int k = 0; k < backup.Length; k++)
          {
            returnValue[k] = backup[k];
          }
          returnValue[backup.Length] = current;
          current = "";
        }
        else
        {
          current += Convert.ToString(Message[i]);
        }
      }
      return returnValue;
    }
  }
}
