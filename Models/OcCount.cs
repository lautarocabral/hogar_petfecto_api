﻿namespace hogar_petfecto_api.Models
{
    public class OcCount
    {
        public OcCount()
        {

        }
        public OcCount(int nroOc)
        {
            NroOc = nroOc;
        }
        public int Id { get; private set; }

        public int NroOc { get; private set; }

        // Si es necesario modificar el número de orden
        public void SetNroOc(int newNroOc)
        {
            NroOc = newNroOc;
        }
    }

}
