using System.Collections;
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace ConsoleAppTEST
{
    public class Range
    {
        public long Start { get; set; }
        public long End { get; set; }
    }
    public static class TestStatic
    {
        private static readonly HttpClient client = new HttpClient();
        public static void Test1()
        {
            int a, b, c;
            a = b = c = 1;
            Console.WriteLine($"{a}, {b}, {c}");
        }
        public static void Test2()
        {
            var tokenSource = new CancellationTokenSource();

            // Lấy token - để sử dụng bởi task, khi task thực thi
            // token.IsCancellationRequested là true nếu có phát yêu cầu dừng
            // bằng cách gọi tokenSource.Cancel
            var token = tokenSource.Token;


            // Tạo task1 có sử dụng CancellationToken
            Task task1 = new Task(
                () => {

                    for (int i = 0; i < 10000; i++)
                    {
                        // Kiểm tra xem có yêu cầu dừng thì kết thúc task
                        if (token.IsCancellationRequested)
                        {
                            Console.WriteLine("TASK1 STOP");
                            token.ThrowIfCancellationRequested();
                            return;
                        }

                        // Chạy tiếp
                        Console.WriteLine("TASK1 runing ... " + i);
                        Thread.Sleep(300);
                    }
                },
                token
            );


            // Tạo task1 có sử dụng CancellationToken
            Task task2 = new Task(
                () => {

                    for (int i = 0; i < 10000; i++)
                    {
                        if (token.IsCancellationRequested)
                        {
                            Console.WriteLine("TASK2 STOP");
                            token.ThrowIfCancellationRequested();
                            return;
                        }
                        Console.WriteLine("TASK2 runing ... " + i);
                        Thread.Sleep(300);
                    }
                },
                token
            );

            // Chạy các task
            task1.Start();
            task2.Start();




            while (true)
            {
                var c = Console.ReadKey().KeyChar;

                // Nếu bấm e sẽ phát yêu cầu dừng task
                if (c == 'e')
                {
                    // phát yêu cầu dừng task
                    tokenSource.Dispose();
                    break;
                }

            }

            Console.WriteLine("Các task đã kết thúc, bấm phím bất kỳ kết thúc chương trình");
            Console.ReadKey();
        }
        public static void Test3()
        {
            List<User> users = new List<User>()
            {
                new User { Email = "wjjudcn", FullName = "wbncj", Password = "wjknc", UserId = 1 },
            };

            User u = users.FirstOrDefault(p => p.Email == "wnc");
            Console.WriteLine(u);
            if (u == null)
                Console.WriteLine(123);
        }
        public static void Test4()
        {
            int ConnectionNumber = 8;
            int FileSize = 1223;
            ConnectionNumber = Math.Max(FileSize / ConnectionNumber, 1);
            for (int chunk = 0; chunk < ConnectionNumber; chunk++)
            {
                int Start = chunk * (FileSize / ConnectionNumber);
                int End = chunk == ConnectionNumber - 1 ? FileSize - 1 : (chunk + 1) * (FileSize / ConnectionNumber) - 1;
                Console.WriteLine($"{Start},{End}");
            }
        }
        public static void Test5()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                Console.Write(i);
            }

            stopwatch.Stop();

            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
        }
        public static void Test7()
        {
            int[] powersOfTwo = Enumerable.Range(3, 11).Select(i => (int)Math.Pow(2, i)).ToArray();
            foreach (int power in powersOfTwo)
            { Console.WriteLine(power); }
        }
        public static async Task Test6()
        {
            string url70 = @"https://cdn.gxx.garenanow.com/gxx/pc/installer/Garena-v2.0-VN.exe";
            string url100 = @"https://getsamplefiles.com/download/mp4/sample-1.mp4";
            string url60 = @"https://download.teamviewer.com/download/TeamViewer_Setup_x64.exe";
            string url140 = @"https://desktop.githubusercontent.com/github-desktop/releases/3.3.6-4616f73d/GitHubDesktopSetup-x64.exe";
            string url585 = @"https://download3.vmware.com/software/WKST-1750-WIN/VMware-workstation-full-17.5.0-22583795.exe";
            int c = 4;
            int[] bufferSize = { 8, 16,32,64,128,80,256,512 }; // Mảng ban đầu của bạn
            for (int i = 0; i < bufferSize.Length; i++)
            {
                bufferSize[i] *= 1024; // Nhân mỗi phần tử trong mảng với 15
            }
            
            HttpClient client = new HttpClient();
            await Parallel.ForEachAsync(bufferSize, async (bs, token) =>
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                await DownloadAsync(bs, client, url70, c, $@"C:\Users\cuong\Downloads\videoof1{url70}{bs}.mp4");
                stopwatch.Stop();

                Console.WriteLine($"Execution Time {bs}: {stopwatch.ElapsedMilliseconds} ms");
            });
            Console.WriteLine();
            await Parallel.ForEachAsync(bufferSize, async (bs, token) =>
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                await DownloadAsync(bs, client, url100, c, $@"C:\Users\cuong\Downloads\videoof2{url100}{bs}.mp4");
                stopwatch.Stop();

                Console.WriteLine($"Execution Time {bs}: {stopwatch.ElapsedMilliseconds} ms");
            });
            Console.WriteLine();
            await Parallel.ForEachAsync(bufferSize, async (bs, token) =>
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                await DownloadAsync(bs, client, url60, c, $@"C:\Users\cuong\Downloads\videoof3{url60}{bs}.mkv");
                stopwatch.Stop();

                Console.WriteLine($"Execution Time {bs}: {stopwatch.ElapsedMilliseconds} ms");
            });
            Console.WriteLine();
            await Parallel.ForEachAsync(bufferSize, async (bs, token) =>
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                await DownloadAsync(bs, client, url140, c, $@"C:\Users\cuong\Downloads\videoof1{url140}{bs}.mp4");
                stopwatch.Stop();

                Console.WriteLine($"Execution Time {bs}: {stopwatch.ElapsedMilliseconds} ms");
            });
            Console.WriteLine();
            await Parallel.ForEachAsync(bufferSize, async (bs, token) =>
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                await DownloadAsync(bs, client, url585, c, $@"C:\Users\cuong\Downloads\videoof1{url585}{bs}.mp4");
                stopwatch.Stop();

                Console.WriteLine($"Execution Time {bs}: {stopwatch.ElapsedMilliseconds} ms");
            });
        }
        public static async IAsyncEnumerable<Range> GetSegmentsAsync(int ConnectionNumber, long FileSize)
        {
            for (int chunk = 0; chunk < ConnectionNumber; chunk++)
            {
                yield return new Range()
                {
                    Start = chunk * (FileSize / ConnectionNumber),
                    End = chunk == ConnectionNumber - 1 ? FileSize - 1 : (chunk + 1) * (FileSize / ConnectionNumber) - 1
                };
            }
        }
        public static async Task<bool> DownloadAsync(int bufferSize, HttpClient httpClient, string UrlFileDownload, int ConnectionNumber, string LocalPath)
        {
            ConcurrentDictionary<long, string> tempFilesDictionary = new ConcurrentDictionary<long, string>();
            try
            {
                using HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Head, new Uri(UrlFileDownload));
                using HttpResponseMessage responseHeader = await httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead);

                long FileSize = responseHeader.Content.Headers.ContentLength.GetValueOrDefault();

                await Parallel.ForEachAsync(GetSegmentsAsync(ConnectionNumber, FileSize), async (segment, token) =>
                {
                    using HttpRequestMessage requestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(UrlFileDownload),
                        Headers = { Range = new RangeHeaderValue(segment.Start, segment.End) }
                    };

                    using HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, token);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string tempFilePath = Path.GetTempFileName();
                        using (Stream stream = await responseMessage.Content.ReadAsStreamAsync(token))
                        {
                            using (FileStream outputFileStream = new FileStream(tempFilePath, FileMode.Create))
                            {
                                byte[] buffer = new byte[bufferSize];
                                int bytesRead;

                                

                                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token)) > 0)
                                {

                                    await outputFileStream.WriteAsync(buffer, 0, bytesRead, token);
                                }

                            }
                        }
                        tempFilesDictionary.TryAdd(segment.Start, tempFilePath);
                    }
                    else
                    {
                        throw new Exception("Error response");
                    }
                });

                foreach (var tempFile in tempFilesDictionary.OrderBy(b => b.Key))
                {
                    using (FileStream tempFileStream = new FileStream(tempFile.Value, FileMode.Open))
                    {
                        using (FileStream destinationStream = new FileStream(LocalPath, FileMode.Append))
                        {
                            byte[] buffer = new byte[bufferSize];
                            int bytesRead;
                            while ((bytesRead = await tempFileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            {
                                await destinationStream.WriteAsync(buffer, 0, bytesRead);
                            }
                        }
                    }
                    File.Delete(tempFile.Value);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                if (File.Exists(LocalPath))
                {
                    File.Delete(LocalPath);
                }

                return false;
            }
            finally
            {
                foreach (var tempFile in tempFilesDictionary)
                {
                    File.Delete(tempFile.Value);
                }
                tempFilesDictionary.Clear();
            }
        }
        public static void Test8()
        {
            File.Delete("dev");
        }
        public static bool Test9()
        {
            try
            {
                // Đặt đoạn mã có thể gây ra lỗi ở đây
                Console.WriteLine(123);
                return true;
            }
            catch (Exception)
            {
                // Xử lý lỗi ở đây
                return false; // Gửi lại lỗi để nó có thể được xử lý ở một nơi khác
            }
            finally
            {
                // Đoạn mã này sẽ luôn được thực thi, dù có lỗi hay không
                Console.WriteLine("Đã hoàn thành hàm ExampleFunction.");
            }
        }
        public static async Task Test10()
        {
            List<int> li = new List<int>();
            for(int i = 0; i < 20; i++)
            {
                li.Add(i);
            }
            await Parallel.ForEachAsync(li,async (i, token) =>
            {
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff tt") + "===" + DateTime.Now);
            });
        }
        public static void Test11()
        {
            //string connectionString = "Data Source=localhost;Initial Catalog=master;Integrated Security=True";
            //string queryString = "SELECT * FROM [sys].[dm_tcp_listener_states]";

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand command = new SqlCommand(queryString, connection);
            //    connection.Open();

            //    SqlDataReader reader = command.ExecuteReader();

            //    try
            //    {
            //        while (reader.Read())
            //        {
            //            Console.WriteLine("\nIP Address: {0}\nPort: {1}", reader["ip_address"], reader["port"]);
            //        }
            //    }
            //    finally
            //    {
            //        reader.Close();
            //    }
            //}
        }
        public static async Task Test12()
        {
            string url = "http://example.com/largefile.zip"; // Thay thế bằng URL của bạn
            string path = "largefile.zip"; // Thay thế bằng đường dẫn lưu tệp của bạn

            try
            {
                

                HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                using (Stream contentStream = await response.Content.ReadAsStreamAsync(), fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                {
                    await contentStream.CopyToAsync(fileStream);
                }

                Console.WriteLine("Tải tệp thành công.");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Có lỗi xảy ra: {e.Message}");
            }
        }
        public static async Task DownloadWithWhile(string url, string path)
        {
            using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var output = File.Create(path))
            {
                byte[] buffer = new byte[1024 * 128];
                int bytesRead;
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await output.WriteAsync(buffer, 0, bytesRead);
                }
            }
        }
        public static async Task DownloadWithCopyToAsync(string url, string path)
        {
            using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var output = File.Create(path))
            {
                await stream.CopyToAsync(output);
            }
        }
        public static async Task Test13()
        {
            string url = @"https://filesamples.com/samples/video/mp4/sample_3840x2160.mp4"; // Thay đổi URL này thành URL của tệp bạn muốn tải về
            string path1 = @"C:\Users\cuong\Downloads\file1.mp4"; // Thay đổi đường dẫn này thành đường dẫn mà bạn muốn lưu tệp
            string path2 = @"C:\Users\cuong\Downloads\file2.mp4"; // Thay đổi đường dẫn này thành đường dẫn mà bạn muốn lưu tệp

            Stopwatch stopwatch1 = new Stopwatch();
            Stopwatch stopwatch2 = new Stopwatch();

            Task task1 = Task.Run(async () =>
            {
                stopwatch1.Start();
                Console.WriteLine(DateTime.Now);
                await DownloadWithWhile(url, path1);
                stopwatch1.Stop();
                Console.WriteLine($"Thời gian chạy hàm DownloadWithWhile: {stopwatch1.ElapsedMilliseconds} ms");
            });

            Task task2 = Task.Run(async () =>
            {
                Console.WriteLine(DateTime.Now);
                stopwatch2.Start();
                await DownloadWithCopyToAsync(url, path2);
                stopwatch2.Stop();
                Console.WriteLine($"Thời gian chạy hàm DownloadWithCopyToAsync: {stopwatch2.ElapsedMilliseconds} ms");
            });

            await Task.WhenAll(task1, task2);

            Console.WriteLine(123);
        }
        public static void PrintObjectField(object obj)
        {
            Type type = obj.GetType();

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            int maxLength = fields.Max(f => f.Name.Length);

            Console.WriteLine("Field".PadRight(maxLength + 10) + "Value");

            Console.WriteLine("".PadRight(maxLength + 100, '-'));

            foreach (FieldInfo field in fields)

            {

                Console.WriteLine($"{field.Name.PadRight(maxLength + 10)}{field.GetValue(obj)}");

            }
        }
        public static void PrintObjectProperty(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            int maxLength = properties.Max(p => p.Name.Length);
            Console.WriteLine("Property".PadRight(maxLength + 10) + "Value");
            Console.WriteLine("".PadRight(maxLength + 100, '-'));
            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine($"{property.Name.PadRight(maxLength + 10)}{property.GetValue(obj)}");
            }

        }
        public static async Task Test14()
        {
            var youtube = new YoutubeClient();
            var videoUrl = @"https://www.youtube.com/watch?v=CpfBvcwe4NA"; // Thay thế bằng URL video của bạn
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
            var streamInfo1 = streamManifest.GetAudioOnlyStreams().OrderByDescending(p => p.Bitrate).FirstOrDefault(p => p.Container == Container.Mp4);
            //while (streamInfo1.MoveNext())
            //{
            //    PrintObjectProperty(streamInfo1.Current);
            //    // Thực hiện công việc của bạn với sequenceEnum.Current
            //}
            //var streamInfo2 = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            ////Console.WriteLine(streamInfo.);
            PrintObjectProperty(streamInfo1);
        }
        public static async Task DownloadFileAsync(this HttpClient httpClient, string requestUri, string filename)
        {
            Console.WriteLine(requestUri);
            using var response = await httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            await using var ms = await response.Content.ReadAsStreamAsync();
            await using var fs = File.Create(filename);
            ms.Seek(0, SeekOrigin.Begin);
            await ms.CopyToAsync(fs);
        }
        public static async Task Test15()
        {
            var youtube = new YoutubeClient();

            var streamManifest = await youtube.Videos.Streams
                                               .GetManifestAsync("Rj5_MPe2JI0");

            var audioStream = streamManifest.GetAudioOnlyStreams()
                                             .GetWithHighestBitrate();

            //using (var output = File.OpenWrite("audio.mp3"))
            //{
            //    await youtube.Videos.Streams.CopyToAsync(audioStream, output);
            //}

            HttpClient httpClient = new HttpClient();   
            using (HttpResponseMessage response = await httpClient.GetAsync(audioStream.Url))
            {
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    using (FileStream outputFileStream = new FileStream("audio.mp3", FileMode.Create))
                    {
                        await stream.CopyToAsync(outputFileStream);
                    }
                }
            }
        }
        public static async Task Test16()
        {
            string outputDirectory = @"C:\Users\cuong\Downloads";

            // List of YouTube video URLs to download
            List<string> videoUrls = new List<string>
            {

                "https://www.youtube.com/watch?v=Rj5_MPe2JI0",
                // Add more video URLs as needed
            };
            try
            {
                foreach (var videoUrl in videoUrls)
                {
                    await DownloadYouTubeVideo(videoUrl, outputDirectory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while downloading the videos: " + ex.Message);
            }
        }
        static async Task DownloadYouTubeVideo(string videoUrl, string outputDirectory)
        {
            var youtube = new YoutubeClient();
            var video = await youtube.Videos.GetAsync(videoUrl);

            // Sanitize the video title to remove invalid characters from the file name
            string sanitizedTitle = string.Join("_", video.Title.Split(Path.GetInvalidFileNameChars()));

            // Get all available muxed streams
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
            var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            //await youtube.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");
            var stream = await youtube.Videos.Streams.GetAsync(streamInfo);

            // Download the stream to a file
            //await youtube.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");

            //using var httpClient = new HttpClient();
            //var stream = await httpClient.GetStreamAsync(streamInfo.Url);
            //var datetime = DateTime.Now;

            //string outputFilePath = Path.Combine(outputDirectory, $"{sanitizedTitle}.{streamInfo.Container}");
            //using var outputStream = File.Create(outputFilePath);
            //await stream.CopyToAsync(outputStream);

            //Console.WriteLine("Download completed!");
            //Console.WriteLine($"Video saved as: {outputFilePath}{datetime}");
        }
        static async Task<bool> DownloadAsyncREUSE(string url, string localpath)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetStreamAsync(url))
                {
                    using ( var stream = new FileStream(localpath, FileMode.Create, FileAccess.Write))
                    {
                        await response.CopyToAsync(stream);
                        return true;
                    }
                }
            }
        }
        public static async Task Test17()
        {
            var youtube = new YoutubeClient();
            var videoUrl = @"https://www.youtube.com/watch?v=CpfBvcwe4NA"; // Thay thế bằng URL video của bạn
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
            var streamInfo1 = streamManifest.GetAudioOnlyStreams().FirstOrDefault(p => p.Container == Container.Mp4);

            var streamInfo2 = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();

            await DownloadWithWhile(streamInfo1.Url, @"C:\Users\cuong\Downloads\a.mp4");
        }
        public static void PrintEnumerable(IEnumerable enumerable)

        {

            foreach (var item in enumerable)

            {

                Console.WriteLine(item);
                PrintObjectProperty(item);
            }

        }
        public static async Task Test18()
        {
            

        }
        public static async Task Test19()
        {
            var youtube = new YoutubeClient();

            // You can specify either the video URL or its ID
            var videoUrl = "https://youtube.com/watch?v=hfWyw9MzNTo";
            var video = await youtube.Videos.GetAsync(videoUrl); // url, title
            PrintObjectProperty(video);

            //var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);

            ////printObjectProperty(streamManifest.GetVideoStreams());
            //PrintObjectProperty(streamManifest.GetMuxedStreams().GetWithHighestVideoQuality());
        }
        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //public static DataTable ToDataTable<T>(List<T> data)
        //{
        //    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        //    DataTable dataTable = new DataTable();

        //    foreach (PropertyDescriptor prop in properties)
        //    {
        //        if (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
        //        {
        //            dataTable.Columns.Add(prop.Name, prop.PropertyType);
        //        }
        //    }

        //    foreach (T item in data)
        //    {
        //        DataRow row = dataTable.NewRow();
        //        foreach (PropertyDescriptor prop in properties)
        //        {
        //            if (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
        //            {
        //                row[prop.Name] = prop.GetValue(item)??DBNull.Value;
        //            }
        //        }
        //        dataTable.Rows.Add(row);
        //    }
        //    return dataTable;
        //}

        public static bool SendEmail(string sendMailTo, string subJect, string body)
        {
            try
            {
                string sendMailFrom = "testpblne@gmail.com";
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.EnableSsl = true;
                SmtpServer.Credentials = new NetworkCredential(sendMailFrom, "jelx rpvk wzvh wmsj");
                SmtpServer.Send(new MailMessage(sendMailFrom, sendMailTo, subJect, body));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GenerateRandomString(int length)
        {
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";

            string characters = uppercase + lowercase + digits;

            Random random = new Random();
            StringBuilder result = new StringBuilder(length);
            result.Append(uppercase[random.Next(uppercase.Length)]);
            result.Append(lowercase[random.Next(lowercase.Length)]);
            result.Append(digits[random.Next(digits.Length)]);

            for (int i = result.Length; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }

            return new string(result.ToString().OrderBy(x => random.Next()).ToArray());
        }

        public static bool CheckPassword(string password)
        {
            return new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$").IsMatch(password);
        }
    }
}
