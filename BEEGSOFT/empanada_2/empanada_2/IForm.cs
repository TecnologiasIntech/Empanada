﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace empanada_2
{
    public interface IForm
    {
        void ChangeTextBoxText(string text, int id);
    }

    public interface IForm2
    {
        void Relogear_ordenes();
    }
}
