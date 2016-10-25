using FourSquare.SharpSquare.Core;

namespace WebViewer.Controllers
{
    public class FourSquareFactory
    {
        static string clientId = "E21YK1GLLWAXPQH5QQXRN2R1GBRTUB4XK2U2HBZQ1M04Y41P";
        static string clientSecret = "05JC445PVAZ4T24VIJI2UZ2JWKFU3METELXOXSFONIXSKAYM";
        string redirectUri = "http://localhost/foursquareuser/oauth2";
        private static SharpSquare _sharpSquareClient;


        public FourSquareFactory()
        {
            _sharpSquareClient = new SharpSquare(clientId, clientSecret);
        }
        public static FourSquareFactory Create()
        {
            return new FourSquareFactory();
        }

        public string GetAuthenticationURI()
        {
            return Client.GetAuthenticateUrl(redirectUri);
        }


        public void Authenticate(string code)
        {
            
            
            Client.GetAccessToken(redirectUri, code);

        }


        public SharpSquare Client => _sharpSquareClient;
    }
}