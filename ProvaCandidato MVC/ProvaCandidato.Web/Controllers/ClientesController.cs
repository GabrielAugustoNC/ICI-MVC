using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Helper;

namespace ProvaCandidato.Controllers
{
    public class ClientesController : GenericClass<Cliente>
    {
        private ContextoPrincipal db = new ContextoPrincipal();


        // GET: Clientes
        public ActionResult Index(string search)
        {
            var clientes = db.Clientes
                             .Where(w => w.Ativo && (string.IsNullOrEmpty(search) || w.Nome.Contains(search)))
                             .Include(c => c.Cidade);

            return View(clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            cliente.Observacoes = db.ClienteObservacoes.Where(w => w.Cliente.Codigo == cliente.Codigo).ToList();
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nome,DataNascimento,CidadeId,Ativo")] Cliente cliente)
        {

            if (ModelState.IsValid)
            {
                if (cliente.DataNascimento.Value <= DateTime.Today)
                {
                    db.Clientes.Add(cliente);
                    db.SaveChanges();
                    MessageHelper.DisplaySuccessMessage(this, "Operação efetuada com sucesso");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DataNascimento", "A data de nascimento de ter valor menor ou igual a data de hoje");
                }
            }

            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nome,DataNascimento,CidadeId,Ativo")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, "Operação efetuada com sucesso");
                return RedirectToAction("Index");
            }
            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            MessageHelper.DisplaySuccessMessage(this, "Operação efetuada com sucesso");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult CreateObservationNewPartial()
        {
            return View("ObservationNew", new ClienteObservacao());
        }

        #region Chamadas Ajax
        [HttpGet]
        public object GetCities()
        {
            var cities = db.Cidades.ToList();
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("SaveClienteObservation")]
        public JsonResult SaveClienteObservation(string observacao, string referencia, int clienteId)
        {
            if (ModelState.IsValid)
            {
                var clienteObservacao = new ClienteObservacao();

                clienteObservacao.Observacao = observacao;
                clienteObservacao.Referencia = referencia;
                clienteObservacao.Cliente = db.Clientes.Find(clienteId);

                db.ClienteObservacoes.Add(clienteObservacao);
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, "Operação efetuada com sucesso");
            }

            return Json(new { result = ModelState.IsValid });
        }
        #endregion
    }
}
