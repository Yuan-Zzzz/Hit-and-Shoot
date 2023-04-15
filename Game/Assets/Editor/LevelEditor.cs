using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelEditor : EditorWindow
{

    private VisualElement rightPane;
    private ListView leftPane;
    private VisualElement bottomPane;


    [MenuItem("Tools/LevelEditor")]
    public static void ShowMyEditor()
    {
        EditorWindow wnd = GetWindow<LevelEditor>();
        wnd.titleContent = new GUIContent("关卡编辑器");

    }
    private void OnGUI()
    {
        position = new Rect(position.x, position.y, 1000,600);
      
          
       
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
        var splitView1 = new TwoPaneSplitView(0,150, TwoPaneSplitViewOrientation.Horizontal);
        var splitView2 = new TwoPaneSplitView(0, 150, TwoPaneSplitViewOrientation.Vertical);
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
    private void OnLevelSelectionChange(IEnumerable<object> selectedItems)
    {
        //清除右侧内容
        rightPane.Clear();

        // Get the selected sprite
        var selectedLevel = selectedItems.First() as LevelData_SO;
        if (selectedLevel == null) return;


        for (int i = 0; i < selectedLevel.bricks.Count; i++)
        {
            var spriteImage = new Image();
            spriteImage.scaleMode = ScaleMode.StretchToFill;
            spriteImage.sprite = selectedLevel.bricks[i].brick.gameObject.GetComponent<SpriteRenderer>().sprite;
            spriteImage.tintColor = new Color(selectedLevel.bricks[i].data.brickColor.r, selectedLevel.bricks[i].data.brickColor.g, selectedLevel.bricks[i].data.brickColor.b,1f);
            spriteImage.style.position = Position.Absolute;
            spriteImage.style.height = 50;
            spriteImage.style.width = 50;
            spriteImage.style.left = selectedLevel.bricks[i].pos.x*50+400;
            spriteImage.style.top = -selectedLevel.bricks[i].pos.y*50+300;
            rightPane.Add(spriteImage);
            //Button editButton = new Button();
            //editButton.text = "Edit";
            //spriteImage.Add(editButton);



        }



    }
}
