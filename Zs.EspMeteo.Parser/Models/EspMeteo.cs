using System.Collections.Generic;
using System.Linq;

namespace Zs.EspMeteo.Parser.Models;

public sealed class EspMeteo
{
    public IReadOnlyList<Sensor> Sensors { get; }

    public EspMeteo(IEnumerable<Sensor> sensors)
    {
        Sensors = sensors.ToList();
    }
}