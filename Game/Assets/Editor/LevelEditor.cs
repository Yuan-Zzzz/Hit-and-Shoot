using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelEditor : EditorWindow
{

    private VisualElement rightPane;
    private ListView leftPane;

    private const int brickSize = 50;
    private const int levelHeight = 10;
    private const int levelWidth = 17;

    public static LevelData_SO selectedLevel;
    public static SingleBrickData currentBrick;

  // Button[,] holderButton = new Button[levelWidth, levelHeight];
    [MenuItem("Tools/LevelEditor")]
    public static void ShowMyEditor()
    {
        EditorWindow wnd = GetWindow<LevelEditor>();
        wnd.titleContent = new GUIContent("关卡编辑器");

    }
    public void CreateGUI()
    {

        //加载关卡数据
        var allObjectGuids = AssetDatabase.FindAssets("t:LevelData_SO");
        var allObjects = new List<LevelData_SO>();
        foreach (var guid in allObjectGuids)
        {
            allObjects.Add(AssetDatabase.LoadAssetAtPath<LevelData_SO>(AssetDatabase.GUIDToAssetPath(guid)));
        }
        //创建窗口
        var splitView1 = new TwoPaneSplitView(0, 150, TwoPaneSplitViewOrientation.Horizontal);
        //  var splitView2 = new TwoPaneSplitView(0, 150, TwoPaneSplitViewOrientation.Vertical);
        rootVisualElement.Add(splitView1);

        leftPane = new ListView();
        splitView1.Add(leftPane);

        rightPane = new VisualElement();
        splitView1.Add(rightPane);


        // Initialize the list view with all sprites' names
        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => { (item as Label).text = allObjects[index].name; };
        leftPane.itemsSource = allObjects;
        leftPane.onSelectionChange += OnLevelSelectionChange;
    }
    private void OnLevelSelectionChange(IEnumerable<object> _selectedLevels)
    {

        //清除右侧内容
        rightPane.Clear();

        // Get the selected sprite
        selectedLevel = _selectedLevels.First() as LevelData_SO;
        if (selectedLevel == null) return;


        //绘制容器按钮
        for (int i = 0; i < levelWidth; i++)
        {
            for (int j = 0; j < levelHeight; j++)
            {
               
               Button holderButton = new Button();
                holderButton.style.position = Position.Absolute;
                holderButton.text = "Holder";
                holderButton.style.height = brickSize;
                holderButton.style.width = brickSize;
                holderButton.style.left = i * brickSize;
                holderButton.style.top = j * brickSize;

                //holderButton[i, j].clicked += () =>
                //{
                //    var window = new BrickEditorWindow(i,j);
                //      window.ShowModal();
                //};
                holderButton.userData = new Vector2(i,j);

                holderButton.clicked += () =>
                {
                   
                    var window = new BrickEditorWindow(holderButton.userData);
                       window.ShowModal();
                };

                holderButton.tooltip = i + "," + j;

                rightPane.Add(holderButton);
                //绘制砖块
                foreach (var brick in selectedLevel.bricks)
                {


                    if (brick.pos.x + (int)levelWidth / 2 == i && brick.pos.y - (int)levelHeight / 2 == -j)
                    {
                        var spriteImage = new Image();
                        spriteImage.scaleMode = ScaleMode.ScaleToFit;
                        spriteImage.sprite = brick.brick.gameObject.GetComponent<SpriteRenderer>().sprite;
                        spriteImage.tintColor = new Color(brick.data.brickColor.r, brick.data.brickColor.g, brick.data.brickColor.b, 1f);
                        holderButton.Add(spriteImage);
                    }


                }
            }
        }
    }



    //private void OnHolderClicked()
    //{
    //    var window = new BrickEditorWindow();
    //    window.ShowModal();
    //}

    public class BrickEditorWindow : EditorWindow
    {
        Vector2 pos;
        public BrickEditorWindow(object _pos)
        {
            pos = (Vector2)_pos;
        }
        private void OnEnable()
        {
            //Color test = selectedLevel.

            //var gameObjectBox = new Box();
            //gameObjectBox.Add(new Label("砖块预制体"));
            //gameObjectBox.Add(new ObjectField());
            //rootVisualElement.Add(gameObjectBox);
            //var colorBox = new Box();
            //colorBox.Add(new Label("砖块颜色"));
            //colorBox.Add(new ColorField());
            //rootVisualElement.Add(colorBox);

            //var countBox = new Box();
            //countBox.Add(new Label("受击次数"));
            //countBox.Add(new IntegerField());
            //rootVisualElement.Add(countBox);

            //var riftBox = new Box();
            //riftBox.Add(new Label("分裂数"));
            //riftBox.Add(new IntegerField());
            //rootVisualElement.Add(riftBox);
        }
        private void CreateGUI()
        {
            rootVisualElement.Add(new Label("编辑砖块"));
            rootVisualElement.Add(new Label(pos.x+" "+pos.y));
        }

    }
}
