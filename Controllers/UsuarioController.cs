using System;
using Microsoft.AspNetCore.Mvc;
using Destino_Certo.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Destino_Certo.Controllers
{
    public class UsuarioController : Controller
    {
       
        
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(Usuario u)
        {
            UsuarioRepository ur = new UsuarioRepository();
            ur.Insert(u);


            ViewBag.Mensagem = " Cadastrado com Sucesso";
            return View("CadastroSucesso");
        }
        public IActionResult CadastroSucesso()
        {
            
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Usuario u)
        {
            UsuarioRepository ur = new UsuarioRepository();
            Usuario usuario = ur.QueryLogin(u);

            
            
            if(usuario != null)
            {
            
                HttpContext.Session.SetInt32("idUsuarioUsuario", usuario.Id);

                HttpContext.Session.SetString("nomeUsuario", usuario.Nome);
                return View("CadastroPacote");
            }
            else
            {
                ViewBag.Mensagem = "Falha no Login";
                return View("Login");
            }
        }
        
        public IActionResult CadastroPacote()
        {
            if (HttpContext.Session.GetInt32("idUsuarioUsuario") == null)
                return RedirectToAction("Login");
            return View();
        }
        [HttpPost]
        public IActionResult CadastroPacote(PacotesTuristicos p)
        {
            if (HttpContext.Session.GetInt32("idUsuarioUsuario") == null)
                return RedirectToAction("Login");

            PacoteTuristicosRepository pc = new PacoteTuristicosRepository();
           
            pc.Insert(p);

            
            ViewBag.Mensagem = "Pacote Cadastrado com Sucesso";
            return View();
        }
        public IActionResult AlterarPacotes()
        {
            if (HttpContext.Session.GetInt32("idUsuarioUsuario") == null)
                return RedirectToAction("Login");
            return View();
        }
        [HttpPost]
        public IActionResult AlterarPacotes(PacotesTuristicos p)
        {
            if (HttpContext.Session.GetInt32("idUsuarioUsuario") == null)
                return RedirectToAction("Login");

            PacoteTuristicosRepository pc = new PacoteTuristicosRepository();
           
            pc.Alterar(p);

            
            ViewBag.Mensagem = "Pacote Alterado com Sucesso";
            return View();
        }
        public IActionResult ExcluirPacotes()
        {
            if (HttpContext.Session.GetInt32("idUsuarioUsuario") == null)
                return RedirectToAction("Login");
            return View();
        }
        [HttpPost]
        public IActionResult ExcluirPacotes(PacotesTuristicos p)
        {
            if (HttpContext.Session.GetInt32("idUsuarioUsuario") == null)
                return RedirectToAction("Login");

            PacoteTuristicosRepository pc = new PacoteTuristicosRepository();
           
            pc.Excluir(p);

            
            ViewBag.Mensagem = "Pacote Excluido com Sucesso";
            return View();
        }
        public IActionResult Listar()
        {
            if(HttpContext.Session.GetInt32("idUsuarioUsuario") == null)
            return RedirectToAction("Login");

            PacoteTuristicosRepository p = new PacoteTuristicosRepository();

            List<PacotesTuristicos> pacotes = p.Listar();
            return View(pacotes);

        }
        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
    }
}