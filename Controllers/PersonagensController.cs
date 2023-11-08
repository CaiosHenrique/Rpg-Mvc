using RpgMvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using RpgMvc.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

{
    
}

public class PersonagensController : Controller
{
public string uriBase = "xyz/Personagens/";

[HttpGet]
public async Task<ActionsResult> IndexAsync()
{
    try
    {
        string uriComplementar = "GetAll";
        HttpClient httpClient = new HttpClient();
        string token = httpContext.Session.GetString("SessionTokenUsuario");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage reponse = await httpClient.GetAsync(uriBase + uriComplementar);
        string serialized = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            List<PersonagemViewModel> listaPersonagens = await Task.Run(() =>
            JsonConvert.DeserializeObject<listaPersonagens<PersonagemViewModel>>(serialized));
            return View (listaPersonagens);
        }
    }
    catch (System.Exception ex)
    {
        TempData["MensagemErro"] = ex.Message;
        return RedirectToAction("Index");
    }
}
[HttpPost]
public async Task<ActionResult> CreateAsync(PersonagemViewModel p)
{
    try
    {
        HttpClient httpClient = new HttpClient();
        string token = HttpContext.Session.GetString("SessionTokenUsuario");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var content = new StringContent(JsonConvert.SerializeObject(p));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        HttpResponseMessage response = await response.Content.ReadAsStringAsync();

        if(response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            TempData["Mensagem"] =string.Format("Personagem {0}, Id {1} salvo com sucesso!", p.Nome, serialized);
            return RedirectToAction("Index");
        }
        else
        throw new System.Exception(serialized);
    }
    catch (System.Exception ex)
    {
        tempData["MensagemErro"] = ex.Message;
        return RedirectToAction("Create");
    }
}
[HttpGet]
public ActionResult Create()
{
    return View();
}
[HttpGet]
public async Task<ActionResult> DetailsAsync(int? id)
{
    try
    {
        HttpClient httpClient = new HttpClient();
        string token = HttpContext.Session.GetString("SessionTokenUsuario");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());
        string serialized = await response.Content.ReadAsStringAsync();

        if(response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            PersonagemViewModel p = await Task.Run(()
            => JsonConvert.DeserializeObject<PersonagemViewModel>(serialized));
            return View(p);
        }
        else
        throw new System.Exception(serialized);
    }
    catch (System.Exception ex)
    {
        TempData["MensagemErro"] = ex.Message;
        return RedirectToAction("Index");
    }
}
}