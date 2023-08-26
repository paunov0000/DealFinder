using System.Net;

var curDate = DateTime.Now.Day;

var kauflandBroschureUrl = @"https://object.storage.eu01.onstackit.cloud/leaflets/pdfs/d093b5be-3523-11ee-be0f-fa163dadd92e/Kaufland-21-08-2023-27-08-2023-04.pdf";

var localFilePath = @"C:\\SomePath"; //TODO: make it work on any pc not just some static path

await DownloadPdfAsync(kauflandBroschureUrl, localFilePath);

Console.WriteLine("PDF downloaded and saved successfully.");


static async Task DownloadPdfAsync(string pdfUrl, string localFilePath)
{
    using (HttpClient client = new HttpClient())
    {
        using (HttpResponseMessage response = await client.GetAsync(pdfUrl))
        {
            if (response.IsSuccessStatusCode)
            {
                using (Stream pdfStream = await response.Content.ReadAsStreamAsync())
                {
                    using (FileStream fileStream = File.Create(localFilePath))
                    {
                        await pdfStream.CopyToAsync(fileStream);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Failed to download PDF. Status code: {response.StatusCode}");
            }
        }
    }
}
