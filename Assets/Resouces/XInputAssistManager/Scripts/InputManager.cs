#region Lisence

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
    /// ※継承禁止、GamePad操作はXInput専用
    /// Inputの管理
    /// 
    /// 作成者:海堂 博
    /// 公開ユーザー:https://github.com/HakuKaido
    /// 作成日時:2018/9/20
    /// </summary>
    public sealed class InputManager : MonoBehaviour
    {
        //インスタンス用フィールド変数
        static InputManager inputManager;

        //生成・破壊用
        static GameObject myObject;

        /// <summary>
        /// ゲームパッドのみを使用するか？
        /// </summary>
        public bool IsUseGamePadOnly { get; set; }

        #region プロパティ

        /// <summary>
        /// キーコンフィグの設定を収納するDictionaryプロパティ（ボタン版）
        /// </summary>
        public Dictionary<InputCode, KeyCode> CodeDictionary { get; private set; }

        /// <summary>
        /// キーコンフィグの設定を収納するDictionaryプロパティ（キーボード版）
        /// </summary>
        public Dictionary<InputCode, KeyCode> CodeDictionaryOnKeyBoard { get; private set; }

        /// <summary>
        /// ゲームパッドのボタンの名前を収納するDictionaryプロパティ
        /// </summary>
        public Dictionary<ButtonList, KeyCode> CodeAtButtonDictionary { get; private set; }

        /// <summary>
        /// キーコンフィグ（Trigger）の設定を収納するDictionaryプロパティ
        /// </summary>
        public Dictionary<InputCode, TriggerAsButton> CodeAtTriggerDictionary { get; private set; }

        /// <summary>
        /// ゲームパッドのTriggerの名前を収納するDictionaryプロパティ
        /// </summary>
        public Dictionary<ButtonList, TriggerAsButton> TriggerAtButtonDictionary { get; private set; }

        #region ButtonList取得用

        /// <summary>
        /// ゲームパッドのボタンの名前を収納するDictionaryプロパティ
        /// </summary>
        public Dictionary<KeyCode, ButtonList> ButtonNameDictionary { get; private set; }

        /// <summary>
        /// ゲームパッドのTriggerの名前を収納するDictionaryプロパティ
        /// </summary>
        public Dictionary<TriggerAsButton, ButtonList> TriggerNameDictionary { get; private set; }

        #endregion

        /// <summary>
        /// 再処理までのカウントをするタイマー
        /// </summary>
        float RestartTimer { get; set; }

        /// <summary>
        /// RT1用
        /// </summary>
        TriggerAsButton RightTrigger_1 { get; set; }

        /// <summary>
        /// LT1用
        /// </summary>
        TriggerAsButton LeftTrigger_1 { get; set; }

        /// <summary>
        /// RT2用
        /// </summary>
        TriggerAsButton RightTrigger_2 { get; set; }

        /// <summary>
        /// LT2用
        /// </summary>
        TriggerAsButton LeftTrigger_2 { get; set; }

        /// <summary>
        /// RT3用
        /// </summary>
        TriggerAsButton RightTrigger_3 { get; set; }

        /// <summary>
        /// LT3用
        /// </summary>
        TriggerAsButton LeftTrigger_3 { get; set; }

        /// <summary>
        /// RT4用
        /// </summary>
        TriggerAsButton RightTrigger_4 { get; set; }

        /// <summary>
        /// LT4用
        /// </summary>
        TriggerAsButton LeftTrigger_4 { get; set; }

        #region Axis処理

        /// <summary>
        /// 水平方向の入力の大きさ取得
        /// </summary>
        /// <returns>水平方向の入力の大きさ</returns>
        public float GetAxisHorizontal
        {
            get { return Input.GetAxis("Horizontal"); }
        }

        /// <summary>
        /// 垂直方向の入力の大きさ取得
        /// </summary>
        /// <returns>垂直方向の入力の大きさ</returns>
        public float GetAxisVertical
        {
            get { return Input.GetAxis("Vertical"); }
        }

        /// <summary>
        /// ※ProjectSettingsのInputに"RightStickHorizontal"をJoyStickAxisの4th axisで追加しておくこと！
        /// 
        /// 右スティック水平方向の入力の大きさ取得
        /// </summary>
        /// <returns>右スティック水平方向の入力の大きさ</returns>
        public float GetAxisRightHorizontal
        {
            get { return Input.GetAxis("RightStickHorizontal"); }
        }

        /// <summary>
        /// ※ProjectSettingsのInputに"RightStickVertical"をJoyStickAxisの5th axisで追加しておくこと！
        /// 
        /// 右スティック垂直方向の入力の大きさ取得
        /// </summary>
        /// <returns>右スティック垂直方向の入力の大きさ</returns>
        public float GetAxisRightVertical
        {
            get { return Input.GetAxis("RightStickVertical"); }
        }

        #endregion

        #endregion

        /// <summary>
        /// インスタンス取得
        /// </summary>
        /// <returns>実体</returns>
        public static InputManager GetInstance
        {
            get
            {
                if (inputManager == null)
                {
                    //GameObjectの生成
                    myObject = new GameObject("InputManagerGO");
                    //InputManagerのAdd
                    myObject.AddComponent<InputManager>();

                    inputManager = myObject.GetComponent<InputManager>();

                    //初期化
                    inputManager.Initialize();
                }

                return inputManager;
            }
        }

        public static void DeleteObjct()
        {
#if UNITY_EDITOR
            DestroyImmediate(myObject);
#endif
        }

        /// <summary>
        /// 再生直後に一回だけ実行
        /// </summary>
        void Awake()
        {
#if UNITY_STANDALONE
            //破壊不可
            DontDestroyOnLoad(gameObject);
#endif
        }

        //初期化メソッド
        private void Initialize()
        {
            //初期化
            IsUseGamePadOnly = false;

            RightTrigger_1 = new TriggerAsButton(ButtonList.RightTrigger_1);
            LeftTrigger_1 = new TriggerAsButton(ButtonList.LeftTrigger_1);
            RightTrigger_2 = new TriggerAsButton(ButtonList.RightTrigger_2);
            LeftTrigger_2 = new TriggerAsButton(ButtonList.LeftTrigger_2);
            RightTrigger_3 = new TriggerAsButton(ButtonList.RightTrigger_3);
            LeftTrigger_3 = new TriggerAsButton(ButtonList.LeftTrigger_3);
            RightTrigger_4 = new TriggerAsButton(ButtonList.RightTrigger_4);
            LeftTrigger_4 = new TriggerAsButton(ButtonList.LeftTrigger_4);
            CodeDictionary = new Dictionary<InputCode, KeyCode>();
            CodeDictionaryOnKeyBoard = new Dictionary<InputCode, KeyCode>();
            CodeAtButtonDictionary = new Dictionary<ButtonList, KeyCode>();
            CodeAtTriggerDictionary = new Dictionary<InputCode, TriggerAsButton>();
            TriggerAtButtonDictionary = new Dictionary<ButtonList, TriggerAsButton>();

            ButtonNameDictionary = new Dictionary<KeyCode, ButtonList>();
            TriggerNameDictionary = new Dictionary<TriggerAsButton, ButtonList>();

            RestartTimer = 0.0f;

            SetButtonName();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        void Update()
        {
            //TriggerNameDictionary内の値の更新処理
            foreach (TriggerAsButton item in TriggerAtButtonDictionary.Values)
            {
                item.Update();
            }
        }

        #region Awakeの中身

        /// <summary>
        /// ※Awakeで呼ぶこと
        /// ※事故防止のためfor文などでは回していない
        /// 
        /// ButtonListとKeyCodeを結びつけるメソッド
        /// </summary>
        void SetButtonName()
        {
            //空白
            CodeAtButtonDictionary.Add(ButtonList.None, KeyCode.None);

            //WindowsOS用
            //XInput
            //Button
            CodeAtButtonDictionary.Add(ButtonList.GamePad_A, KeyCode.Joystick1Button0);   //Aボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_B, KeyCode.Joystick1Button1);   //Bボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_X, KeyCode.Joystick1Button2);   //Xボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_Y, KeyCode.Joystick1Button3);   //Yボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_L, KeyCode.Joystick1Button4);   //Lボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_R, KeyCode.Joystick1Button5);   //Rボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_Back, KeyCode.Joystick1Button6);   //Backボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_Start, KeyCode.Joystick1Button7);   //Startボタン

            CodeAtButtonDictionary.Add(ButtonList.GamePad_A_2, KeyCode.Joystick2Button0);   //Aボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_B_2, KeyCode.Joystick2Button1);   //Bボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_X_2, KeyCode.Joystick2Button2);   //Xボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_Y_2, KeyCode.Joystick2Button3);   //Yボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_L_2, KeyCode.Joystick2Button4);   //Lボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_R_2, KeyCode.Joystick2Button5);   //Rボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_Back_2, KeyCode.Joystick2Button6);   //Backボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_Start_2, KeyCode.Joystick2Button7);   //Startボタン

            CodeAtButtonDictionary.Add(ButtonList.GamePad_A_3, KeyCode.Joystick3Button0);   //Aボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_B_3, KeyCode.Joystick3Button1);   //Bボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_X_3, KeyCode.Joystick3Button2);   //Xボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_Y_3, KeyCode.Joystick3Button3);   //Yボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_L_3, KeyCode.Joystick3Button4);   //Lボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_R_3, KeyCode.Joystick3Button5);   //Rボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_Back_3, KeyCode.Joystick3Button6);   //Backボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_Start_3, KeyCode.Joystick3Button7);   //Startボタン

            CodeAtButtonDictionary.Add(ButtonList.GamePad_A_4, KeyCode.Joystick4Button0);   //Aボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_B_4, KeyCode.Joystick4Button1);   //Bボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_X_4, KeyCode.Joystick4Button2);   //Xボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_Y_4, KeyCode.Joystick4Button3);   //Yボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_L_4, KeyCode.JoystickButton4);   //Lボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_R_4, KeyCode.Joystick4Button5);   //Rボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_Back_4, KeyCode.Joystick4Button6);   //Backボタン
            CodeAtButtonDictionary.Add(ButtonList.GamePad_Start_4, KeyCode.Joystick4Button7);   //Startボタン

            //Trigger
            TriggerAtButtonDictionary.Add(ButtonList.LeftTrigger_1, LeftTrigger_1);         //LT1
            TriggerAtButtonDictionary.Add(ButtonList.RightTrigger_1, RightTrigger_1);   //RT1
            TriggerAtButtonDictionary.Add(ButtonList.LeftTrigger_2, LeftTrigger_2);         //LT2
            TriggerAtButtonDictionary.Add(ButtonList.RightTrigger_2, RightTrigger_2);   //RT2
            TriggerAtButtonDictionary.Add(ButtonList.LeftTrigger_3, LeftTrigger_3);         //LT3
            TriggerAtButtonDictionary.Add(ButtonList.RightTrigger_3, RightTrigger_3);   //RT3
            TriggerAtButtonDictionary.Add(ButtonList.LeftTrigger_4, LeftTrigger_4);         //LT4
            TriggerAtButtonDictionary.Add(ButtonList.RightTrigger_4, RightTrigger_4);   //RT4

            KeyCode[] keyCodes = new KeyCode[CodeAtButtonDictionary.Count];
            ButtonList[] buttonLists = new ButtonList[CodeAtButtonDictionary.Count];

            CodeAtButtonDictionary.Keys.CopyTo(buttonLists, 0);
            CodeAtButtonDictionary.Values.CopyTo(keyCodes, 0);

            for (int i = 0; i < CodeAtButtonDictionary.Count; i++)
            {
                ButtonNameDictionary.Add(keyCodes[i], buttonLists[i]);
            }

            buttonLists = new ButtonList[TriggerAtButtonDictionary.Count];
            TriggerAsButton[] triggerAsButtons = new TriggerAsButton[TriggerAtButtonDictionary.Count];

            TriggerAtButtonDictionary.Keys.CopyTo(buttonLists, 0);
            TriggerAtButtonDictionary.Values.CopyTo(triggerAsButtons, 0);

            for (int j = 0; j < TriggerAtButtonDictionary.Count; j++)
            {
                TriggerNameDictionary.Add(triggerAsButtons[j], buttonLists[j]);
            }
        }

        /// <summary>
        /// InputCodeとKeyCodeを結びつけるメソッド
        /// </summary>
        public void SetKeyCode(InputCode inputCode, KeyCode keyCode)
        {
            //エディタ使用時用のデフォルトのキー設定
            //例（キーボード版）:CodeDictionaryOnKeyBoard.Add(InputCode.操作, KeyCode.対応させるキー名);
            CodeDictionaryOnKeyBoard.Add(inputCode, keyCode);
        }

        /// <summary>
        /// InputCodeとButtonListを結びつけるメソッド
        /// </summary>
        public void SetButtonCode(InputCode inputCode, ButtonList buttonList)
        {
            //エディタ使用時用のデフォルトのキー設定
            //例（ボタン版）:CodeDictionary.Add(InputCode.操作, ButtonNameDictionary[ButtonList.対応させるボタン名]);
            CodeDictionary.Add(inputCode, CodeAtButtonDictionary[buttonList]);
        }

        #endregion

        #region キーの状態取得

        /// <summary>
        /// キーを押してない状態から押したときtrue
        /// </summary>
        /// <param name="inputCode">操作の名前</param>
        /// <returns>キーの状態</returns>
        public bool GetKeyDown(InputCode inputCode)
        {
            //現在の入力キーがあり、かつ入力があって、使用するのがゲームパッドのみでないなら
            if (CodeDictionaryOnKeyBoard.ContainsKey(inputCode)
                && Input.GetKeyDown(CodeDictionaryOnKeyBoard[inputCode])
                && !IsUseGamePadOnly)
            {
                //キーボード
                return Input.GetKeyDown(CodeDictionaryOnKeyBoard[inputCode]);
            }

            //現在の入力キーがCodeDictionaryに入っていなければ
            if (!CodeDictionary.ContainsKey(inputCode))
            {
                //トリガーの状態を返す
                return CodeAtTriggerDictionary[inputCode].IsTriggerDown;
            }

            //ボタンのKeyCodeを返す
            return Input.GetKeyDown(CodeDictionary[inputCode]);
        }

        /// <summary>
        /// キーを押し続けている時true
        /// </summary>
        /// <param name="inputCode">操作の名前</param>
        /// <returns>キーの状態</returns>
        public bool GetKey(InputCode inputCode)
        {
            //現在の入力キーがあり、かつ入力があって、使用するのがゲームパッドのみでないなら
            if (CodeDictionaryOnKeyBoard.ContainsKey(inputCode)
                && Input.GetKeyDown(CodeDictionaryOnKeyBoard[inputCode])
                && !IsUseGamePadOnly)
            {
                //キーボード
                return Input.GetKey(CodeDictionaryOnKeyBoard[inputCode]);
            }

            //現在の入力キーがCodeDictionaryに入っていなければ
            if (!CodeDictionary.ContainsKey(inputCode))
            {
                //トリガーの状態を返す
                return CodeAtTriggerDictionary[inputCode].GetTriggerState;
            }

            //ボタンのKeyCodeを返す
            return Input.GetKey(CodeDictionary[inputCode]);

        }

        /// <summary>
        /// キーを押している状態から離した時true
        /// </summary>
        /// <param name="inputCode">操作の名前</param>
        /// <returns>キーの状態</returns>
        public bool GetKeyUp(InputCode inputCode)
        {
            //現在の入力キーがあり、かつ入力があって、使用するのがゲームパッドのみでないなら
            if (CodeDictionaryOnKeyBoard.ContainsKey(inputCode)
                && Input.GetKeyDown(CodeDictionaryOnKeyBoard[inputCode])
                && !IsUseGamePadOnly)
            {
                //キーボード
                return Input.GetKeyUp(CodeDictionaryOnKeyBoard[inputCode]);
            }

            //現在の入力キーがCodeDictionaryに入っていなければ
            if (!CodeDictionary.ContainsKey(inputCode))
            {
                //トリガーの状態を返す
                return CodeAtTriggerDictionary[inputCode].IsTriggerUp;
            }

            //ボタンのKeyCodeを返す
            return Input.GetKeyUp(CodeDictionary[inputCode]);
        }

        #endregion

        /// <summary>
        /// キーの設定
        /// </summary>
        /// <param name="inputCord">操作の名前</param>
        /// <param name="key">キーコード</param>
        public void SetButton(InputCode inputCode, KeyCode key)
        {
            //現在の入力キーがCodeTriggerDictionaryに入っていたら
            if (CodeAtTriggerDictionary.ContainsKey(inputCode))
            {
                //消しておく(競合回避)
                CodeAtTriggerDictionary.Remove(inputCode);
                CodeDictionary.Add(inputCode, key);
                return;
            }

            CodeDictionary[inputCode] = key;
        }

        /// <summary>
        /// キーの設定（トリガー版）
        /// 
        /// </summary>
        /// <param name="inputCord">操作の名前</param>
        /// <param name="trigger">キーコード</param>
        public void SetTrigger(InputCode inputCode, TriggerAsButton trigger)
        {
            //現在の入力キーがCodeDictionaryに入っていたら
            if (CodeDictionary.ContainsKey(inputCode))
            {
                //消しておく(競合回避)
                CodeDictionary.Remove(inputCode);
                CodeAtTriggerDictionary.Add(inputCode, trigger);
                return;
            }

            CodeAtTriggerDictionary[inputCode] = trigger;
        }

        /// <summary>
        /// KeyCodeを返す
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public KeyCode GetKeyCodeAtButton(ButtonList button)
        {
            return CodeAtButtonDictionary[button];
        }

        /// <summary>
        /// Triggerを返す
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public TriggerAsButton GetTriggerAtButton(ButtonList button)
        {
            return TriggerAtButtonDictionary[button];
        }

        /// <summary>
        /// Buttonを返す
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public ButtonList GetButtonAtInputCode(InputCode inputCode)
        {
            //XInputでの入力だったら
            if (CodeDictionary.ContainsKey(inputCode)) return ButtonNameDictionary[CodeDictionary[inputCode]];
            //キーボードからの入力だったら
            else if (CodeDictionaryOnKeyBoard.ContainsKey(inputCode)) return ButtonNameDictionary[CodeDictionaryOnKeyBoard[inputCode]];
            //トリガーからの入力だったら
            else if (CodeAtTriggerDictionary.ContainsKey(inputCode)) return TriggerNameDictionary[CodeAtTriggerDictionary[inputCode]];

            return ButtonList.None;
        }

        /// <summary>
        /// ボタン（トリガー）名で直接判定する
        /// 前のフレームに押されていなくて現在のフレームで押されていたらtrue
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool GetKeyDownAtButton(ButtonList button)
        {
            //ボタンかトリガーかの判別
            if (CodeAtButtonDictionary.ContainsKey(button)) return Input.GetKeyDown(GetKeyCodeAtButton(button));
            else if (TriggerAtButtonDictionary.ContainsKey(button)) return TriggerAtButtonDictionary[button].IsTriggerDown;

            return false;
        }

        /// <summary>
        /// ボタン（トリガー）名で直接判定する
        /// キーを押し続けている時true
        /// </summary>
        /// <param name="inputCode">操作の名前</param>
        /// <returns>キーの状態</returns>
        public bool GetKeyAtButton(ButtonList button)
        {
            //ボタンかトリガーかの判別
            if (CodeAtButtonDictionary.ContainsKey(button)) return Input.GetKey(GetKeyCodeAtButton(button));
            else if (TriggerAtButtonDictionary.ContainsKey(button)) return TriggerAtButtonDictionary[button].GetTriggerState;

            return false;
        }

        /// <summary>
        /// ボタン（トリガー）名で直接判定する
        /// キーを押している状態から離した時true
        /// </summary>
        /// <param name="inputCode">操作の名前</param>
        /// <returns>キーの状態</returns>
        public bool GetKeyUpAtButton(ButtonList button)
        {
            //ボタンかトリガーかの判別
            if (CodeAtButtonDictionary.ContainsKey(button)) return Input.GetKeyUp(GetKeyCodeAtButton(button));
            else if (TriggerAtButtonDictionary.ContainsKey(button)) return TriggerAtButtonDictionary[button].IsTriggerUp;

            return false;
        }

        /// <summary>
        /// いずれかのボタン（トリガー）が押されたら
        /// </summary>
        /// <returns></returns>
        public bool AnyButtonDown()
        {
            //ButtonListの数だけ回して、どれかが押されていたらtureを返す
            for (int i = 0; i < (int)ButtonList.End; i++)
            {
                if (inputManager.GetKeyDownAtButton((ButtonList)i)) return true;
            }

            return false;
        }
    }
}