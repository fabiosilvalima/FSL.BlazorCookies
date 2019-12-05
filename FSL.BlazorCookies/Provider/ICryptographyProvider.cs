namespace FSL.BlazorCookies.Provider
{
    public interface ICryptographyProvider
    {
        string Encrypt(
            string info);

        string DeCrypt(
            string info);
    }
}
