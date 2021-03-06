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

namespace XInputAssistManager
{
    /// <summary>
    /// ジョイスティックボタンのリスト
    /// 
    /// 作成者:海堂 博
    /// 公開ユーザー:https://github.com/HakuKaido
    /// 作成日時:2018/9/20
    /// </summary>
    public enum ButtonList
    {
        None = 0,

        //参考:Xbox360forWindows
        GamePad_A = 1,          //Aボタン
        GamePad_B = 2,          //Bボタン
        GamePad_X = 3,          //Xボタン
        GamePad_Y = 4,          //Yボタン
        GamePad_L = 5,          //Lボタン
        GamePad_R = 6,         //Rボタン
        GamePad_Start = 7,   //Startボタン
        GamePad_Back = 8,   //Backボタン

        GamePad_A_2 = 9,          //2のAボタン
        GamePad_B_2 = 10,          //2のBボタン
        GamePad_X_2 = 11,          //2のXボタン
        GamePad_Y_2 = 12,          //2のYボタン
        GamePad_L_2 = 13,          //2のLボタン
        GamePad_R_2 = 14,         //2のRボタン
        GamePad_Start_2 = 15,   //2のStartボタン
        GamePad_Back_2 = 16,   //2のBackボタン

        GamePad_A_3 = 17,          //3のAボタン
        GamePad_B_3 = 18,          //3のBボタン
        GamePad_X_3 = 19,          //3のXボタン
        GamePad_Y_3 = 20,          //3のYボタン
        GamePad_L_3 = 21,          //3のLボタン
        GamePad_R_3 = 22,         //3のRボタン
        GamePad_Start_3 = 23,   //3のStartボタン
        GamePad_Back_3 = 24,   //3のBackボタン

        GamePad_A_4 = 25,          //4のAボタン
        GamePad_B_4 = 26,          //4のBボタン
        GamePad_X_4 = 27,          //4のXボタン
        GamePad_Y_4 = 28,          //4のYボタン
        GamePad_L_4 = 29,          //4のLボタン
        GamePad_R_4 = 30,         //4のRボタン
        GamePad_Start_4 = 31,   //4のStartボタン
        GamePad_Back_4 = 32,   //4のBackボタン

        RightTrigger_1 = 33,       //RT
        LeftTrigger_1 = 34,         //LT
        RightTrigger_2 = 35,       //2のRT
        LeftTrigger_2 = 36,         //2のLT
        RightTrigger_3 = 37,       //3のRT
        LeftTrigger_3 = 38,         //3のLT
        RightTrigger_4 = 39,       //4のRT
        LeftTrigger_4 = 40,         //4のLT

        //KEYBOARDの追加処理はこの下へ

        //個数取得用
        End = 99,
    }
}