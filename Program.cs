using System.Text;
using Microsoft.Azure.Devices.Client;
using device.Models;
using Newtonsoft.Json;

var connectionString = "HostName=tjorvenIOT.azure-devices.net;DeviceId=pctjorven;SharedAccessKey=vhLhIx3dmdN7ptyglg87Qks62ZD6S6m+QYIUQhCZ4wM=";

using var deviceClient = DeviceClient.CreateFromConnectionString(connectionString);


//// open connection explicitly
await deviceClient.OpenAsync();



while (true)
{
    await SendMessage();
    Thread.Sleep(1000);
}


async Task SendMessage()
{
    TemperatureMessage tpm = new TemperatureMessage();
    Random rnd = new Random();
    tpm.Temperature = rnd.Next(0, 100);

    var json = JsonConvert.SerializeObject(tpm);
    using var message = new Message(Encoding.UTF8.GetBytes(json));
    await deviceClient.SendEventAsync(message);
    Console.WriteLine("A single message is sent");

}