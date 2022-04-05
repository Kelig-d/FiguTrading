using System;
using System.Collections.Generic;
using System.Text;

namespace FiguTrading.Interfaces
{
    public interface IMyImageCompressor
    {
        string ImageCompressor(byte[] bitmap);

    }
}