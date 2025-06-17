namespace LanguageFeatures.Models
{
    public class MyAsyncMethods
    {

        /// <summary>
        /// Forma antiga
        /// </summary>
        /// <returns></returns>
        public static Task<long?> GetPageLengthConfuse()
        {
            HttpClient client = new HttpClient();

            var httpTask = client.GetAsync("http://manning.com");

            return httpTask.ContinueWith((Task<HttpResponseMessage> antecedent) =>
            {
                return antecedent.Result.Content.Headers.ContentLength;
            });
        }


        /// <summary>
        /// Forma nova
        /// </summary>
        /// <returns></returns>
        public async static Task<long?> GetPageLength()
        {
            HttpClient client = new HttpClient();
            var httpMessage = await client.GetAsync("http://manning.com");
            return httpMessage.Content.Headers.ContentLength;
        }
    }
}
