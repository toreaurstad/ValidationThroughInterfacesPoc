using System.ComponentModel.DataAnnotations;

namespace ValidationThroughInterfacesPoc.Test
{
    public class Car : IVehicle
    {
        [MinLength(2, ErrorMessage = "Minimum length of model is two characters")]
        public string Model { get; set; }
        public string Make { get; set; }
        public int WheelCount { get; set; }
    }
}
