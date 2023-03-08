using System.ComponentModel;

namespace TCCFatecWorkshop.Models.Enums
{
    public enum ServicesSituation
    {
        [Description("Pendente")]
        Pending=0,
        [Description("Faturado")]
        Billed =1,
        [Description("Atrasado")]
        Delayed =2,
        [Description("Cancelado")]
        Canceled =3
    }
}
