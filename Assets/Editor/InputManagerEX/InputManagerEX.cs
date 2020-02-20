using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using XInputAssistManager;

public enum CodeType
{
    KEYBOARD,
    BUTTON,
    TRIGGER,
}

public class InputManagerEX : EditorWindow
{
    static InputManagerEX inputManagerEX;

    [SerializeField]
    InputManager inputManager;
    
    float scroll = 0;

    /// <summary>
    /// どれを選択しているか
    /// </summary>
    int selected;

    /// <summary>
    /// 操作の数
    /// </summary>
    int inputCodeCount;

    /// <summary>
    /// 操作のリスト
    /// </summary>
    [SerializeField]
    List<string> inputCode = new List<string>();

    /// <summary>
    /// InputCodeのリスト
    /// </summary>
    [SerializeField]
    List<InputCode> inputs = new List<InputCode>();

    /// <summary>
    /// ButtonListのリスト
    /// </summary>
    [SerializeField]
    List<ButtonList> buttons = new List<ButtonList>();

    // MenuItem("メニュー名/項目名") のフォーマットで記載
    [MenuItem("Custom/InputManagerEX")]
    static void ShowWindow()
    {
        //インスタンス生成（複数の同一ウィンドウを表示しない）
        if (inputManagerEX == null)
        {
            inputManagerEX = CreateInstance<InputManagerEX>();
        }

        // ウィンドウを表示
        EditorWindow.GetWindow<InputManagerEX>();
    }

    private void OnEnable()
    {
        inputManager = InputManager.GetInstance;

        //Enumの中身を確認
        LoadEnum();
    }

    /**
     * ウィンドウの中身
     */
    void OnGUI()
    {
        selected = GUILayout.Toolbar(selected, new string[] { "InputCode", "CodeJoin" });

        Display();
    }

    void Display()
    {
        switch (selected)
        {
            #region 操作名
            case 0:
                inputCodeCount = EditorGUILayout.IntField("操作の数", inputCodeCount);
                using (var scrollView = new EditorGUILayout.ScrollViewScope(new Vector2(0, scroll)))
                {
                    for (int i = 0; i < inputCodeCount; i++)
                    {
                        try
                        {
                            inputCode[i] = EditorGUILayout.TextField("操作の名前", inputCode[i]);
                        }
                        catch
                        {
                            inputCode.Add("");
                            inputCode[i] = EditorGUILayout.TextField("操作の名前", inputCode[i]);
                        }
                    }

                    scroll = scrollView.scrollPosition.y;
                }

                //横並びの開始
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Refresh"))
                {
                    Debug.Log("画面更新開始");
                    try
                    {
                        LoadEnum();

                        Debug.Log("画面更新完了");
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError(e.Message);
                        throw;
                    }
                }

                if (GUILayout.Button("Export"))
                {
                    Debug.Log("出力開始");
                    try
                    {
                        CreateEnum();
                        ClearDictionary();

                        Debug.Log("出力完了");
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError(e.Message);
                        throw;
                    }
                }

                //横並び終了
                EditorGUILayout.EndHorizontal();
                break;
            #endregion
            case 1:
                try
                {
                    using (var scrollView = new EditorGUILayout.ScrollViewScope(new Vector2(0, scroll)))
                    {
                        //更新されるたびに初期化する
                        Dictionary<InputCode, KeyCode> keyListDic = new Dictionary<InputCode, KeyCode>();
                        Dictionary<InputCode, ButtonList> buttonListDic = new Dictionary<InputCode, ButtonList>();

                        //表示部分更新処理
                        for (int i = 0; i < (int)InputCode.END; i++)
                        {
                            EditorGUILayout.BeginHorizontal();
                            #region 操作
                            EditorGUILayout.BeginVertical();

                            //Enumのポップアップ表示
                            GUILayout.Label("操作の名前");
                            //string型でinputListsと同時に生成などしてるので問題ない
                            GUILayout.Label(inputCode[i]);

                            EditorGUILayout.EndVertical();
                            #endregion

                            #region ボタン
                            EditorGUILayout.BeginVertical();

                            GUILayout.Label("ボタンの名前");
                            buttons[i] = (ButtonList)EditorGUILayout.EnumPopup(buttons[i]);

                            if ((32 < (int)buttons[i]) && ((int)buttons[i] < 41) && !inputManager.CodeAtTriggerDictionary.ContainsKey(inputs[i]))
                                inputManager.SetTrigger(inputs[i], inputManager.GetTriggerAtButton(buttons[i]));
                            else if ((0 < buttons[i]) && ((int)buttons[i] < 33) && !inputManager.CodeDictionary.ContainsKey(inputs[i]))
                                inputManager.SetButton(inputs[i], inputManager.GetKeyCodeAtButton(buttons[i]));
                            else if (!inputManager.CodeDictionaryOnKeyBoard.ContainsKey(inputs[i]))
                                inputManager.SetKeyCode(inputs[i], inputManager.GetKeyCodeAtButton(buttons[i]));

                            EditorGUILayout.EndVertical();
                            #endregion

                            #region キー
                            EditorGUILayout.BeginVertical();

                            if (inputManager.CodeAtTriggerDictionary.ContainsKey(inputs[i]) && inputManager.TriggerAtButtonDictionary.ContainsKey(buttons[i]))
                            {
                                GUILayout.Label("トリガー");
                                GUILayout.Label(inputManager.GetTriggerAtButton(buttons[i]).ToString());
                            }
                            else
                            {
                                GUILayout.Label("キーコード");
                                GUILayout.Label(inputManager.GetKeyCodeAtButton(buttons[i]).ToString());
                            }

                            EditorGUILayout.EndVertical();
                            #endregion

                            EditorGUILayout.EndHorizontal();
                        }

                        #region メッセージ

                        EditorGUILayout.BeginVertical();
                        for (int i = 0; i < (int)InputCode.END; i++)
                        {
                            int count = 0;

                            for (int j = 0; j < (int)InputCode.END; j++)
                            {
                                if ((buttons[i] == buttons[j]) && (i != j))
                                {
                                    count++;
                                }
                            }

                            EditorGUILayout.BeginHorizontal();
                            if (count > 0)
                            {
                                GUILayout.Label("Attention!!：同一のボタンが割り当てられています！『対象の操作名：" + inputs[i] + ", 対象のボタン：" + buttons[i] + ", 重複している個数：" + count + "』");
                            }
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            if (buttons[i] == ButtonList.None)
                            {
                                GUILayout.Label("Attention!!：ボタンが割り当てられていません！『対象の操作名：" + inputs[i] + "』");
                            }
                            EditorGUILayout.EndHorizontal();

                            GUILayout.Space(10);
                        }
                        EditorGUILayout.EndVertical();


                        #endregion
                        scroll = scrollView.scrollPosition.y;
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.Message);
                    throw;
                }
                break;
        }
    }

    void ClearDictionary()
    {
        //InputCodeが更新されるごとにDictionaryは更新される
        //事故防止
        inputManager.CodeDictionaryOnKeyBoard.Clear();
        inputManager.CodeAtTriggerDictionary.Clear();
        inputManager.CodeDictionary.Clear();

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// InputCodeの生成
    /// </summary>
    void CreateEnum()
    {
        string path = Application.dataPath + "\\Resouces\\XInputAssistManager\\Scripts\\InputCode.cs";
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("namespace XInputAssistManager");
        stringBuilder.AppendLine("{");
        stringBuilder.AppendLine("   public enum InputCode");
        stringBuilder.AppendLine("   {");

        //リストの中身を入れる
        for (int i = 0; i < inputCodeCount; i++)
        {
            stringBuilder.Append("     ");
            stringBuilder.Append(inputCode[i]);
            stringBuilder.AppendLine(",");
        }

        stringBuilder.Append("     ");
        stringBuilder.Append("END");
        stringBuilder.AppendLine(",");

        stringBuilder.AppendLine("   }");
        stringBuilder.AppendLine("}");

        // 文字コードを指定
        Encoding enc = Encoding.GetEncoding("UTF-8");

        // ファイルを開く
        StreamWriter writer = new StreamWriter(path, false, enc);

        // テキストを書き込む
        writer.Write(stringBuilder.ToString());

        // ファイルを閉じる
        writer.Close();

        Debug.Log(path + "に出力");

        InputManager.DeleteObjct();

        AssetDatabase.Refresh();

        try
        {
            LoadEnum();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            throw;
        }
    }

    /// <summary>
    /// InputCodeや対応するKeyCode、ButtonListの読み込みと関連処理
    /// </summary>
    void LoadEnum()
    {
        Debug.Log("列挙リストの読み込み開始");

        inputs.Clear();
        inputCode.Clear();
        buttons.Clear();

        try
        {
            //リストの中身作成
            for (int i = 0; i < (int)InputCode.END; i++)
            {
                inputs.Add((InputCode)i);
                inputCode.Add(((InputCode)i).ToString());

                buttons.Add(inputManager.GetButtonAtInputCode(inputs[i]));
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            throw;
        }

        inputCodeCount = inputs.Count;

        AssetDatabase.Refresh();
    }
}
