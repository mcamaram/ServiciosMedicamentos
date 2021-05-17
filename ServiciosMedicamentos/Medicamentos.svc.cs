using ServiciosMedicamentos.Clases;
using ServiciosMedicamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiciosMedicamentos
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Medicamentos : IMedicamentos
    {
        public int eliminarMedicamento(int iidMedicamento)
        {
            int rpta = 0;
            try
            {
                using (var db = new MedicoEntities1())
                {
                    Medicamento oMedicamento = db.Medicamento.Where(p => p.IIDMEDICAMENTO == iidMedicamento).First();
                    oMedicamento.BHABILITADO = 0;
                    db.SaveChanges();
                    rpta = 1;
                }
            }
            catch (Exception e)
            {
                rpta = 0;
            }
            return rpta;
        }

        public List<FormaFarmaceuticaCLS> listarFormaFarmaceutica()
        {
            var lstFormaFarmaceutica = new List<FormaFarmaceuticaCLS>();
            try
            {
                using(var db = new MedicoEntities1())
                {
                    lstFormaFarmaceutica = db.FormaFarmaceutica.Where(p => p.BHABILITADO == 1).Select(P => new FormaFarmaceuticaCLS
                    {
                        iidformafarmaceutica = P.IIDFORMAFARMACEUTICA,
                        nombreFormaFarmaceutica = P.NOMBRE
                    }).ToList();
                }
            }
            catch(Exception e)
            {
                lstFormaFarmaceutica = null;
            }
            return lstFormaFarmaceutica;
        }

        public List<MedicamentoCLS> listarMedicamentos()
        {
            var lstMedicamento = new List<MedicamentoCLS>();
            try
            {
                using(var db = new MedicoEntities1())
                {
                    lstMedicamento = (from medicamento in db.Medicamento
                                      join formafarmaceutica in db.FormaFarmaceutica
                                      on medicamento.IIDFORMAFARMACEUTICA equals formafarmaceutica.IIDFORMAFARMACEUTICA
                                      select new MedicamentoCLS
                                      {
                                        iidmedicamento = medicamento.IIDMEDICAMENTO,
                                        nombre = medicamento.NOMBRE,
                                        precio = (decimal)medicamento.PRECIO,
                                        nombreFormaFarmaceutica = formafarmaceutica.NOMBRE,
                                        concentracion = medicamento.CONCENTRACION,
                                        presentacion = medicamento.PRESENTACION,
                                        stock = (int)medicamento.STOCK
                                      }).ToList();
                }
            }
            catch(Exception e)
            {
                lstMedicamento = null;
            }
            return lstMedicamento;
        }

        public MedicamentoCLS recuperarMedicamento(int iidMedicamento)
        {
            var objMedicamentocls = new MedicamentoCLS();
            try
            {
                using(var db = new MedicoEntities1())
                {
                    Medicamento objMedicamento = db.Medicamento.Where(p => p.IIDMEDICAMENTO == iidMedicamento).First();
                    objMedicamentocls.iidmedicamento = objMedicamento.IIDMEDICAMENTO;
                    objMedicamentocls.iidformafarmaceutica = (int)objMedicamento.IIDFORMAFARMACEUTICA;
                    objMedicamentocls.nombre = objMedicamento.NOMBRE;
                    objMedicamentocls.precio = (decimal)objMedicamento.PRECIO;
                    objMedicamentocls.stock = (int)objMedicamento.STOCK;
                    objMedicamentocls.concentracion = objMedicamento.CONCENTRACION;
                    objMedicamentocls.presentacion = objMedicamento.PRESENTACION;
                }
            }
            catch(Exception e)
            {
                objMedicamentocls = null;
            }
            return objMedicamentocls;
        }

        public int registrarYactualizarMedicamento(MedicamentoCLS oMedicamentoCLS)
        {
            int rpta = 0;
            try
            {
                using(var db = new MedicoEntities1())
                {
                    if(oMedicamentoCLS.iidformafarmaceutica == 0)
                    {
                        Medicamento omedicamento = new Medicamento();
                        omedicamento.NOMBRE = oMedicamentoCLS.nombre;
                        omedicamento.PRECIO = oMedicamentoCLS.precio;
                        omedicamento.STOCK = oMedicamentoCLS.stock;
                        omedicamento.IIDFORMAFARMACEUTICA = oMedicamentoCLS.iidformafarmaceutica;
                        omedicamento.CONCENTRACION = oMedicamentoCLS.concentracion;
                        omedicamento.PRESENTACION = oMedicamentoCLS.presentacion;
                        omedicamento.BHABILITADO = 1;
                        db.Medicamento.Add(omedicamento);
                        db.SaveChanges();
                        rpta = 1;
                    }
                    else
                    {
                        Medicamento objMedicamento = db.Medicamento.Where(p => p.IIDMEDICAMENTO == oMedicamentoCLS.iidmedicamento).First();
                        objMedicamento.IIDMEDICAMENTO = oMedicamentoCLS.iidmedicamento;
                        objMedicamento.IIDFORMAFARMACEUTICA = oMedicamentoCLS.iidformafarmaceutica;
                        objMedicamento.NOMBRE = oMedicamentoCLS.nombre;
                        objMedicamento.PRECIO = oMedicamentoCLS.precio;
                        objMedicamento.STOCK = oMedicamentoCLS.stock;
                        objMedicamento.CONCENTRACION = oMedicamentoCLS.concentracion;
                        objMedicamento.PRESENTACION = oMedicamentoCLS.presentacion;
                        db.SaveChanges();
                        rpta = 1;
                    }
                }
            }
            catch(Exception e)
            {
                rpta = 0;
            }
            return rpta;
        }
    }
}
