using Avalonia.Data;
using Avalonia.Data.Converters;
using PlaylistEditor.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace PlaylistEditor.Views.Resources
{
    public class EnumToBooleanConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // XAMLでのデータバインドの記述にあるパラメーターを文字列として取り出す
            // そこには、Enumのいずれかの値が入っているはずである
            var param = parameter as string;
            if (param == null)
                return DependencyProperty.UnsetValue;

            // パラメーターを列挙型に変換する（例："Red"→Enum.Red）
            object paramValue = Enum.Parse(typeof(ItemType), param);

            // バインディングソースの値（value）とパラメーターが等しかったらtrueを返す
            return paramValue.Equals(value);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var param = parameter as string;
            if (param == null)
                return DependencyProperty.UnsetValue;

            // バインディングソースに書き戻すときは、パラメーター文字列に見合った列挙型を返せばよい
            return Enum.Parse(typeof(ItemType), param);
        }
    }
}
