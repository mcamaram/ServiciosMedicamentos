using ServiciosMedicamentos.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiciosMedicamentos
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IMedicamentos
    {
        //Listado Medicamentos
        [OperationContract]
        List<MedicamentoCLS> listarMedicamentos();
        //Lista forma farmaceutica
        [OperationContract]
        List<FormaFarmaceuticaCLS> listarFormaFarmaceutica();
        //Recuperar Medicamento
        [OperationContract]
        MedicamentoCLS recuperarMedicamento(int iidMedicamento);
        //Agregar y editar medicamento
        [OperationContract]
        int registrarYactualizarMedicamento(MedicamentoCLS oMedicamentoCLS);
        //Eliminar medicamento
        [OperationContract]
        int eliminarMedicamento(int iidMedicamento);
        // TODO: agregue aquí sus operaciones de servicio
    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
}
