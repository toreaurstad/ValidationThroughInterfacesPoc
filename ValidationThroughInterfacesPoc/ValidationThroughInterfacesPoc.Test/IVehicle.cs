using System.ComponentModel.DataAnnotations;

namespace ValidationThroughInterfacesPoc.Test
{
    public interface IVehicle
    {
        [Required(ErrorMessage = "Provide model of the vehicle, e.g. A4")]
        string Model { get; set; }

        [Required(ErrorMessage = "Provide make of the vehicle, e.g. Audi")]
        string Make { get; set; }


        [Range(1, 16, ErrorMessage = "Provide numer of wheels, 1-16")]
        int WheelCount { get; set; }
    }
}
