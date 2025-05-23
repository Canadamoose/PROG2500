﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Screens_01
{
    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.Equals(parameter);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => (bool)value ? parameter : Binding.DoNothing;
    }
}
