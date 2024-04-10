using System.Security.Cryptography;

// Generate a secure key with the required size for HMAC-SHA512
//byte[] keyBytes = GenerateSecureKey(64); // 64 bytes = 512 bits

// Convert the byte array to a base64-encoded string (for storage or transmission)
//string base64Key = Convert.ToBase64String(keyBytes);

//Console.WriteLine(base64Key);

Console.WriteLine("INICIO DEL PROGRAMA");

await Metodo1();
await Metodo2();

Console.WriteLine("FIN DEL PROGRAMA");

Console.ReadLine();

//async | await
static async Task Metodo1()
{
    Console.WriteLine("Inicio del Metodo1");
    await Task.Delay(4000);
    Console.WriteLine("Fin del Metodo1");
}

static async Task Metodo2()
{
    Console.WriteLine("Inicio del Metodo2");
    await Task.Delay(7000);
    Console.WriteLine("Fin del Metodo2");
}

static byte[] GenerateSecureKey(int keySizeInBytes)
{
    using (var randomNumberGenerator = new RNGCryptoServiceProvider())
    {
        byte[] keyBytes = new byte[keySizeInBytes];
        randomNumberGenerator.GetBytes(keyBytes);
        return keyBytes;
    }
}