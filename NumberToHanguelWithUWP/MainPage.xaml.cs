using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NumberToHanguelWithUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            String sData = textBox.Text;
            String Korean ="";
            int iData;

            if (!int.TryParse(sData, out iData) || !change_number_to_korean(iData, out Korean))
            {
                SimpleMessageDialog("입력값을 확인해주세요");
                return;
            }
            textBlock.Text = Korean;
        }

        public bool change_number_to_korean(int iData, out String result)
        {
            result = "";

            //숫자의 범위를 벗어남
            if (-99999 > iData || iData > 99999)
            {
                return false;
            }

           // 0인 경우
            if( iData == 0)
            {
                result += "영";
                return true;
            }

            //음수인 경우
            if ( iData < 0)
            {
                iData *= -1;
                result += "마이너스 ";
            }

            /* 본격적인 바꾸는 코드 */
            String[] oneDigit = { "", "", "이", "삼", "사", "오", "육", "칠", "팔", "구" };
            String[] tensDigit = {"","","십", "백", "천", "만"};
            String sData = iData.ToString();


            int num;
            int numLength = sData.Length;
            for (int i=0; i<numLength; i++)
            {
                int.TryParse(sData[i].ToString(),out num);
                result += oneDigit[num];

                if( num != 0)
                {
                     result += tensDigit[numLength-i];
                }
            }

            if( sData[numLength-1] == '1')
            {
                result += "일";
            }
            return true;

        }

        private async void SimpleMessageDialog(String text)
        {
            var dialog = new MessageDialog(text);
            await dialog.ShowAsync();
        }
    }
}
