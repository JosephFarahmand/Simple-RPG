namespace DataBank
{
    public struct ProfileEntity
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Token { get; private set; }
        public string Email { get; private set; }
        //public string Nickname { get; private set; }
        public int CoinAmount { get; private set; }
        public int GemAmount { get; private set; }
        public int Level { get; private set; }
        public string SkinId { get; private set; }
        public float CurrentXP { get; private set; }


        public ProfileEntity(string username, string password) : this()
        {
            Username = username;
            Password = password;
            Token = string.Empty;
            Email = StaticData.defaultEmail;
            //Nickname = "New Player";
            CoinAmount = 0;
            GemAmount = 0;
            Level = 1;
            SkinId = "0";
            CurrentXP = 0;
        }

        public ProfileEntity(int id, string username, string password, string token, string email, int coinAmount, int gemAmount, int level, string skinId, float currentXP)
        {
            Id = id;
            Username = username;
            Password = password;
            Token = token;
            Email = email;
            //Nickname = nickname;
            CoinAmount = coinAmount;
            GemAmount = gemAmount;
            Level = level;
            SkinId = skinId;
            CurrentXP = currentXP;
        }

        public void SetId(int newId)
        {
            Id = newId;
        }
    }
}