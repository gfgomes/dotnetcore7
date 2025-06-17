namespace LanguageFeatures.Models
{
    public class MyAsyncMethods
    {

        /// <summary>
        /// Forma antiga
        /// </summary>
        /// <returns></returns>
        public static Task<long?> GetPageLengthConfuseAndOld()
        {
            Console.WriteLine("Iniciando o método...");

            HttpClient client = new HttpClient();

            var httpTask = client.GetAsync("http://manning.com");


            Console.WriteLine("Requisição enviada. Registrando ContinueWith...");

            //httpTask.ContinueWith(t =>
            //{
            //    if (t.Exception != null)
            //        Console.WriteLine("Erro: " + t.Exception.InnerException.Message);
            //    else
            //        Console.WriteLine("Requisição concluída com sucesso");

            //    return t.Result.Content.Headers.ContentLength;
            //});

            return httpTask.ContinueWith((Task<HttpResponseMessage> antecedent) =>
            {
                Console.WriteLine("Requisição finalizada. Obtendo ContentLength...");

                var length = antecedent.Result.Content.Headers.ContentLength;

                Console.WriteLine($"ContentLength recebido: {length}");

                return length;
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

        public static async Task<IEnumerable<long?>> GetPageLengths(List<string> output, params string[] urls)
        {
            List<long?> results = new List<long?>();
            HttpClient client = new HttpClient();

            foreach (string url in urls)
            {
                output.Add($"Started request for {url}");
                var httpMessage = await client.GetAsync($"http://{url}");

                results.Add(httpMessage.Content.Headers.ContentLength);
                output.Add($"Completed request for {url}");
            }
            return results;
        }
    }
}
