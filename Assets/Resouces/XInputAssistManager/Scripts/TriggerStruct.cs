﻿#region Lisence

/*
【English】
BSD 2-Clause License

Copyright (c) 2019, Haku Kaido
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

【日本語】
Copyright (c) <2019>, <Haku Kaido>
All rights reserved.

ソースコード形式かバイナリ形式か、変更するかしないかを問わず、以下の条件を満たす場合に限り、再頒布および使用が許可されます。 

・ソースコードを再頒布する場合、上記の著作権表示、本条件一覧、および下記免責条項を含めること。 
・バイナリ形式で再頒布する場合、頒布物に付属のドキュメント等の資料に、上記の著作権表示、本条件一覧、および下記免責条項を含めること。 
 
本ソフトウェアは、著作権者およびコントリビューターによって「現状のまま」提供されており、明示黙示を問わず、商業的な使用可能性、および特定の目的に対する適合性に関する暗黙の保証も含め、またそれに限定されない、いかなる保証もありません。著作権者もコントリビューターも、事由のいかんを問わず、 損害発生の原因いかんを問わず、かつ責任の根拠が契約であるか厳格責任であるか（過失その他の）不法行為であるかを問わず、仮にそのような損害が発生する可能性を知らされていたとしても、本ソフトウェアの使用によって発生した（代替品または代用サービスの調達、使用の喪失、データの喪失、利益の喪失、業務の中断も含め、またそれに限定されない）直接損害、間接損害、偶発的な損害、特別損害、懲罰的損害、または結果損害について、一切責任を負わないものとします。 

このライセンスはBSD 2-Clause Licenseを利用しています
*/

#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XInputAssistManager
{
    /// <summary>
    /// Axisの値を取るための構造体
    /// 
    /// 作成者:海堂 博
    /// 公開ユーザー:https://github.com/HakuKaido
    /// 作成日時:2018/10/23
    /// </summary>
    public struct TriggerStruct
    {
        ButtonList trigger;
        float axis;

        /// <summary>
        /// コンストラクタ 
        /// </summary>
        /// <param name="trigger">※RightTriggerかLeftTriggerを入れること！！</param>
        public TriggerStruct(ButtonList trigger)
        {
            this.trigger = trigger;
            axis = 0;
        }

        /// <summary>
        /// Axisの値を取得し、返すメソッド
        /// 
        /// ※ProjectSettingsのInputに"LT(number)"をJoyStickAxisの9th axisで追加しておくこと！
        /// ※ProjectSettingsのInputに"RT(number)"をJoyStickAxisの10th axisで追加しておくこと！
        /// </summary>
        /// <returns>axis(0か1)</returns>
        public float GetAxis()
        {
            switch (trigger)
            {
                case ButtonList.RightTrigger_1:
                    axis = Input.GetAxis("RT1");
                    break;
                case ButtonList.LeftTrigger_1:
                    axis = Input.GetAxis("LT1");
                    break;
                case ButtonList.RightTrigger_2:
                    axis = Input.GetAxis("RT2");
                    break;
                case ButtonList.LeftTrigger_2:
                    axis = Input.GetAxis("LT2");
                    break;
                case ButtonList.RightTrigger_3:
                    axis = Input.GetAxis("RT3");
                    break;
                case ButtonList.LeftTrigger_3:
                    axis = Input.GetAxis("LT3");
                    break;
                case ButtonList.RightTrigger_4:
                    axis = Input.GetAxis("RT4");
                    break;
                case ButtonList.LeftTrigger_4:
                    axis = Input.GetAxis("LT4");
                    break;
            }

            return axis;
        }
    }
}