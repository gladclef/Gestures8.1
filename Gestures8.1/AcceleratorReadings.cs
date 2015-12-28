using Microsoft.Band.Sensors;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestures8._1
{
    class AcceleratorReadings
    {
        private List<ConcurrentQueue<double>> registeredReadings = new List<ConcurrentQueue<double>>() {
            new ConcurrentQueue<double>(),
            new ConcurrentQueue<double>(),
            new ConcurrentQueue<double>()};
        public static int READINGS_MAX = 200;
        public static AcceleratorReadings Instance = new AcceleratorReadings();

        public void registerUpdatedReading(Object sender, BandSensorReadingEventArgs<IBandAccelerometerReading> reading)
        {
            registeredReadings.ElementAt(0).Enqueue(reading.SensorReading.AccelerationX);
            registeredReadings.ElementAt(1).Enqueue(reading.SensorReading.AccelerationY);
            registeredReadings.ElementAt(2).Enqueue(reading.SensorReading.AccelerationZ);

            double retval = 0;
            foreach (ConcurrentQueue<double> readings in registeredReadings)
            {
                while (readings.Count > READINGS_MAX)
                {
                    readings.TryDequeue(out retval);
                }
            }
        }

        public ConcurrentQueue<double> getXReadings()
        {
            return registeredReadings.ElementAt(0);
        }

        public ConcurrentQueue<double> getYReadings()
        {
            return registeredReadings.ElementAt(1);
        }

        public ConcurrentQueue<double> getZReadings()
        {
            return registeredReadings.ElementAt(2);
        }
    }
}
