using Microsoft.Band;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestures8._1
{
    class BandFinder
    {
        public static BandFinder Instance = new BandFinder();

        private IBandClient bandClient = null;

        public async Task<IBandClient> findBand()
        {
            // return the already established bandClient
            if (bandClient != null)
            {
                return bandClient;
            }

            // find the set of bands
            System.Diagnostics.Debug.WriteLine("finding bands");
            IBandInfo[] pairedBands = await BandClientManager.Instance.GetBandsAsync();
            System.Diagnostics.Debug.WriteLine("found " + pairedBands.Length + " bands");

            // connect to the one band
            bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]);
            System.Diagnostics.Debug.WriteLine("Found band " + bandClient);
            return bandClient;
        }
    }
}
