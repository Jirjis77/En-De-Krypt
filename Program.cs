var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader())
);

var app = builder.Build();
app.UseCors();

app.MapGet("/", () => "Hello World!");

// Endpoint för kryptering
app.MapGet("/encrypt", (string name, int shift) => EncryptName(name, shift));

// Endpoint för avkryptering
app.MapGet("/decrypt", (string name, int shift) => DecryptName(name, shift));

app.Run();

// Metod för att kryptera namn
string EncryptName(string name, int shift)
{
    char[] buffer = name.ToCharArray();
    for (int i = 0; i < buffer.Length; i++)
    {
        char letter = buffer[i];
        letter = (char)(letter + shift);
        if (letter > 'z')
        {
            letter = (char)(letter - 26);
        }
        else if (letter < 'a')
        {
            letter = (char)(letter + 26);
        }
        buffer[i] = letter;
    }
    return new string(buffer);
}

// Metod för att avkryptera namn
string DecryptName(string name, int shift)
{
    return EncryptName(name, -shift);
}
