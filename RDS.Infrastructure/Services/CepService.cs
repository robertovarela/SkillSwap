using System.Net.Http.Json;
using RDS.Core.Libraries.Services;

namespace RDS.Infrastructure.Services;

public class CepService //: ICepService
{
    public async Task<LocalAddress?> SearchByPostalCode(string postalCode)
    {
        postalCode = postalCode.Replace(".", string.Empty).Replace("-", string.Empty);
        var url = $"https://viacep.com.br/ws/{postalCode}/json/";

        var http = new HttpClient();
        return await http.GetFromJsonAsync<LocalAddress>(url);
    }

    public async Task<T> GetDataOfCep<T>(string cep, T model) where T : class, new()
    {
        model.GetType().GetProperty("State")?.SetValue(model, string.Empty);
        model.GetType().GetProperty("City")?.SetValue(model, string.Empty);
        model.GetType().GetProperty("Neighborhood")?.SetValue(model, string.Empty);
        model.GetType().GetProperty("Street")?.SetValue(model, string.Empty);

        var address = await SearchByPostalCode(cep);

        model.GetType().GetProperty("State")?.SetValue(model, address?.UF);
        model.GetType().GetProperty("City")?.SetValue(model, address?.Localidade);
        model.GetType().GetProperty("Neighborhood")?.SetValue(model, address?.Bairro);
        model.GetType().GetProperty("Street")?.SetValue(model, address?.Logradouro);
        model.GetType().GetProperty("Complement")?.SetValue(model, address?.Complemento);

        return model;
    }
}

