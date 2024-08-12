using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Base_Tarea1.Models;

namespace Base_Tarea1.Controllers
{
    public class PacienteController : Controller
    {
        private readonly string filePath = "pacientes.json";

        public IActionResult Index()
        {
            var pacientes = LeerPacientes();
            return View(pacientes); // Devuelve la vista con la lista de pacientes
        }

        [HttpPost]
        public IActionResult AgregarPaciente(Paciente nuevoPaciente)
        {
            var pacientes = LeerPacientes();
            pacientes.Add(nuevoPaciente);
            GuardarPacientes(pacientes);
            return RedirectToAction("Index");
        }

        private List<Paciente> LeerPacientes()
        {
            if (!System.IO.File.Exists(filePath))
            {
                return new List<Paciente>();
            }

            var jsonData = System.IO.File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Paciente>>(jsonData) ?? new List<Paciente>();
        }

        private void GuardarPacientes(List<Paciente> pacientes)
        {
            var jsonData = JsonSerializer.Serialize(pacientes);
            System.IO.File.WriteAllText(filePath, jsonData);
        }
    }
}
