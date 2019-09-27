using System;

public class Message
{
    public enum MSG_ID
    {
        MSG_ID_TICK = 1,
        MSG_ID_LOGIN = 2,

    };

    [System.Serializable]
    public class User
    {
        public int iGameLevel;
        public string uid;
    }
}
