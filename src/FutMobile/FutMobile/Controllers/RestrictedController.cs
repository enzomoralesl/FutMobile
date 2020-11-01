using FutMobile.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FutMobile.Controllers
{
    [Authorize]
    public class RestrictedController : Controller
    {
        // GET: Restricted
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Ajuda()
        {
            return View("Ajuda");
        }

        //
        // POST: /Restricted/Ajuda
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Ajuda(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Email 1: Email para o administrador

                    var Info = new ContactViewModel { Email = model.Email, Mensagem = model.Mensagem, Nome = model.Nome, Sobrenome = model.Sobrenome, DDD = model.DDD, Tel = model.Tel };

                    IdentityMessage message = new IdentityMessage();

                    string mensagemeditada = Info.Mensagem.Replace(Environment.NewLine, "<br />");

                    message.Body = mensagemeditada + "<br /><br /><br />Informações do usuário:<br />Nome do usuário: " + Info.Nome + " " + Info.Sobrenome + "<br />Email do usuario: " + Info.Email + "<br />Telefone do usuario: (" + Info.DDD + ") " + Info.Tel;
                    message.Destination = "marcelopts151@gmail.com";
                    message.Subject = "Solicitação de Contato - " + Info.Nome + " " + Info.Sobrenome;

                    EmailService emailService = new EmailService();

                    await emailService.SendAsync(message);

                    // Email 2: Email de confirmação de solicitação de suporte para o usuario

                    IdentityMessage message2 = new IdentityMessage();

                    message2.Body = "Solicitação de contato enviada com sucesso!";
                    message2.Destination = Info.Email;
                    message2.Subject = "Sua solicitação de Contato no FutMobile foi enviada!";

                    EmailService emailService2 = new EmailService();

                    await emailService2.SendAsync(message2);
                }
                catch
                {
                    ModelState.AddModelError("", "Ocorreu um erro, o e-mail não foi enviado. Por favor, tente novamente.");
                    return View(model);
                }
            }

            // Se chegamos até aqui e houver alguma falha, exiba novamente o formulário
            return View(model);
        }
    }
}